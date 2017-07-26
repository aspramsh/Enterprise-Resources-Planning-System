using ERPS.DataModel.Entities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ERPS.DataModel.Entities.Management;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ERPS.Utils.Functionals
{
    public class ManagementStreamHandler
    {
        public static void GetDataFromRequest<T>(Task<Stream> request, out BinaryFormatter binaryFormatter, out T entityBase)
            where T : EntityBase
        {
            var taskStream = request;
            taskStream.Wait();
            binaryFormatter = new BinaryFormatter();
            entityBase = (T)binaryFormatter.Deserialize(taskStream.Result);
        }

        public static WebResponse SendObject<T>(T @object, string commandUrl, string requestMethod)
            where T : EntityBase //ISerializable
        {
            // Server url
            //string commandUrl = "http://localhost:53865/api/dal/GetDataFromDb";
            // Initializing request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(commandUrl);
            //request.Method = WebRequestMethods.Http.Post;
            request.Method = requestMethod;
            // Wasn't working, added: 
            request.ContentType = "application/octet-stream";
            // Binary serialization
            var bf = new BinaryFormatter();
            //bf.Serialize(request.GetRequestStream(), employee);
            //request.GetRequestStream();
            using (Stream requestStream = request.GetRequestStream())
            {
                bf.Serialize(requestStream, @object);
            }
            //System.Diagnostics.Debugger.Launch();
            // Sending request to the server
            WebResponse response = request.GetResponse();
            return response;
        }

        public static T RecieveObject<T>(WebResponse response)
            where T : EntityBase //ISerializable
        {
            BinaryFormatter bf = new BinaryFormatter();
            // Getting response from the server
            using (Stream responseStream = response.GetResponseStream())
            {
                var ms = new MemoryStream();
                responseStream.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                ms.Position = 0;

                // Desirializing response-stream to expected object type
                //T empl = (T)bf.Deserialize(responseStream);
                T empl = (T)bf.Deserialize(ms);

                return empl;
            }
        }

        public static byte[] SerializeToByteArray(EntityBase entity)
        {
            var bf = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                bf.Serialize(stream, entity);
                stream.Seek(0, SeekOrigin.Begin);
                stream.Position = 0;
                return stream.ToArray();
            }
        }

        public static HttpResponseMessage SendEntityGetResponseFromServer<T>(BinaryFormatter bf, T entityBase, string commandUrl, string requestMethod)
          where T : EntityBase
        {
            var resultStream = new MemoryStream();
            bf.Serialize(resultStream, entityBase);


            ERPS.Utils.Functionals.ManagementStreamHandler.SendObject(entityBase, commandUrl, requestMethod);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(resultStream.ToArray())
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        public static HttpResponseMessage SendEntityBaseResponse<T>(T entityBase, BinaryFormatter bf)
            where T : EntityBase
        {
            var newResultStream = new MemoryStream();
            bf.Serialize(newResultStream, entityBase);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(newResultStream.ToArray())
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        //public static List<T> RecieveResponce<T>(WebResponse response, Task<Stream> request)
        //    where T : EntityBase
        //{
        //       var taskStream = request;
        //    taskStream.Wait();
        //    BinaryFormatter bf = new BinaryFormatter();
        //    var employee = (T)bf.Deserialize(taskStream.Result);

        //    // Getting response from the server
        //    using (Stream responseStream = response.GetResponseStream())
        //    {
        //        // Desirializing response-stream to expected object type
        //        T empl = (T)bf.Deserialize(responseStream);
        //        return empl;
        //    }

        //    var set = new List<T>();
        //    try
        //    {
        //        var content = StringToBytes(s);
        //        var formatter = new BinaryFormatter();
        //        using (var ms = new MemoryStream(content))
        //        {
        //            using (var ds = new DeflateStream(ms, CompressionMode.Decompress, true))
        //            {
        //                set = (DataSet)formatter.Deserialize(ds);
        //            }
        //        }
        //    }
        //}

        public static WebResponse SendRequest(string commandUrl, string requestMethod)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(commandUrl);
            request.Method = requestMethod;
            request.ContentType = "application/octet-stream";
            // Getting WebResponse
            return request.GetResponse();
        }
    }
}
