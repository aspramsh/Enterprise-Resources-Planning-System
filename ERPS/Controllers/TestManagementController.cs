using ERPS.DataModel.Entities.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPS.ServiceLayer.Controllers
{
    public class TestManagementController : Controller
    {
        // GET: TestManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestManagement(string str)
        {
            var response = ERPS.ServiceLayer.Functionals.Manager.Instance().GetManagementTest(str);
            ViewData["response"] = response;
            return View();
        }

        public ActionResult TestSendEmployee()
        {
            // Converting json to entity
            var employee = new Employee() { Id = 4,/* Age = 35,*/ Name = "Poghos" /*, Role = Role.TeamLead*/ };
            var response = ERPS.ServiceLayer.Functionals.Manager.Instance().GetInfoAboutEmployee(employee);
            ViewData["response"] = response;
            return View();
        }
    }
}