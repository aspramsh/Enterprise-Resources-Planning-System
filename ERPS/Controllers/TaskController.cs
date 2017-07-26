using ERPS.DataModel.Entities.Common;
using ERPS.ServiceLayer.Functionals;
using ERPS.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;

namespace ERPS.ServiceLayer.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddTask()
        {
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.AddTask = "Task/AddTask";
            TasksAndEmployees tasks = new TasksAndEmployees();
            tasks.task = new TaskModel();
            var employees = Manager.Instance().GetEmployees();
            tasks.employees = new List<EmployeeFinanceModel>();
            foreach (var item in employees)
            {
                EmployeeFinanceModel emp = new EmployeeFinanceModel();
                emp.Id = item.Id;
                emp.Name = item.Name;
                emp.LastName = item.LastName;
                tasks.employees.Add(emp);
            }
            tasks.projects = Manager.Instance().GetProjects();
            tasks.states = Manager.Instance().GetStates();
            tasks.severities = Manager.Instance().GetSeverities();
            return View(tasks);
        }
        [HttpPost]
        public ActionResult AddTask(TaskModel task)
        {
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.AddTask = "Task/AddTask";
            TasksAndEmployees tasks = new TasksAndEmployees();
            Manager.Instance().AddTask(task);
            List<TaskModel> Tasks = Manager.Instance().GetTasks();
            return View("GetTasks", Tasks);
        }
        [HttpPost]
        public ActionResult UploadAttachments()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    Attachment attach = new Attachment();
                    attach.FileName = Request.Files.AllKeys[0].ToString();
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    attach.File = target.ToArray();
                    Manager.Instance().UploadAttachment(attach);
                }
            }
            List<TaskModel> tasks = Manager.Instance().GetTasks();
            return Json(new { success = true });

        }
        public ActionResult GetTasks()
        {
            List<TaskModel> tasks = Manager.Instance().GetTasks();

            return View(tasks);
        }
        public ActionResult GetTaskById(int Id)
        {
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.AddTask = "Task/AddTask";
            ViewBag.UploadAttachments = "Task/UploadAttachments";
            TasksAndEmployees tasksAndEmployees = new TasksAndEmployees();
            List<TaskModel> tasks = Manager.Instance().GetTasks();
            TaskModel task = new TaskModel();
            var employees = Manager.Instance().GetEmployees();
            tasksAndEmployees.employees = new List<EmployeeFinanceModel>();
            foreach (var item in employees)
            {
                EmployeeFinanceModel emp = new EmployeeFinanceModel();
                emp.Id = item.Id;
                emp.Name = item.Name;
                emp.LastName = item.LastName;
                tasksAndEmployees.employees.Add(emp);
            }
            tasksAndEmployees.projects = Manager.Instance().GetProjects();
            tasksAndEmployees.states = Manager.Instance().GetStates();
            tasksAndEmployees.severities = Manager.Instance().GetSeverities();
            foreach (TaskModel item in tasks)
            {
                if (item.Id == Id)
                {
                    task = item;
                }
            }
            tasksAndEmployees.task = task;
            return View(tasksAndEmployees);
        }
        public ActionResult DeleteTask(TaskId task)
        {
            Manager.Instance().DeleteTask(task);
            List<TaskModel> tasks = Manager.Instance().GetTasks();
            return View("GetTasks", tasks);
        }
        public FileResult DownloadAttachments(int Id)
        {
            byte[] image = Manager.Instance().DownloadAttachments(Id);
            return File(image, "image/JPEG", Id.ToString() + ".Jpeg");
        }
    }
}