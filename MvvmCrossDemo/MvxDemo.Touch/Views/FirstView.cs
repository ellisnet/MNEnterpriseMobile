using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MvxDemo.Touch.Views {
    [Register("FirstView")]
    public class FirstView : MvxViewController {
        public override void ViewDidLoad() {
            View = new UIView() { BackgroundColor = UIColor.White };
            base.ViewDidLoad();

            // ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
                EdgesForExtendedLayout = UIRectEdge.None;

            var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();

            Add(new UILabel(new RectangleF(10, 10, 300, 40)) { Text = "E-mail Address:" });

            var emailAddress = new UITextField(new RectangleF(10, 50, 300, 40));
            emailAddress.BackgroundColor = UIColor.LightGray;
            Add(emailAddress);
            set.Bind(emailAddress).To(vm => vm.EmailAddress);

            Add(new UILabel(new RectangleF(10, 90, 300, 40)) { Text = "Password:" });

            var password = new UITextField(new RectangleF(10, 130, 300, 40));
            password.BackgroundColor = UIColor.LightGray;
            Add(password);
            set.Bind(password).To(vm => vm.Password);

            UIColor iOS7BlueLight = UIColor.FromRGB(60, 153, 255);
            var loginButton = new UIButton(UIButtonType.Custom);
            loginButton.SetTitle("Login", UIControlState.Normal);
            loginButton.Frame = new RectangleF(10, 190, 300, 40);
            loginButton.BackgroundColor = iOS7BlueLight;
            loginButton.Layer.CornerRadius = 10.0f;
            Add(loginButton);
            set.Bind(loginButton).To(vm => vm.LoginCommand);

            set.Apply();
        }
    }
}