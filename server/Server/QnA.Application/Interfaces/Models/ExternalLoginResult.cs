namespace QnA.Application.Interfaces.Models
{
    public class ExternalLoginResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Picture { get; set; }

        public string Error { get; set; }
    }
}
