using Domain.ValueObject;
using Domain.ValueObject.Base;
using Domain.ValueObject.Exceptions;

namespace Domain.Visa_Servise;

public class Officer : Entity<Guid>
{
    public PersonName Nam { get; private set; }
    public OfficerPosition Position { get; private set; }

    private readonly List<VisaApplication> _processedApplications = new();
    public IReadOnlyCollection<VisaApplication> ProcessedApplications => _processedApplications.AsReadOnly();

    protected Officer() { }

    public Officer(Guid id, PersonName name, OfficerPosition position) : base(id)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Position = position ?? throw new ArgumentNullValueException(nameof(position));
    }

    public bool ChangePosition(OfficerPosition newPosition)
    {
        if (newPosition == null) throw new ArgumentNullValueException(nameof(newPosition));
        if (Position == newPosition) return false;
        Position = newPosition;
        return true;
    }

    public void AssignToApplication(VisaApplication application)
    {
        if (application.Officer == this) return;
        application.SetOfficer(this);
        if (!_processedApplications.Contains(application))
            _processedApplications.Add(application);
    }
}