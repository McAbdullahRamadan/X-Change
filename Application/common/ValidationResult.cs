namespace Application.common
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationError> Errors { get; set; } = new();

        public static ValidationResult Success() => new() { IsValid = true };

        public static ValidationResult Failure(List<ValidationError> errors)
            => new() { IsValid = false, Errors = errors };
    }

    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
