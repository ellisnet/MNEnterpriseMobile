using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;

using MYPROJECT.Core.Widgets;

namespace MYPROJECT.Droid.Widgets {

    //NOTE: The following class requires something like this in the Setup.cs of the Android project:
    //
    //   using Cirrious.CrossCore;
    //   using MYPROJECT.Core.Widgets;
    //   using MYPROJECT.Droid.Widgets;
    //
    //protected override void InitializeLastChance() {
    //    base.InitializeLastChance();
    //    Mvx.RegisterType<IAlertMessage, DroidAlertMessage>();
    //}

    public class DroidAlertMessage : IAlertMessage {

        private TaskCompletionSource<AlertMessageResult> _tcs;

        public Task<AlertMessageResult> ShowAsync(string text, 
            string caption = "", 
            AlertMessageButtons buttons = AlertMessageButtons.OK, 
            AlertMessageIcon icon = AlertMessageIcon.None) {

            _tcs = new TaskCompletionSource<AlertMessageResult>();
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

            if (activity == null) {
                Console.WriteLine("Couldn't find top activity.");
            }

            if (activity != null && text != null && text.Trim() != "") {
                text = text.Trim();
                if (caption == null) { caption = "Message"; } else { caption = caption.Trim(); }

                var dialog = new AlertDialog.Builder(activity);
                //TODO: Figure out how set the custom title
                //if (caption != "") { builder.SetCustomTitle(need to create custom view from caption); }
                dialog.SetMessage(text);
                dialog.SetCancelable(false);

                #region Figure out the buttons
                switch (buttons) {
                    case AlertMessageButtons.AbortRetryIgnore:
                        dialog.SetPositiveButton("Retry", delegate { _tcs.SetResult(AlertMessageResult.Retry); });
                        dialog.SetNeutralButton("Ignore", delegate { _tcs.SetResult(AlertMessageResult.Ignore); });
                        dialog.SetNegativeButton("Abort", delegate { _tcs.SetResult(AlertMessageResult.Abort); });
                        break;
                    case AlertMessageButtons.OK:
                        dialog.SetPositiveButton("OK", delegate { _tcs.SetResult(AlertMessageResult.OK); });
                        break;
                    case AlertMessageButtons.OKCancel:
                        dialog.SetPositiveButton("OK", delegate { _tcs.SetResult(AlertMessageResult.OK); });
                        dialog.SetNegativeButton("Cancel", delegate { _tcs.SetResult(AlertMessageResult.Cancel); });
                        break;
                    case AlertMessageButtons.RetryCancel:
                        dialog.SetPositiveButton("Retry", delegate { _tcs.SetResult(AlertMessageResult.Retry); });
                        dialog.SetNegativeButton("Cancel", delegate { _tcs.SetResult(AlertMessageResult.Cancel); });
                        break;
                    case AlertMessageButtons.YesNo:
                        dialog.SetPositiveButton("Yes", delegate { _tcs.SetResult(AlertMessageResult.Yes); });
                        dialog.SetNegativeButton("No", delegate { _tcs.SetResult(AlertMessageResult.No); });
                        break;
                    case AlertMessageButtons.YesNoCancel:
                        dialog.SetPositiveButton("Yes", delegate { _tcs.SetResult(AlertMessageResult.Yes); });
                        dialog.SetNeutralButton("No", delegate { _tcs.SetResult(AlertMessageResult.No); });
                        dialog.SetNegativeButton("Cancel", delegate { _tcs.SetResult(AlertMessageResult.Cancel); });
                        break;
                    default:
                        throw new ArgumentException("The specified AlertMessage button set is not available in Android projects.", "buttons");
                        break;
                }
                #endregion

                dialog.Create().Show();
            }
            else {
                _tcs.SetResult(AlertMessageResult.None);
            }

            return _tcs.Task;
        }
    }
}
