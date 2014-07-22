using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using NewEmployee.Data.Models;

namespace NewEmployee.WebApi.Controllers
{
    public class EmployeeController : ApiController {
        public Employee Get(int id) {
            using (var repository = new Repository()) {
                return repository.LoginEmployee(id);
            }
        }
    }

    public class DocumentController : ApiController {
        public List<Document> Get(int id) {
            using (var repository = new Repository()) {
                return repository.GetDocument(id);
            }
        }
    }

    public class TestsController : ApiController {
        public List<Test> Get() {
            using (var repository = new Repository()) {
                return repository.GetTests();
            }
        }
    }

    public class TestDetailController : ApiController {
        public Test Get(int id) {
            using (var repository = new Repository()) {
                return repository.GetTestDetails(id, false);
            }
        }
    }

    public class TestScoreController : ApiController {
        public TestScore Post([FromBody]Test value) {
            using (var repository = new Repository()) {
                return repository.SaveTestGetScore(value);
            }
        }
    }
}
