namespace VisaService.ValueObjects.Exceptions;

public class ArgumentNullOrWhiteSpaceException(string paramName)
    : ArgumentNullException(paramName, $"Аргумент \"{paramName}\" не может быть null, пустым или состоять только из пробелов");
 