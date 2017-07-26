using ERPS.DataModel.Entities.Common;
using ERPS.DataModel.Entities.Finance;
using ERPS.DataModel.Entities.HR;
using ManagementTeam = ERPS.DataModel.Entities.Management;
using ERPS.DataProviders.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections;
using ERPS.DataModel.Entities.Management.Tasks;
using ERPS.DataModel.Entities.Notification;
using ERPS.Utils.Functionals;
using Dapper;
using ERPS.DataModel.Entities.Management;

namespace ERPS.DataProviders.Providers
{
	public class SQLProvider : IDataProvider
	{
		public string ManagementGetData()
		{
			return null;
		}

        /// <summary>
        /// Function call Store Procedure GEtHREmployees
        /// </summary>
        /// <returns>All HR employees from HREmployee table</returns>
		#region HRTeam
		public HREntities GetHREmployee()
		{

			using (SqlConnection con = new SqlConnection
				 (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
              
                HREntities employees = new HREntities();
                SqlCommand cmd = new SqlCommand("dbo.GetHREmployees", con);
              
                    try
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure; ;
                        con.Open();
                   
                    }
                    catch (SqlException ex)
                    {
                        Logger.Logger.Addlog(ex.Message + ',' + ex.Procedure + ',' + ex.LineNumber);
                    }
                
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HREntity employee = new HREntity();
                        employee.Id = Int32.Parse(reader[0].ToString());
                        employee.Name = reader[1].ToString();
                        employee.LastName = reader[2].ToString();
                        employee.Phone = reader[3].ToString();
                        employee.Address = reader[4].ToString();
                        employees.ListOfEntities.Add(employee);
                    
                    }
                     return employees;
            }

		}

