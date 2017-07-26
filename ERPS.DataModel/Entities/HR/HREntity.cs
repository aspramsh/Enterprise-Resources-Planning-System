using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERPS.DataModel.Entities.Common;

namespace ERPS.DataModel.Entities.HR
{[Serializable]
    public class HREntity: Employee
    {
       
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Passport { get; set; }
        public string SocialId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfHiring { get; set; }
        public bool MaritalStatus { get; set; }

    }
    [Serializable]
    public class HREntities:Employee
    {
        public HREntities()
        {
            ListOfEntities=new List<HREntity>();
        }
        public List<HREntity> ListOfEntities { get; set; }
    }
}
