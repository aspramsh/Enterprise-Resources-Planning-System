using ERPS.ServiceLayer.Functionals;
using ERPS.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPS.ServiceLayer.Controllers
{
    public class GetEmployeeManagmentByIdController : Controller
    {
        // GET: GetEmployeeManagmentById
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetEmployeeById(ERPS.ServiceLayer.Models.EmployeeManagementId employee)
        {
         //   EmployeeManagmentId empl = Manager.Instance().GetEmployeeManagmentById(employee);
            return View();
        }
    }
}