using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Finance
{
    [Serializable]
    public class EmployeeFinance : Employee
    {
        public DateTime SalaryDate { get; set; }
        public int Salary { get; set; }

    }
}
