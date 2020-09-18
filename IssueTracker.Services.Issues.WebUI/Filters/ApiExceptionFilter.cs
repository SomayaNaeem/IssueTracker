using IssueTracker.Services.Issues.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IssueTracker.Services.Issues.WebUI.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {

        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilter()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(ExistException),HandleExistException},
                {typeof(AccessDeniedException),HandleAccessDeniedException }
            };
        }



        public override void OnException(ExceptionContext context)
        {
            // If known exception, will use registered handler.
            TryHandleException(context);

            // If not, use default beahviour.
            base.OnException(context);
        }

        private void TryHandleServiceException(ExceptionContext context)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            context.ExceptionHandled = true;
        }
        private void TryHandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
                _exceptionHandlers[type].Invoke(context);
            else
                TryHandleServiceException(context);
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            context.Result = new BadRequestObjectResult(new ErrorCode<IDictionary<string, string[]>>(exception.Errors)
            {
                Code = HttpStatusCode.BadRequest,
                Message = "One or more validation errors occurred.",
                Description = "One or more validation errors occurred."
            });

            context.ExceptionHandled = true;
        }
        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            context.Result = new NotFoundObjectResult(new ErrorCode<string[]>(null)
            {
                Code = HttpStatusCode.NotFound,
                Message = "The specified resource was not found.",
                Description = exception.Message
            });

            context.ExceptionHandled = true;
        }
        private void HandleExistException(ExceptionContext context)
        {
            var exception = context.Exception as ExistException;

            context.Result = new BadRequestObjectResult(new ErrorCode<string[]>(null)
            {
                Code = HttpStatusCode.BadRequest,
                Message = "The specified resource was Exist.",
                Description = exception.Message
            });

            context.ExceptionHandled = true;
        }
        private void HandleAccessDeniedException(ExceptionContext context)
        {
            var exception = context.Exception as AccessDeniedException;

            context.Result = new BadRequestObjectResult(new ErrorCode<string[]>(null)
            {
                Code = HttpStatusCode.Forbidden,
                Message = "Access denied to remove this resource.",
                Description = exception.Message
            });

            context.ExceptionHandled = true;
        }
    }
    public class ErrorCode<T>
    {
        public ErrorCode(T errors)
        {
            Errors = errors;
        }
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Description { get; set; }
        public T Errors { get; set; }
    }
}
