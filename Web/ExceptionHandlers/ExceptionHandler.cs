using Domain;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Web.ExceptionHandlers;

/// <summary>
/// Менеджер ошибок
/// </summary>
/// <param name="logger"> Логгер </param>
public class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext ctx, Exception e, CancellationToken token)
    {
        logger.LogError(e, $"Exception occurred: {e.Message}");

        var messageTitle = "Server error";
        var messageStatusCode = StatusCodes.Status500InternalServerError;
        var detail = e.Message;

        if (e is QuestionException)
        {
            ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
            messageTitle = "Bad Request";
            messageStatusCode = StatusCodes.Status400BadRequest;
        }

        else if (e is InvalidOperationException)
        {
            if (e.InnerException is NpgsqlException) detail = e.InnerException.Message;
        }

        await ctx.Response.WriteAsJsonAsync(new ProblemDetails
                                            {
                                                Title = messageTitle,
                                                Status = messageStatusCode,
                                                Detail = detail,
                                                Type = e.GetType().Name,
                                                Instance = $"{ctx.Request.Method} {ctx.Request.Path}"
                                            }, token);

        return true;
    }
}