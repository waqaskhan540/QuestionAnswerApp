namespace QnA.Authorization.Server.Validators
{
    public class ValidationResult
    {
        public bool Error { get; set; }
        public string ValidationError { get; set; }

        public static ValidationResult Fail(string errorMessage)
        {
            return new ValidationResult
            {
                Error = true,
                ValidationError = errorMessage
            };
        }

        public static ValidationResult Success()
        {
            return new ValidationResult
            {
                Error = false
            };
        }
    }
}
