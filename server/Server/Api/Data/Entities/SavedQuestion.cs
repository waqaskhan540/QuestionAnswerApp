using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Entities
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
