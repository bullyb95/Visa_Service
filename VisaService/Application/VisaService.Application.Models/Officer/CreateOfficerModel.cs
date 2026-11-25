using VisaService.Application.Models.Base;

namespace VisaService.Application.Models.Officer;

public record class CreateOfficerModel(
    string LastName,
    string FirstName,
    string MiddleName,
    string Position) : ICreateModel;
