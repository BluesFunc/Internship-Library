namespace WebAPI.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
  
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Server Internal Error");
            logger.LogError(e.Message);
            logger.LogError(e.StackTrace);
        }
    }
}