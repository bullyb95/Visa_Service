using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisaService.Application.Models.Applicant;
using VisaService.Application.Services.Abstractions;
using VisaService.WebHost.Requests.Applicant;
using VisaService.WebHost.Responses.Applicant;
using VisaService.WebHost.Responses.VisaApplication;

namespace VisaService.WebHost.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ApplicantsController(IApplicantsApplicationService applicantsApplicationService,
                                  IVisaApplicationsApplicationService visaApplicationsApplicationService,
                                  IMapper mapper) : ControllerBase
{
    /// <summary>Получить всех заявителей.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApplicantShortResponse>))]
    public async Task<IActionResult> GetAllApplicants(CancellationToken cancellationToken)
    {
        var applicants = await applicantsApplicationService.GetApplicantsAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<ApplicantShortResponse>>(applicants));
    }

    /// <summary>Получить заявителя по id (вместе с его заявками).</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicantDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetApplicantById(Guid id, CancellationToken cancellationToken)
    {
        var applicant = await applicantsApplicationService.GetApplicantByIdAsync(id, cancellationToken);
        if (applicant is null)
            return NotFound($"Applicant with id:{id} not found");
        return Ok(mapper.Map<ApplicantDetailedResponse>(applicant));
    }

    /// <summary>Найти заявителей по фамилии.</summary>
    [HttpGet("by-last-name/{lastName}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApplicantShortResponse>))]
    public async Task<IActionResult> GetApplicantsByLastName(string lastName, CancellationToken cancellationToken)
    {
        var applicants = await applicantsApplicationService.GetApplicantsByLastNameAsync(lastName, cancellationToken);
        return Ok(mapper.Map<IEnumerable<ApplicantShortResponse>>(applicants));
    }

    /// <summary>Получить все заявки заявителя.</summary>
    [HttpGet("{id:guid}/applications")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VisaApplicationDetailedResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetApplicantApplications(Guid id, CancellationToken cancellationToken)
    {
        var applicant = await applicantsApplicationService.GetApplicantByIdAsync(id, cancellationToken);
        if (applicant is null)
            return NotFound($"Applicant with id:{id} not found");

        var applications = await visaApplicationsApplicationService.GetVisaApplicationsByApplicantIdAsync(id, cancellationToken);
        return Ok(mapper.Map<IEnumerable<VisaApplicationDetailedResponse>>(applications));
    }

    /// <summary>Зарегистрировать заявителя.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApplicantShortResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> CreateApplicant(CreateApplicantRequest request, CancellationToken cancellationToken)
    {
        var applicant = mapper.Map<CreateApplicantModel>(request);
        var createdApplicant = await applicantsApplicationService.CreateApplicantAsync(applicant, cancellationToken);
        if (createdApplicant is null)
            return BadRequest("Applicant can not be created");

        var applicantResponse = mapper.Map<ApplicantShortResponse>(createdApplicant);
        return CreatedAtAction(nameof(GetApplicantById), new { applicantResponse.Id }, applicantResponse);
    }

    /// <summary>Удалить заявителя (его заявки удаляются каскадно).</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> DeleteApplicant(Guid id, CancellationToken cancellationToken)
    {
        if (!await applicantsApplicationService.DeleteApplicantAsync(id, cancellationToken))
            return NotFound($"Applicant with id:{id} not found");
        return NoContent();
    }
}
