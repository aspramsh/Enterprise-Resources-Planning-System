using ERPS.Utils.Functionals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using ManagementTeam = ERPS.DataModel.Entities.Management;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;
using ERPS.DataModel.Entities.Common;
using ERPS.DataModel.Entities.Management.Tasks;
using ERPS.DataModel.Entities.Management;

namespace ERPS.Management.Functionals
{
    public class Manager /*: Singleton, ISingleton1*/
    {
        #region Singleton
        private static Manager _instance;

        public static Manager Instance
        {
            get
            {
                return _instance == null ? _instance = new Manager() : _instance;
            }
        }

        private Manager()/*:base((ISingleton1)this)*/ { }
        #endregion
        //public Manager () { }
        internal string Method()
        {
            // HttpWebRequest request = new HttpWebRequest(); 
            string commandUrl = "http://localhost:53865/api/dal/GetDataFromDb";

            // Initialising a request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(commandUrl);
            request.Method = "GET";

            //GetResponse returns webResponse
            WebResponse response = request.GetResponse();


            using (Stream stream = response.GetResponseStream())
            {
                //var memStream = new MemoryStream();
                var sr = new StreamReader(stream);
                // Reads all the stream to the end
                string str = sr.ReadToEnd();

                //Console.WriteLine(str);
                return str;
            }
        }

        internal ManagementTeam.Collection<T> GetEntities<T>(Task<Stream> request, string requestUrl)
            where T : EntityBase, new()
        {
            //BinaryFormatter bf;
            // Getting collection of employees from request stream
            ManagementTeam.Collection</*ManagementTeam.*/T> entities;
            // This isn't needed, as the stream is empty
            //GetDataFromRequest<ManagementTeam.Collection<ManagementTeam.Employee>>(request, out bf, out employees);
            // Server sending request to: "http://localhost:53865/api/DAL/ManagementGetEmployees"
            //return Utils.Functionals.ManagementStreamHandler.SendEntityGetResponseFromServer(bf, employees, "http://localhost:53865/api/DAL/ManagementGetEmployees", "GET");
            //System.Diagnostics.Debugger.Launch();
            var response = Utils.Functionals.ManagementStreamHandler.SendRequest(requestUrl, "GET");
            //System.Diagnostics.Debugger.Launch();
            entities = Utils.Functionals.ManagementStreamHandler.RecieveObject<ManagementTeam.Collection</*Employee*/T>>(response);
            return entities;
        }

        // This went to UTILS prj
        //private static HttpResponseMessage SendEntityGetResponseFromServer<T>(BinaryFormatter bf, T entityBase, string commandUrl, string requestMethod)
        //    where T : EntityBase
        //{
        //    var resultStream = new MemoryStream();
        //    bf.Serialize(resultStream, entityBase);


        //    ERPS.Utils.Functionals.ManagementStreamHandler.SendObject(entityBase, commandUrl, requestMethod);
        //    var result = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ByteArrayContent(resultStream.ToArray())
        //    };
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    return result;
        //}
        internal HttpResponseMessage LeaveFeedback(Task<Stream> request, string urlToSendRequest)
        {
            BinaryFormatter bf;
            Feedback feedback;
            Utils.Functionals.ManagementStreamHandler.GetDataFromRequest(request, out bf, out feedback);

            var resultStream = new MemoryStream();
            bf.Serialize(resultStream, feedback);

            var response = Utils.Functionals.ManagementStreamHandler.SendObject(feedback, urlToSendRequest, "POST");
            var entityBase = Utils.Functionals.ManagementStreamHandler.RecieveObject<EntityBase>(response);

            return Utils.Functionals.ManagementStreamHandler.SendEntityBaseResponse(entityBase, bf);
        }

        internal HttpResponseMessage GetEmployeeFeedbacks(Task<Stream> request, string urlToSendRequest)
        {
            BinaryFormatter bf;
            EntityBase employee;
            Utils.Functionals.ManagementStreamHandler.GetDataFromRequest(request, out bf, out employee);

            var resultStream = new MemoryStream();
            bf.Serialize(resultStream, employee);

            var response = Utils.Functionals.ManagementStreamHandler.SendObject(employee, urlToSendRequest, "POST");
            var employeeFeedbacks = Utils.Functionals.ManagementStreamHandler.RecieveObject<ManagementTeam.Collection<Feedback>>(response);

            return Utils.Functionals.ManagementStreamHandler.SendEntityBaseResponse(employeeFeedbacks, bf);
        }

