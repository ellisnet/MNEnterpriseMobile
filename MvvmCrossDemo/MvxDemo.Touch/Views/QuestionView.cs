using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;

using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using MvxDemo.Models;
using MvxDemo.Touch.ViewParts;
using MvxDemo.Core.ViewModels;

namespace MvxDemo.Touch.Views {
    [Register("QuestionView")]
    public class QuestionView : MvxViewController {

        List<Question> _questions;
        int numQuestions = 0;

        public override void ViewDidLoad() {
            View = new UIView() { BackgroundColor = UIColor.White };
            base.ViewDidLoad();

            // ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
                EdgesForExtendedLayout = UIRectEdge.None;

            _questions = ((QuestionViewModel)ViewModel).Questions;
            numQuestions = _questions.Count;
            ((QuestionViewModel)this.ViewModel).PropertyChanged += (s, e) => {
                if (e.PropertyName == "Questions" &&
                    ((QuestionViewModel)ViewModel).Questions.Count > numQuestions) {

                    _questions = ((QuestionViewModel)ViewModel).Questions;
                    numQuestions = _questions.Count;
                    BuildQuestionTable(_questions);
                }
            };

            if (numQuestions > 0) BuildQuestionTable(_questions);

            var set = this.CreateBindingSet<QuestionView, Core.ViewModels.QuestionViewModel>();

            UIColor iOS7BlueLight = UIColor.FromRGB(60, 153, 255);
            var saveButton = new UIButton(UIButtonType.Custom);
            saveButton.SetTitle("Save Answers", UIControlState.Normal);
            saveButton.Frame = new RectangleF(10, UIScreen.MainScreen.Bounds.Height - 110, 300, 40);
            saveButton.BackgroundColor = iOS7BlueLight;
            saveButton.Layer.CornerRadius = 10.0f;
            Add(saveButton);
            set.Bind(saveButton).To(vm => vm.SaveAnswersCommand);

            set.Apply();
        }

        void BuildQuestionTable(List<Question> questions) {
            var questionTable = new UITableView(
                new RectangleF(0, 0,
                    UIScreen.MainScreen.Bounds.Width,
                    UIScreen.MainScreen.Bounds.Height - 120),
                    UITableViewStyle.Grouped);
            var tableSource = new QuestionTableViewSource(questions);
            questionTable.Source = tableSource;
            Add(questionTable);
        }
    }
}
