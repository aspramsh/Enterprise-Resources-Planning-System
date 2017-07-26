using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ERPS.Utils.Functionals
{
    public class RouteHandler
    {
        public static string GetUrlBase(HttpRequestBase request, UrlHelper url)
        {
            return string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url.Content("~"));
        }
    }
}
