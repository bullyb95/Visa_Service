using VisaService.Domain.Exceptions;
using VisaService.WebHost.Responses;

namespace VisaService.WebHost.Middlewares;

/// <summary>
/// Перехватывает исключения из Domain/Application и превращает их в понятный HTTP-ответ,
/// чтобы клиент получал 400/409 с текстом ошибки, а не 500 со стектрейсом.
/// </summary>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (status, error) = exception switch
        {
            // нарушение бизнес-правил: заявка в неверном состоянии, чужая заявка, чужой офицер
            InvalidApplicationStateException => (StatusCodes.Status409Conflict, "InvalidApplicationState"),
            ApplicationNotBelongApplicantException => (StatusCodes.Status409Conflict, "ApplicationNotBelongApplicant"),
            AnotherOfficerEditApplicationException => (StatusCodes.Status409Conflict, "AnotherOfficerEditApplication"),
            // невалидное значение Value Object (длина, пустая строка, формат email)
            ArgumentException => (StatusCodes.Status400BadRequest, "InvalidArgument"),
            FormatException => (StatusCodes.Status400BadRequest, "InvalidFormat"),
            OperationCanceledException => (499, "RequestCancelled"),
            _ => (StatusCodes.Status500InternalServerError, "InternalServerError")
        };

        if (status == StatusCodes.Status500InternalServerError)
            logger.LogError(exception, "Необработанное исключение при запросе {Method} {Path}",
                context.Request.Method, context.Request.Path);
        else
            logger.LogWarning("Запрос {Method} {Path} завершился ошибкой {Status}: {Message}",
                context.Request.Method, context.Request.Path, status, exception.Message);

        var message = status == StatusCodes.Status500InternalServerError
            ? "Внутренняя ошибка сервера"
            : exception.Message;

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(new ErrorResponse(status, error, message, context.TraceIdentifier));
    }
}
