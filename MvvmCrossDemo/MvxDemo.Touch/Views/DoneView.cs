using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MvxDemo.Touch.Views {
    [Register("DoneView")]
    public class DoneView : MvxViewController {
        public override void ViewDidLoad() {
            View = new UIView() { BackgroundColor = UIColor.White };
            base.ViewDidLoad();

            // ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
                EdgesForExtendedLayout = UIRectEdge.None;

            Add(new UILabel(new RectangleF(10, 10, 300, 40)) { Text = "Thank you!" });
        }
    }
}