using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Management.Tasks
{
    [Serializable]
    public class TasksCollection: EntityBase
    {
        public List<Task> tasksCollection { get; set; } = new List<Task>();
    }
}
