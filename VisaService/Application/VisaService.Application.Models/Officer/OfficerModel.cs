using VisaService.Application.Models.Base;

namespace VisaService.Application.Models.Officer;

public record class OfficerModel(
    Guid Id,
    string LastName,
    string FirstName,
    string MiddleName,
    string Position) : PersonModel(Id, LastName, FirstName, MiddleName);
