using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERPS.DataModel.Entities.Management.Tasks
{
    [Serializable]
    public class Attachment : EntityBase
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
    }
}
