using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using NewEmployee.App.Views;

namespace NewEmployee.App
{
	public class App {

        public static readonly string ApiBaseUrl = "http://192.168.1.120/NewEmployee/";

		public static Page GetMainPage() {
            //return new ContentPage
            //{
            //    Content = new Label {
            //        Text = "Hello, Forms !",
            //        VerticalOptions = LayoutOptions.CenterAndExpand,
            //        HorizontalOptions = LayoutOptions.CenterAndExpand,
            //    },
            //};
            var loginPage = new LoginPage();
            return new NavigationPage(loginPage);
		}
	}
}
