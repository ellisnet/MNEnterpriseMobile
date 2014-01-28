using System;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using MvxDemo.Models;

namespace MvxDemo.Touch.ViewParts {
    public class QuestionTableViewSource : UITableViewSource {

        private List<Question> _questions;
        private NSString cellIdentifier = new NSString("QuestionTableCell");

        public QuestionTableViewSource(List<Question> questions) {
            _questions = questions;
        }

        public override int NumberOfSections(UITableView tableView) {
            return _questions.Count;
        }

        public override string TitleForHeader(UITableView tableView, int section) {
            return _questions[section].QuestionOrder.ToString() + " - " + 
                _questions[section].QuestionText;
        }

        public override int RowsInSection(UITableView tableview, int section) {
            return _questions[section].PossibleAnswers.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
            tableView.CellAt(indexPath).Accessory = UITableViewCellAccessory.Checkmark;

            for (int i = 0; i < _questions[indexPath.Section].PossibleAnswers.Count; i++) {
                if (i == indexPath.Row) {
                    _questions[indexPath.Section].PossibleAnswers[i].IsSelected = true;                    
                }
                else {
                    _questions[indexPath.Section].PossibleAnswers[i].IsSelected = false;
                    var currentCell = tableView.CellAt(NSIndexPath.FromRowSection(i, indexPath.Section));
                    if (currentCell != null) 
                        currentCell.Accessory = UITableViewCellAccessory.None;
                }
            }
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            Answer cellAnswer = _questions[indexPath.Section].PossibleAnswers[indexPath.Row];
            cell.TextLabel.Text = cellAnswer.AnswerText;
            if (cellAnswer.IsSelected) {
                cell.Accessory = UITableViewCellAccessory.Checkmark;
            }
            else {
                cell.Accessory = UITableViewCellAccessory.None;
            }
            return cell;
        }
    }
}