using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Xamarin.Forms;

using NewEmployee.Data;
using NewEmployee.Data.Models;
using NewEmployee.App.Views;

namespace NewEmployee.App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public LoginViewModel(INavigation navigation) {
            _navigation = navigation;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _pageTitle = "Application Login";
        public string PageTitle {
            get { return _pageTitle; }
            set {
                value = (value ?? "").Trim();
                if (value != _pageTitle) {
                    _pageTitle = value;
                    OnPropertyChanged("PageTitle");
                }                 
            }
        }

        private string _employeeId = "";
        public string EmployeeId {
            get { return _employeeId; }
            set {
                if (value != _employeeId) {
                    _employeeId = value;
                    OnPropertyChanged("EmployeeId");
                }
            }
        }

        private Command<string> loginCommand;
        public Command LoginCommand {
            get {
                return loginCommand ?? (loginCommand = new Command<string>(DoLoginCommand, CanLogin));
            }
        }

        private bool CanLogin(string employeeId) {
            return !String.IsNullOrWhiteSpace(employeeId);
        }

        private async void DoLoginCommand(string employeeId) {
            if (!String.IsNullOrWhiteSpace(employeeId)) {
                using (var client = new WebApiClient(App.ApiBaseUrl)) {
                    Employee emp = await client.GetAsync<Employee>("Employee", employeeId);
                    if (emp != null && emp.EmployeeId > 0) {
                        this.EmployeeId = "";
                        _navigation.PushAsync(new TrainingPage(emp));
                    }
                }
            }
        }

    }
}
