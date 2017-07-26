using ERPS.ServiceLayer.Functionals;
using ERPS.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPS.ServiceLayer.Controllers
{
    public class DeleteEmployeeManagementController : Controller
    {
        // GET: DeleteEmployeeManagement
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult DeleteEmployee(EmployeeManagementId employee)
		{
			Manager.Instance().DeleteEmployeeManagement(employee);
			return View(employee);
		}
	}
}