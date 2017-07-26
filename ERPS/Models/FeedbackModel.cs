using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPS.ServiceLayer.Models
{
    public class FeedbackModel
    {
        public ICollection<EmployeeManagementModel> Employees { get; set; }
        public EmployeeManagementModel Employee { get; set; } = new EmployeeManagementModel();
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ReviewerId { get; set; }
        public bool WorkedTogether { get; set; }
        public bool WishToWorkTogether { get; set; }
        public string PossitiveSide { get; set; }
        public string NegativeSide { get; set; }
        public string ThingsToImprove { get; set; }
        public string Message { get; set; }
    }
}