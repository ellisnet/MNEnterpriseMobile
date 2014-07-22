using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using NewEmployee.Data.Models;

namespace NewEmployee.App.Views
{
    public class TrainingPage : TabbedPage {

        private Employee _employee;

        public TrainingPage(Employee employee) {
            _employee = employee;
            this.Title = "Training for " + employee.Name;
            //Children.Add(new ContentPage {
            //    Title = "Documents",
            //    Padding = 10,
            //    Content = new Label {
            //        Text = "Documents go here"
            //    }
            //});
            Children.Add(new DocumentsPage());
            Children.Add(new ContentPage {
                Title = "Tests",
                Padding = 10,
                Content = new Label {
                    Text = "Tests go here"
                }
            });
        }

    }
}
