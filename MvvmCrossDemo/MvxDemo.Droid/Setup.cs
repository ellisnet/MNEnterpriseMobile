using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;

using Cirrious.CrossCore;
using MvxDemo.Core.Widgets;
using MvxDemo.Droid.Widgets;

namespace MvxDemo.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeLastChance() {
            base.InitializeLastChance();
            Mvx.RegisterType<IAlertMessage, DroidAlertMessage>();
        }
    }
}