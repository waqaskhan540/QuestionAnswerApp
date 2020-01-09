namespace QnA.Domain.Entities
{
    public class QuestionFollowing
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }

        public AppUser User { get; set; }
        public Question Question { get; set; }
    }
}
