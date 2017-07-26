using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPS.ServiceLayer.Models
{
    public class EmployeeManagementModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        //public int Age { get; set; }
        public string Team { get; set; }
        public string Role { get; set; }
        public string Project { get; set; }
        public string Task { get; set; }
    }
}