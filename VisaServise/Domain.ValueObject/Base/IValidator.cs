namespace Domain.ValueObject.Base;

public interface IValidator<in T>
{
    void Validate(T value);
}