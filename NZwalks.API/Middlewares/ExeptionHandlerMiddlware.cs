using System.Net;

namespace NZwalks.API.Middlewares
{
    public class ExeptionHandlerMiddlware
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next;

        public ExeptionHandlerMiddlware(ILogger logger,
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //Log This Exception
                logger.LogError(ex, $"{errorId}:{ex.Message}");

                //Return A Custom Exerror Response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";


                var error = new
                {
                    Id=errorId,
                    ErrorMassage = "Something went wrong!"
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
