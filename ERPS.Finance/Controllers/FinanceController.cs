using ERPS.Finance.Functionals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace ERPS.Finance.Controllers
{
    public class FinanceController : ApiController
    {
        [Route("api/Finance/GetEmployeeById")]
        [HttpPost]
        public HttpResponseMessage GetEmployeeById()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            byte[] Bytes = Manager.Instance().GetEmployeeById(byteArray);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Bytes)
            };
            return result;
        }
        [Route("api/Finance/UpdateEmployee")]
        [HttpPost]
        public string UpdateEmployee()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().UpdateEmployee(byteArray);

            return "success.";
        }
        [Route("api/Finance/DeleteEmployee")]
        [HttpPost]
        public string DeleteEmployee()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().DeleteEmployee(byteArray);

            return "success.";
        }

         [HttpGet]
        public HttpResponseMessage GetAllEmployees()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Manager.Instance().SerializeEmployeeFinance())
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }
}
