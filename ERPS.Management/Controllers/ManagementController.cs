using ERPS.DataModel.Entities.Common;
using ERPS.Management.Functionals;
using ERPS.Utils.Functionals;
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

namespace ERPS.Management.Controllers
{
    public class ManagementController : ApiController
    {
        [HttpGet]
        [Route("api/Management/GetEmployees")]
        public HttpResponseMessage GetEmployees()
        {
            //var employees = ERPS.Management.Functionals.Manager.Instance.GetEmployees<Employee>(Request.Content.ReadAsStreamAsync());
            //var serverUrl = "http://localhost:53865/api/DAL/ManagementGetEmployees";
            var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["DALServerUrl"].ConnectionString
                + DestinationNames.GetEmployees;
            var employees = ERPS.Management.Functionals.Manager.Instance.GetEntities<DataModel.Entities.Management.Employee>(Request.Content.ReadAsStreamAsync(), serverUrl);
            //System.Diagnostics.Debugger.Launch();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(ERPS.Utils.Functionals.ManagementStreamHandler.SerializeToByteArray(employees))
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //System.Diagnostics.Debugger.Launch();
            return response;
        }

        [HttpPost]
        [Route("api/Management/UpdateEmployee")]
        public HttpResponseMessage UpdateEmployee()
        {
            //System.Diagnostics.Debugger.Launch();
            /*System.Configuration.ConfigurationManager.ConnectionStrings["UpdateEmployee"].ConnectionString*/
            //var serverUrl = "http://localhost:53865/api/DAL/ManagementUpdateEmployee";
            var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["DALServerUrl"]
                + DestinationNames.UpdateEmployee;
            return Functionals.Manager.Instance.UpdateEntity<DataModel.Entities.Management.Employee>(Request.Content.ReadAsStreamAsync(), serverUrl);
            //return ERPS.Management.Functionals.Manager.Instance.UpdateEmployee(Request.Content.ReadAsStreamAsync());
        }

        [HttpPost]
        [Route("api/Management/LeaveFeedback")]
        public HttpResponseMessage LeaveFeedback()
        {
            //System.Diagnostics.Debugger.Launch();
            /*System.Configuration.ConfigurationManager.ConnectionStrings["UpdateEmployee"].ConnectionString*/
            //"http://localhost:53865/api/DAL/ManagementLeaveFeedback"
            var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["DALServerUrl"]
                + DestinationNames.LeaveFeedback;
            return Functionals.Manager.Instance.LeaveFeedback(Request.Content.ReadAsStreamAsync(), serverUrl);
        }

        [HttpPost]
        [Route("api/Management/GetEmployeeFeedbacks")]
        public HttpResponseMessage GetEmployeeFeedbacks()
        {
            //System.Diagnostics.Debugger.Launch();
            //"http://localhost:53865/api/DAL/ManagementGetEmployeeFeedbacks"
            var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["DALServerUrl"]
                + DestinationNames.GetEmployeeFeedbacks;
            return Manager.Instance.GetEmployeeFeedbacks(Request.Content.ReadAsStreamAsync(), serverUrl);
        }

        [HttpGet]
        [Route("api/Management/GetFeedbacks")]
        public HttpResponseMessage GetFeedbacks()
        {
            //"http://localhost:53865/api/DAL/ManagementGetFeedbacks"
            var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["DALServerUrl"]
                + DestinationNames.GetFeedbacks;
            var feedbacks = ERPS.Management.Functionals.Manager.Instance.GetEntities<DataModel.Entities.Management.Feedback>(Request.Content.ReadAsStreamAsync(), serverUrl);
            //System.Diagnostics.Debugger.Launch();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(ERPS.Utils.Functionals.ManagementStreamHandler.SerializeToByteArray(feedbacks))
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //System.Diagnostics.Debugger.Launch();
            return response;
        }

        [Route("api/Management/DeleteEmployee")]
		[HttpPost]
		public string DeleteEmployee()
		{
			Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

			task.Wait();

			byte[] byteArray = task.Result;
			Manager.Instance.DeleteEmployee(byteArray);

			return "success.";
		}
        [HttpGet]
        public HttpResponseMessage GetAllTasks()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Formatter.GetBinary<DataModel.Entities.Management.Tasks.TasksCollection>(Manager.Instance.GetTasks()))
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
	}
}
