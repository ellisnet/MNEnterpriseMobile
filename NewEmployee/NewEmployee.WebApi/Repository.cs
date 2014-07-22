using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using NewEmployee.Data.Models;
using System.Web;

namespace NewEmployee.WebApi {
    public class Repository : IDisposable {

        public static readonly string documentUrlBase = "http://192.168.1.120/NewEmployee/Home/Document/";

        public static string connectionString;

        //The constructor of a new Repository object that sets up our database connection string
        public Repository() {
            //Goes and gets the connection string from the web.config file the first time a
            //  Repository object is created.
            connectionString = connectionString ?? ConfigurationManager.AppSettings["DatabaseConnection"];
        }

        public Employee LoginEmployee(int employeeId) {
            Employee result = null;

            using (var connection = new SqlConnection(connectionString)) {
                using (var command = new SqlCommand("dbo.LoginEmployee", connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@EmployeeID", employeeId));
                    using (var adapter = new SqlDataAdapter(command)) {
                        using (var ds = new DataSet()) {
                            adapter.Fill(ds);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                                DataRow row = ds.Tables[0].Rows[0];
                                result = new Employee {
                                    EmployeeId = row.Field<int>("EmployeeID"),
                                    Name = row.Field<string>("Name"),
                                    EmailAddress = row.Field<string>("EmailAddress")
                                };
                            }
                        }
                    }
                }
            }

            return result;
        }

        public List<Document> GetDocument(int documentId) {
            var result = new List<Document>();

            using (var connection = new SqlConnection(connectionString)) {
                using (var command = new SqlCommand("dbo.GetDocument", connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    if (documentId > 0) command.Parameters.Add(new SqlParameter("@DocumentID", documentId));
                    using (var adapter = new SqlDataAdapter(command)) {
                        using (var ds = new DataSet()) {
                            adapter.Fill(ds);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                                foreach (DataRow row in ds.Tables[0].Rows) {
                                    result.Add(new Document {
                                        DocumentId = row.Field<int>("DocumentID"),
                                        Title = row.Field<string>("Title"),
                                        File = row.Field<string>("File"),
                                        Url = documentUrlBase + row.Field<int>("DocumentID").ToString()
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public List<Test> GetTests() {
            var result = new List<Test>();

            using (var connection = new SqlConnection(connectionString)) {
                using (var command = new SqlCommand("dbo.GetTests", connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var adapter = new SqlDataAdapter(command)) {
                        using (var ds = new DataSet()) {
                            adapter.Fill(ds);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                                foreach (DataRow row in ds.Tables[0].Rows) {
                                    result.Add(new Test {
                                        TestId = row.Field<int>("TestID"),
                                        Title = row.Field<string>("Title"),
                                        Description = row.Field<string>("Description")
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public Test GetTestDetails(int testId, bool returnCorrect = true) {
            Test result = null;

            using (var connection = new SqlConnection(connectionString)) {
                using (var command = new SqlCommand("dbo.GetTestDetails", connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@TestID", testId));
                    using (var adapter = new SqlDataAdapter(command)) {
                        using (var ds = new DataSet()) {
                            adapter.Fill(ds);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                                DataRow testRow = ds.Tables[0].Rows[0];
                                result = new Test {
                                    TestId = testRow.Field<int>("TestID"),
                                    Title = testRow.Field<string>("Title"),
                                    Description = testRow.Field<string>("Description"),
                                    PassingScore = testRow.Field<int>("PassingScore"),
                                    Questions = new List<Question>()
                                };

                                foreach (DataRow questionRow in ds.Tables[1].Rows) {
                                    var question = new Question {
                                        QuestionId = questionRow.Field<int>("QuestionID"),
                                        QuestionOrder = questionRow.Field<int>("QuestionOrder"),
                                        QuestionText = questionRow.Field<string>("QuestionText"),
                                        Answers = new List<Answer>()
                                    };

                                    foreach (DataRow answerRow in ds.Tables[2].AsEnumerable()
                                        .Where(a => a.Field<int>("QuestionID") == question.QuestionId))
                                    {
                                        var answer = new Answer {
                                            AnswerId = answerRow.Field<int>("AnswerID"),
                                            AnswerOrder = answerRow.Field<int>("AnswerOrder"),
                                            AnswerText = answerRow.Field<string>("AnswerText"),
                                            CorrectAnswer = returnCorrect && answerRow.Field<bool>("CorrectAnswer")
                                        };
                                        question.Answers.Add(answer);
                                    }
                                    result.Questions.Add(question);
                                 }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public TestScore SaveTestGetScore(Test userTest) {
            var result = new TestScore {
                EmployeeId = userTest.EmployeeId,
                TestId = userTest.TestId
            };

            Test checkText = GetTestDetails(userTest.TestId, true);

            int numCorrect = 0;
            foreach (Question question in userTest.Questions) {
                int userAnswer = (question.Answers.Where(a => a.UserAnswer).Count() > 0) ?
                    question.Answers.Where(a => a.UserAnswer).First().AnswerId : -1;
                if (checkText.Questions
                    .Where(q => q.QuestionId == question.QuestionId).Single().Answers
                    .Where(a => a.CorrectAnswer).Select(c => c.AnswerId)
                    .Contains(userAnswer))
                    numCorrect++;
            }

            if (numCorrect > 0) {
                result.Score = Convert.ToInt32(Math.Round(((double)numCorrect / (double)checkText.Questions.Count) * (double)100, MidpointRounding.AwayFromZero));
            }

            result.Pass = result.Score >= checkText.PassingScore;

            using (var connection = new SqlConnection(connectionString)) {
                using (var command = new SqlCommand("dbo.SaveTestScore", connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@EmployeeID", result.EmployeeId));
                    command.Parameters.Add(new SqlParameter("@TestID", result.TestId));
                    command.Parameters.Add(new SqlParameter("@Score", result.Score));
                    command.Parameters.Add(new SqlParameter("@Pass", result.Pass ? 1 : 0));

                    connection.Open();
                    int sqlResult = command.ExecuteNonQuery();
                }
            }

            return result;
        }

        public void Dispose() {
            connectionString = null;
        }
    }
}
