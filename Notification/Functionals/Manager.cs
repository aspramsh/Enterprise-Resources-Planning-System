using ERPS.DataModel.Entities.Notification;
using ERPS.Notification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web;

namespace Notification.Functionals
{
	public class Manager
	{
		private static Manager instance = null;

		public static Manager Instance()
		{
			if (null == instance)
			{
				instance = new Manager();
			}
			return instance;
		}
		public byte[] GetNotifications()
		{
			string BaseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
			string url = BaseUrl + DestinationNames.GetNotifications;

			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
			req.Method = "GET";
			HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
			using (Stream stream = resp.GetResponseStream())
			{
				BinaryFormatter bf = new BinaryFormatter();
				var sr = new StreamReader(stream);
				Notifications notifications = (Notifications)bf.Deserialize(stream);
				if (notifications == null)
					return null;
				using (MemoryStream ms = new MemoryStream())
				{
					bf.Serialize(ms, notifications);
					return ms.ToArray();
				}
			}
		}
	}
}