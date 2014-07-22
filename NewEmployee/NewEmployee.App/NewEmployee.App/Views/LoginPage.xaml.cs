using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NewEmployee.App.ViewModels;

namespace NewEmployee.App.Views
{
	public partial class LoginPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            this.BindingContext = new LoginViewModel(Navigation);
		}
	}
}
