using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEmployee.Data.Models {
    public class Question {

        private int _questionId;
        private int _questionOrder;
        private string _questionText;
        private List<Answer> _answers;
        private bool _questionAnswered;

        public int QuestionId {
            get { return _questionId; }
            set { _questionId = value; }
        }

        public int QuestionOrder {
            get { return _questionOrder; }
            set { _questionOrder = value; }
        }

        public string QuestionText {
            get { return _questionText; }
            set { _questionText = value; }
        }

        public List<Answer> Answers {
            get { return _answers; }
            set { _answers = value; }
        }

        public bool QuestionAnswered {
            get { return _questionAnswered; }
            set { _questionAnswered = value; }
        }

    }
}
