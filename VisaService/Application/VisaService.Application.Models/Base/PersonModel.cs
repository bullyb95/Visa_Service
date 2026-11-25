namespace VisaService.Application.Models.Base;

public abstract record class PersonModel(Guid Id, string LastName, string FirstName, string MiddleName)
    : IModel<Guid>;
