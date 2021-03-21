using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contenedores.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutMe()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AboutProject()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}