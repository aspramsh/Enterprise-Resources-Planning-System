using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPS.DataModel.Entities.HR;
using ERPS.DataModel.Entities.Finance;
using ManagementTeam = ERPS.DataModel.Entities.Management;
using System.Data;
using ERPS.DataModel.Entities.Common;
using ERPS.DataModel.Entities.Notification;
using ERPS.DataModel.Entities.Management.Tasks;

namespace ERPS.DataProviders.Interfaces
{
    public interface IDataProvider
    {
        string ManagementGetData();
        HREntities GetHREmployee();
        EmployeesFinance GetEmployeeFinance();
        string AddEmployeeFinance(EmployeeFinance employee);
        string DeleteEmployeeFinance(EmployeeFinanceForGetAndDelete employee);
		EmployeeFinance GetEmployeeFinanceById(EmployeeFinanceForGetAndDelete employee);
	    Notifications GetNotifications();
        string AddHREmployee(HREntity employee);
        string DeleteHREmployee(int id);
        HREntity GetHREmployeeById(int id);

        #region Task
        string AddTask(ManagementTeam.Task task);
        List<ManagementTeam.Project> GetProjects();
        List<ManagementTeam.TaskState> GetStates();
        List<Severity> GetSeverities();
        string AddAttachmentId(Attachment attachment);
        string DeleteTask(TaskForDelete task);
        List<Employee> GetEmployees();
        #endregion

        #region Management
        string DeleteEmployeeManagement(ManagementTeam.EmployeeForGetAndDelete employee);
        //ManagementTeam.Collection<Employee> ManagementGetListEntities(string storedProcedure);
        ManagementTeam.Collection<T> ManagementGetEmployees<T>()
            where T : ManagementTeam.Employee, new();
        ManagementTeam.Collection<T> ManagementGetListEntities<T>(string storedProcedure)
            where T : Employee, new();
        EntityBase ManagementUpdateEmployee(ManagementTeam.Employee employee);
        string ManagementAddCommonEmployee(ManagementTeam.Employee employee);
        //ERPS.DataModel.Entities.Management.Collection<T> GetCollection<T>(string storedProcedure)
        //where T : EntityBase;
        ERPS.DataModel.Entities.Management.Collection<T> GetCollection<T>(string storedProcedure)
            where T : EntityBase; /*IEnumerable<EntityBase>, new();*/
        EntityBase ManagementLeaveFeedback(ManagementTeam.Feedback feedback);
        #endregion
    }
}
