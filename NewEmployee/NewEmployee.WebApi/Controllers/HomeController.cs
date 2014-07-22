using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewEmployee.WebApi.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Title = "Home Page";

            return View();
        }

        public FileStreamResult Document(int id) {
            string docFile = "";
            using (var repository = new Repository()) {
                docFile = repository.GetDocument(id).Single().File;
            }
            using (var fs = new FileStream(Server.MapPath("../../Content/Documents/" + docFile), FileMode.Open, FileAccess.Read)) {
                using (var br = new BinaryReader(fs)) {
                    byte[] pdfBytes = br.ReadBytes(Convert.ToInt32(fs.Length));
                    var ms = new MemoryStream(pdfBytes);
                    ms.Seek(0, SeekOrigin.Begin);
                    return File(ms, "application/pdf", docFile);
                }
            }
        }
    }
}
