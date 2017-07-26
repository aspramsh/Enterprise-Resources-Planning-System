using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.DynamicData;
using System.Web.Http;
using ERPS.DataModel.Entities.HR;
using System.Xml.Serialization;
using System.Configuration;

namespace ERPS.HR.Functionals
{
    public class HRManager
    {
        private static HRManager instance = null;
        /// <summary>
        //static constructor 
        /// </summary>
        /// <returns> instance if it exists or
        /// create new and returned </returns>
        public static HRManager Instance()
        {
            if (null == instance)
            {
                instance = new HRManager();
            }
            return instance;
        }
        string baseUrl = ConfigurationManager.ConnectionStrings["DalServerName"].ConnectionString;
        public string PostEmployee(HREntities employees)
        {
          
            HttpWebRequest request = Utils.Functionals.Communicator.PostReques<HREntities>(baseUrl+HRDestinations.AddDalemployee, "POST", employees);
            HttpWebResponse response = Utils.Functionals.Communicator.CreateResponse(request);
            return Utils.Functionals.Communicator.GetResponse(response);
        }
        /// <summary>
        /// Function create HTTpRequest and call GEtDalEmployees 
        /// function 
        /// </summary>
        /// <returns>serialized HREntitiis model</returns>
           public byte[] HRManagerGetEmployees()
        {
           
            HttpWebRequest req = Utils.Functionals.Communicator.GetRequest(baseUrl+HRDestinations.GetDalEmployee, "GET");
           HttpWebResponse resp = Utils.Functionals.Communicator.CreateResponse(req);
            HREntities employees = Utils.Functionals.Communicator.ParseResponse<HREntities>(resp.GetResponseStream());
            return Utils.Functionals.Formatter.GetBinary<HREntities>(employees);

        }
   public Candidate HRCandidate()
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["ExternalServerName"].ConnectionString;
            HttpWebRequest req = Utils.Functionals.Communicator.GetRequest(baseUrl+HRDestinations.GetDalCandidate, "GET");
            HttpWebResponse resp = Utils.Functionals.Communicator.CreateResponse(req);
            using (Stream stream = resp.GetResponseStream())
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Candidate));
                Candidate candidate = (Candidate)formatter.Deserialize(stream);
                var sr = new StreamReader(stream);
                return candidate;
            }
        }
        /// <summary>
        /// Create HTTpRequest for send id to GEtHREmployeeByid function
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>serialized HRMployee datamodel</returns>
        public byte[] GetHREmployeeById(byte[] bytes)
        {
            HREntity hrentity = null;
            try
            {
             
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl+HRDestinations.GetDalEmployeeId);
                request.Method = "POST";
                request.ContentType = "application/octet-stream";
                using (Stream strRequest = request.GetRequestStream())
                {
                    strRequest.Write(bytes, 0, bytes.Length);
                }
                HttpWebResponse response = Utils.Functionals.Communicator.CreateResponse(request);
                hrentity = Utils.Functionals.Communicator.ParseResponse<HREntity>(response.GetResponseStream());
            }
            catch(WebException ex)
            {
                Logger.Logger.Addlog(ex.InnerException.ToString() + " " + ex.Message.ToString());
            }
            return Utils.Functionals.Formatter.GetBinary<HREntity>(hrentity);
        }

        /// <summary>
        /// Function post HrEntityId model,
        /// </summary>
        /// <param name="HRid"></param>
        /// <returns> message about result</returns>
        public string DeleteEmployee(HREntityID  HRid)
        {
            HttpWebRequest request = Utils.Functionals.Communicator.PostReques<HREntityID>(baseUrl+HRDestinations.DeleteDalEmployee, "POST", HRid);
            HttpWebResponse response = Utils.Functionals.Communicator.CreateResponse(request);
            return Utils.Functionals.Communicator.GetResponse(response);

        }
    }
}