        internal HttpResponseMessage UpdateEntity<T>(Task<Stream> request, string urlToSendRequest)
            where T : EntityBase
        {
            BinaryFormatter bf;
            T entity;
            Utils.Functionals.ManagementStreamHandler.GetDataFromRequest(request, out bf, out entity);

            var resultStream = new MemoryStream();
            bf.Serialize(resultStream, entity);

            var response = Utils.Functionals.ManagementStreamHandler.SendObject<T>(entity, urlToSendRequest, "POST");
            var entityBase = Utils.Functionals.ManagementStreamHandler.RecieveObject<EntityBase>(response);
            //var result = new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new ByteArrayContent(resultStream.ToArray())
            //};

            // This should be replaced
            //var newResultStream = new MemoryStream();
            //bf.Serialize(newResultStream, entityBase);
            //var result = new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new ByteArrayContent(newResultStream.ToArray())
            //};
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            //return result;
            // With this
            return Utils.Functionals.ManagementStreamHandler.SendEntityBaseResponse(entityBase, bf);
        }

        internal HttpResponseMessage UpdateEmployee(Task<Stream> request)
        {
            //Stream stream = Request.Content.readtoend
            //Task<Stream> taskStream = Request.Content.ReadAsStreamAsync();

            // Instead of this 
            //var taskStream = request;
            //taskStream.Wait();
            //BinaryFormatter bf = new BinaryFormatter();
            //var employee = (Employee)bf.Deserialize(taskStream.Result);

            // This
            BinaryFormatter bf;
            ManagementTeam.Employee employee;
            GetDataFromRequest<ManagementTeam.Employee>(request, out bf, out employee);

            // Do some stuff

            var resultStream = new MemoryStream();
            bf.Serialize(resultStream, employee);
            //System.Diagnostics.Debugger.Launch();
            // Server sending request to: "http://localhost:53865/api/DAL/ManagementUpdateEmployeeOld"
            /* ConfigurationManager.ConnectionStrings["UpdateEmployee"].ConnectionString*/
            // Internal server error here
            ERPS.Utils.Functionals.ManagementStreamHandler.SendObject<ManagementTeam.Employee>(employee, "http://localhost:53865/api/DAL/ManagementUpdateEmployeeOld", "POST");
            //System.Diagnostics.Debugger.Launch();
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                //Content = new StreamContent(Functionals.Manager.Instance().GetSerializedObject())
                Content = new ByteArrayContent(resultStream.ToArray())
            };
            //result.Content = new StreamContent(Functionals.Manager.Instance().GetSerializedObject());
            //System.Diagnostics.Debugger.Launch();
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //return Manager.Instance.GetDataAbout(employee);
            return result;
        }

        internal ManagementTeam.Employee GetDataAbout(ManagementTeam.Employee employee)
        {
            // Server url
            string commandUrl = "http://localhost:53865/api/dal/GetDataFromDb";
            // Initializing request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(commandUrl);
            request.Method = WebRequestMethods.Http.Post;
            // Wasn't working, added: 
            request.ContentType = "application/octet-stream";
            // Binary serialization
            BinaryFormatter bf = new BinaryFormatter();
            //bf.Serialize(request.GetRequestStream(), employee);
            //request.GetRequestStream();
            using (Stream requestStream = request.GetRequestStream())
            {
                bf.Serialize(requestStream, employee);
            }
            // Sending request to the server
            WebResponse response = request.GetResponse();
            // Getting response from the server
            //using (Stream responseStream = response.GetResponseStream())
            {
                Stream responseStream = response.GetResponseStream();

                var memoryStream = new MemoryStream();
                responseStream.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.Position = 0;
                // Desirializing response-stream to expected object type
                //Employee empl = (Employee)bf.Deserialize(responseStream);
                ManagementTeam.Employee empl = (ManagementTeam.Employee)bf.Deserialize(memoryStream);

                responseStream.Close();
                memoryStream.Close();
                return empl;
            }

            //{
            //    string url = textBox2.Text;
            //    //string commandUrl = url + "name=" + textBox1.Text + "&age=" + textBox3.Text;
            //    string commandUrl = "http://localhost:53865/api/dal/GetDataFromDb";

            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(commandUrl);
            //    request.ContentLength = 0;
            //    request.Method = "POST";
            //    WebResponse response = request.GetResponse();
            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        var sr = new StreamReader(stream);
            //        var str = sr.ReadToEnd();
            //        richTextBox1.Text = str;
            //    }
            //}
        }

