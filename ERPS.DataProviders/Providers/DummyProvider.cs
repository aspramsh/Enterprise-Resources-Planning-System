using ERPS.DataProviders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ERPS.DataModel.Entities.HR;
using ERPS.DataModel.Entities.Finance;
using ERPS.DataModel.Entities.Management;
using System.Data;
using ERPS.DataModel.Entities.Common;
using ManagementTeam = ERPS.DataModel.Entities.Management;
using ERPS.DataModel.Entities.Notification;
using ERPS.DataModel.Entities.Management.Tasks;

namespace ERPS.DataProviders.Providers
{
    public class DummyProvider : IDataProvider
    {
        #region FinanceTeam
        public int FinanceGetData()
        {
            return 2000;
        }
        public EmployeesFinance GetEmployeeFinance()
        {
            EmployeesFinance employees = new EmployeesFinance();
            EmployeeFinance employee1 = new EmployeeFinance();
            employee1.Id = 1;
            employee1.Name = "Jane";
            employee1.Salary = 2000;
            employees.Employees.Add(employee1);

            EmployeeFinance employee2 = new EmployeeFinance();
            employee2.Id = 2;
            employee2.Name = "Jack";
            employee2.Salary = 1000;
            employees.Employees.Add(employee2);
            return employees;
        }
        public string AddEmployeeFinance(EmployeeFinance employee)
        {
            return null;
        }
        public string DeleteEmployeeFinance(EmployeeFinance employee)
        {
            return null;
        }
		public EmployeeFinance GetEmployeeFinanceById(EmployeeFinanceForGetAndDelete employee)
		{
			EmployeeFinance Employee = new EmployeeFinance();
			Employee.Id = employee.Id;
			Employee.Name = "Jane";
			Employee.Salary = 2000;
			return Employee;
		}
        #endregion

        #region HRTeam
        public HREntities GetHREmployee()
        {
            HREntity employee = new HREntity();
            employee.Name = "Garnik";
            employee.LastName = "Gexamich";
            employee.DateOfBirth = new DateTime(2016, 4, 4);
            employee.Passport = "1234445";
            employee.DateOfHiring = new DateTime(2017, 4, 4);

            HREntity employee1 = new HREntity();
            employee1.Name = "Romantik";

            HREntities employees = new HREntities();
            employees.ListOfEntities.Add(employee);
            employees.ListOfEntities.Add(employee1);
            return employees;
        }
       public  HREntity GetHREmployeeById(int id) { return new HREntity(); }
       public  string AddHREmployee(HREntity employee) { return "Added"; }
       public   string DeleteHREmployee(int id) { return "Delete"; }

        #endregion

        #region ManagementTeam
        public string ManagementGetData()
        {
            return "Data from the Dummy provider!";
        }

        public string ManagementAddCommonEmployee(DataModel.Entities.Management.Employee employee)
        {
            return null;
        }
		public string DeleteEmployeeManagement(ManagementTeam.EmployeeForGetAndDelete employee)
		{
			return null;
		}

		public ManagementTeam.Collection<T> GetCollection<T>(string storedProcedure) /*where T : EntityBase*/
            where T: EntityBase
        {
            throw new NotImplementedException();
        }
		

		public string DeleteEmployeeFinance(EmployeeFinanceForGetAndDelete employee)
        {
            throw new NotImplementedException();
        }

        ManagementTeam.Collection<T> ManagementGetListEntities<T>(string storedProcedure)
          where T :EntityBase, new()
        { 
            throw new NotImplementedException();
        }

        Collection<T> IDataProvider.ManagementGetListEntities<T>(string storedProcedure)
        {
            throw new NotImplementedException();
        }
        #endregion
        public Notifications GetNotifications()
		{
			return null;
		}

        public string AddTask(ManagementTeam.Task task)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetProjects()
        {
            throw new NotImplementedException();
        }

        public List<TaskState> GetStates()
        {
            throw new NotImplementedException();
        }

        public List<Severity> GetSeverities()
        {
            throw new NotImplementedException();
        }

        public Collection<T> ManagementGetEntities<T>(string storedProcedure) where T : ManagementTeam.Employee, new()
        {
            throw new NotImplementedException();
        }

        public EntityBase ManagementUpdateEmployee(ManagementTeam.Employee employee)
        {
            throw new NotImplementedException();
        }

        public string AddAttachmentId(Attachment attachment)
        {
            throw new NotImplementedException();
        }

        public string DeleteTask(TaskForDelete task)
        {
            throw new NotImplementedException();
        }

        public EntityBase ManagementLeaveFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public Collection<T> ManagementGetEmployees<T>() where T : ManagementTeam.Employee, new()
        {
            throw new NotImplementedException();
        }

        public List<DataModel.Entities.Common.Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
