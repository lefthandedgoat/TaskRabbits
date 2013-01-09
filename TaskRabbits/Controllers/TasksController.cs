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
        public ActionResult Index(int rabbitId)
        {
            var tasks = new[] 
            {
                new 
                {
                    Id = 1,
                    RabbitId = 1,
                    Description = "Test",
                    DueDate = "1/1/2013",
                    SaveUrl = "/Tasks/Update/1",
                    DeleteUrl = "/Tasks/Delete/1"
                }
            };

            return Json(new 
            {
                Tasks = tasks, 
                CreateTaskUrl = "/Tasks/Create?rabbitId=1"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update()
        {
            return Json(new
            {
                Id = 1,
                RabbitId = 1,
                Description = "Test",
                DueDate = "1/1/2013",
                SaveUrl = "/Tasks/Update/1",
                DeleteUrl = "/Tasks/Delete/1"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create()
        {
            return Json(new
            {
                Id = 2,
                RabbitId = 1,
                Description = "Test",
                DueDate = "1/1/2013",
                SaveUrl = "/Tasks/Update/2",
                DeleteUrl = "/Tasks/Delete/2"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return new EmptyResult();
        }
    }
}
