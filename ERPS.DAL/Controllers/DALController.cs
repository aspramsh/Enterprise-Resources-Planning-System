using ERPS.DAL.Functionals;
using ERPS.DataModel.Entities.HR;
using ERPS.DataModel.Entities.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web.Http;
using ERPS.DataProviders.Providers;
using ERPS.DataModel.Entities.Notification;
using ERPS.DataProviders.Interfaces;

namespace ERPS.DAL.Controllers
{
    public class DALController : ApiController
    {
        public object AddHrEmployee { get; private set; }

        #region ManagementTeam
        private SQLProvider _sqlProvider = new SQLProvider();
        [HttpPost]
        [Route("api/dal/GetDataFromDb")]
        public HttpResponseMessage GetDataFromDb()
        //public Employee GetDataFromDb()
        {
            //var response = new HttpResponseMessage();
            //return Functionals.Manager.Instance().GetEmployee();
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                //Content = new StreamContent(Functionals.Manager.Instance().GetSerializedObject())
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetSerializedObject())
            };
            //result.Content = new StreamContent(Functionals.Manager.Instance().GetSerializedObject());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        [HttpGet]
        [Route("api/DAL/ManagementGetEmployees")]
        public HttpResponseMessage ManagementGetEmployees()
        {
            //System.Diagnostics.Debugger.Launch();
            Func<Collection<Employee>> getEmployeesMethod = this._sqlProvider.ManagementGetEmployees<Employee>;
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().ManagementGetEntitiesSerialized<DataModel.Entities.Management.Employee>(getEmployeesMethod))
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //System.Diagnostics.Debugger.Launch();
            return response;
        }

        [HttpPost]
        [Route("api/DAL/ManagementUpdateEmployee")]
        public HttpResponseMessage ManagementUpdateEmployee()
        {
            //System.Diagnostics.Debugger.Launch();
            return ERPS.DAL.Functionals.Manager.Instance().ManagementUpdateEmployee(Request.Content.ReadAsStreamAsync());
        }

        [HttpPost]
        [Route("api/DAL/ManagementLeaveFeedback")]
        public HttpResponseMessage ManagementLeaveFeedback()
        {
            //System.Diagnostics.Debugger.Launch();
            return Manager.Instance().ManagementLeaveFeedback(Request.Content.ReadAsStreamAsync());
        }

        [HttpPost]
        [Route("api/DAL/ManagementGetEmployeeFeedbacks")]
        public HttpResponseMessage ManagementGetEmployeeFeedbacks()
        {
            //System.Diagnostics.Debugger.Launch();
            return Manager.Instance().ManagementExecuteSPForRequestedEntity(Request.Content.ReadAsStreamAsync(), _sqlProvider.ManagementGetEmployeeFeedbacks);
            //Func<Collection<Feedback>> getFeedbacksMethod = _sqlProvider.ManagementGetEmployeeFeedbacks;
            //var response = new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new ByteArrayContent(Functionals.Manager.Instance().ManagementGetEntitiesSerialized<DataModel.Entities.Management.Feedback>(getFeedbacksMethod))
            //};
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            ////System.Diagnostics.Debugger.Launch();
            //return response;
        }

        [HttpGet]
        [Route("api/DAL/ManagementGetFeedbacks")]
        public HttpResponseMessage ManagementGetFeedbacks()
        {
            //System.Diagnostics.Debugger.Launch();
            Func<Collection<Feedback>> getFeedbacksMethod = _sqlProvider.ManagementGetFeedbacks;
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().ManagementGetEntitiesSerialized<DataModel.Entities.Management.Feedback>(getFeedbacksMethod))
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //System.Diagnostics.Debugger.Launch();
            return response;
        }

        //[HttpPost]
        //[Route("api/DAL/ManagementUpdateEmployee")]
        //public HttpResponseMessage ManagementUpdateEmployeeOld()
        //{
        //    //System.Diagnostics.Debugger.Launch();
        //    ERPS.DAL.Functionals.Manager.Instance().ManagementUpdateEmployeeOld(Request.Content.ReadAsStreamAsync());
        //    return new HttpResponseMessage();
        //}

        [HttpGet]
        //[Route("api/dal/GetDataFromDb?id={id}")]
        //[Route("api/dal/GetDataFromDb/{id}")]
        public string GetDataFromDb([FromUri]int id, [FromUri]int id1)
        {
            return $"Get your entity {Functionals.Manager.Instance().GetEntity(4)}";
        }

        [HttpGet]
        //[Route("api/dal/GetDataFromDb")]
        //[Route("api/dal/GetDataFromDb?id={id}")]
        //[Route("api/dal/GetDataFromDb/{id}")]
        public string GetDataFromDb1([FromUri]int id, [FromUri]int id1)
        {
            return $"Get your entity {Functionals.Manager.Instance().GetEntity(4)}";
        }

        [Route("api/DAL/DeleteEmployeeManagement")]
		[HttpPost]
		public string DeleteEmployeeManagment()
		{
			Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();
			task.Wait();

			byte[] byteArray = task.Result;
			Manager.Instance().DeleteEmployeeManagement(byteArray);

			return "Deleted.";
		}

        [Route("api/DAL/GetAllTasks")]
        [HttpGet]
        public HttpResponseMessage GetAllTasks()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetTasks())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        #endregion

        #region HRTeam
        private IDataProvider provider = new SQLProvider();
        /// <summary>
        /// Function call HRDalManagerGetEmployee function create new memorystream from object
        /// </summary>
        /// <returns>HTTPResonseMEssage </returns>
        [HttpGet]
        public HttpResponseMessage GetDalEmployees()
        {
          
            var ms = new MemoryStream(ERPS.DAL.Functionals.Manager.Instance().HRDALManagerGetEmployee());
            return Utils.Functionals.Communicator.ResponseMessage(ms);
        }
        /// <summary>
        /// Function deserialized HREntity and call AddHREmployee() 
        /// from sqlprovider for edd employee
        /// </summary>
        /// <returns></returns>
        [Route("api/DAL/AddDalEmployee")]
        [HttpPost]
        public string AddDalEmployee()
        {
          
            Task<Stream> task = Request.Content.ReadAsStreamAsync();
            task.Wait();
            Stream EntityStream = task.Result;
            BinaryFormatter bf = new BinaryFormatter();
            HREntities employees = (HREntities)bf.Deserialize(EntityStream);
            return provider.AddHREmployee(employees.ListOfEntities[0]);
           
        }
        /// <summary>
        /// function received stream and deserizalized it 
        /// call DEleteHREmployee function from Sqlprovider
        /// </summary>
        /// <returns></returns>
        [Route("api/DAL/DeleteDalEmployee")]
        [HttpPost]
        public string DeleteDalEmployee()
        {
         
            Task<Stream> task = Request.Content.ReadAsStreamAsync();
            task.Wait();
            Stream EntityStream = task.Result;
            BinaryFormatter bf = new BinaryFormatter();
            HREntityID employee = (HREntityID)bf.Deserialize(EntityStream);
            return provider.DeleteHREmployee(employee.Id);
       
        }
        /// <summary>
        /// function received stream and call HRGEtEmployeeById 
        /// function from SQlprovider
        /// </summary>
        /// <returns>HTTPREsponseMessage</returns>
        [Route("api/DAL/GEtHREmployeeById")]
        [HttpPost]
        public HttpResponseMessage GetHREmployeeByid()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();
            task.Wait();

            byte[] byteArray = task.Result;
            Byte[] Bytes = Manager.Instance().HRGetEmployeeById(byteArray);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Bytes)
            };
            return result;
        }
        #endregion

        #region FinanceTeam

        [Route("api/DAL/GetEmployeesFinance")]
        [HttpGet]
        public HttpResponseMessage GetEmployeesFinance()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetEmployeesFinance())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        [Route("api/DAL/UpdateEmployeeFinance")]
        [HttpPost]
        public string UpdateEmployeeFinance()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();
            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().UpdateEmployeeFinance(byteArray);

            return "Updated.";
        }

        [Route("api/DAL/DeleteEmployeeFinance")]
        [HttpPost]
        public string DeleteEmployeeFinance()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();
            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().DeleteEmployeeFinance(byteArray);

            return "Deleted.";
        }
		[Route("api/DAL/GetEmployeeFinanceById")]
		[HttpPost]
		public HttpResponseMessage GetEmployeeFinanceById()
		{
			Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();
			task.Wait();

			byte[] byteArray = task.Result;
			Byte[] Bytes = Manager.Instance().GetEmployeeFinanceById(byteArray);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Bytes)
            };
            return result;
		}
        #endregion
        #region Task
        [Route("api/DAL/AddTask")]
        [HttpPost]
        public string AddTask()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();
            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().AddTask(byteArray);

            return "The task is added.";
        }
        [Route("api/DAL/UploadAttachments")]
        [HttpPost]
        public string UploadAttachments()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();
            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().UploadAttachment(byteArray);

            return "Added";
        }
        [Route("api/DAL/DownLoadAttachments")]
        [HttpGet]
        public HttpResponseMessage DownLoadAttachments(int Id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().DownloadAttachment(Id))
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/Jpg");
            return result;
        }

        [Route("api/DAL/GetProjects")]
        [HttpGet]
        public HttpResponseMessage GetProjects()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetProjects())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/DAL/GetStates")]
        [HttpGet]
        public HttpResponseMessage GetStates()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetStates())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/DAL/GetSeverities")]
        [HttpGet]
        public HttpResponseMessage GetSeverities()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetSeverities())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/DAL/GetEmployees")]
        [HttpGet]
        public HttpResponseMessage GetEmployees()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetEmployees())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/DAL/GetTasks")]
        [HttpGet]
        public HttpResponseMessage GetTasks()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Functionals.Manager.Instance().GetTasks())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/DAL/DeleteTask")]
        [HttpPost]
        public string DeleteTask()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();
            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().DeleteTask(byteArray);

            return "Deleted.";
        }
        #endregion
        #region Notification
        [HttpGet]
		public HttpResponseMessage GetNotifications()
		{
			var ms = new MemoryStream(ERPS.DAL.Functionals.Manager.Instance().GetNotifications());
			ms.Seek(0, SeekOrigin.Begin);
			ms.Position = 0;
			var result = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new ByteArrayContent(ms.ToArray())
			};
			result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

			return result;

		}
		[HttpPost]
		public string AddEvent()
		{
			SQLProvider provider = new SQLProvider();
			Task<Stream> task = Request.Content.ReadAsStreamAsync();
			task.Wait();
			Stream EntityStream = task.Result;
			BinaryFormatter bf = new BinaryFormatter();
			Notifications events = (Notifications)bf.Deserialize(EntityStream);
			return provider.AddNotification(events.ListOfEvents[0]);

		}

		#endregion
	}
}