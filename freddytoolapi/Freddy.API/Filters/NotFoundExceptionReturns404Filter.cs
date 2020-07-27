using Freddy.Application.Core.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Freddy.API.Filters
{
    public class NotFoundExceptionReturns404Filter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.ExceptionHandled = false;
            }
        }
    }
}