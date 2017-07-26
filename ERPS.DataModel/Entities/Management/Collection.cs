using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.Management
{
    //[Serializable]
    //public class Collection<T> : EntityBase
    //    where T : ICollection<EntityBase>, new()
    //{
    //    //public IEnumerable<T> { get; set; }
    //    public T Entities { get; set; } = new T();
    //}
    [Serializable]
    public class Collection<T> : EntityBase
      where T : EntityBase
    {
        public ICollection<T> Entities { get; set; }
    }
}
