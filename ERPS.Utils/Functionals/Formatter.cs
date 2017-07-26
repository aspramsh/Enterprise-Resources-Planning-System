using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.Utils.Functionals
{
    public static class Formatter
    {
        public static byte[] GetBinary<T>(T entity) where T : EntityBase
        {

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, entity);
            ms.Seek(0, SeekOrigin.Begin);
            ms.Position = 0;
            return ms.ToArray();
        }
    }
}
