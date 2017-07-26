using ERPS.DataModel.Entities.Management;
using ERPS.Management.Functionals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace ERPS.Management.Controllers
{
    public class TaskController : ApiController
    {
        [Route("api/Task/AddTask")]
        [HttpPost]
        public string AddTask()
        {
            System.Threading.Tasks.Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance.AddTask(byteArray);

            return "success.";
        }
        [Route("api/Task/GetProjects")]
        [HttpGet]
        public HttpResponseMessage GetProjects()
        {
            List<Project> projects = Manager.Instance.GetProjects();
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Utils.Functionals.Communicator.SerializeEntityList(projects))
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/Task/GetStates")]
        [HttpGet]
        public HttpResponseMessage GetStates()
        {
            List<TaskState> states = Manager.Instance.GetStates();
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Utils.Functionals.Communicator.SerializeEntityList(states))
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/Task/GetEmployees")]
        [HttpGet]
        public HttpResponseMessage GetEmployees()
        {
            List<DataModel.Entities.Common.Employee> employees = Manager.Instance.GetEmployees();
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Utils.Functionals.Communicator.SerializeEntityList(employees))
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/Task/GetSeverities")]
        [HttpGet]
        public HttpResponseMessage GetSeverities()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Manager.Instance.SerializeSeverities())
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/Task/UploadAttachments")]
        [HttpPost]
        public string UploadAttachments()
        {
            System.Threading.Tasks.Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance.UploadAttachments(byteArray);

            return "success.";
        }
        [Route("api/Task/DownloadAttachments")]
        [HttpGet]
        public HttpResponseMessage DownloadAttachments(int Id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Manager.Instance.DownloadAttachments(Id))
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/Jpg");
            return result;
        }
        [Route("api/Task/GetTasks")]
        [HttpGet]
        public HttpResponseMessage GetTasks()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Manager.Instance.SerializeTasks())
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
        [Route("api/Task/DeleteTask")]
        [HttpPost]
        public string DeleteTask()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance.DeleteTask(byteArray);

            return "success.";
        }

    }
}
