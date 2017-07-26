using ERPS.DataModel.Entities.Notification;
using ERPS.Notification;
using Notification.Functionals;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web.Http;

namespace Notification.Controllers
{
    public class NotificationController : ApiController
    {
		[Route("api/Notification/GetNotifications")]
		[HttpGet]
		public HttpResponseMessage GetNotifications()
		{
			var ms = new MemoryStream(Manager.Instance().GetNotifications());
            return ERPS.Utils.Functionals.Communicator.ResponseMessage(ms);

		}

		[Route("api/Notification/AddEvent")]
		[HttpPost]
		public string AddEvent()
		{
			Task<Stream> task = Request.Content.ReadAsStreamAsync();
			task.Wait();
			Stream EntityStream = task.Result;
			BinaryFormatter bf = new BinaryFormatter();
			Notifications events = (Notifications)bf.Deserialize(EntityStream);
			string BaseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
			HttpWebRequest request = ERPS.Utils.Functionals.Communicator.PostReques<Notifications>(BaseUrl + DestinationNames.AddEvent, "POST", events);

			WebResponse response = request.GetResponse();
			using (Stream stream = response.GetResponseStream())
			{
				var sr = new StreamReader(stream);
				var str = sr.ReadToEnd();
				return str;
			}

		}
	}
}
