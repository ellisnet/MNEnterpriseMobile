using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEmployee.Data.Models {
    public class Document {

        private int _documentId;
        private string _title;
        private string _file;
        private string _url;

        public int DocumentId {
            get { return _documentId; }
            set { _documentId = value; }
        }

        public string Title {
            get { return _title; }
            set { _title = value; }
        }

        public string File {
            get { return _file; }
            set { _file = value; }
        }

        public string Url {
            get { return _url; }
            set { _url = value; }
        }

    }
}
