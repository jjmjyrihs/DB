﻿using System;
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
            Service.SQL_Inquire SSI = new Service.SQL_Inquire();
            List<Model.BookData> Data = new List<Model.BookData>();
            Data = SSI.Find(listdata.Book_Search.ToString());
            @ViewBag.result = Data;
            return View();
        }
    }
}