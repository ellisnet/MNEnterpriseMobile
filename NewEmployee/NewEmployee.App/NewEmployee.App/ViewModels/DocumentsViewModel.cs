using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using NewEmployee.Data;
using NewEmployee.Data.Models;
using NewEmployee.App.Views;

namespace NewEmployee.App.ViewModels {
    public class DocumentsViewModel : INotifyPropertyChanged {

        private INavigation _navigation;

        public DocumentsViewModel(INavigation navigation) {
            _navigation = navigation;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _pageTitle = "Documents";
        public string PageTitle {
            get { return _pageTitle; }
            set {
                value = (value ?? "").Trim();
                if (value != _pageTitle) {
                    _pageTitle = value;
                    OnPropertyChanged("PageTitle");
                }                 
            }
        }

        private ObservableCollection<Document> _documents = new ObservableCollection<Document>();
        public ObservableCollection<Document> Documents {
            get { return _documents; }
            set { _documents = (value == null) ? new ObservableCollection<Document>() : value; }
        }

        public async Task<bool> GetDocumentList() {
            List<Document> docs;
            using (var client = new WebApiClient(App.ApiBaseUrl)) {
                docs = await client.GetAsync<List<Document>>("Document", "0");
            }
            //Should be able to just add each doc to this.Documents, but that seems to not
            //  work consistently on iOS
            var _tempDocs = new ObservableCollection<Document>();
            foreach (Document doc in docs) {
                _tempDocs.Add(doc);
                //this.Documents.Add(doc);
            }
            _documents = _tempDocs;
            OnPropertyChanged("Documents");
   
            return true;
        }

        public void ShowDocumentPage(Document document) {
            if (document != null) {
                _navigation.PushAsync(new DocumentPage(document));
            }
        }

    }
}
