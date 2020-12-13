namespace QnA.Domain.Entities
{
    public class ProfileImage
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }

        public int UserId { get; set; }

        public AppUser User { get; set; }
    }
}