        /// <summary>
        /// Function receive HREntity Datamodel and insert it into HREmployee table 
        /// </summary>
        /// <param name="Datamodel HRntity employee"></param>
        /// <returns>Returned meesage about result </returns>
		public string AddHREmployee(HREntity employee)
		{

			using (SqlConnection con = new SqlConnection
				(ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{//its called when  added new employee
             
                if (employee.Id == 0)
                {
                
                        try
                        {
                            SqlCommand cmd = new SqlCommand("dbo.AddHREmployee", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Name", employee.Name);
                            cmd.Parameters.AddWithValue("@SurName", employee.LastName);
                            cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                            cmd.Parameters.AddWithValue("@Address", employee.Address);
                            cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth.Date);
                            cmd.Parameters.AddWithValue("@Passport", employee.Passport);
                            cmd.Parameters.AddWithValue("@SocialCard", employee.SocialId);
                            cmd.Parameters.AddWithValue("@Description", employee.Description);
                            cmd.Parameters.AddWithValue("@DateOfHiring", employee.DateOfHiring.Date);
                            con.Open();   
                            cmd.ExecuteNonQuery();
                       
                        }
                        catch (SqlException ex)
                        {
                            Logger.Logger.Addlog(ex.Message+','+ex.Procedure+','+ex.LineNumber);
                        };
                    }
                
                //its call when updated existed employee
                else
                {
                   
                        try
                        {
                            SqlCommand cmd = new SqlCommand("dbo.UpdateHREmployee", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Id", employee.Id);
                            cmd.Parameters.AddWithValue("@Name", employee.Name);
                            cmd.Parameters.AddWithValue("@SurName", employee.LastName);
                            cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                            cmd.Parameters.AddWithValue("@Address", employee.Address);
                            cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                            cmd.Parameters.AddWithValue("@Passport", employee.Passport);
                            cmd.Parameters.AddWithValue("@SocialCard", employee.SocialId);
                            cmd.Parameters.AddWithValue("@Description", employee.Description);
                            cmd.Parameters.AddWithValue("@DateOfHiring", employee.DateOfHiring);
                            con.Open();
                           
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Logger.Logger.Addlog(ex.Message + ',' + ex.Procedure + ',' + ex.LineNumber);
                        };
                    } 
                  
                
			}
			return "Added";
		}

        /// <summary>
        /// Function received int id and deleted employee 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Message about result</returns>
        public string DeleteHREmployee(int id)
		{
			using (SqlConnection con = new SqlConnection
				   (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{
               
                    try
                    {
                        SqlCommand cmd = new SqlCommand("dbo.RemoveHREmployee", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Logger.Logger.Addlog(ex.Message + ',' + ex.Procedure + ',' + ex.LineNumber);
                    }
                }
				return "Delete";
			
		}
       /// <summary>
       /// function call GEtHREmployeeByID Store Procedure
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <returns> marked employee</returns>
		public HREntity GetHREmployeeById(int id)
		{
			HREntity emp = new HREntity();
			using (SqlConnection con = new SqlConnection
				(ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
              
                SqlCommand cmd = new SqlCommand("dbo.GethrEmployeeById", con);
             
                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HRID", id);
                       
                        con.Open();
                    }
                    catch (SqlException ex)
                    {
                        Logger.Logger.Addlog(ex.Message + ',' + ex.Procedure + ',' + ex.LineNumber);
                    }
                
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					emp.Id = Int32.Parse(reader[0].ToString());
					emp.Name = reader[1].ToString();
					emp.LastName = reader[2].ToString();
					emp.Phone = reader[3].ToString();
					emp.Address = reader[4].ToString();
					emp.DateOfBirth = DateTime.Parse(reader[5].ToString());
					emp.Passport = reader[6].ToString();
					emp.SocialId = reader[7].ToString();
					emp.Description = reader[8].ToString();
					emp.DateOfHiring = DateTime.Parse(reader[9].ToString());
				}
			}
			return emp;
		}

      

        #endregion

        #region ManagementTeam
        public Collection<T> ManagementGetEmployees<T>()
            where T : DataModel.Entities.Management.Employee, new()
        {
            var employees = new ManagementTeam.Collection<T>();
            employees.Entities = new List<T>();
            using (SqlConnection connection = new SqlConnection
              (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                var cmd = new SqlCommand("dbo.umsp_GetManagementEmployees", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // select Id, Name, Surname, Team, Role, Project, Task
                    var employee = new T();
                    employee.Id = Int32.Parse(reader[0].ToString());
                    employee.Name = reader[1].ToString();
                    employee.LastName = reader[2].ToString();
                    employee.Team = reader[3].ToString();
                    employee.Role = reader[4].ToString();
                    employee.Project = reader[5].ToString();
                    employee.Task = reader[6].ToString();
                    employees.Entities.Add(employee);
                    //employees.Entities = (T)employees.Entities.AddToIEnumerable(employee);
                    //employees.Entities.Add<Employee>(employee);
                }
            }
            return employees;
        }

        public Collection<Feedback> ManagementGetEmployeeFeedbacks(EntityBase employee)
        {
            var feedbacks = new ManagementTeam.Collection<ManagementTeam.Feedback>();
            feedbacks.Entities = new List<ManagementTeam.Feedback>();
            using (SqlConnection connection = new SqlConnection
              (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                var cmd = new SqlCommand("dbo.umsp_GetEmployeeFeedbacks", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@employeeId", employee.Id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //SELECT Id, EmployeeId, ReviewerId, WorkedTogether,
                    //WishToWorkTogether, PossitiveSide, NegativeSide, ThingsToImprove, Message
                    var feedback = new ManagementTeam.Feedback();
                    // TODO: modify sp, as information about employees maybe needed
                    feedback.Id = Int32.Parse(reader[0].ToString());
                    feedback.EmployeeId = int.Parse(reader[1].ToString());
                    feedback.ReviewerId = int.Parse(reader[2].ToString());
                    feedback.WorkedTogether = Boolean.Parse(reader[3].ToString());
                    feedback.WishToWorkTogether = Boolean.Parse(reader[4].ToString());
                    feedback.PossitiveSide = reader[5].ToString();
                    feedback.NegativeSide = reader[6].ToString();
                    feedback.ThingsToImprove = reader[7].ToString();
                    feedback.Message = reader[8].ToString();

                    feedbacks.Entities.Add(feedback);
                }
            }
            return feedbacks;
        }

        public Collection<Feedback> ManagementGetFeedbacks()
        {
            var feedbacks = new ManagementTeam.Collection<ManagementTeam.Feedback>();
            feedbacks.Entities = new List<ManagementTeam.Feedback>();
            using (SqlConnection connection = new SqlConnection
              (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                var cmd = new SqlCommand("dbo.umsp_GetFeedbacks", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //SELECT Id, EmployeeId, ReviewerId, WorkedTogether,
                    //WishToWorkTogether, PossitiveSide, NegativeSide, ThingsToImprove, Message
                    var feedback = new ManagementTeam.Feedback();
                    // TODO: modify sp, as information about employees maybe needed
                    feedback.Id = Int32.Parse(reader[0].ToString());
                    feedback.EmployeeId = int.Parse(reader[1].ToString());
                    feedback.ReviewerId = int.Parse(reader[2].ToString());
                    feedback.WorkedTogether = Boolean.Parse(reader[3].ToString());
                    feedback.WishToWorkTogether = Boolean.Parse(reader[4].ToString());
                    feedback.PossitiveSide = reader[5].ToString();
                    feedback.NegativeSide = reader[6].ToString();
                    feedback.ThingsToImprove = reader[7].ToString();
                    feedback.Message = reader[8].ToString();

                    feedbacks.Entities.Add(feedback);
                }
            }
            return feedbacks;
        }

        // Generic comes to the Employee and becomes non generic. 
        // This was discussed and found out that it's a config solution
        public ManagementTeam.Collection<T> ManagementGetListEntities<T>(string storedProcedure)
            where T : DataModel.Entities.Common.Employee, new()
        {
            var employees = new ManagementTeam.Collection<T>();
            employees.Entities = new List<T>();
            using (SqlConnection connection = new SqlConnection
              (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                var cmd = new SqlCommand(storedProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var employee = new T();
                    employee.Id = Int32.Parse(reader[0].ToString());
                    employee.Name = reader[1].ToString();
                    employee.LastName = reader[2].ToString();
                    employees.Entities.Add(employee);
                    //employees.Entities = (T)employees.Entities.AddToIEnumerable(employee);
                    //employees.Entities.Add<Employee>(employee);
                }
            }
            return employees;
        }

        public EntityBase ManagementLeaveFeedback(ManagementTeam.Feedback feedback)
        {
            var resultEntityBase = new EntityBase();
            try
            {
                using (var connection = new SqlConnection
                (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
                {
                    var command = new SqlCommand("umsp_LeaveFeedback", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@employeeId", feedback.EmployeeId);
                    command.Parameters.AddWithValue("@reviewerId", feedback.ReviewerId);
                    command.Parameters.AddWithValue("@workedTogether", feedback.WorkedTogether);
                    command.Parameters.AddWithValue("@wishToWorkTogether", feedback.WishToWorkTogether);
                    command.Parameters.AddWithValue("@possitiveSide", feedback.PossitiveSide);
                    command.Parameters.AddWithValue("@negativeSide", feedback.NegativeSide);
                    command.Parameters.AddWithValue("@thingsToImprove", feedback.ThingsToImprove);
                    command.Parameters.AddWithValue("@message", feedback.Message);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                resultEntityBase.StatusInfo = "Feedback was successfully submited";
            }
            catch(SqlException ex)
            {
                resultEntityBase.StatusInfo = ex.ToString() + "ERROR! Couldn't pass data to DB";
            }
            return resultEntityBase;
        }

        public EntityBase ManagementUpdateEmployee(ManagementTeam.Employee employee)
        {
            //System.Diagnostics.Debugger.Launch();
            var resultEntityBase = new EntityBase();
            try
            {
                using (var connection = new SqlConnection
                (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
                {
                    var command = new SqlCommand("dbo.umsp_UpdateEmployee", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@employeeId", employee.Id);
                    //command.Parameters.AddWithValue("@name", employee.Name);
                    //command.Parameters.AddWithValue("@surname", employee.Surname);
                    command.Parameters.AddWithValue("@team", employee.Team);
                    command.Parameters.AddWithValue("@role", employee.Role);
                    command.Parameters.AddWithValue("@project", employee.Project);
                    command.Parameters.AddWithValue("@task", employee.Task);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                resultEntityBase.StatusInfo = "Employee's data was successfully updated";
            }
            catch (SqlException ex)
            {
                resultEntityBase.StatusInfo = ex.ToString() + "ERROR! Couldn't pass data to DB";
            }
            return resultEntityBase;
        }

        public string ManagementAddCommonEmployee(ManagementTeam.Employee employee)
		{
			//System.Diagnostics.Debugger.Launch();
			using (var connection = new SqlConnection
				(ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{
				var command = new SqlCommand("dbo.umsp_AddEmployee", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@employeeName", employee.Name);
				command.Parameters.AddWithValue("@employeeSurname", employee.Surname);

				connection.Open();
				command.ExecuteNonQuery();
			}
			return "Employee was successfully added";
		}
		public string DeleteEmployeeManagement(ManagementTeam.EmployeeForGetAndDelete employee)
		{
			using (SqlConnection con = new SqlConnection
				(ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{
				SqlCommand cmd = new SqlCommand("dbo.umsp_DeleteEmployee", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id", employee.Id);
				con.Open();
				cmd.ExecuteNonQuery();
			}
			return "Deleted.";
		}


        
        #endregion

        #region CommonFunctionality
        public ERPS.DataModel.Entities.Management.Collection<T> GetCollection<T>(string storedProcedure)
		 where T : EntityBase
		{
			var employees = new List<T>();
			using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{
				//      employees = connection.Query<T>(storedProcedure, commandType: CommandType.StoredProcedure).ToList();
			}
			//System.Diagnostics.Debugger.Launch();
			var resultEmployees = new ERPS.DataModel.Entities.Management.Collection<T>();
			resultEmployees.Entities = employees;
			return resultEmployees;
		}
		#endregion

		#region FinanceTeam

		public EmployeesFinance GetEmployeeFinance()
		{
			EmployeesFinance employees = new EmployeesFinance();
			using (SqlConnection con = new SqlConnection
			  (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{
				SqlCommand cmd = new SqlCommand("dbo.GetSalaries", con);
				cmd.CommandType = CommandType.StoredProcedure;
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					EmployeeFinance employee = new EmployeeFinance();
					employee.Id = Int32.Parse(reader[0].ToString());
					employee.Name = reader[1].ToString();
					employee.LastName = reader[2].ToString();
					int e = 0;
					Int32.TryParse(reader[3].ToString(), out e);
					employee.Salary = e;
                    DateTime d;
                    DateTime.TryParse(reader[4].ToString(), out d);
                    employee.SalaryDate = d;
                    employees.Employees.Add(employee);
				}
			}
			return employees;
		}
		public string AddEmployeeFinance(EmployeeFinance employee)
		{
			using (SqlConnection con = new SqlConnection
				(ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{
				SqlCommand cmd = new SqlCommand("dbo.UpdateSalary", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id", employee.Id);
				cmd.Parameters.AddWithValue("@Name", employee.Name);
				cmd.Parameters.AddWithValue("@LastName", employee.Name);
				cmd.Parameters.AddWithValue("@Salary", employee.Salary);
				con.Open();
				cmd.ExecuteNonQuery();
			}
			return "Added.";
		}
		public string DeleteEmployeeFinance(EmployeeFinanceForGetAndDelete employee)
		{
			using (SqlConnection con = new SqlConnection
				(ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{
				SqlCommand cmd = new SqlCommand("dbo.DeleteSalary", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id", employee.Id);
				con.Open();
				cmd.ExecuteNonQuery();
			}
			return "Deleted.";
		}
		public int FinanceGetData()
		{
			return 1;
		}
		public EmployeeFinance GetEmployeeFinanceById(EmployeeFinanceForGetAndDelete employee)
		{
			EmployeeFinance emp = new EmployeeFinance();
			using (SqlConnection con = new SqlConnection
				(ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{
				SqlCommand cmd = new SqlCommand("dbo.GetEmployeeFinanceById", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id", employee.Id);
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					emp.Id = Int32.Parse(reader[0].ToString());
					emp.Name = reader[1].ToString();
                    emp.LastName = reader[2].ToString();
					emp.Salary = Int32.Parse(reader[3].ToString());
				}
			}
			return emp;
		}

        #endregion
        #region Task
        public string AddTask(ManagementTeam.Task task)
        {
            using (SqlConnection con = new SqlConnection
                (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.AddTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", task.Id);
                cmd.Parameters.AddWithValue("@PlannedStart", ((object)task.PlannedStart) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PlannedEnd", ((object)task.PlannedEnd) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ActualStart", ((object)task.ActualStart) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ActualEnd", ((object)task.ActualEnd) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", ((object)task.Description) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Source", ((object)task.Source) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Revision", ((object)task.Revision) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ReporterId", task.ReporterId);
                cmd.Parameters.AddWithValue("@AssigneeId", task.AssigneeId);
                cmd.Parameters.AddWithValue("@ProjectId", task.ProjectId);
                cmd.Parameters.AddWithValue("@Comments", ((Object)task.Comments) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StateId", task.StateId);
                if (task.severity == Severity.Task)
                    cmd.Parameters.AddWithValue("@SeverityId", (int)Severity.Task + 1);
                else
                    cmd.Parameters.AddWithValue("@SeverityId", (int)Severity.Bug + 1);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return "Added.";
        }
        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();
            using (SqlConnection con = new SqlConnection
              (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.GetProjects", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Project project = new Project();
                    project.Id = Int32.Parse(reader[0].ToString());
                    project.Name = reader[1].ToString();
                    int e = 0;
                    Int32.TryParse(reader[3].ToString(), out e);
                    project.TypeId = e;
                    projects.Add(project);
                }
            }
            return projects;
        }
        public List<TaskState> GetStates()
        {
            List<TaskState> states = new List<TaskState>();
            using (SqlConnection con = new SqlConnection
              (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.GetStates", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TaskState state = new TaskState();
                    state.Id = Int32.Parse(reader[0].ToString());
                    state.Name = reader[1].ToString();
                    states.Add(state);
                }
                return states;
            }
        }
        public List<Severity> GetSeverities()
        {
            List<Severity> s = new List<Severity>();
            using (SqlConnection con = new SqlConnection
              (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.GetSeverities", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Severity e = 0;
                    e = (Severity)Enum.Parse(typeof(Severity), reader["Severity"].ToString());
                    s.Add(e);
                }
                return s;
            }
        }
        public List<DataModel.Entities.Common.Employee> GetEmployees()
        {
            List<DataModel.Entities.Common.Employee> employees = new List<DataModel.Entities.Common.Employee>();
            using (SqlConnection con = new SqlConnection
              (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.GetEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DataModel.Entities.Common.Employee employee = new DataModel.Entities.Common.Employee();
                    employee.Id = Int32.Parse(reader[0].ToString());
                    employee.Name = reader[1].ToString();
                    employee.LastName = reader[1].ToString();
                    employees.Add(employee);
                }
                return employees;
            }
        }
        public string AddAttachmentId(Attachment attachment)
        {
            using (SqlConnection con = new SqlConnection
                (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.AddAttachment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskId", Int32.Parse(attachment.FileName));
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return "Added.";
        }

        public TasksCollection GetAllTasks()
        {
            TasksCollection tasks = new TasksCollection();
            using (SqlConnection con = new SqlConnection
              (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.GetTasks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    DataModel.Entities.Management.Task task = new DataModel.Entities.Management.Task();
                    DateTime dt = DateTime.MinValue;
                    task.Id = Int32.Parse(reader[0].ToString());
                    task.PlannedStart = DateTime.TryParse(reader[1].ToString(), out dt) ? dt : DateTime.MinValue;
                    task.PlannedEnd = DateTime.TryParse(reader[2].ToString(), out dt) ? dt : DateTime.MinValue;
                    task.ActualStart = DateTime.TryParse(reader[3].ToString(), out dt) ? dt : DateTime.MinValue;
                    task.ActualEnd = DateTime.TryParse(reader[4].ToString(), out dt) ? dt : DateTime.MinValue;

                    task.Description = reader[5].ToString();
                    task.Source = reader[6].ToString();
                    task.Revision = Int32.Parse(reader[7].ToString());
                    task.Comments = reader[8].ToString();
                    task.ReporterName = reader[9].ToString();
                    task.AssigneeName = reader[10].ToString();
                    task.ProjectName = reader[11].ToString();
                    task.State = reader[12].ToString();
                    task.Severity = reader[13].ToString();
                    task.ReporterId = Int32.Parse(reader[14].ToString());
                    task.AssigneeId = Int32.Parse(reader[15].ToString());
                    task.ProjectId = Int32.Parse(reader[16].ToString());
                    task.StateId = Int32.Parse(reader[17].ToString());
                    task.SeverityId = Int32.Parse(reader[18].ToString());
                    tasks.tasksCollection.Add(task);
                }
            }
            return tasks;
        }
 
        public string DeleteTask(TaskForDelete task)
        {
            using(SqlConnection con = new SqlConnection
                (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.DeleteTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", task.Id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return "Deleted.";
        }

        #endregion
        #region Notifications
        public Notifications GetNotifications()
		{
			using (SqlConnection con = new SqlConnection
				 (ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{
				Notifications notifications = new Notifications();
				SqlCommand cmd1 = new SqlCommand("dbo.GetNotifications", con);
				SqlCommand cmd2 = new SqlCommand("dbo.GetBirthdays", con);
				cmd1.CommandType = System.Data.CommandType.StoredProcedure; ;
				con.Open();
				SqlDataReader reader1 = cmd1.ExecuteReader();
				while (reader1.Read())
				{
					Notification notification = new Notification();
					notification.Id = Int32.Parse(reader1[0].ToString());
					notification.Type = reader1[1].ToString();
					notification.Address = reader1[3].ToString();
					notification.Date = DateTime.Parse(reader1[2].ToString());
					notification.EveryYear = bool.Parse(reader1[4].ToString());
					notifications.ListOfEvents.Add(notification);

				}
				reader1.Close();
				SqlDataReader reader2 = cmd2.ExecuteReader();
				while (reader2.Read())
				{
					Birthday birthday = new Birthday();
					birthday.Id = Int32.Parse(reader2[0].ToString());
					birthday.FirstName = reader2[1].ToString();
					birthday.LastName = reader2[2].ToString();
					birthday.Date = DateTime.Parse(reader2[3].ToString());
					notifications.ListOfBirthdays.Add(birthday);

				}
				return notifications;
			}

		}

		public string AddNotification(Notification ev)
		{
			using (SqlConnection con = new SqlConnection
				(ConfigurationManager.ConnectionStrings["SQLProviderConnectionString"].ConnectionString))
			{
				if (ev.Id == 0)
				{
					SqlCommand cmd = new SqlCommand("dbo.AddEvent", con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@Type", ev.Type);
					cmd.Parameters.AddWithValue("@Address", ev.Address);
					cmd.Parameters.AddWithValue("@Date", ev.Date);
					cmd.Parameters.AddWithValue("@EveryYear", ev.EveryYear);
					con.Open();
					cmd.ExecuteNonQuery();
				}
				else
				{
					SqlCommand cmd = new SqlCommand("dbo.UpdateEvent", con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@Id", ev.Id);
					cmd.Parameters.AddWithValue("@Type", ev.Type);
					cmd.Parameters.AddWithValue("@Address", ev.Address);
					cmd.Parameters.AddWithValue("@Date", ev.Date);
					con.Open();
					cmd.ExecuteNonQuery();
				}
			}
			return "Added";
	
		}

        

        #endregion

    }
}
