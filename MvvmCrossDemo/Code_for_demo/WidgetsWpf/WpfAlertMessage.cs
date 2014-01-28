using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using MYPROJECT.Core.Widgets;

namespace MYPROJECT.Wpf.Widgets {

    //NOTE: The following class requires something like this in the Setup.cs of the WPF project:
    //
    //   using Cirrious.CrossCore;
    //   using MYPROJECT.Core.Widgets;
    //   using MYPROJECT.Wpf.Widgets;
    //
    //   protected override void InitializeLastChance() {
    //      base.InitializeLastChance();
    //      Mvx.RegisterType<IAlertMessage, WpfAlertMessage>();
    //   }

    public class WpfAlertMessage : IAlertMessage {

        async public Task<AlertMessageResult> ShowAsync(string text, 
            string caption = "", 
            AlertMessageButtons buttons = AlertMessageButtons.OK, 
            AlertMessageIcon icon = AlertMessageIcon.None) {

            AlertMessageResult result = AlertMessageResult.None;

            if (text != null && text.Trim() != "") {
                text = text.Trim();
                if (caption == null) { caption = "Message"; } else { caption = caption.Trim(); }

                MessageBoxButton myButton = MessageBoxButton.OK;
                #region Figure out the button
                switch (buttons) {
                    case AlertMessageButtons.AbortRetryIgnore:
                        throw new NotImplementedException("The AbortRetryIgnore AlertMessage button set is not implemented in WPF.");
                        break;
                    case AlertMessageButtons.OK:
                        myButton = MessageBoxButton.OK;
                        break;
                    case AlertMessageButtons.OKCancel:
                        myButton = MessageBoxButton.OKCancel;
                        break;
                    case AlertMessageButtons.RetryCancel:
                        throw new NotImplementedException("The RetryCancel AlertMessage button set is not implemented in WPF.");
                        break;
                    case AlertMessageButtons.YesNo:
                        myButton = MessageBoxButton.YesNo;
                        break;
                    case AlertMessageButtons.YesNoCancel:
                        myButton = MessageBoxButton.YesNoCancel;
                        break;
                    default:
                        throw new ArgumentException("The specified AlertMessage button set is not available in WPF projects.", "buttons");
                        break;
                }
                #endregion

                MessageBoxImage myIcon = MessageBoxImage.None;
                #region Figure out the icon
                switch (icon) {
                    case AlertMessageIcon.Asterisk:
                        myIcon = MessageBoxImage.Asterisk;
                        break;
                    case AlertMessageIcon.Error:
                        myIcon = MessageBoxImage.Error;
                        break;
                    case AlertMessageIcon.Exclamation:
                        myIcon = MessageBoxImage.Exclamation;
                        break;
                    case AlertMessageIcon.Hand:
                        myIcon = MessageBoxImage.Hand;
                        break;
                    case AlertMessageIcon.Information:
                        myIcon = MessageBoxImage.Information;
                        break;
                    case AlertMessageIcon.None:
                        myIcon = MessageBoxImage.None;
                        break;
                    case AlertMessageIcon.Question:
                        myIcon = MessageBoxImage.Question;
                        break;
                    case AlertMessageIcon.Stop:
                        myIcon = MessageBoxImage.Stop;
                        break;
                    case AlertMessageIcon.Warning:
                        myIcon = MessageBoxImage.Warning;
                        break;
                    default:
                        throw new ArgumentException("The specified AlertMessage icon set is not available in WPF projects.", "icon");
                        break;
                }
                #endregion

                MessageBoxResult myResult = MessageBox.Show(text, caption, myButton, myIcon);
                #region Figure out the result
                switch (myResult) {
                    case MessageBoxResult.Cancel:
                        result = AlertMessageResult.Cancel;
                        break;
                    case MessageBoxResult.No:
                        result = AlertMessageResult.No;
                        break;
                    case MessageBoxResult.OK:
                        result = AlertMessageResult.OK;
                        break;
                    case MessageBoxResult.Yes:
                        result = AlertMessageResult.Yes;
                        break;
                    case MessageBoxResult.None:
                        result = AlertMessageResult.None;
                        break;
                }
                #endregion
            }
            
            return result;
        }

    }
}
