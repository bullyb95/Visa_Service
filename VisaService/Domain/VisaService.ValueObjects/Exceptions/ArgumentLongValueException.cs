namespace VisaService.ValueObjects.Exceptions;

public class ArgumentLongValueException(string paramName, string value, int maxLength)
    : FormatException($"Длина \"{paramName}\" ({value.Length}) превышает максимально допустимую ({maxLength})")
{
    public string Value => value;
    public int MaxLength => maxLength;
}
 