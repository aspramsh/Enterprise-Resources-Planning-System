using ERPS.DataModel.Entities.Management;
using ERPS.Management.Functionals;
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
    public class TestWAPIManagerController : ApiController
    {
        //[HttpGet]
        //public void GetMethod()
        //{
        //    return;
        //}

        [HttpGet]
        [Route("api/TestWAPIManager/GetTestWAPI")]
        public string GetTestWAPI(int id)
        {
            //Manager manager = new Manager();
            //manager.Instance<Manager>().Method();
            return Manager.Instance.Method();
            //return id;
        }

        //public HttpResponseMessage GetEmployeeFinance()
        //{
        //    var result = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ByteArrayContent(FinanceManager.Instance().SerializeEmployeeFinance())
        //    };
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    return result;
        //}

        [HttpPost]
        [Route("api/TestWAPIManager/GetTestEmployee")]
        //public Employee GetTestEmployee()
        public HttpResponseMessage GetTestEmployee()
        {
            //Stream stream = Request.Content.readtoend
            Task<Stream> taskStream = Request.Content.ReadAsStreamAsync();
            taskStream.Wait();
            BinaryFormatter bf = new BinaryFormatter();
            var employee = (Employee)bf.Deserialize(taskStream.Result);
            var resultStream = new MemoryStream();
            bf.Serialize(resultStream, employee);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                //Content = new StreamContent(Functionals.Manager.Instance().GetSerializedObject())
                Content = new ByteArrayContent(resultStream.ToArray())
            };
            //result.Content = new StreamContent(Functionals.Manager.Instance().GetSerializedObject());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //return Manager.Instance.GetDataAbout(employee);
            return result;
        }
    }
}
