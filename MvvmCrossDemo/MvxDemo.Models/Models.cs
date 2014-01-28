using System.Collections.Generic;

namespace MvxDemo.Models {

    //The Question model
    public class Question {
        public int QuestionId { get; set; }
        public int QuestionOrder { get; set; }
        public string QuestionText { get; set; }
        public List<Answer> PossibleAnswers { get; set; }
    }

    //The Answer model
    public class Answer {
        public int AnswerId { get; set; }
        public int AnswerOrder { get; set; }
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
        public bool IsSelected { get; set; }  //note: IsSelected defaults to false for a new Answer object
    }

    //The User model
    public class User {
        public int UserId { get; set; }  //note: UserId defaults to 0 for a new User object
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    //The UserAnswer model
    public class UserAnswer {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}