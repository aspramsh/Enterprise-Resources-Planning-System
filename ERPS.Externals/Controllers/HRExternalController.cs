using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ERPS.Externals.Controllers
{
    public class HRExternalController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage SendCandidate()
        { 
            var ms = new MemoryStream(ERPS.Externals.EXternalManager.ExternalManager.Instance().GetCandidate());
            return Utils.Functionals.Communicator.ResponseMessage(ms);
        }
    }
}
