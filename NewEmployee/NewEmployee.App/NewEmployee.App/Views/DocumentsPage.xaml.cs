using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using NewEmployee.App.ViewModels;
using NewEmployee.Data.Models;

namespace NewEmployee.App.Views {
	public partial class DocumentsPage {
        private DocumentsViewModel _viewModel;

		public DocumentsPage() {
			InitializeComponent();
            _viewModel = new DocumentsViewModel(Navigation);
            this.BindingContext = _viewModel;
            _viewModel.GetDocumentList();
		}

        private void OnDocumentSelected(object sender, ItemTappedEventArgs args) {
            _viewModel.ShowDocumentPage(args.Item as Document);
            uiDocumentsList.SelectedItem = null;
        }
	}
}
