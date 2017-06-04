using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class ShoppingDataController : Controller
    {
        // GET: Shopping
        public ActionResult Index(string isbn,string Search)
        {
            return RedirectToAction("index", "Inquire", new { Book_Search = Search });
        }
    }
}