using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

using MYPROJECT.Core.Widgets;

namespace MYPROJECT.Store.Widgets {

    //NOTE: The following class requires something like this in the Setup.cs of the Windows Store project:
    //
    //   using Cirrious.CrossCore;
    //   using MYPROJECT.Core.Widgets;
    //   using MYPROJECT.Store.Widgets;
    //
    //   protected override void InitializeLastChance() {
    //        base.InitializeLastChance();
    //        Mvx.RegisterType<IAlertMessage, StoreAlertMessage>();
    //    }

    public class StoreAlertMessage : IAlertMessage {

        private string _buttonPressed = "";

        private void CommandInvokedHandler(IUICommand command) {
            _buttonPressed = command.Label;
        }

        async public Task<AlertMessageResult> ShowAsync(string text,
            string caption = "",
            AlertMessageButtons buttons = AlertMessageButtons.OK,
            AlertMessageIcon icon = AlertMessageIcon.None) {

            _buttonPressed = "";
            AlertMessageResult result = AlertMessageResult.None;

            if (text != null && text.Trim() != "") {
                text = text.Trim();
                if (caption == null) { caption = "Message"; } else { caption = caption.Trim(); }

                MessageDialog myMessage = new MessageDialog(text, caption);

                #region Figure out the buttons
                switch (buttons) {
                    case AlertMessageButtons.AbortRetryIgnore:
                        myMessage.Commands.Add(new UICommand("Abort", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.Commands.Add(new UICommand("Retry", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.Commands.Add(new UICommand("Ignore", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.CancelCommandIndex = 0;
                        myMessage.DefaultCommandIndex = 1;
                        break;
                    case AlertMessageButtons.OK:
                        myMessage.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.DefaultCommandIndex = 0;
                        break;
                    case AlertMessageButtons.OKCancel:
                        myMessage.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.DefaultCommandIndex = 0;
                        myMessage.CancelCommandIndex = 1;
                        break;
                    case AlertMessageButtons.RetryCancel:
                        myMessage.Commands.Add(new UICommand("Retry", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.DefaultCommandIndex = 0;
                        myMessage.CancelCommandIndex = 1;
                        break;
                    case AlertMessageButtons.YesNo:
                        myMessage.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.Commands.Add(new UICommand("No", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.DefaultCommandIndex = 0;
                        myMessage.CancelCommandIndex = 1;
                        break;
                    case AlertMessageButtons.YesNoCancel:
                        myMessage.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.Commands.Add(new UICommand("No", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                        myMessage.DefaultCommandIndex = 0;
                        myMessage.CancelCommandIndex = 2;
                        break;
                    default:
                        throw new ArgumentException("The specified AlertMessage button set is not available in Windows Store projects.", "buttons");
                        break;
                }
                #endregion

                await myMessage.ShowAsync();

                #region Figure out the result
                switch (_buttonPressed) {
                    case "Abort":
                        result = AlertMessageResult.Abort;
                        break;
                    case "Cancel":
                        result = AlertMessageResult.Cancel;
                        break;
                    case "Ignore":
                        result = AlertMessageResult.Ignore;
                        break;
                    case "No":
                        result = AlertMessageResult.No;
                        break;
                    case "OK":
                        result = AlertMessageResult.OK;
                        break;
                    case "Retry":
                        result = AlertMessageResult.Retry;
                        break;
                    case "Yes":
                        result = AlertMessageResult.Yes;
                        break;
                    default:
                        result = AlertMessageResult.None;
                        break;
                }
                #endregion
            }

            return result;
        }

    }

}
