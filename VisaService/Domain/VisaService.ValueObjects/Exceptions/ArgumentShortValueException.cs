namespace VisaService.ValueObjects.Exceptions;

public class ArgumentShortValueException(string paramName, string value, int minLength)
    : FormatException($"Длина \"{paramName}\" ({value.Length}) меньше минимально допустимой ({minLength})")
{
    public string Value => value;
    public int MinLength => minLength;
}
 