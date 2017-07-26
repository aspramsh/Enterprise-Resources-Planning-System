using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPS.ServiceLayer.Models
{
    public class TasksAndEmployees
    {
        public List<EmployeeFinanceModel> employees = new List<EmployeeFinanceModel>(); 
        public TaskModel task { get; set; }
        public List<ProjectModel> projects = new List<ProjectModel>();
        public List<TaskStateModel> states = new List<TaskStateModel>();
        public List<SeverityModel> severities = new List<SeverityModel>();
    }
}