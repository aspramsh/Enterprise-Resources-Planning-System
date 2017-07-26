using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Management
{
    [Serializable]
    public class Feedback : EntityBase
    {
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
