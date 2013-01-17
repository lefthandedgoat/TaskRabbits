using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskRabbits.Controllers
{
    public class ApiController : Controller
    {
        
        public ActionResult Index()
        {
            return Json(new
            {
                GetRabbitsUrl = Url.RouteUrl(new
                {
                    controller = "Rabbits",
                    action = "Index"
                })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
