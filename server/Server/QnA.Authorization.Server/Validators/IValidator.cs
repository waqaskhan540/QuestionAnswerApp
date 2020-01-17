namespace QnA.Authorization.Server.Validators
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T input);
    }
}
