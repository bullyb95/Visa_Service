namespace Domain.ValueObject.Exceptions;

public class ArgumentNullOrWhiteSpaceException(string paramName)
    : ArgumentNullException(paramName, $"The \"{paramName}\" must not be null, empty or consist only of white-space characters.");