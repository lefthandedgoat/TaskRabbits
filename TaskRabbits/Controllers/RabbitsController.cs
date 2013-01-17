using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Highway.Data.Interfaces;
using Highway.Data.QueryObjects;
using TaskRabbits.DataAccess;
using TaskRabbits.Models;

namespace TaskRabbits.Controllers
{
    public class RabbitsController : Controller
    {
        private readonly IRepository _repo;

        public RabbitsController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var rabbits = _repo.Find(new AllRabbits());

            var viewModels = rabbits.Select(x => new RabbitViewModel(x, Url));

            return Json(new
            {
                Rabbits = viewModels,
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
