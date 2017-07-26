using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Notification
{
	[Serializable]
	public class Notification:EntityBase
	{
		
		public DateTime Date { get; set; }
		public string Type { get; set; }
		public string Address { get; set; }
		public bool EveryYear { get; set; }
	}
	[Serializable]
	public class Birthday : EntityBase
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime Date { get; set; }
		
	}
		[Serializable]
	public class Notifications:EntityBase
	{
		public Notifications()
		{
			ListOfEvents = new List<Notification>();
			ListOfBirthdays = new List<Birthday>();
		}
		public List<Notification> ListOfEvents { get; set; }
		public List<Birthday> ListOfBirthdays { get; set; }
	}
}
