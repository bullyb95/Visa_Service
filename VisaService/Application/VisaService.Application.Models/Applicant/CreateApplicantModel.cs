using VisaService.Application.Models.Base;

namespace VisaService.Application.Models.Applicant;

public record class CreateApplicantModel(
    string LastName,
    string FirstName,
    string MiddleName,
    string Email,
    string Phone) : ICreateModel;
