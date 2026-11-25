using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisaService.Application.Models.VisaApplication;
using VisaService.Application.Services.Abstractions;
using VisaService.WebHost.Requests.VisaApplication;
using VisaService.WebHost.Responses;
using VisaService.WebHost.Responses.VisaApplication;

namespace VisaService.WebHost.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VisaApplicationsController(IVisaApplicationsApplicationService visaApplicationsApplicationService,
                                        IApplicantsApplicationService applicantsApplicationService,
                                        IOfficersApplicationService officersApplicationService,
                                        IApplicantActionsApplicationService applicantActionsApplicationService,
                                        IOfficerActionsApplicationService officerActionsApplicationService,
                                        IMapper mapper) : ControllerBase
{
    // ------------------------------------------------------------------ чтение

    /// <summary>Получить все заявки.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VisaApplicationShortResponse>))]
    public async Task<IActionResult> GetAllVisaApplications(CancellationToken cancellationToken)
    {
        var applications = await visaApplicationsApplicationService.GetVisaApplicationsAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<VisaApplicationShortResponse>>(applications));
    }

    /// <summary>Получить заявку по id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisaApplicationDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetVisaApplicationById(Guid id, CancellationToken cancellationToken)
    {
        var application = await visaApplicationsApplicationService.GetVisaApplicationByIdAsync(id, cancellationToken);
        if (application is null)
            return NotFound($"Visa application with id:{id} not found");
        return Ok(mapper.Map<VisaApplicationDetailedResponse>(application));
    }

    // ------------------------------------------------------------------ действия заявителя

    /// <summary>Заявитель создаёт заявку.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VisaApplicationDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
    public async Task<IActionResult> CreateVisaApplication(CreateVisaApplicationRequest request, CancellationToken cancellationToken)
    {
        if (await applicantsApplicationService.GetApplicantByIdAsync(request.ApplicantId, cancellationToken) is null)
            return NotFound($"Applicant with id:{request.ApplicantId} not found");

        if (await visaApplicationsApplicationService.IsNumberTakenAsync(request.Number, cancellationToken))
            return Conflict($"Visa application with number:{request.Number} already exists");

        var application = mapper.Map<CreateVisaApplicationModel>(request);
        var createdApplication = await applicantActionsApplicationService.CreateApplicationAsync(application, cancellationToken);
        if (createdApplication is null)
            return BadRequest("Visa application can not be created");

        var applicationResponse = mapper.Map<VisaApplicationDetailedResponse>(createdApplication);
        return CreatedAtAction(nameof(GetVisaApplicationById), new { applicationResponse.Id }, applicationResponse);
    }

    /// <summary>Заявитель заполняет анкету.</summary>
    [HttpPost("fill-form")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisaApplicationDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> FillForm(ApplicantActionRequest request, CancellationToken cancellationToken)
    {
        var notFound = await CheckApplicantActionAsync(request, cancellationToken);
        if (notFound is not null)
            return notFound;

        var application = await applicantActionsApplicationService.FillFormAsync(
            mapper.Map<ApplicantActionModel>(request), cancellationToken);
        if (application is null)
            return NotFound($"Visa application with id:{request.ApplicationId} not found");
        return Ok(mapper.Map<VisaApplicationDetailedResponse>(application));
    }

    /// <summary>Заявитель подаёт документы.</summary>
    [HttpPost("submit-documents")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisaApplicationDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> SubmitDocuments(ApplicantActionRequest request, CancellationToken cancellationToken)
    {
        var notFound = await CheckApplicantActionAsync(request, cancellationToken);
        if (notFound is not null)
            return notFound;

        var application = await applicantActionsApplicationService.SubmitDocumentsAsync(
            mapper.Map<ApplicantActionModel>(request), cancellationToken);
        if (application is null)
            return NotFound($"Visa application with id:{request.ApplicationId} not found");
        return Ok(mapper.Map<VisaApplicationDetailedResponse>(application));
    }

    /// <summary>Заявитель узнаёт результат рассмотрения заявки.</summary>
    [HttpGet("{applicationId:guid}/result")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisaApplicationResultResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetResult(Guid applicationId, [FromQuery] Guid applicantId, CancellationToken cancellationToken)
    {
        var request = new ApplicantActionRequest(applicantId, applicationId);
        var notFound = await CheckApplicantActionAsync(request, cancellationToken);
        if (notFound is not null)
            return notFound;

        var result = await applicantActionsApplicationService.ReceiveResultAsync(
            mapper.Map<ApplicantActionModel>(request), cancellationToken);
        if (result is null)
            return NotFound($"Visa application with id:{request.ApplicationId} not found");
        return Ok(mapper.Map<VisaApplicationResultResponse>(result));
    }

    /// <summary>Заявитель удаляет свою заявку.</summary>
    [HttpDelete("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> DeleteVisaApplication(Guid applicationId, [FromQuery] Guid applicantId, CancellationToken cancellationToken)
    {
        var request = new ApplicantActionRequest(applicantId, applicationId);
        var notFound = await CheckApplicantActionAsync(request, cancellationToken);
        if (notFound is not null)
            return notFound;

        await applicantActionsApplicationService.DeleteApplicationAsync(
            mapper.Map<ApplicantActionModel>(request), cancellationToken);
        return NoContent();
    }

    // ------------------------------------------------------------------ действия офицера

    /// <summary>Офицер принимает заявку в работу.</summary>
    [HttpPost("accept")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisaApplicationDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
    public Task<IActionResult> AcceptApplication(OfficerActionRequest request, CancellationToken cancellationToken)
        => ExecuteOfficerActionAsync(request, officerActionsApplicationService.AcceptApplicationAsync, cancellationToken);

    /// <summary>Офицер проверяет данные заявки.</summary>
    [HttpPost("verify")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisaApplicationDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
    public Task<IActionResult> VerifyData(OfficerActionRequest request, CancellationToken cancellationToken)
        => ExecuteOfficerActionAsync(request, officerActionsApplicationService.VerifyDataAsync, cancellationToken);

    /// <summary>Офицер выдаёт визу.</summary>
    [HttpPost("issue")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisaApplicationDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
    public Task<IActionResult> IssueVisa(OfficerActionRequest request, CancellationToken cancellationToken)
        => ExecuteOfficerActionAsync(request, officerActionsApplicationService.IssueVisaAsync, cancellationToken);

    /// <summary>Офицер отказывает в визе.</summary>
    [HttpPost("reject")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisaApplicationDetailedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse))]
    public Task<IActionResult> RejectVisa(OfficerActionRequest request, CancellationToken cancellationToken)
        => ExecuteOfficerActionAsync(request, officerActionsApplicationService.RejectVisaAsync, cancellationToken);

    // ------------------------------------------------------------------ helpers

    private async Task<IActionResult?> CheckApplicantActionAsync(ApplicantActionRequest request, CancellationToken cancellationToken)
    {
        if (await applicantsApplicationService.GetApplicantByIdAsync(request.ApplicantId, cancellationToken) is null)
            return NotFound($"Applicant with id:{request.ApplicantId} not found");

        if (await visaApplicationsApplicationService.GetVisaApplicationByIdAsync(request.ApplicationId, cancellationToken) is null)
            return NotFound($"Visa application with id:{request.ApplicationId} not found");

        return null;
    }

    private async Task<IActionResult> ExecuteOfficerActionAsync(
        OfficerActionRequest request,
        Func<OfficerActionModel, CancellationToken, Task<VisaApplicationModel?>> action,
        CancellationToken cancellationToken)
    {
        if (await officersApplicationService.GetOfficerByIdAsync(request.OfficerId, cancellationToken) is null)
            return NotFound($"Officer with id:{request.OfficerId} not found");

        var application = await action(mapper.Map<OfficerActionModel>(request), cancellationToken);
        if (application is null)
            return NotFound($"Visa application with id:{request.ApplicationId} not found");

        return Ok(mapper.Map<VisaApplicationDetailedResponse>(application));
    }
}