        private static void GetDataFromRequest<T>(Task<Stream> request, out BinaryFormatter binaryFormatter, out T entityBase)
            //where T : EntityBase
        {
            var taskStream = request;
            taskStream.Wait();
            binaryFormatter = new BinaryFormatter();
            entityBase = (T)binaryFormatter.Deserialize(taskStream.Result);
        }

		//private static Employee GetEmployeeFromSerializer(Employee employee)
		//{
		//    BinaryFormatter formatter;
		//    Stream stream;
		//    GetSerializedEmployee(employee, out formatter, out stream);

		//    stream.Seek(0, SeekOrigin.Begin);
		//    stream.Position = 0;

		//    //Deserialize
		//    return (Employee)formatter.Deserialize(stream);
		//}

		//private static void GetSerializedEmployee(Employee employee, out BinaryFormatter formatter, out Stream stream)
		//{
		//    //Binary
		//    formatter = new BinaryFormatter();
		//    stream = new MemoryStream();
		//    //Serialize
		//    formatter.Serialize(stream, employee);
		//}
		public string DeleteEmployee(byte[] bytes)
		{
			string url = "http://localhost:53865/api/DAL/DeleteEmployeeManagement";
			ManagementTeam.EmployeeForGetAndDelete deserialized = Communicator.SendRequest<ManagementTeam.EmployeeForGetAndDelete>
				(bytes, url, "POST");
			return "Success";
		}


        #region Task
        public string AddTask(byte[] bytes)
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerUrl"].ConnectionString;
            string url = baseUrl + DestinationNames.AddTask;
            ManagementTeam.Task deserialized = Communicator.SendRequest<ManagementTeam.Task>
                (bytes, url, "POST");
            return "Success";
        }
        public List<Project> GetProjects()
        {
            List<Project> deserialized = new List<Project>();
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerUrl"].ConnectionString;
            string url = baseUrl + DestinationNames.GetProjects;
            deserialized = Communicator.GetEntities<Project>(url);
            return deserialized;
        }
        public List<TaskState> GetStates()
        {
            List<TaskState> deserialized = new List<TaskState>();
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerUrl"].ConnectionString;
            string url = baseUrl + DestinationNames.GetStates;
            deserialized = Communicator.GetEntities<TaskState>(url);
            return deserialized;
        }
        public byte[] SerializeSeverities()
        {
            List<Severity> s = this.GetSeverities();
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, s);
            memorystream.Flush();
            memorystream.Position = 0;
            byte[] Bytes = memorystream.ToArray();
            return Bytes;
        }
        public List<Severity> GetSeverities()
        {
            List<Severity> deserialized = new List<Severity>();
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerUrl"].ConnectionString;
            string url = baseUrl + DestinationNames.GetSeverities;
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
                deserialized = bfd.Deserialize(memStr) as List<Severity>;
            }

            return deserialized;
        }
        public string UploadAttachments(byte[] bytes)
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerUrl"].ConnectionString;
            string url = baseUrl + DestinationNames.UploadAttachment;
            Attachment deserialized = Communicator.SendRequest<Attachment>
                (bytes, url, "POST");
            return "Success";
        }
        public byte[] DownloadAttachments(int Id)
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerUrl"].ConnectionString;
            string url = baseUrl + DestinationNames.DownloadAttachment + Id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                return memStr.ToArray();
            }
        }
        public List<DataModel.Entities.Common.Employee> GetEmployees()
        {
            List<DataModel.Entities.Common.Employee> deserialized = new List<DataModel.Entities.Common.Employee>();
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerUrl"].ConnectionString;
            string url = baseUrl + DestinationNames.GetTaskEmployees;
            deserialized = Communicator.GetEntities<DataModel.Entities.Common.Employee>(url);
            return deserialized;
        }
        public byte[] SerializeTasks()
        {
            TasksCollection tasks = this.GetTasks();
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, tasks);
            memorystream.Flush();
            memorystream.Position = 0;
            byte[] Bytes = memorystream.ToArray();
            return Bytes;
        }
        public TasksCollection GetTasks()
        {
            TasksCollection tasks = new TasksCollection();
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerUrl"].ConnectionString;
            string url = baseUrl + DestinationNames.GetTasks;
            tasks = Communicator.GetEntitiesList<TasksCollection>(url);
            return tasks;
        }
        public string DeleteTask(byte[] bytes)
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerUrl"].ConnectionString;
            string url = baseUrl + DestinationNames.DeleteTask;
            TaskForDelete deserialized = Communicator.SendRequest<TaskForDelete>
                (bytes, url, "POST");
            return "Success";
        }
        #endregion
    }
}