using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oak;

namespace TaskRabbits.Controllers
{
    public class RabbitsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var rabbits = new[] 
            {
                new { Id = 1, Name = "Test", TasksUrl = "/Tasks?rabbitId=1" },
                new { Id = 2, Name = "Test 2", TasksUrl = "/Tasks?rabbitId=2" }
            };

            return Json(new
            {
                Rabbits = rabbits,
                CreateRabbitUrl = Url.RouteUrl(new { controller = "Rabbits", action = "Create" })
            }, 
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create()
        {
            if (true /*isvalid*/)
            {
                return Json(new
                {
                    Id = 3,
                    Name = "Test 3",
                    TasksUrl = "/Tasks?rabbitId=3"
                },
                JsonRequestBehavior.AllowGet);
            }
            else
            {
                var errorPayload = new
                {
                    Errors = new[] 
                    {
                        new { Key = "Name", Value = "Name is required." },
                        new { Key = "Name", Value = "Name must be unique." }
                    }
                };

                return Json(errorPayload, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
