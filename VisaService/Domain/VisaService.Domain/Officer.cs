using VisaService.ValueObjects;
using VisaService.ValueObjects.Base;
using VisaService.Domain.Exceptions;

namespace VisaService.Domain;

public class Officer : Entity<Guid>
{
    public LastName LastName { get; private set; }
    public FirstName FirstName { get; private set; }
    public MiddleName MiddleName { get; private set; }
    public OfficerPosition Position { get; private set; }  

    protected Officer() : base()
    {
        LastName = default!;
        FirstName = default!;
        MiddleName = default!;
        Position = default!;
    }

    public Officer(Guid id, LastName lastName, FirstName firstName, MiddleName middleName, OfficerPosition position)
        : base(id)
    {
        LastName = lastName ?? throw new ArgumentNullValueException(nameof(lastName));
        FirstName = firstName ?? throw new ArgumentNullValueException(nameof(firstName));
        MiddleName = middleName ?? throw new ArgumentNullValueException(nameof(middleName));
        Position = position ?? throw new ArgumentNullValueException(nameof(position));
    }

    public bool UpdatePosition(OfficerPosition newPosition)
    {
        if (newPosition == null) throw new ArgumentNullValueException(nameof(newPosition));
        if (Position == newPosition) return false;
        Position = newPosition;
        return true;
    }

    public override string ToString()
        => $"{LastName} {FirstName} {MiddleName}";
}
