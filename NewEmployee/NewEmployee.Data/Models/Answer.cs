using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEmployee.Data.Models {
    public class Answer {

        private int _answerId;
        private int _answerOrder;
        private string _answerText;
        private bool _correctAnswer;
        private bool _userAnswer;

        public int AnswerId {
            get { return _answerId; }
            set { _answerId = value; }
        }

        public int AnswerOrder {
            get { return _answerOrder; }
            set { _answerOrder = value; }
        }

        public string AnswerText {
            get { return _answerText; }
            set { _answerText = value; }
        }

        public bool CorrectAnswer {
            get { return _correctAnswer; }
            set { _correctAnswer = value; }
        }

        public bool UserAnswer {
            get { return _userAnswer; }
            set { _userAnswer = value; }
        }

    }
}
