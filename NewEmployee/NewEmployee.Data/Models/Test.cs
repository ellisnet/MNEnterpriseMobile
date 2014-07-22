using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEmployee.Data.Models {
    public class Test {

        private int _testId;
        private int _employeeId;
        private string _title;
        private string _description;
        private int _passingScore;
        private List<Question> _questions;

        public int TestId {
            get { return _testId; }
            set { _testId = value; }
        }

        public int EmployeeId {
            get { return _employeeId; }
            set { _employeeId = value; }
        }

        public string Title {
            get { return _title; }
            set { _title = value; }
        }

        public string Description {
            get { return _description; }
            set { _description = value; }
        }

        public int PassingScore {
            get { return _passingScore; }
            set { _passingScore = value; }
        }

        public List<Question> Questions {
            get { return _questions; }
            set { _questions = value; }
        }

    }
}
