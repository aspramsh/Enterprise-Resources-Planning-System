using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Management
{
    [Serializable]
    public class Employee : Common.Employee
    {
       // public string Name { get; set; } = "Employee";
        public string Surname { get; set; } = "EmployeeSurname";
        //public int Age { get; set; } = 25;
        //public List<Skill> Skills { get; set; }
        public string Team { get; set; } = "";
        public string Role { get; set; } = "";
        public string Project { get; set; } = "";
        public string Task { get; set; } = "";
        //public Team Team { get; set; } = new Team();
        //public Role Role { get; set; }
        //public Project Project { get; set; }
        //public List<Task> Tasks { get; set; }

        public override string ToString() => $"Id: {this.Id}, Name: {this.Name}";

        //public static explicit operator Employee( employeeModel)
        //{
            
        //}
    }

    //[Serializable]
    //public enum Role
    //{
    //    UIDeveloper,
    //    TeamLead,
    //    DBAdministrator
    //}
}
