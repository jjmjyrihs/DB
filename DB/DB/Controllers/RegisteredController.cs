using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DB.Controllers
{
    public class RegisteredController : Controller
    {
        // GET: Registered
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Insert(string one, string two)
        {
            ViewBag.one = one;
            ViewBag.two = two;
            return View();
        }
    }
}