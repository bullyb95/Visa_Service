namespace VisaService.WebHost.Responses;

/// <summary>Единый формат ошибки, который возвращает ExceptionHandlingMiddleware.</summary>
public record class ErrorResponse(int Status, string Error, string Message, string TraceId);
