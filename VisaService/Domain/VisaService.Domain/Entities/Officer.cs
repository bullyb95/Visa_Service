using VisaService.ValueObjects;
using VisaService.ValueObjects.Base;
using VisaService.Domain.Exceptions;

namespace VisaService.Domain.Entities;

public class Officer : Entity<Guid>
{
    public LastName LastName { get; private set; }
    public FirstName FirstName { get; private set; }
    public MiddleName MiddleName { get; private set; }
    public OfficerPosition Position { get; private set; }

    protected Officer() : base() { }

    public Officer(
        LastName lastName,
        FirstName firstName,
        MiddleName middleName,
        OfficerPosition position)
        : this(Guid.NewGuid(), lastName, firstName, middleName, position) { }

    protected Officer(
        Guid id,
        LastName lastName,
        FirstName firstName,
        MiddleName middleName,
        OfficerPosition position)
        : base(id)
    {
        LastName = lastName ?? throw new ArgumentNullValueException(nameof(lastName));
        FirstName = firstName ?? throw new ArgumentNullValueException(nameof(firstName));
        MiddleName = middleName ?? throw new ArgumentNullValueException(nameof(middleName));
        Position = position ?? throw new ArgumentNullValueException(nameof(position));
    }

    public void AcceptApplication(VisaApplication application)
    {
        if (application == null) throw new ArgumentNullValueException(nameof(application));
        application.AcceptApplication(this);
    }

    public void VerifyData(VisaApplication application)
    {
        if (application == null) throw new ArgumentNullValueException(nameof(application));
        application.VerifyData(this);
    }

    public void IssueVisa(VisaApplication application)
    {
        if (application == null) throw new ArgumentNullValueException(nameof(application));
        application.IssueVisa(this);
    }

    public void RejectVisa(VisaApplication application)
    {
        if (application == null) throw new ArgumentNullValueException(nameof(application));
        application.RejectVisa(this);
    }

    public override string ToString()
        => $"{LastName} {FirstName} {MiddleName}";
}
