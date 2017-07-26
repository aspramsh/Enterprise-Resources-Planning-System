using ERPS.DataModel.Entities.HR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ERPS.ServiceLayer.Controllers
{
    public class HRController : Controller

    {

        // GET: AddEmployees
        public ActionResult AddEmployee()
        {
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.AddEmployee = "HR/AddEmployeeHelper";
            ViewBag.UploadCv = "HR/UploadCv";
            return View();
        }
        [HttpPost]
        public void AddEmployeeHelper(Models.EmployeeHRModel employee)
        {
            ERPS.ServiceLayer.Functionals.Manager.Instance().PostEmployee(employee);
         
        }
        // GET: GetHREmployees
        public ActionResult GetHREmployees()
        {
        
            Models.EmployeesHRModel result = new Models.EmployeesHRModel();
            result = ERPS.ServiceLayer.Functionals.Manager.Instance().GetEmployeesHR();
            return View(result);
        }
        //Delete view
        public ActionResult DeleteEmployee(int id)
        {
            ERPS.ServiceLayer.Functionals.Manager.Instance().DeleteEmployee(id);
            return View();
        }
        // GET: candidates
        public ActionResult GetCandidates()
        {
            Models.HRCandidates result = new Models.HRCandidates();
            result = ERPS.ServiceLayer.Functionals.Manager.Instance().HRCandidate();
            ViewBag.Something = result;
            return View(result);
        }
        //Edit view
        public ActionResult GetHrEmployeeById(int id)
        { string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.AddEmployee = "HR/AddEmployeeHelper";
            HREntityID temp = new HREntityID();
            temp.Id = id;
            Models.EmployeeHRModel emp = ERPS.ServiceLayer.Functionals.Manager.Instance().GetHREmployeeById(temp);
            return View("GEtHREmployeeById", emp);
        }
        /// <summary>
        /// upload Employee Cv and save it in Cv folder
        /// </summary>
        /// <returns>message about result</returns>
        [HttpPost]
        public ActionResult UploadCv()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    string CvName = Request.Files.AllKeys[0].ToString();
                    Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Cv/"), CvName + ".pdf");
                    file.SaveAs(path);
                }


            }
            return View("AddEmployee");
        }
        public FileResult DownloadCv(string data)
        {
           string Data = data.Trim();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename"+Data+".pdf");
            return new FilePathResult(Server.MapPath("/Cv/"+Data+".pdf"), "application/pdf");
           
        }
    }
}