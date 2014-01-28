using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace MvxDemo.Droid.Views
{
    [Activity(Label = "Done")]
    public class DoneView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DoneView);
        }
    }
}