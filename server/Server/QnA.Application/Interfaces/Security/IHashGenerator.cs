namespace QnA.Application.Interfaces.Security
{
    public interface IHashGenerator
    {
        string ComputeHash(string value);
    }
}
