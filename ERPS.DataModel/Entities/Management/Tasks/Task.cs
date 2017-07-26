using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Management
{
    [Serializable]
    public class Task : EntityBase
    {
        public DateTime? PlannedStart { get; set; }
        public DateTime? PlannedEnd { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? ActualEnd { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public int Revision { get; set; }
        public byte[] Attachment { get; set; }
        public int ReporterId { get; set; }
        public string ReporterName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int AssigneeId { get; set; }
        public string AssigneeName { get; set; }
        public string Comments { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
        public Tasks.Severity severity { get; set; }
        public string Severity { get; set; }
        public int SeverityId { get; set; }
    }
}
