using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ERPS.DataModel.Entities.HR;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ERPS.HR.Controllers
{

    public class HRController : ApiController
    {
        /// <summary>
        /// function for Get all employees from HrEmployee table 
        /// called HRMAnagerGEtEmployees function from HRmanager
        /// </summary>
        /// <returns>HTTPResponseMEssage</returns>
        [Route("api/HR/GetEmployeesHR")]
        [HttpGet]
        public HttpResponseMessage GetEmployeesHR()
        {
            var ms = new MemoryStream(ERPS.HR.Functionals.HRManager.Instance().HRManagerGetEmployees());
            return Utils.Functionals.Communicator.ResponseMessage(ms);
        }
        /// <summary>
        /// function for Get all candidates from External Web Api
        /// 
        /// </summary>
        /// <returns>HTTPREsponseMEssage</returns>
        [Route("api/HR/GetCandidateList")]
        [HttpGet]
        public HttpResponseMessage GetCandidateList()
        {
            var result = new HttpResponseMessage();
            try
            {
                Candidate candidate = new Candidate();
                candidate = ERPS.HR.Functionals.HRManager.Instance().HRCandidate();
                XmlSerializer XmlFormatter = new XmlSerializer(typeof(Candidate));
                var ms = new MemoryStream();
                XmlFormatter.Serialize(ms, candidate);
               result= Utils.Functionals.Communicator.ResponseMessage(ms);
                return result;
            }catch(Exception ex)
            {
                Logger.Logger.Addlog(ex.InnerException + " " + ex.Message);
                return result;
            }
        }


        /// <summary>
        /// Function create HTTpREquest and call AddDAlEmployee function
        /// 
        /// </summary>
        /// <returns>serialized HREmployee entity</returns>
        [Route("api/HR/HRPost")]
        [HttpPost]
        public string HRPost()
        {
            Stream EntityStream =null;
            try
            {
                Task<Stream> task = Request.Content.ReadAsStreamAsync();
                task.Wait();
                EntityStream = task.Result;
            }
            catch(Exception ex)
            {
                Logger.Logger.Addlog(ex.InnerException + ", " + ex.Message);
            }
            HREntities employees = Utils.Functionals.Communicator.ParseResponse<HREntities>(EntityStream);
            return ERPS.HR.Functionals.HRManager.Instance().PostEmployee(employees);
        }
        /// <summary>
        /// function create HTTPREquest call DeleteEmployees function 
        /// 
        /// </summary>
        /// <returns>message about result</returns>
        [Route("api/HR/HRDeleteEmployee")]
        public string HRDeleteEmployee()
        {
            Stream EntityStream = null;
            try
            {
                Task<Stream> task = Request.Content.ReadAsStreamAsync();
                task.Wait();
                EntityStream = task.Result;
            }
            catch (Exception ex)
            {
                Logger.Logger.Addlog(ex.InnerException + " " + ex.Message);
            }
            HREntityID HRid = Utils.Functionals.Communicator.ParseResponse<HREntityID>(EntityStream);
            return ERPS.HR.Functionals.HRManager.Instance().DeleteEmployee(HRid);
        }
        /// <summary>
        /// Called GEtHREmployeeByid function from HRManager 
        /// </summary>
        /// <returns>HTTPREsponsemessage</returns>
        [Route("api/HR/GetEmployeeHRById")]
        [HttpPost]
        public HttpResponseMessage GetEmployeeHRById()
        {
            var result = new HttpResponseMessage();
            try
            {
                Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();
                task.Wait();
                byte[] byteArray = task.Result;
                byte[] Bytes = ERPS.HR.Functionals.HRManager.Instance().GetHREmployeeById(byteArray);
                 result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(Bytes)
                };
                return result;
            }catch(WebException ex)
            {
                Logger.Logger.Addlog(ex.InnerException + " " + ex.Message);
              return result;
            }
        }
    }

}
