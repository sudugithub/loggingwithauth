using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service.Exceptions;
using SpanTechnologyTask.Utils;

namespace SpanTechnologyTask.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidAuthException)
            {
                var invalideAuthException = context.Exception as InvalidAuthException;
                var response = new GenericResponse<Exception>(false, invalideAuthException.Message);

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 401,
                };
            }
            else if (context.Exception is NotFoundException)
            {
                var notFoundException = context.Exception as NotFoundException;
                var response = new GenericResponse<Exception>(false, notFoundException.Message);

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 404
                };
            }
            else
            {
                var response = new GenericResponse<Exception>(false, context.Exception.Message);
                context.Result = new ObjectResult(response)
                {
                    StatusCode = 400,
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
