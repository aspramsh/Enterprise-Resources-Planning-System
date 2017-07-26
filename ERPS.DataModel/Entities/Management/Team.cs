using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Management
{
    [Serializable]
    public class Team
    {
        public string Name { get; set; }
        public List<Employee> Members { get; set; }
    }
}
