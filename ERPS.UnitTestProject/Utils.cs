using ERPS.DataModel.Entities.Management;
using ERPS.Utils.Functionals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.UnitTestProject
{
    [TestClass]
    public class Utils
    {
        [TestMethod]
        public void SendRequest()
        {
            string commandUrl = "http://localhost:53654/api/TestWAPIManager/GetTestEmployee";
            Employee emp = new Employee() { Id = 4 }; 

            var emp2 = Communicator.SendRequest<Employee, Employee>(emp, commandUrl, "POST", "application/octet-stream");
            Debug.Assert(emp2.Id > 0);
        }
       
    }
}
