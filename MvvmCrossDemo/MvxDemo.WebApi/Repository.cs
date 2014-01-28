using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using MvxDemo.Models;

namespace MvxDemo.WebApi {
    public class Repository : IDisposable {

        public static string connectionString;

        //The constructor of a new Repository object that sets up our database connection string
        public Repository() {
            //Goes and gets the connection string from the web.config file the first time a
            //  Repository object is created.
            connectionString = connectionString ?? ConfigurationManager.AppSettings["DatabaseConnection"];
        }

        public User LoginUser(string emailAddress, string password) {
            //This method uses ADO.NET to execute the [MvxDemo].[LoginUser] stored procedure
            //  and populates (and returns) a new User object with the results.
            //  Note that if no matching data is found in the database, a User object will
            //  still be returned, but it will have a UserId of 0.

            var result = new User();

            using (var connection = new SqlConnection(connectionString)) {
                using (var command = new SqlCommand("MvxDemo.LoginUser", connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@EmailAddress", emailAddress));
                    command.Parameters.Add(new SqlParameter("@Password", password));
                    using (var adapter = new SqlDataAdapter(command)) {
                        using (var ds = new DataSet()) {
                            adapter.Fill(ds);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                                result.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                                result.FirstName = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
                                result.LastName = Convert.ToString(ds.Tables[0].Rows[0]["LastName"]);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public List<Question> GetQuestionsAndAnswers() {
            //This method uses ADO.NET to execute the [MvxDemo].[GetQuestionsAndAnswers] stored procedure
            //  and populates (and returns) a list of questions and their possible answers with the results

            var result = new List<Question>();

            using (var connection = new SqlConnection(connectionString)) {
                using (var command = new SqlCommand("MvxDemo.GetQuestionsAndAnswers", connection)) {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var adapter = new SqlDataAdapter(command)) {
                        using (var ds = new DataSet()) {
                            adapter.Fill(ds);
                            foreach (DataRow questionRow in ds.Tables[0].Rows) {
                                var newQuestion = new Question() {
                                    QuestionId = Convert.ToInt32(questionRow["ID"]),
                                    QuestionOrder = Convert.ToInt32(questionRow["QuestionOrder"]),
                                    QuestionText = Convert.ToString(questionRow["QuestionText"]),
                                    PossibleAnswers = new List<Answer>()
                                };

                                foreach (DataRow answerRow in ds.Tables[1].AsEnumerable()
                                    .Where(row => row.Field<int>("QuestionID") == Convert.ToInt32(questionRow["ID"]))
                                    .ToList()) {
                                    var newAnswer = new Answer() {
                                        AnswerId = Convert.ToInt32(answerRow["ID"]),
                                        AnswerOrder = Convert.ToInt32(answerRow["AnswerOrder"]),
                                        AnswerText = Convert.ToString(answerRow["AnswerText"]),
                                        QuestionId = Convert.ToInt32(questionRow["ID"])
                                    };
                                    newQuestion.PossibleAnswers.Add(newAnswer);
                                }
                                result.Add(newQuestion);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public bool SaveUserAnswers(List<UserAnswer> userAnswers) {
            //This method takes a list of UserAnswers and saves it to the database by calling the 
            //  [MvxDemo].[SaveUserAnswer] stored procedure via ADO.NET

            bool result = false;

            if (userAnswers != null) {
                using (var connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    foreach (UserAnswer answer in userAnswers) {
                        using (var command = new SqlCommand("MvxDemo.SaveUserAnswer", connection)) {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@UserID", answer.UserId));
                            command.Parameters.Add(new SqlParameter("@QuestionID", answer.QuestionId));
                            command.Parameters.Add(new SqlParameter("@AnswerID", answer.AnswerId));
                            command.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                    result = true;
                }
            }
            return result;
        }

        public void Dispose() {
            connectionString = null;
        }
    }
}
