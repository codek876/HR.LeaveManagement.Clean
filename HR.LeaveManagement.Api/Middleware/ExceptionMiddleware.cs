using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using SendGrid.Helpers.Errors.Model;
using System.Net;
using BadRequestException = HR.LeaveManagement.Application.Contracts.Exceptions.BadRequestException;
using NotFoundException = HR.LeaveManagement.Application.Contracts.Exceptions.NotFoundException;

namespace HR.LeaveManagement.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem = new();
            
            switch (ex) {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(badRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;
                case NotFoundException NotFound:
                    problem = new CustomProblemDetails
                    {
                        Title = NotFound.Message,
                        Status = (int)statusCode,
                        Detail = ex.InnerException?.Message,
                        Type = nameof(NotFoundException),
                    };
                    break;
                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Detail = ex.StackTrace,
                        Type = nameof(HttpStatusCode.InternalServerError),
                    };
                    break;

            }

            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
