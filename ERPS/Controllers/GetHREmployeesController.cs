using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPS.ServiceLayer.Controllers
{
    public class GetHREmployeesController : Controller
    {
        // GET: GetHREmployees
        public ActionResult Index()
        {
            Models.EmployeesHRModel result = new Models.EmployeesHRModel();
            result = ERPS.ServiceLayer.Functionals.Manager.Instance().GetEmployeesHR();     
            return View(result);
        }
    public ActionResult DeleteEmployee(int id)
        {
             ERPS.ServiceLayer.Functionals.Manager.Instance().DeleteEmployee(id);
             return View();
        }
    }
}