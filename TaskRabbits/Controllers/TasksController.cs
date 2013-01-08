using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TaskRabbits.Controllers
{
    public class TasksController : Controller
    {
        public ActionResult Index(int rabbitId)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Update()
        {
            return null;
        }

        [HttpPost]
        public ActionResult Create()
        {
            return null;
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return null;
        }
    }
}
