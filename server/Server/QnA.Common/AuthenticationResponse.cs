namespace QnA.Common
{
    public class AuthenticationResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public UserDto User { get; set; }

        public static AuthenticationResponse Error(string message)
        {
            return new AuthenticationResponse { Success = false, Message = message };
        }

        public static AuthenticationResponse Succeed(string message,UserDto user = null)
        {
            return new AuthenticationResponse { Success = true, Message = message, User = user };
        }

        
    }
}
