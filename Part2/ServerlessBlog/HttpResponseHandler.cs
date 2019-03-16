using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AzureFromTheTrenches.Commanding;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Abstractions.Http;
using FunctionMonkey.Commanding.Abstractions.Validation;
using Microsoft.AspNetCore.Mvc;
using ServerlessBlog.Application.Exceptions;

namespace ServerlessBlog
{
    public class HttpResponseHandler : IHttpResponseHandler
    {
        private static readonly Dictionary<Type, HttpStatusCode> ExceptionResponses = new Dictionary<Type, HttpStatusCode>
        {
            {typeof(PostNotFoundException), HttpStatusCode.NotFound}
        };

        public Task<IActionResult> CreateResponseFromException<TCommand>(TCommand command, Exception ex) where TCommand : ICommand
        {
            Exception unwrappedException = ex;
            if (ex is CommandExecutionException cex)
            {
                unwrappedException = cex.InnerException;
            }

            if (ExceptionResponses.TryGetValue(unwrappedException.GetType(), out HttpStatusCode code))
            {
                return Task.FromResult((IActionResult)new StatusCodeResult((int)code));
            }

            return Task.FromResult((IActionResult)new InternalServerErrorResult());
        }

        public Task<IActionResult> CreateResponse<TCommand, TResult>(TCommand command, TResult result) where TCommand : ICommand<TResult>
        {
            return null;
        }

        public Task<IActionResult> CreateResponse<TCommand>(TCommand command)
        {
            return null;
        }

        public Task<IActionResult> CreateValidationFailureResponse<TCommand>(TCommand command, ValidationResult validationResult) where TCommand : ICommand
        {
            return null;
        }
    }
}
