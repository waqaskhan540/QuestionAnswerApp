namespace QnA.Application.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string name, string email, int userId);
    }
}
