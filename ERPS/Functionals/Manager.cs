using ManagementTeam = ERPS.DataModel.Entities.Management;
using ERPS.DataModel.Entities.Finance;
using ERPS.DataModel.Entities.HR;
using ERPS.ServiceLayer.Models;
using ERPS.Utils.Functionals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Configuration;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ERPS.DataModel.Entities.Common;
using ERPS.DataModel.Entities.Management;
using ERPS.DataModel.Entities.Notification;
using ERPS.DataModel.Entities.Management.Tasks;
using System.Data.SqlTypes;

namespace ERPS.ServiceLayer.Functionals
{

    public class Manager
    {

        #region Singleton

        private static Manager _instance;

        public static Manager Instance()
        {
            if (_instance == null)
            {
                _instance = new Manager();
            }
            return _instance;
        }

        private Manager()
        {
        }

        #endregion

        #region ManagementTeam
        // ATTENTION:
        // Management = ERPS.DataModel.Entities.Management;

        internal string GetManagementTest(string s)
        {
            Console.WriteLine($"Got following string {s}");
            // HttpWebRequest request = new HttpWebRequest(); 
            string commandUrl = "http://localhost:53654/api/TestWAPIManager/GetTestWAPI?id=8";

            // Initialising a request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(commandUrl);
            request.Method = "GET";

            //GetResponse returns webResponse
            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                //var memStream = new MemoryStream();
                var sr = new StreamReader(stream);
                // Reads all the stream to the end
                string str = sr.ReadToEnd();

                Console.WriteLine(str);
                return str;
            }
        }

        /// <summary>
        /// Returns T type entities
        /// </summary>
        /// <typeparam name="T">Should be EntityBase type</typeparam>
        /// <param name="requestUrl">Url to send request to</param>
        /// <returns></returns>
        internal ICollection<T> ManagementGetEntities<T>(string requestUrl)
            where T : EntityBase
        {
            //"http://localhost:53654/api/UpdateEmployee/UpdateEmployee"
            var response = ERPS.Utils.Functionals.ManagementStreamHandler.SendRequest(requestUrl, "GET");
            var collection = ManagementStreamHandler.RecieveObject<ERPS.DataModel.Entities.Management.Collection<T>>(response);
            return collection.Entities;
        } 

        /// <summary>
        /// Tries to update the provided entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="requestUrl"></param>
        /// <returns>Returns an entity with StatusInfo</returns>
        internal EntityBase ManagementUpdateEntity<T>(T entity, string requestUrl)
            where T : EntityBase
        {
            WebResponse response = Utils.Functionals.ManagementStreamHandler.SendObject(entity, requestUrl, "POST");
            return Utils.Functionals.ManagementStreamHandler.RecieveObject<EntityBase>(response);
        }

        /// <summary>
        /// Returns employee with filled in data
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal ManagementTeam.Employee GetInfoAboutEmployee(ManagementTeam.Employee employee)
        {
            // Server Url
            string commandUrl = "http://localhost:53654/api/TestWAPIManager/GetTestEmployee";
            // Initializing request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(commandUrl);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/octet-stream";
            // Binary serialization
            BinaryFormatter bf = new BinaryFormatter();

            using (Stream requestStream = request.GetRequestStream())
            {
                bf.Serialize(requestStream, employee);
                //request.ContentLength = requestStream.Length;
                //requestStream.Write(requestStream, 0, request.ContentLength);
            }

            // Sending request to the server
            WebResponse response = request.GetResponse();
            // Getting response from the server
            using (Stream responseStream = response.GetResponseStream())
            {
                // Desirializing response-stream to expected object type
                ManagementTeam.Employee empl = (ManagementTeam.Employee)bf.Deserialize(responseStream);
                return empl;
            }
        }

        /// <summary>
        /// Send a POST request to requestUrl
        /// </summary>
        /// <param name="feedback">Deedback to send</param>
        /// <param name="requestUrl">Url where to send</param>
        /// <returns></returns>
        internal EntityBase ManagementLeaveFeedback(Feedback feedback, string requestUrl)
        {
            WebResponse response = Utils.Functionals.ManagementStreamHandler.SendObject(feedback, requestUrl, "POST");
            return Utils.Functionals.ManagementStreamHandler.RecieveObject<EntityBase>(response);
        }

