using Library.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace LibraryWebApi.ExceptionHandlerMiddleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {

        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            var details = new ProblemDetails();

            switch (exception)
            {
               case UserCreationException validationCredentialsException:
                    details.Status = (int)HttpStatusCode.BadRequest;
                    details.Title = "Validation Error";
                    details.Detail = "Password or username is invalid";
                    break;
                case BookDataException validationCredentialsException:
                    details.Status = (int)HttpStatusCode.BadRequest;
                    details.Title = "Validation Error";
                    details.Detail = "ISBN already exists";
                    break;
                case InvalidPassworException validationCredentialsException:
                    details.Status = (int)HttpStatusCode.BadRequest;
                    details.Title = "Validation Error";
                    details.Detail = "Password is invalid";
                    break;
                case DataValidationException dataValidationException:
                    details.Status = (int)HttpStatusCode.BadRequest;
                    details.Title = "Data Validation Error";
                    details.Detail = "Input data is invalid";
                    break;
                case EntityNotFoundException userNotFoundException:
                    details.Status = (int)HttpStatusCode.NotFound;
                    details.Title = "Entity Not Found";
                    details.Detail = userNotFoundException.Message;
                    break;
                case LoginAlreadyExistsException userException:
                    details.Status = (int)HttpStatusCode.Unauthorized;
                    details.Title = "Login Is In Use";
                    details.Detail = userException.Message;
                    break;
                case WrongPasswordException userException:
                    details.Status = (int)HttpStatusCode.Unauthorized;
                    details.Title = "Wrong Password";
                    details.Detail = userException.Message;
                    break;
                case BookTakenException bookTakenException:
                    details.Status = (int)HttpStatusCode.BadRequest;
                    details.Title = "Book Taken";
                    details.Detail = bookTakenException.Message;
                    break;
                case InvalidCoverImageException bookCoverImageException:
                    details.Status = (int)HttpStatusCode.BadRequest;
                    details.Title = "Book Cover Image Invalid";
                    details.Detail = bookCoverImageException.Message;
                    break;
                default:
                    details.Status = (int)HttpStatusCode.InternalServerError;
                    details.Title = "Internal Server Error";
                    details.Detail = "An unexpected error occurred.";
                    break;
            }

            details.Type = details.Status switch
            {
                400 => "Client Error",
                401 => "Unauthorized",
                500 => "Server Error",
                _ => "Unknown",
            };

            var response = JsonSerializer.Serialize(details);
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(response, cancellationToken);

            return true;
        }
    }
}
