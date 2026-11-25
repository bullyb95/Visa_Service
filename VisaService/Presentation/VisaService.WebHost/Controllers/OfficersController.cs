using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisaService.Application.Models.Officer;
using VisaService.Application.Services.Abstractions;
using VisaService.WebHost.Requests.Officer;
using VisaService.WebHost.Responses.Officer;
using VisaService.WebHost.Responses.VisaApplication;

namespace VisaService.WebHost.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OfficersController(IOfficersApplicationService officersApplicationService,
                                IVisaApplicationsApplicationService visaApplicationsApplicationService,
                                IMapper mapper) : ControllerBase
{
    /// <summary>Получить всех визовых офицеров.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfficerShortResponse>))]
    public async Task<IActionResult> GetAllOfficers(CancellationToken cancellationToken)
    {
        var officers = await officersApplicationService.GetOfficersAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<OfficerShortResponse>>(officers));
    }

    /// <summary>Получить офицера по id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfficerDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetOfficerById(Guid id, CancellationToken cancellationToken)
    {
        var officer = await officersApplicationService.GetOfficerByIdAsync(id, cancellationToken);
        if (officer is null)
            return NotFound($"Officer with id:{id} not found");
        return Ok(mapper.Map<OfficerDetailedResponse>(officer));
    }

    /// <summary>Найти офицеров по фамилии.</summary>
    [HttpGet("by-last-name/{lastName}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfficerShortResponse>))]
    public async Task<IActionResult> GetOfficersByLastName(string lastName, CancellationToken cancellationToken)
    {
        var officers = await officersApplicationService.GetOfficersByLastNameAsync(lastName, cancellationToken);
        return Ok(mapper.Map<IEnumerable<OfficerShortResponse>>(officers));
    }

    /// <summary>Получить заявки, назначенные офицеру.</summary>
    [HttpGet("{id:guid}/applications")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VisaApplicationDetailedResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetOfficerApplications(Guid id, CancellationToken cancellationToken)
    {
        var officer = await officersApplicationService.GetOfficerByIdAsync(id, cancellationToken);
        if (officer is null)
            return NotFound($"Officer with id:{id} not found");

        var applications = await visaApplicationsApplicationService.GetVisaApplicationsByOfficerIdAsync(id, cancellationToken);
        return Ok(mapper.Map<IEnumerable<VisaApplicationDetailedResponse>>(applications));
    }

    /// <summary>Добавить визового офицера.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OfficerShortResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> CreateOfficer(CreateOfficerRequest request, CancellationToken cancellationToken)
    {
        var officer = mapper.Map<CreateOfficerModel>(request);
        var createdOfficer = await officersApplicationService.CreateOfficerAsync(officer, cancellationToken);
        if (createdOfficer is null)
            return BadRequest("Officer can not be created");

        var officerResponse = mapper.Map<OfficerShortResponse>(createdOfficer);
        return CreatedAtAction(nameof(GetOfficerById), new { officerResponse.Id }, officerResponse);
    }

    /// <summary>Удалить офицера (в его заявках офицер станет не назначен).</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> DeleteOfficer(Guid id, CancellationToken cancellationToken)
    {
        if (!await officersApplicationService.DeleteOfficerAsync(id, cancellationToken))
            return NotFound($"Officer with id:{id} not found");
        return NoContent();
    }
}
