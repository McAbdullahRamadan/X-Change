using System.Net;

namespace Application.Responses
{
    public class DataResponse<T>
    {
        public readonly HttpStatusCode StatusCode;

        public bool IsSuccess { get; private set; }

        public ResultStatus Status { get; private set; }

        public T? Data { get; private set; }

        public IEnumerable<string>? Errors { get; private set; }

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

        public static DataResponse<T> Failure(IEnumerable<string> errors) =>
     new DataResponse<T>(false, ResultStatus.BadRequest, default, errors);
        public static DataResponse<T> Success(T data) =>
            new DataResponse<T>(true, ResultStatus.Success, data, null);

        public static DataResponse<T> Created(T data) =>
            new DataResponse<T>(true, ResultStatus.Created, data, null);

        public static DataResponse<T> Deleted() =>
            new DataResponse<T>(true, ResultStatus.Deleted, default, null);

        public static DataResponse<T> BadRequest(IEnumerable<string> errors) =>
            new DataResponse<T>(false, ResultStatus.BadRequest, default, errors);

        public static DataResponse<T> Unauthorized(string error) =>
            new DataResponse<T>(false, ResultStatus.Unauthorized, default, new[] { error });

        public static DataResponse<T> NotFound(string error) =>
            new DataResponse<T>(false, ResultStatus.NotFound, default, new[] { error });

        public static DataResponse<T> UnprocessableEntity(IEnumerable<string> errors) =>
            new DataResponse<T>(false, ResultStatus.UnprocessableEntity, default, errors);
    }
}
