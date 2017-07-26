using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Common
{
    [Serializable]
    public class EntityBase
    {
        public int Id { get; set; }

        public string StatusInfo { get; set; }
    }
}
