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

namespace ERPS.Management.Controllers
{
    public class AddEmployeeController : ApiController
    {
        /// <summary>
        /// Gets an employee from stream, sends request to DAL
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AddEmployee/AddEmployeeToTeam")]
        public HttpResponseMessage AddEmployeeToTeam()
        {
            //System.Diagnostics.Debugger.Launch();
            return ERPS.Management.Functionals.Manager.Instance.AddEmployeeToTeam(Request.Content.ReadAsStreamAsync());
        }
    }
}
