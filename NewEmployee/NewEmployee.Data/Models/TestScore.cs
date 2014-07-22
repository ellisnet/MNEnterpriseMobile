using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEmployee.Data.Models {
    public class TestScore {

        private int _employeeId;
        private int _testId;
        private int _score;
        private bool _pass;

        public int EmployeeId {
            get { return _employeeId; }
            set { _employeeId = value; }
        }

        public int TestId {
            get { return _testId; }
            set { _testId = value; }
        }

        public int Score {
            get { return _score; }
            set { _score = value; }
        }

        public bool Pass {
            get { return _pass; }
            set { _pass = value; }
        }

    }
}
