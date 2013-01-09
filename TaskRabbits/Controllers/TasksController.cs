﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskRabbits.Repositories;
using Oak;
using System.Web.Routing;

namespace TaskRabbits.Controllers
{
    public class TasksController : Controller
    {
        dynamic tasks = new Tasks();

        public ActionResult Index(int rabbitId)
        {
            IEnumerable<dynamic> results = tasks.ForRabbit(rabbitId);

            results.ForEach(ApplyLinksAndFormatting);

            return new DynamicJsonResult(new 
            { 
                Tasks = results, 
                CreateTaskUrl = Link("Tasks", "Create", new { rabbitId })
            });
        }

        [HttpPost]
        public ActionResult Update()
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Create()
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return new EmptyResult();
        }

        void ApplyLinksAndFormatting(dynamic task)
        {
            task.SaveUrl = Link("Tasks", "Update", new { task.Id });
            task.DeleteUrl = Link("Tasks", "Delete", new { task.Id });

            if (!(task.DueDate is string)) task.DueDate = task.DueDate.ToShortDateString();
        }

        public string Link(string controller, string action, object args)
        {
            var routes = new RouteValueDictionary(args);

            routes.Add("controller", controller);
            routes.Add("action", action);

            return Url.RouteUrl(routes);
        }
    }
}
