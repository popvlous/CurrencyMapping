namespace CurrencyMapping.Middlewares
{
    public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { Message = "Middleware Handle Error, Error Message :" + ex.Message  });
            }
        }
    }
}
