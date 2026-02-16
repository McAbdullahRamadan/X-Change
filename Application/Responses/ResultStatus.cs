namespace Application.Responses
{
    public enum ResultStatus
    {
        Success = 200,
        Created = 201,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        UnprocessableEntity = 422,
        Deleted = 204
    }
}
