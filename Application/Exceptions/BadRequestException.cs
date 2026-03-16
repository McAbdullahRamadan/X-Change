using System.Net;

namespace Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public object? Errors { get; }

        public BadRequestException(string message)
            : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }

        public BadRequestException(string message, object errors)
            : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Errors = errors;
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
