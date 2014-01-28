using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYPROJECT.Core.Widgets {

    public interface IAlertMessage {

        //Example usage:
        //using MYPROJECT.Core.Widgets;
        //
        //IAlertMessage myAlertMsg = Mvx.Resolve<IAlertMessage>();
        //AlertMessageResult myResult = await myAlertMsg.ShowAsync("Alert message - are you sure?", "Alert Title", AlertMessageButtons.OKCancel);
        //if (myResult != AlertMessageResult.Cancel) {
        //    //Do something here
        //}

        Task<AlertMessageResult> ShowAsync(string text,
            string caption = "",
            AlertMessageButtons buttons = AlertMessageButtons.OK,
            AlertMessageIcon icon = AlertMessageIcon.None);

    }

    public enum AlertMessageResult {
        None = 0,
        Abort,
        Cancel,
        Ignore,
        No,
        OK,
        Retry,
        Yes
    }

    public enum AlertMessageButtons {
        OK = 0,
        AbortRetryIgnore,
        OKCancel,
        RetryCancel,
        YesNo,
        YesNoCancel
    }

    public enum AlertMessageIcon {
        None = 0,
        Asterisk,
        Error,
        Exclamation,
        Hand,
        Information,
        Question,
        Stop,
        Warning
    }
}
