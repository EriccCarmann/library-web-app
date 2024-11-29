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
                case DataValidationException dataValidationException:
                    details.Status = (int)HttpStatusCode.BadRequest;
                    details.Title = "Validation Error";
                    details.Detail = dataValidationException.Message;
                    break;
                case BookTakenException bookTakenException:
                    details.Status = (int)HttpStatusCode.BadRequest;
                    details.Title = "Book Already Taken";
                    details.Detail = bookTakenException.Message;
                    break;
                case InvalidCoverImageException bookCoverImageException:
                    details.Status = (int)HttpStatusCode.BadRequest;
                    details.Title = "Invalid Book Cover";
                    details.Detail = bookCoverImageException.Message;
                    break;
                case UserCreationException userCreationException:
                    details.Status = (int)HttpStatusCode.BadRequest;
                    details.Title = "User Creation Error";
                    details.Detail = "Password or username is invalid.";
                    break;
                case WrongPasswordException wrongPasswordException:
                    details.Status = (int)HttpStatusCode.Unauthorized;
                    details.Title = "Authentication Failed";
                    details.Detail = "Incorrect password.";
                    break;
                case TokenExpiredException tokenExpiredException:
                    details.Status = (int)HttpStatusCode.Unauthorized;
                    details.Title = "Token Expired";
                    details.Detail = "The provided authentication token has expired.";
                    break;
                case LoginAlreadyExistsException loginAlreadyExistsException:
                    details.Status = (int)HttpStatusCode.Conflict;
                    details.Title = "Login Already Exists";
                    details.Detail = loginAlreadyExistsException.Message;
                    break;
                case BookDataException bookDataException:
                    details.Status = (int)HttpStatusCode.Conflict;
                    details.Title = "Duplicate Book Data";
                    details.Detail = "The provided ISBN already exists.";
                    break;
                case EntityNotFoundException entityNotFoundException:
                    details.Status = (int)HttpStatusCode.NotFound;
                    details.Title = "Resource Not Found";
                    details.Detail = entityNotFoundException.Message;
                    break;
                default:
                    details.Status = (int)HttpStatusCode.InternalServerError;
                    details.Title = "Internal Server Error";
                    details.Detail = "An unexpected error occurred.";
                    break;
            }

            var response = JsonSerializer.Serialize(details);
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(response, cancellationToken);

            return true;
        }
    }
}
