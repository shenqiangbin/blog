﻿using Blog.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class XSSController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string data)
        {
            try
            {
                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/xss.txt"), true))
                {
                    _testData.WriteLine(data+ "\r\n\r\n\r\n");  
                }
                return Content("");
            }
            catch (Exception ex)
            {
                LogService.Instance.AddAsync(Models.Level.Error, ex);
            }
            return Content("");
        }
    }
}