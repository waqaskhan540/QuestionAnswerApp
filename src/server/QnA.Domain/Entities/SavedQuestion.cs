using System;

namespace QnA.Domain.Entities
{
    public class SavedQuestion
    {
       public int Id {get;set;}
       public int QuestionId {get;set;}
       public int UserId {get;set;}

       public DateTime DateTime {get;set;}

       public Question Question {get;set;}
       public AppUser User {get;set;}

    }
}
