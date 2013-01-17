using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskRabbits.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Backbone()
        {
            return View();
        }

        public ActionResult Knockout()
        {
            return View();
        }

        public ActionResult Angular()
        {
            return View();
        }
    }
}
