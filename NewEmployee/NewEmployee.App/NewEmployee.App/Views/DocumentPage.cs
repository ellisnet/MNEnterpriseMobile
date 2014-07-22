using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using NewEmployee.Data.Models;

namespace NewEmployee.App.Views
{
    public class DocumentPage : ContentPage {

        public DocumentPage(Document document) {
            Title = document.Title;
            var documentView = new WebView {
                Source = new UrlWebViewSource {
                    //Url = document.Url
                    // can't seem to pull up my PDFs from the Url to the WebApi, so doing a Bing search instead
                    Url = "http://www.bing.com/search?q=" + document.Title.Trim().ToLower().Replace(" ", "+")
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            Content = documentView;
        }

    }
}
