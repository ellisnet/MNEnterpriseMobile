using System.Collections.Generic;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

using MvxDemo.Core.Widgets;
using MvxDemo.Models;

namespace MvxDemo.Core.ViewModels {
    public class FirstViewModel
        : MvxViewModel {
        private string apiBaseUrl = "http://192.168.1.109/MvxDemo/";

        private string _emailAddress = "";
        public string EmailAddress {
            get { return _emailAddress; }
            set { _emailAddress = value; RaisePropertyChanged(() => EmailAddress); }
        }

        private string _password = "";
        public string Password {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(() => Password); }
        }

        private MvxCommand _loginCommand;
        public System.Windows.Input.ICommand LoginCommand {
            get { _loginCommand = _loginCommand ?? new MvxCommand(DoLogin); return _loginCommand; }
        }

        private async void DoLogin() {
            if (_emailAddress.Trim() != "" && _password.Trim() != "") {
                using (var apiClient = new WebApiClient(apiBaseUrl)) {
                    User user = await apiClient.GetAsync<User>("User", _emailAddress, _password);
                    IAlertMessage alertMsg = Mvx.Resolve<IAlertMessage>();

                    if (user != null && user.UserId != 0) {
                        AlertMessageResult msgResult =
                            await alertMsg.ShowAsync(
                            "Welcome back, " + user.FirstName + " " + user.LastName + ".",
                            "Login Successful",
                            AlertMessageButtons.OK);
                        ShowViewModel<QuestionViewModel>(new { userId = user.UserId, apiBaseUrl = this.apiBaseUrl });
                    }
                    else {
                        AlertMessageResult msgResult =
                            await alertMsg.ShowAsync(
                            "Unable to login with this username and password.",
                            "Login Unsuccessful",
                            AlertMessageButtons.OK);
                    }
                }
            }
        }
    }
}
