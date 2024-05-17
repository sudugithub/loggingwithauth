using Data.Domain;
using Service.LogEventService;

namespace SpanTechnologyTask.Middlewares
{
    public class RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, ILogEventService logEventService)
        {
            try
            {
                context.Request.EnableBuffering();
                var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                var originalBodyStream = context.Response.Body;

                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                // Continue down the middleware pipeline
                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var logEntry = new LogEvent
                {
                    Method = context.Request.Method,
                    EndPoint = context.Request.Path,
                    RequestBody = requestBody,
                    ResponseBody = responseBodyText,
                    StatusCode = context.Response.StatusCode
                };

                await logEventService.Create(logEntry);

                await responseBody.CopyToAsync(originalBodyStream);
                context.Response.Body = originalBodyStream;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
