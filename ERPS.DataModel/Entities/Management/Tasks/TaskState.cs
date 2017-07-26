using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Management
{
    [Serializable]
    public class TaskState : EntityBase
    {
        public string Name { get; set; }
        string Description { get; set; }
    }
}
