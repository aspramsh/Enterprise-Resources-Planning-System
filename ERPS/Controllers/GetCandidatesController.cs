using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPS.ServiceLayer.Controllers
{
    public class GetCandidatesController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            Models.HRCandidates result = new Models.HRCandidates();
            result = ERPS.ServiceLayer.Functionals.Manager.Instance().HRCandidate();
            ViewBag.Something = result;
            return View(result);
        }
    }
}