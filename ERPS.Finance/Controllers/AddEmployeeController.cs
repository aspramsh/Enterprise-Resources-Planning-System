﻿using ERPS.DataModel.Entities.Finance;
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
    public class AddEmployeeController : ApiController
    {
        [HttpPost]
        public string UpdateEmployee()
        {
            Task<byte[]> task = this.Request.Content.ReadAsByteArrayAsync();

            task.Wait();

            byte[] byteArray = task.Result;
            Manager.Instance().UpdateEmployee(byteArray);

            return "success.";
        }
    }
}
