using ERPS.DataModel.Entities.Common;
using ERPS.DataModel.Entities.HR;
using ERPS.DataModel.Entities.Management.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.Utils.Functionals
{
    public static class Communicator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"> StatusBase</typeparam>
        /// <typeparam name="K">EntityBase </typeparam>C:\Users\besuc\Documents\test\ERPS.Utils\Functionals\Communicator.cs
        /// <param name="entity"></param>
        /// <param name="url"></param>
        /// <param name="requestType"></param>
        /// <param name="httpMethod"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public  static T SendRequest<T, K>(K entity, string url, string requestType, string contentType) where T : EntityBase, new() where K : EntityBase
        {
            //try
            //{
                BinaryFormatter formatter = new BinaryFormatter();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = requestType;
                request.ContentType = contentType;
                Stream strRequest = request.GetRequestStream();
                MemoryStream memorystream = new MemoryStream();
                formatter.Serialize(memorystream, entity);
                memorystream.Flush();
                memorystream.Position = 0;
                byte[] Bytes = memorystream.ToArray();

                T resp = SendRequest<T>(Bytes, url, requestType);
                return resp;         
           // }
            /*catch (Exception ex)
            {
                EntityBase sb = new EntityBase();
                sb.StatusInfo = "Communication Error: " + ex.ToString();
                return (T)sb;
            } */   
        }

        public static T SendRequest<T>(byte[] bytes, string url, string requestType) where T : EntityBase, new()
        {
            //try
            //{
                // The request should be sent. This is the only point to send the request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = requestType;
                request.ContentType = "application/octet-stream";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);           

                WebResponse response = request.GetResponse();
                Stream str = response.GetResponseStream();
                T entity = new T();
                return entity;
            //}
            //catch (Exception ex)
            /*{
                EntityBase sb = new EntityBase();
                sb.StatusInfo = "Communication Error: " + ex.ToString();
                return (T)sb;
            }*/
        }

        public static T ParseResponse<T>(Stream responseStream) where T : EntityBase
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                responseStream.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                ms.Position = 0;
                T data = (T)bf.Deserialize(ms);
                return data;
            }
        } 
        /// <summary>
        /// Function create httpwebrequest with given parametrers
        /// </summary> 
        /// <typeparam name="T">EntityBase</typeparam>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="entity"></param>
        /// <returns>HTTpWebRequest</returns>
        public static HttpWebRequest PostReques<T>(string url,string method,EntityBase entity)where T:EntityBase
        {
            HttpWebRequest request = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/octet-stream";
                request.Method = method; // To let ASP know that the content is binary
                request.KeepAlive = true;
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream memory = new MemoryStream();
                bf.Serialize(memory, entity);
                memory.Seek(0, SeekOrigin.Begin);
                memory.Position = 0;
                Stream requestStream = request.GetRequestStream();
                BinaryWriter binWriter = new BinaryWriter(requestStream);
                // Write the data to the stream.
                binWriter.Write(memory.ToArray());
            }
            catch(WebException ex)
            {
                Logger.Logger.Addlog(ex.Message+','+ex.Status);
            }
            return request;
        }
        /// <summary>
        /// function  reat response stream
        /// </summary>
        /// <param name="response"></param>
        /// <returns> string </returns>
        public static string GetResponse(WebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                string str = null;
                try
                {
                    var sr = new StreamReader(stream);
                    str = sr.ReadToEnd();
                    return str;
                }
                catch(Exception ex)
                {
                    Logger.Logger.Addlog(ex.Message + ',' + ex.InnerException);
                    return str;
                }
            }
        }
        /// <summary>
        /// create httprespnsemessage with given memorystream
        /// </summary>
        /// <param name="memorystream"></param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage ResponseMessage(MemoryStream memorystream)
        {
            var result = new HttpResponseMessage();
            try
            {
                memorystream.Seek(0, SeekOrigin.Begin);
                memorystream.Position = 0;
                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(memorystream.ToArray())
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch(WebException ex)
            {
                Logger.Logger.Addlog(ex.Message + ',' + ex.Status);
            }
            return result;
        }
        /// <summary>
        /// Create HttpWebRequest with given paramtrers
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns>HttpWebRequest</returns>
        public static HttpWebRequest GetRequest(string url,string method)
        {
            HttpWebRequest req = null;
            try
            {
                req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
            }
            catch (WebException ex)
            { Logger.Logger.Addlog(ex.Message + ',' + ex.Status); }
            return req;

        }
        public static HttpWebResponse CreateResponse(HttpWebRequest request)
        {
            HttpWebResponse response = null;
            try
            {
            response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                Logger.Logger.Addlog(ex.InnerException + " " + ex.Message);
            }
            return response;
        }
        public static List<T> GetEntities<T>(string url) where T : EntityBase
        {
            List<T> deserialized = new List<T>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                BinaryFormatter bfd = new BinaryFormatter();
                deserialized = bfd.Deserialize(memStr) as List<T>;
            }
            return deserialized;
        }
        public static T GetEntitiesList<T>(string url) where T : EntityBase, new()
        {
            T deserialized = new T();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                BinaryFormatter bfd = new BinaryFormatter();
                deserialized = bfd.Deserialize(memStr) as T;
            }
            return deserialized;
        }
        public static byte[] SerializeEntityList<T>(List<T> entities) where T : EntityBase
        {
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, entities);
            memorystream.Flush();
            memorystream.Position = 0;
            byte[] Bytes = memorystream.ToArray();
            return Bytes;
        }
    }
}