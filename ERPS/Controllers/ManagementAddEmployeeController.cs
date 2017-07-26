using ERPS.DataModel.Entities.Management;
using ERPS.ServiceLayer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ERPS.ServiceLayer.Controllers
{
    public class ManagementAddEmployeeController : Controller
    {
        // GET: ManagementAddEmployee
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> AddEmployeeToTeam(Employee employee)
        public ActionResult AddEmployeeToTeam()
        {
            // This won't be in the future
            var employee = new Employee() { Id = 1, Name = "UnnamedUser123", Age = 8 };
            // This musn't be here, but anyways 
            ERPS.ServiceLayer.Functionals.Manager.Instance().ManagementAddEmployeeToTeam(employee);
            return View();
            return View(employee);
        }
        
        [System.Web.Http.HttpPost]
        public ActionResult ParseJSONEmployee(EmployeeManagementModel employee)
        {
            // Parsing JSON to Employee instance
            //var employee = JsonConvert.DeserializeObject(jsonObj);
            //dynamic employee = JObject.Parse(jsonObj);
            
            return View("AddEmployeeToTeam", employee);
        }
    }
}