        /// <summary>
        /// Sends request to the server to get all the feedbacks
        /// </summary>
        /// <returns></returns>
        internal ICollection<FeedbackModel> GetFeedbacks()
        {
            //var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["ManagementGetFeedbacks"].ConnectionString;
            var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString
                + DestinationNames.ManagementGetFeedbacks;
            var feedbacks = ManagementGetEntities<ManagementTeam.Feedback>(serverUrl);
            //return Manager.Instance().CastToFeedbackModels(feedbacks);
            return Manager.Instance().CastToModelsCollection<ManagementTeam.Feedback, FeedbackModel>(feedbacks, Manager.Instance().CastToFeedbackModel);
        }

        /// <summary>
        /// Gets all feedbacks relevant to the provided employee
        /// </summary>
        /// <param name="employee">Only Id is needed</param>
        /// <returns></returns>
        internal IEnumerable<FeedbackModel> GetEmployeeFeedbacks(EntityBase employee)
        {
            var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString
               + DestinationNames.ManagementGetEmployeeFeedbacks;
            var feedbacks =
                Manager.Instance().GetEntitiesCollection<EntityBase, ManagementTeam.Collection<ManagementTeam.Feedback>>
                (employee, serverUrl);
            IEnumerable<FeedbackModel> feedbackModels =
                Manager.Instance().CastToModelsCollection<ManagementTeam.Feedback, FeedbackModel>(feedbacks.Entities, Manager.Instance().CastToFeedbackModel);
            return feedbackModels;
        }

        /// <summary>
        /// Sends an entity to get T type objects relevant to the entity
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="entities"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        internal C GetEntitiesCollection<E, C>(E entity, string requestUrl)
            where E : EntityBase 
            where C : EntityBase
        {
            WebResponse response = Utils.Functionals.ManagementStreamHandler.SendObject(entity, requestUrl, "POST");
            return Utils.Functionals.ManagementStreamHandler.RecieveObject<C>(response);
        }

        /// <summary>
        /// Casts collection of entities to collection of models.
        /// </summary>
        /// <typeparam name="E">Entity type</typeparam>
        /// <typeparam name="M">Model type</typeparam>
        /// <param name="entities">Collection of entities that should be casted</param>
        /// <param name="castEntityToModelMethod">Method that casts an E entity to a M model</param>
        /// <returns></returns>
        internal ICollection<M> CastToModelsCollection<E, M>(IEnumerable<E> entities, Func<E, M> castEntityToModelMethod)
        {
            var models = new List<M>();
            foreach (var entity in entities)
            {
                models.Add(castEntityToModelMethod.Invoke(entity));
            }
            return models;
        }

        /// <summary>
        /// Casts Feedback entity to a Feedback Model
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        internal FeedbackModel CastToFeedbackModel(ManagementTeam.Feedback feedback)
        {
            var feedbackModel = new FeedbackModel()
            {
                Id = feedback.Id,
                //Employee.Id = feedback.EmployeeId,
                //EmployeeId = feedback.EmployeeId,
                ReviewerId = feedback.ReviewerId,
                WorkedTogether = feedback.WorkedTogether,
                WishToWorkTogether = feedback.WishToWorkTogether,
                PossitiveSide = feedback.PossitiveSide,
                NegativeSide = feedback.NegativeSide,
                ThingsToImprove = feedback.ThingsToImprove,
                Message = feedback.Message
            };
            feedbackModel.Employee.Id = feedback.EmployeeId;
            return feedbackModel;

            //return new FeedbackModel()
            //{
            //    Id = feedback.Id,
            //    EmployeeId = feedback.EmployeeId, 
            //    ReviewerId = feedback.ReviewerId, 
            //    WorkedTogether = feedback.WorkedTogether, 
            //    WishToWorkTogether = feedback.WishToWorkTogether, 
            //    PossitiveSide = feedback.PossitiveSide, 
            //    NegativeSide = feedback.NegativeSide, 
            //    ThingsToImprove = feedback.ThingsToImprove, 
            //    Message = feedback.Message
            //};
        }

