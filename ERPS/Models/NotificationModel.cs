using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPS.ServiceLayer.Models
{
  
    public class NotificationModel
    {
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public bool EveryYear { get; set; }
    }
 
    public class BirthdayModel
    {
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }

    }
 
    public class NotificationsModel
    {
        public NotificationsModel()
        {
            ListOfEvents = new List<NotificationModel>();
            ListOfBirthdays = new List<BirthdayModel>();
        }
        public List<NotificationModel> ListOfEvents { get; set; }
        public List<BirthdayModel> ListOfBirthdays { get; set; }
    }
}