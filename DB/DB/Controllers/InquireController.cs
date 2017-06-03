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

        public ActionResult Index(Model.BookData listdata)
        {
            Service.SQL_Inquire SS = new Service.SQL_Inquire();
            List<Model.BookData> Data = new List<Model.BookData>();
            Data = SS.Find(listdata.Book_Search.ToString());
            @ViewBag.result = Data;
            HttpCookie cook = Request.Cookies["cookie"];
            if (Request.Cookies["cookie"] == null) {
                @ViewBag.acc = "aa";
            }
            else
            {
                @ViewBag.acc = cook["account"].ToString() + "  " + cook["pwd"].ToString();
            }            
            return View();
        }
    }
}