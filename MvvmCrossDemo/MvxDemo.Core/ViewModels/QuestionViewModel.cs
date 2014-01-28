using System.Collections.Generic;
using System.Linq;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

using MvxDemo.Core.Widgets;
using MvxDemo.Models;

namespace MvxDemo.Core.ViewModels {
    public class QuestionViewModel
        : MvxViewModel {

        private int _userId = 0;
        private string _apiBaseUrl;

        public async void Init(int userId, string apiBaseUrl) {
            _userId = userId;
            _apiBaseUrl = apiBaseUrl;
            List<Question> tempQuestions = null;
            using (var apiClient = new WebApiClient(_apiBaseUrl)) {
                tempQuestions = await apiClient.GetAsync<List<Question>>("Questions");
            }
            if (tempQuestions != null && tempQuestions.Count > 0) {
                Questions = tempQuestions;
            }
        }

        private List<Question> _questions = new List<Question>();
        public List<Question> Questions {
            get { return _questions; }
            set { _questions = value; RaisePropertyChanged(() => Questions); }
        }

        private MvxCommand _saveAnswersCommand;
        public System.Windows.Input.ICommand SaveAnswersCommand {
            get { _saveAnswersCommand = _saveAnswersCommand ?? new MvxCommand(DoSaveAnswers); return _saveAnswersCommand; }
        }

        private async void DoSaveAnswers() {
            var answers = new List<UserAnswer>();
            foreach (var answer in _questions.Select(q =>
                new { q.QuestionId, AnswerId = FindSelectedAnswer(q.PossibleAnswers) })) {

                if (answer.AnswerId > 0) {
                    answers.Add(new UserAnswer() {
                        UserId = _userId,
                        QuestionId = answer.QuestionId,
                        AnswerId = answer.AnswerId
                    });
                }
            }

            IAlertMessage alertMsg = Mvx.Resolve<IAlertMessage>();
            AlertMessageResult msgResult = AlertMessageResult.Yes;

            if (answers.Count == 0) {
                msgResult = await alertMsg.ShowAsync(
                    "You did not answer any questions. Are you sure you want to continue?",
                    "No Questions Answered",
                    AlertMessageButtons.YesNo);
            }
            else if (answers.Count < _questions.Count) {
                msgResult = await alertMsg.ShowAsync(
                    "You did not answer all of the questions. Are you sure you want to continue?",
                    "Not All Questions Answered",
                    AlertMessageButtons.YesNo);
            }

            if (msgResult == AlertMessageResult.Yes) {
                if (answers.Count > 0) {
                    using (var apiClient = new WebApiClient(_apiBaseUrl)) {
                        await apiClient.PostAsync<List<UserAnswer>>(answers, "UserAnswers");
                    }
                }
                ShowViewModel<DoneViewModel>();
            }
        }

        private int FindSelectedAnswer(List<Answer> answers) {
            int result = 0;

            if (answers != null && answers.Count > 0 && answers.Where(a => a.IsSelected == true).Count() == 1)
                result = answers.Where(a => a.IsSelected == true).Single().AnswerId;

            return result;
        }
    }
}