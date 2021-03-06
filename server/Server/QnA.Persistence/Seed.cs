﻿using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QnA.Persistence
{
    public static class DbContextExtensions
    {
        public static void SeedData(this DatabaseContext context)
        {

            if (!context.Users.Any())
            {
                var password = "10000.R4IRL7OubrIK0rB8umMKEg==.ZQ1q4pjcA9QLJhPolHFo6o1vsgFEX2tWasf0uQ3aQO0="; //password
                var users = new List<AppUser>
                {
                    new AppUser {Email = "test1@gmail.com",PasswordHash=password,FirstName="Test",LastName="last"},
                    new AppUser {Email = "test2@gmail.com",PasswordHash=password,FirstName="Test",LastName="last"}
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!context.Questions.Any())
            {
                var questions = new List<Question>
                {
                    new Question {
                        
                        UserId = 1,
                        QuestionText = "Objects sick condemned ive harold by for not and and sullen his scorching sacred said to talethis fellow by far",
                        DateTime= DateTime.Now.AddDays(-1)
                    },
                    new Question
                    {
                        
                        UserId = 2,
                        QuestionText = "Them deem as of and by mother for harold within will hall coffined that mote he and awake revel childe?",
                        DateTime = DateTime.Now.AddDays(-2)
                    },
                    new Question
                    {
                        
                        UserId = 1,
                        QuestionText = "His satiety them call chaste his feud his where he long nor pangs than that basked mote a were lemans?",
                        DateTime = DateTime.Now.AddDays(-3)
                    },
                    new Question
                    {
                        
                        UserId = 2,
                        QuestionText = "Be alone delight name longed sister spoiled made aye ofttimes shun muse worse cared longdeserted?",
                        DateTime = DateTime.Now.AddMonths(-1)
                    },
                    new Question {
                        
                        UserId = 1,
                        QuestionText = "Objects sick condemned ive harold by for not and and sullen his scorching sacred said to talethis fellow by far",
                        DateTime= DateTime.Now.AddDays(-1)
                    },
                    new Question
                    {
                        
                        UserId = 2,
                        QuestionText = "Them deem as of and by mother for harold within will hall coffined that mote he and awake revel childe?",
                        DateTime = DateTime.Now.AddDays(-2)
                    },
                    new Question
                    {
                        
                        UserId = 1,
                        QuestionText = "His satiety them call chaste his feud his where he long nor pangs than that basked mote a were lemans?",
                        DateTime = DateTime.Now.AddDays(-3)
                    },
                    new Question
                    {
                        
                        UserId = 2,
                        QuestionText = "Be alone delight name longed sister spoiled made aye ofttimes shun muse worse cared longdeserted?",
                        DateTime = DateTime.Now.AddMonths(-1)
                    },
                    new Question {
                        
                        UserId = 1,
                        QuestionText = "Objects sick condemned ive harold by for not and and sullen his scorching sacred said to talethis fellow by far",
                        DateTime= DateTime.Now.AddDays(-1)
                    },
                    new Question
                    {
                        
                        UserId = 2,
                        QuestionText = "Them deem as of and by mother for harold within will hall coffined that mote he and awake revel childe?",
                        DateTime = DateTime.Now.AddDays(-2)
                    },
                    new Question
                    {
                        
                        UserId = 1,
                        QuestionText = "His satiety them call chaste his feud his where he long nor pangs than that basked mote a were lemans?",
                        DateTime = DateTime.Now.AddDays(-3)
                    },
                    new Question
                    {
                        
                        UserId = 2,
                        QuestionText = "Be alone delight name longed sister spoiled made aye ofttimes shun muse worse cared longdeserted?",
                        DateTime = DateTime.Now.AddMonths(-1)
                    },
                    new Question {
                        
                        UserId = 1,
                        QuestionText = "Objects sick condemned ive harold by for not and and sullen his scorching sacred said to talethis fellow by far",
                        DateTime= DateTime.Now.AddDays(-1)
                    },
                    new Question
                    {
                        
                        UserId = 2,
                        QuestionText = "Them deem as of and by mother for harold within will hall coffined that mote he and awake revel childe?",
                        DateTime = DateTime.Now.AddDays(-2)
                    },
                    new Question
                    {
                        
                        UserId = 1,
                        QuestionText = "His satiety them call chaste his feud his where he long nor pangs than that basked mote a were lemans?",
                        DateTime = DateTime.Now.AddDays(-3)
                    },
                    new Question
                    {
                        
                        UserId = 2,
                        QuestionText = "Be alone delight name longed sister spoiled made aye ofttimes shun muse worse cared longdeserted?",
                        DateTime = DateTime.Now.AddMonths(-1)
                    },new Question {
                        
                        UserId = 1,
                        QuestionText = "Objects sick condemned ive harold by for not and and sullen his scorching sacred said to talethis fellow by far",
                        DateTime= DateTime.Now.AddDays(-1)
                    },
                    new Question
                    {
                        
                        UserId = 2,
                        QuestionText = "Them deem as of and by mother for harold within will hall coffined that mote he and awake revel childe?",
                        DateTime = DateTime.Now.AddDays(-2)
                    },
                    new Question
                    {
                        
                        UserId = 1,
                        QuestionText = "His satiety them call chaste his feud his where he long nor pangs than that basked mote a were lemans?",
                        DateTime = DateTime.Now.AddDays(-3)
                    },
                    new Question
                    {
                        
                        UserId = 2,
                        QuestionText = "Be alone delight name longed sister spoiled made aye ofttimes shun muse worse cared longdeserted?",
                        DateTime = DateTime.Now.AddMonths(-1)
                    }
                };

                context.Questions.AddRange(questions);
                context.SaveChanges();
            }
        }
    }
}
