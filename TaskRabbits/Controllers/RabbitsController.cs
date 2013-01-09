using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oak;
using TaskRabbits.Repositories;

namespace TaskRabbits.Controllers
{
    public class RabbitsController : Controller
    {
        Rabbits rabbits = new Rabbits();

        [HttpGet]
        public ActionResult Index()
        {
            var results = rabbits.All();

            results.ForEach(s => s.TasksUrl = Url.RouteUrl(new 
            { 
                controller = "Tasks",
                action = "Index",
                rabbitId = s.Id
            }));

            return new DynamicJsonResult(new
            {
                Rabbits = results,
                CreateRabbitUrl = Url.RouteUrl(new { controller = "Rabbits", action = "Create" })
            });
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