        /// <summary>
        /// Casts FeedbackModel to Feedback Entity
        /// </summary>
        /// <param name="feedbackModel">Model to cast</param>
        /// <returns></returns>
        internal ManagementTeam.Feedback CastToFeedbackEntity(FeedbackModel feedbackModel)
        {
            return new ManagementTeam.Feedback()
            {
                //EmployeeId = feedbackModel.EmployeeId,
                EmployeeId = feedbackModel.Employee.Id,
                ReviewerId = feedbackModel.ReviewerId,
                WorkedTogether = feedbackModel.WorkedTogether,
                WishToWorkTogether = feedbackModel.WishToWorkTogether,
                PossitiveSide = feedbackModel.PossitiveSide,
                NegativeSide = feedbackModel.NegativeSide,
                ThingsToImprove = feedbackModel.ThingsToImprove,
                Message = feedbackModel.Message
            };
        }

        /// <summary>
        /// Casts DataModel entity to MVC Model entity
        /// </summary>
        /// <param name="employee"></param>
        internal EmployeeManagementModel CastToManagementEmployeeModel(ManagementTeam.Employee employee)
        {
            return new EmployeeManagementModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                //Surname = employee.Surname,
                Surname = employee.LastName,
                Team = employee.Team,
                Task = employee.Task,
                Project = employee.Project,
                Role = employee.Role
            };
        }

        /// <summary>
        /// Casts collection of DataModel entities to collection of MVC Model entities
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        internal IEnumerable<EmployeeManagementModel> CastToCommonEmployeesModel(IEnumerable<DataModel.Entities.Common.Employee> employees)
        {
            var employeesModel = new List<EmployeeManagementModel>();
            foreach (var employee in employees)
            {
                employeesModel.Add(this.CastToCommonEmployeeModel(employee));
            }
            return employeesModel;
        }

        /// <summary>
        /// Casts model entity to DataModel entity
        /// </summary>
        /// <param name="employeeModel">Model</param>
        /// <returns>Data Model</returns>
        internal ManagementTeam.Employee CastToManagementEmployeeEntity(EmployeeManagementModel employeeModel)
        {
            return new ManagementTeam.Employee()
            {
                Id = employeeModel.Id,
                Name = employeeModel.Name,
                Surname = employeeModel.Surname,
                LastName = employeeModel.Surname,
                Role = employeeModel.Role,
                Project = employeeModel.Project,
                Task = employeeModel.Task,
                Team = employeeModel.Team
            };
        }

        /// <summary>
        /// Casts DataModel entity to MVC Model entity
        /// </summary>
        /// <param name="employee"></param>
        private EmployeeManagementModel CastToCommonEmployeeModel(DataModel.Entities.Common.Employee employee)
        {
            return new EmployeeManagementModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.LastName
            };
        }

        public string DeleteEmployeeManagement(ERPS.ServiceLayer.Models.EmployeeManagementId employee)
		{
			ManagementTeam.EmployeeForGetAndDelete Employee = new ManagementTeam.EmployeeForGetAndDelete();
			Employee.Id = employee.Id;
			string url = "http://localhost:53654/api/Management/DeleteEmployee";
			ManagementTeam.EmployeeForGetAndDelete deserialized = Communicator.SendRequest<ManagementTeam.EmployeeForGetAndDelete, ManagementTeam.EmployeeForGetAndDelete>
				(Employee, url, "POST", "application/octet-stream");
			return "Success";
		}

         public TasksCollection GetAllTasks()
        {
            TasksCollection list = new TasksCollection();
            string url = "http://localhost:53654/api/Management/GetAllTasks";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                BinaryFormatter bfd = new BinaryFormatter();
                list = bfd.Deserialize(memStr) as TasksCollection;
            }
            return list;
        }

		#endregion

		#region HRTeam
        /// <summary>
        /// created HttpRequest called GEtEmployeesHR function 
        /// deserialized HREntitiess model stream, convert datamodel to model
        /// </summary>
        /// <returns>HREmployees Model</returns>
		public Models.EmployeesHRModel GetEmployeesHR()
        {
     
            HREntities employees = new HREntities();
            string baseUrl = ConfigurationManager.ConnectionStrings["HRServerName"].ConnectionString;
            HttpWebRequest req = Utils.Functionals.Communicator.GetRequest(baseUrl+DestinationNames.GetHREmployees , "GET");
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                 employees = Utils.Functionals.Communicator.ParseResponse<HREntities>(resp.GetResponseStream());
            }
            catch (WebException ex)
            {
                Logger.Logger.Addlog( ex.Message+','+ex.Status);
            }
                EmployeesHRModel hremployees = new EmployeesHRModel();
                foreach (HREntity el in employees.ListOfEntities)
                {
                    Models.EmployeeHRModel item = new EmployeeHRModel();
                    item.Id = el.Id;
                    item.Name = el.Name;
                    item.LastName = el.LastName;
                    item.Address = el.Address;
                    item.Phone = el.Phone;
                    hremployees.ListOFEmployee.Add(item);

                }
                return hremployees;
            
        }
        /// <summary>
        /// called GetCandidates function 
        /// received stream deserialized it convert Candidate datamodel to Candidate Model
        /// 
        /// </summary>
        /// <returns> CAndidates Model</returns>
        public HRCandidates HRCandidate()
        {
            HttpWebResponse resp = null;
            HRCandidates candidates = new HRCandidates();
            string baseUrl = ConfigurationManager.ConnectionStrings["HRServerName"].ConnectionString;
            HttpWebRequest req = Utils.Functionals.Communicator.GetRequest(baseUrl+DestinationNames.GEtCandidates, "GET");
            resp = Utils.Functionals.Communicator.CreateResponse(req);
          
            
                try {
                Stream stream = resp.GetResponseStream();
                XmlSerializer serializer = new XmlSerializer(typeof(Candidate));
                var sr = new StreamReader(stream);
                Candidate candidateEntity = (Candidate)serializer.Deserialize(stream);
                    foreach (CandidateEmployee el in candidateEntity.ListofCandidate)
                    {
                        HRCandidate item = new HRCandidate();
                        item.Name = el.Name;
                        item.LastName = el.LastName;
                        item.Email = el.Email;
                        item.Address = el.Address;
                        item.Phone = el.Phone;
                        candidates.ListOfCandidate.Add(item);
                    }
                }catch(Exception ex)
                {
                  
                    Logger.Logger.Addlog(ex.InnerException + " " + ex.Message);
                    return candidates;
                }
                return candidates;
            

        }

     /// <summary>
     /// received Employee Model, convert it to datamodel 
     /// and send to HR layer
     /// </summary>
     /// <param name="HREntity"></param>
     /// <returns></returns>
        public string PostEmployee(EmployeeHRModel HREntity)
        {
            HREntities entitiy = new HREntities();
         //Convert Model to Entity
            HREntity hrentiy = new HREntity();
            {
                hrentiy.Id = HREntity.Id;
                hrentiy.Name = HREntity.Name;
                hrentiy.LastName = HREntity.LastName;
                hrentiy.DateOfBirth = HREntity.DateOfBirth;
                hrentiy.DateOfHiring = HREntity.DateOfHiring;
                hrentiy.Address = HREntity.Address;
                hrentiy.Description = HREntity.Description;
                hrentiy.MaritalStatus = HREntity.MaritalStatus;
                hrentiy.Passport = HREntity.Passport;
                hrentiy.Phone = HREntity.Phone;
                hrentiy.SocialId = HREntity.SocialId;
                entitiy.ListOfEntities.Add(hrentiy);
            }
            string baseUrl = ConfigurationManager.ConnectionStrings["HRServerName"].ConnectionString;
            HttpWebRequest request = Utils.Functionals.Communicator.PostReques<HREntities>(baseUrl+ DestinationNames.AddHREmployee, @"POST", entitiy);
            return Utils.Functionals.Communicator.GetResponse(request.GetResponse());
         
        }
        /// <summary>
        /// received int 
        /// send HREntityId datamodel to HRDeleteEmployee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>meesage about result</returns>
        public string DeleteEmployee(int id)
        {
            HREntityID deleteId = new HREntityID();
            deleteId.Id = id;
            string baseUrl = ConfigurationManager.ConnectionStrings["HRServerName"].ConnectionString;
            HttpWebRequest request = Utils.Functionals.Communicator.PostReques<HREntityID>(baseUrl+DestinationNames.DeleteHREmployee, "POST", deleteId);
            return Utils.Functionals.Communicator.GetResponse(request.GetResponse());

        }
        /// <summary>
        /// received id 
        /// created HRentityId entity and send it to GEtEmployeeById function
        /// In the response, the function receives HREntity Model
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>HREntity Model</returns>
        public Models.EmployeeHRModel GetHREmployeeById(HREntityID employee)
        {
            
            HREntity deserialized = null;
            string baseUrl = ConfigurationManager.ConnectionStrings["HRServerName"].ConnectionString;
            HttpWebRequest request = Utils.Functionals.Communicator.PostReques<HREntity>(baseUrl+DestinationNames.GetHREmployeeId, "POST", employee);
            try
            {
                WebResponse response = request.GetResponse();
                deserialized = Utils.Functionals.Communicator.ParseResponse<HREntity>(response.GetResponseStream());
            }
            catch(WebException ex)
            {
                Logger.Logger.Addlog(ex.InnerException + " " + ex.Message); 
            }

            Models.EmployeeHRModel hrmodel = new EmployeeHRModel();
            hrmodel.Id = deserialized.Id;
            hrmodel.Name = deserialized.Name;
            hrmodel.LastName = deserialized.LastName;
            hrmodel.Passport = deserialized.Passport;
            hrmodel.Phone = deserialized.Phone;
            hrmodel.SocialId = deserialized.SocialId;
            hrmodel.Address = deserialized.Address;
            hrmodel.DateOfBirth = deserialized.DateOfBirth;
            hrmodel.DateOfHiring = deserialized.DateOfHiring;
            hrmodel.Description = deserialized.Description;
          
            return hrmodel;
        }
        #endregion

        #region FinanceTeam

        public EmployeesFinance GetEmployeesFinance()
        {
            EmployeesFinance deserialized = new EmployeesFinance();
            string baseUrl = ConfigurationManager.ConnectionStrings["FinanceServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetFinanceEmployees;
            deserialized = Communicator.GetEntitiesList<EmployeesFinance>(url);
            return deserialized;
        }
        public List<EmployeeFinanceModel> CastToEmployeesFinanceModel(EmployeesFinance employees)
        {
            List<EmployeeFinanceModel> Salaries = new List<EmployeeFinanceModel>();
            foreach (EmployeeFinance employee in employees.Employees)
            {
                EmployeeFinanceModel emp = new EmployeeFinanceModel();
                emp.Id = employee.Id;
                emp.Name = employee.Name;
                emp.LastName = employee.LastName;
                emp.Salary = employee.Salary;
                emp.SalaryDate = employee.SalaryDate;
                Salaries.Add(emp);
            }
            return Salaries;
        }
        public EmployeeFinance CreateEmployee(EmployeeFinanceModel employee)
        {
            EmployeeFinance Employee = new EmployeeFinance();
            Employee.Id = employee.Id;
            Employee.Name = employee.Name;
            Employee.Salary = employee.Salary;
            return Employee;
        }

        public string UpdateEmployeeFinance(EmployeeFinanceModel employee)
        {
            EmployeeFinance Employee = this.CreateEmployee(employee);
            string baseUrl = ConfigurationManager.ConnectionStrings["FinanceServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.UpdateEmployeeFinance;
            EmployeeFinance deserialized = Communicator.SendRequest<EmployeeFinance, EmployeeFinance>
                (Employee, url, "POST", "application/octet-stream");
            return "Success";
        }

        public string DeleteEmployeeFinance(EmployeeFinanceId employee)
        {
            EmployeeFinanceForGetAndDelete Employee = new EmployeeFinanceForGetAndDelete();
            Employee.Id = employee.Id;
            string baseUrl = ConfigurationManager.ConnectionStrings["FinanceServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.DeleteEmployeeFinance;
            EmployeeFinanceForGetAndDelete deserialized = Communicator.SendRequest<EmployeeFinanceForGetAndDelete, EmployeeFinanceForGetAndDelete>
                (Employee, url, "POST", "application/octet-stream");
            return "Success";
        }
		public EmployeeFinance GetEmployeeFinanceById(EmployeeFinanceId employee)
		{
            EmployeeFinanceForGetAndDelete Employee = new EmployeeFinanceForGetAndDelete();
            EmployeeFinance deserialized = null;

            Employee.Id = employee.Id;
            string baseUrl = ConfigurationManager.ConnectionStrings["FinanceServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetEmployeeFinanceById;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/octet-stream";
            using (Stream strRequest = request.GetRequestStream())
            {
                MemoryStream memorystream = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memorystream, Employee);
                memorystream.Flush();
                memorystream.Position = 0;
                byte[] Bytes = memorystream.ToArray();
                strRequest.Write(Bytes, 0, Bytes.Length);
            }

            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())

            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                BinaryFormatter bfd = new BinaryFormatter();
                deserialized = bfd.Deserialize(memStr) as EmployeeFinance;
            }

            return deserialized;
        }
        public EmployeeFinanceModel CastToEmployeeFinance(EmployeeFinance employee)
        {
            EmployeeFinanceModel emp = new EmployeeFinanceModel();
            emp.Id = employee.Id;
            emp.Name = employee.Name;
            emp.LastName = employee.LastName;
            emp.Salary = employee.Salary;
            return emp;
        }
        #endregion
        #region Task
        public ManagementTeam.Task CreateTask(TaskModel Task)
        {
            ManagementTeam.Task task = new ManagementTeam.Task();
            task.Id = Task.Id;
            task.StateId = Task.StateId;
            task.PlannedStart = Task.PlannedStart;
            task.PlannedEnd = Task.PlannedEnd;
            task.ActualStart = Task.ActualStart;
            task.ActualEnd = Task.ActualEnd;
            task.Description = Task.Description;
            task.Source = Task.Source;
            task.Comments = Task.Comments;
            task.Revision = Task.Revision;
            task.ProjectId = Task.ProjectId;
            task.ReporterId = Task.ReporterId;
            task.AssigneeId = Task.AssigneeId;
            return task;
        }
        

        public string AddTask(TaskModel task)
        {
            ManagementTeam.Task Task = this.CreateTask(task);
            string baseUrl = ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.AddTask;
            ManagementTeam.Task deserialized = Communicator.SendRequest<ManagementTeam.Task, ManagementTeam.Task>
                (Task, url, "POST", "application/octet-stream");
            return "Success";
        }
        public List<ProjectModel> GetProjects()
        {
            List<Project> prjs = new List<Project>();
            string baseUrl = ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetProjects;
            prjs = Utils.Functionals.Communicator.GetEntities<Project>(url);
            List<ProjectModel> projects = new List<ProjectModel>();
            foreach (var item in prjs)
            {
                ProjectModel project = new ProjectModel();
                project.Id = item.Id;
                project.Name = item.Name;
                projects.Add(project);
            }
            return projects;
        }
        public List<TaskStateModel> GetStates()
        {
            List<TaskState> deserialized = new List<TaskState>();
            string baseUrl = ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetStates;
            deserialized = Utils.Functionals.Communicator.GetEntities<TaskState>(url);
            List<TaskStateModel> states = new List<TaskStateModel>();
            foreach (var item in deserialized)
            {
                TaskStateModel state = new TaskStateModel();
                state.Id = item.Id;
                state.Name = item.Name;
                states.Add(state);
            }
            return states;
        }
        public List<SeverityModel> GetSeverities()
        {
            List<Severity> deserialized = new List<Severity>();
            string baseUrl = ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetSeverities;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                BinaryFormatter bfd = new BinaryFormatter();
                deserialized = bfd.Deserialize(memStr) as List<Severity>;
            }
            List<SeverityModel> s = new List<SeverityModel>();
            foreach ( var item in deserialized)
            {
                SeverityModel v = (SeverityModel)Enum.Parse(typeof(Severity), item.ToString());
                s.Add(v);
            }
            return s;
        }
        public List<DataModel.Entities.Common.Employee> GetEmployees()
        {
            List<DataModel.Entities.Common.Employee> deserialized = new List<DataModel.Entities.Common.Employee>();
            string baseUrl = ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetTaskEmployees;
            deserialized = Communicator.GetEntities<DataModel.Entities.Common.Employee>(url);
            List<DataModel.Entities.Common.Employee> employees = new List<DataModel.Entities.Common.Employee>();
            foreach (var item in deserialized)
            {
                DataModel.Entities.Common.Employee employee = new DataModel.Entities.Common.Employee();
                employee.Id = item.Id;
                employee.Name = item.Name;
                employees.Add(employee);
            }
            return employees;
        }
        public string UploadAttachment(Models.Attachment attach)
        {
            ManagementTeam.Tasks.Attachment attachment = new ManagementTeam.Tasks.Attachment();
            attachment.FileName = attach.FileName;
            attachment.File = attach.File;
            string baseUrl = ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.UploadAttachment;
            ManagementTeam.Tasks.Attachment deserialized = Communicator.SendRequest<ManagementTeam.Tasks.Attachment, 
                ManagementTeam.Tasks.Attachment>
                (attachment, url, "POST", "application/octet-stream");
            return "Success";
        }
        public byte[] DownloadAttachments(int Id)
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.DownloadAttachment + Id.ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                return memStr.ToArray();
            }
        }
        public List<TaskModel> GetTasks()
        {
            TasksCollection deserialized = new TasksCollection();
            string baseUrl = ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetTasks;
            deserialized = Communicator.GetEntitiesList<TasksCollection>(url);
            List<TaskModel> tasks = new List<TaskModel>();
            foreach (var item in deserialized.tasksCollection)
            {
                TaskModel task = new TaskModel();
                task.Id = item.Id;
                task.PlannedStart = item.PlannedStart;
                task.PlannedEnd = item.PlannedEnd;
                task.ActualStart = item.ActualStart;
                task.ActualEnd = item.ActualEnd;
                task.Comments = item.Comments;
                task.Source = item.Source;
                task.Revision = item.Revision;
                task.Description = item.Description;
                task.ReporterName = item.ReporterName;
                task.AssigneeName = item.AssigneeName;
                task.ProjectName = item.ProjectName;
                task.State = item.State;
                task.SeverityType = item.Severity;
                task.ReporterId = item.ReporterId;
                task.AssigneeId = item.AssigneeId;
                task.StateId = item.StateId;
                task.ProjectId = item.ProjectId;
                task.SeverityId = item.SeverityId;
                tasks.Add(task);
            }
            return tasks;
        }
        public string DeleteTask(TaskId task)
        {
            TaskForDelete Task = new TaskForDelete();
            Task.Id = task.Id;
            string baseUrl = ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.DeleteTask;
            TaskForDelete deserialized = Communicator.SendRequest<TaskForDelete, TaskForDelete>
                (Task, url, "POST", "application/octet-stream");
            return "Success";
        }
        #endregion
        #region Notification
        public NotificationsModel GetNotifications()
		{
			string BaseUrl = ConfigurationManager.ConnectionStrings["NotificationServerName"].ConnectionString;
			string url = BaseUrl + DestinationNames.GetNotifications;
			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
			req.Method = "GET";
			HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
			using (Stream stream = resp.GetResponseStream())
			{
				BinaryFormatter bf = new BinaryFormatter();
				var sr = new StreamReader(stream);
				Notifications notifications = (Notifications)bf.Deserialize(stream);
				NotificationsModel notificationsModel = new NotificationsModel();
				foreach (Notification el in notifications.ListOfEvents)
				{
					Models.NotificationModel item = new NotificationModel();
					DateTime newDt = new DateTime(DateTime.Now.Year, el.Date.Month, el.Date.Day);
					item.Date = newDt;
					item.Address = el.Address;
					item.Type = el.Type;
					notificationsModel.ListOfEvents.Add(item);
					
				}
                foreach(Birthday el in notifications.ListOfBirthdays)
                {
                    BirthdayModel day = new BirthdayModel();
                    day.Date = el.Date;
                    day.FirstName = el.FirstName;
                    day.LastName = el.LastName;
                    notificationsModel.ListOfBirthdays.Add(day);
                }
				return notificationsModel;
			}
		}
		public string AddEvent(NotificationModel Event)
		{
			Notifications Events = new Notifications();
			//Convert Model to Entity
			Notification ev = new Notification();
            {
             
                ev.Type = Event.Type;
                ev.Address = Event.Address;
                ev.Date = Event.Date;
                ev.EveryYear = Event.EveryYear;
                Events.ListOfEvents.Add(ev);
            }
			string BaseUrl = ConfigurationManager.ConnectionStrings["NotificationServerName"].ConnectionString;
			HttpWebRequest request = Utils.Functionals.Communicator.PostReques<Notifications>(BaseUrl + DestinationNames.AddEvent, "POST", Events);
			WebResponse response = request.GetResponse();
            return Utils.Functionals.Communicator.GetResponse(response);
        }
		
	#endregion
}

}
