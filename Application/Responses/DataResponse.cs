using System.Net;

namespace Application.Responses
{
    public class DataResponse<T>
    {
        public bool IsSuccess { get; private set; }

        public ResultStatus Status { get; private set; }

        public T? Data { get; private set; }

        public IEnumerable<string>? Errors { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public bool Succeeded { get; private set; }
        public object Meta { get; private set; }
        public string Message { get; private set; }

        private DataResponse(
            bool isSuccess,
            ResultStatus status,
            T? data,
            IEnumerable<string>? errors)
        {
            IsSuccess = isSuccess;
            Status = status;
            Data = data;
            Errors = errors;
        }

        public DataResponse()
        {
        }

        public DataResponse<T> Success<T>(T entites, object meta = null)
        {
            return new DataResponse<T>()
            {
                Data = entites,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Meta = meta,
                Message = ""
            };
        }
        // ✅ Success
        public static DataResponse<T> Success(T data, string meesage) =>
            new(true, ResultStatus.Success, data, null);

        // ✅ Created
        public static DataResponse<T> Created(T data) =>
            new(true, ResultStatus.Created, data, null);

        // ✅ Deleted
        public static DataResponse<T> Deleted() =>
            new(true, ResultStatus.Deleted, default, null);

        // ❌ Bad Request
        public static DataResponse<T> BadRequest(IEnumerable<string> errors) =>
            new(false, ResultStatus.BadRequest, default, errors);

        // ❌ Unauthorized
        public static DataResponse<T> Unauthorized(IEnumerable<string> errors) =>
            new(false, ResultStatus.Unauthorized, default, errors);

        // ❌ Not Found
        public static DataResponse<T> NotFound(string error) =>
            new(false, ResultStatus.NotFound, default, new[] { error });

        // ❌ Unprocessable Entity
        public static DataResponse<T> UnprocessableEntity(IEnumerable<string> errors) =>
            new(false, ResultStatus.UnprocessableEntity, default, errors);
    }
}
