using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class InquireController : Controller
    {
        // GET: Inquire
        public ActionResult Index(string BookName)
        {
            Service.SQL SS = new Service.SQL();
            List<Model.BookData> Data = new List<Model.BookData>();
            Data = SS.Find();
            @ViewBag.result = Data;
            return View();
        }
    }
}