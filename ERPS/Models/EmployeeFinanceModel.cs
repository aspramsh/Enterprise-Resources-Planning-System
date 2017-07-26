using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPS.ServiceLayer.Models
{
    public class EmployeeFinanceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public DateTime SalaryDate { get; set; }
    }
} 