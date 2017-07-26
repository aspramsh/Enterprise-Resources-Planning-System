using ERPS.ServiceLayer.Functionals;
using ERPS.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPS.ServiceLayer.Controllers
{
	public class NotificationController : Controller
	{
        //// GET: Notification
        public ActionResult Index()
        {
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.AddEvent = "Notification/AddEvent";
            return View("Notification");
        }
       
        public ActionResult AddEvent(NotificationModel Event)
        {
            string result = Manager.Instance().AddEvent(Event);
            return View();
        }
        public ActionResult GetNotifications()
		{
			NotificationsModel result = new NotificationsModel();
			result = Manager.Instance().GetNotifications();
			return View(result);
		}
       
	
	}
}