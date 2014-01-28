using MonoTouch.UIKit;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;

using Cirrious.CrossCore;
using MvxDemo.Core.Widgets;
using MvxDemo.Touch.Widgets;

namespace MvxDemo.Touch
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
		{
		}

		protected override IMvxApplication CreateApp ()
		{
			return new Core.App();
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeLastChance() {
            base.InitializeLastChance();
            Mvx.RegisterType<IAlertMessage, TouchAlertMessage>();
        }
	}
}