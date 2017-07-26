using ERPS.Management.Functionals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ERPS.ServiceLayer.Controllers
{
    public class AddEmployeeController : Controller
    {
        static string CvName = null;
        // GET: AddEmployee
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Models.EmployeeHRModel employee)
        {
            employee.CvName = CvName;
            CvName = null;
            string result = ERPS.ServiceLayer.Functionals.Manager.Instance().PostEmployee(employee);
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    CvName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Cv/"), CvName);
                    file.SaveAs(path);
                }


            }
            return View("Index");
        }
    }
}