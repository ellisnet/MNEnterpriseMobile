using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NewEmployee.Data;
using NewEmployee.Data.Models;

namespace NewEmployee.WebApi.TestClient {
    public partial class MainForm : Form {

        private readonly string apiBaseUrl = "http://192.168.1.120/NewEmployee/";

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {

        }

        private async void uiTestDocument_Click(object sender, EventArgs e) {
            List<Document> docs = null;
            using (var client = new WebApiClient(apiBaseUrl)) {
                docs = await client.GetAsync<List<Document>>("Document", "1");
            }
            string breakhere = "to look at docs";
        }

        private async void uiTestAllDocuments_Click(object sender, EventArgs e) {
            List<Document> docs = null;
            using (var client = new WebApiClient(apiBaseUrl)) {
                docs = await client.GetAsync<List<Document>>("Document", "0");
            }
            string breakhere = "to look at docs";
        }

        private async void uiTestEmployee_Click(object sender, EventArgs e) {
            Employee emp = null;
            using (var client = new WebApiClient(apiBaseUrl)) {
                emp = await client.GetAsync<Employee>("Employee", "4001");
            }
            string breakhere = "to look at emp";
        }

        private async void uiTestTests_Click(object sender, EventArgs e) {
            List<Test> tests = null;
            using (var client = new WebApiClient(apiBaseUrl)) {
                tests = await client.GetAsync<List<Test>>("Tests");
            }
            string breakhere = "to look at tests";
        }

        private async void uiTestTestDetail_Click(object sender, EventArgs e) {
            Test test = null;
            using (var client = new WebApiClient(apiBaseUrl)) {
                test = await client.GetAsync<Test>("TestDetail", "2");
            }
            string breakhere = "to look at test";
        }

        private async void uiTestTestScore_Click(object sender, EventArgs e) {
            TestScore score = null;

            using (var client = new WebApiClient(apiBaseUrl)) {
                Test test = await client.GetAsync<Test>("TestDetail", "2");

                foreach (Question question in test.Questions) {
                    question.Answers.Last().UserAnswer = true;
                }
                test.EmployeeId = 4001;

                score = await client.PostAsync<Test, TestScore>(test, "TestScore");
            }

            string breakhere = "to look at score";
        }
    }
}
