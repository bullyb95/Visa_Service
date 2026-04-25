namespace VisaService.ValueObjects.Exceptions;

public class ValidatorNullException(string paramName)
    : ArgumentNullException(paramName, $"Валидатор \"{paramName}\" должен быть указан");
 