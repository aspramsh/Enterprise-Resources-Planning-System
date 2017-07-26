using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPS.ServiceLayer.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PlannedStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PlannedEnd { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ActualStart { get; set; }
        [DataType(DataType.Date)]
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
        public SeverityModel severity { get; set; }
        public string SeverityType { get; set; }
        public int SeverityId { get; set; }
    }
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class TaskStateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public enum SeverityModel
    {
        Task,
        Bug
    }
    public class Attachment
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
    }
}