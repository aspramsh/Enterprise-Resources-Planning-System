using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERPS.DataModel.Entities.Status;
using ERPS.DataModel.Entities.Finance;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CallGenericSendRequest()
        {
            EmployeeFinance elf = new EmployeeFinance();
            elf.Salary = 123434;
            StatusBase status = ERPS.Utils.Functionals.Communicator.SendRequest<StatusBase, EmployeeFinance>(elf, "http://hellomelloyello.com", "POST", "application/octet-stream");

            int i = 0; 
            ++i;
        }
    }
}
