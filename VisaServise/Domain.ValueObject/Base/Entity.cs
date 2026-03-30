namespace Domain.ValueObject.Base;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; }

    protected Entity(TId id) => Id = id;
    protected Entity() { }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(left, right);
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);
}