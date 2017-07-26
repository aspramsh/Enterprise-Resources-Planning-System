using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Common
{
    [Serializable]
    public class Employee : EntityBase
    {
        public string LastName { get; set; }
        public string Name { get; set; }
    }
}
