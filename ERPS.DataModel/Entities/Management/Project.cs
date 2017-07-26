using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Management
{
    [Serializable]
    public class Project : EntityBase
    {
        public string Name { get; set; }
        public int TypeId { get; set; }
        public List<Task> CurrentTasks { get; set; }
        public Team WorkingTeam { get; set; }
    }
    [Serializable]
    public enum Type
    {
        UI,
        DB,
        MiddleLayer
    }
}
