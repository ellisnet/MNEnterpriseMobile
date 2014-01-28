using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MvxDemo.Models;

namespace MvxDemo.WebApi.Controllers {
    public class UserController : ApiController {
        public User Get(string emailAddress, string password) {
            using (var repository = new Repository()) {
                return repository.LoginUser(emailAddress, password);
            }
        }
    }

    public class QuestionsController : ApiController {
        public List<Question> Get() {
            using (var repository = new Repository()) {
                return repository.GetQuestionsAndAnswers();
            }
        }
    }

    public class UserAnswersController : ApiController {
        public bool Post([FromBody]List<UserAnswer> value) {
            using (var repository = new Repository()) {
                return repository.SaveUserAnswers(value);
            }
        }
    }
}