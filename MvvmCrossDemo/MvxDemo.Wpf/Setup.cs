using System.Windows.Threading;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Wpf.Platform;
using Cirrious.MvvmCross.Wpf.Views;

using Cirrious.CrossCore;
using MvxDemo.Core.Widgets;
using MvxDemo.Wpf.Widgets;

namespace MvxDemo.Wpf
{
    public class Setup
        : MvxWpfSetup
    {
        public Setup(Dispatcher dispatcher, IMvxWpfViewPresenter presenter)
            : base(dispatcher, presenter)
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
            Mvx.RegisterType<IAlertMessage, WpfAlertMessage>();
        }
    }
}
