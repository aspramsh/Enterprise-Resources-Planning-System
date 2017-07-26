using ERPS.DataModel.Entities.Finance;
using ERPS.Finance.Functionals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ERPS.Finance.Controllers
{
    public class GetEmployeeByIdController : ApiController
    {
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
	}
}
