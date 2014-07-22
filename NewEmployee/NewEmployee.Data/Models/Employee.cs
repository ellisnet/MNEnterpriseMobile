using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEmployee.Data.Models {
    public class Employee {

        private int _employeeId;
        private string _name;
        private string _emailAddress;

        public int EmployeeId {
            get { return _employeeId; }
            set { _employeeId = value; }
        }

        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        public string EmailAddress {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

    }
}
