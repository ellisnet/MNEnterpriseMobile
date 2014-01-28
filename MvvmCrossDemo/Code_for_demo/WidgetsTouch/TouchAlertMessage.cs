using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MYPROJECT.Core.Widgets;

namespace MYPROJECT.Touch.Widgets {

    //NOTE: The following class requires something like this in the Setup.cs of the MonoTouch/iOS project:
    //
    //   using Cirrious.CrossCore;
    //   using MYPROJECT.Core.Widgets;
    //   using MYPROJECT.Touch.Widgets;
    //
    //protected override void InitializeLastChance() {
    //    base.InitializeLastChance();
    //    Mvx.RegisterType<IAlertMessage, TouchAlertMessage>();
    //}

    public class TouchAlertMessage : IAlertMessage {

        private TaskCompletionSource<AlertMessageResult> _tcs;

        public Task<AlertMessageResult> ShowAsync(string text, 
            string caption = "", 
            AlertMessageButtons buttons = AlertMessageButtons.OK, 
            AlertMessageIcon icon = AlertMessageIcon.None) {

            _tcs = new TaskCompletionSource<AlertMessageResult>();

            if (text != null && text.Trim() != "") {
                text = text.Trim();
                if (caption == null) { caption = "Message"; } else { caption = caption.Trim(); }

                Console.WriteLine("Showing message: " + text);

                UIAlertView alert = new UIAlertView {
                    Title = caption,
                    Message = text
                };

                #region Figure out the buttons
                switch (buttons) {
                    case AlertMessageButtons.AbortRetryIgnore:
                        alert.AddButton("Abort");
                        alert.AddButton("Retry");
                        alert.AddButton("Ignore");
                        alert.Clicked += (s, e) => {
                            switch (e.ButtonIndex) {
                                case 0:
                                    _tcs.SetResult(AlertMessageResult.Abort);
                                    break;
                                case 1:
                                    _tcs.SetResult(AlertMessageResult.Retry);
                                    break;
                                case 2:
                                    _tcs.SetResult(AlertMessageResult.Ignore);
                                    break;
                                default:
                                    _tcs.SetResult(AlertMessageResult.None);
                                    break;
                            }
                        };
                        break;
                    case AlertMessageButtons.OK:
                        alert.AddButton("OK");
                        alert.Clicked += (s, e) => {
                            switch (e.ButtonIndex) {
                                case 0:
                                    _tcs.SetResult(AlertMessageResult.OK);
                                    break;
                                default:
                                    _tcs.SetResult(AlertMessageResult.None);
                                    break;
                            }
                        };
                        break;
                    case AlertMessageButtons.OKCancel:
                        alert.AddButton("OK");
                        alert.AddButton("Cancel");
                        alert.Clicked += (s, e) => {
                            switch (e.ButtonIndex) {
                                case 0:
                                    _tcs.SetResult(AlertMessageResult.OK);
                                    break;
                                case 1:
                                    _tcs.SetResult(AlertMessageResult.Cancel);
                                    break;
                                default:
                                    _tcs.SetResult(AlertMessageResult.None);
                                    break;
                            }
                        };
                        break;
                    case AlertMessageButtons.RetryCancel:
                        alert.AddButton("Retry");
                        alert.AddButton("Cancel");
                        alert.Clicked += (s, e) => {
                            switch (e.ButtonIndex) {
                                case 0:
                                    _tcs.SetResult(AlertMessageResult.Retry);
                                    break;
                                case 1:
                                    _tcs.SetResult(AlertMessageResult.Cancel);
                                    break;
                                default:
                                    _tcs.SetResult(AlertMessageResult.None);
                                    break;
                            }
                        };
                        break;
                    case AlertMessageButtons.YesNo:
                        alert.AddButton("Yes");
                        alert.AddButton("No");
                        alert.Clicked += (s, e) => {
                            switch (e.ButtonIndex) {
                                case 0:
                                    _tcs.SetResult(AlertMessageResult.Yes);
                                    break;
                                case 1:
                                    _tcs.SetResult(AlertMessageResult.No);
                                    break;
                                default:
                                    _tcs.SetResult(AlertMessageResult.None);
                                    break;
                            }
                        };
                        break;
                    case AlertMessageButtons.YesNoCancel:
                        alert.AddButton("Yes");
                        alert.AddButton("No");
                        alert.AddButton("Cancel");
                        alert.Clicked += (s, e) => {
                            switch (e.ButtonIndex) {
                                case 0:
                                    _tcs.SetResult(AlertMessageResult.Yes);
                                    break;
                                case 1:
                                    _tcs.SetResult(AlertMessageResult.No);
                                    break;
                                case 2:
                                    _tcs.SetResult(AlertMessageResult.Cancel);
                                    break;
                                default:
                                    _tcs.SetResult(AlertMessageResult.None);
                                    break;
                            }
                        };
                        break;
                    default:
                        throw new ArgumentException("The specified AlertMessage button set is not available in iOS projects.", "buttons");
                        break;
                }
                #endregion

                alert.Show();
            }
            else {
                _tcs.SetResult(AlertMessageResult.None);
            }

            return _tcs.Task;
        }
    }
}
