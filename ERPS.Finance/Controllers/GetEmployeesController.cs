using ERPS.Finance.Functionals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ERPS.Finance.Controllers
{
    public class GetEmployeesController : ApiController
    {
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
