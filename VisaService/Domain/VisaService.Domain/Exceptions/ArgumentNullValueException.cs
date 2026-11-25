namespace VisaService.Domain.Exceptions;

public class ArgumentNullValueException(string paramName)
    : ArgumentNullException(paramName, $"Аргумент \"{paramName}\" не может быть null");
