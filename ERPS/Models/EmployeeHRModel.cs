using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPS.ServiceLayer.Models
{
   
    public class EmployeeHRModel
        {
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Passport { get; set; }
            public string SocialId { get; set; }
            public string Description { get; set; }
            public DateTime DateOfHiring { get; set; }
            public bool MaritalStatus { get; set; }
          
    }
    
    public class EmployeesHRModel
    {
        public EmployeesHRModel()
        {
           ListOFEmployee = new List<EmployeeHRModel>();
        }
        public List<EmployeeHRModel> ListOFEmployee { get; set; }
    }
}