using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsStore.Platform;
using Windows.UI.Xaml.Controls;

using Cirrious.CrossCore;
using MvxDemo.Core.Widgets;
using MvxDemo.Store.Widgets;

namespace MvxDemo.Store
{
    public class Setup : MvxStoreSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
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
            Mvx.RegisterType<IAlertMessage, StoreAlertMessage>();
        }
    }
}