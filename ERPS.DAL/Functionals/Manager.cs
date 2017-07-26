using ManagementTeam = ERPS.DataModel.Entities.Management;
using ERPS.DataProviders.Interfaces;
using ERPS.DataProviders.Providers;
using ERPS.Utils.Functionals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using ERPS.DataModel.Entities.HR;
using ERPS.DataModel.Entities.Finance;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Data;
using ERPS.DataModel.Entities.Common;
using ERPS.DataModel.Entities.Management.Tasks;
using ERPS.DataModel.Entities.Notification;
using ERPS.DataModel.Entities.Management;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Mvc;

namespace ERPS.DAL.Functionals
{
	public class Manager
	{
		//private static DALManager instance = null;
		//DataProvider.IDataProvider dataprovider = new DataProvider.DummyProvider();
		private IDataProvider _provider = new DummyProvider();
		private IDataProvider _sqlProvider = new SQLProvider();

		#region Singleton
		private static Manager _instance;

		public static Manager Instance()
		{
			if (_instance == null)
			{ _instance = new Manager(); }
			return _instance;
		}

		private Manager() { _provider = new DummyProvider(); }
        #endregion

        #region ManagementTeam
        //public Stream GetSerializedStream()
        //{

        //    BinaryFormatter bf = new BinaryFormatter();

        //    Stream requestStream = new MemoryStream();
        //    {
        //        bf.Serialize(requestStream, new Employee() { Id = 100, Name = "R" });
        //    }
        //    var result = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ByteArrayContent(requestStream.);
        //    };
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    requestStream.Close();
        //    return result;
        //}

        internal HttpResponseMessage ManagementLeaveFeedback(Task<Stream> request)
        {
            request.Wait();
            var bf = new BinaryFormatter();
            var feedback = (Feedback)bf.Deserialize(request.Result);

            var resultEntityBase = this._sqlProvider.ManagementLeaveFeedback(feedback);

            return Utils.Functionals.ManagementStreamHandler.SendEntityBaseResponse(resultEntityBase, bf);
        }

        internal HttpResponseMessage ManagementExecuteSPForRequestedEntity(Task<Stream> request, Func<EntityBase, EntityBase> spMethod)
        {
            request.Wait();
            var bf = new BinaryFormatter();
            var employee = (EntityBase)bf.Deserialize(request.Result);

            var resultBaseEntity = spMethod.Invoke(employee);

            var resultStream = new MemoryStream();
            bf.Serialize(resultStream, resultBaseEntity);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(resultStream.ToArray())
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        internal HttpResponseMessage ManagementUpdateEmployee(Task<Stream> request)
        {
            var taskStream = request;
            taskStream.Wait();
            var bf = new BinaryFormatter();
            var employee = (ManagementTeam.Employee)bf.Deserialize(taskStream.Result);

            // Do some stuff
            // Call SP
            // DB DESKTOP-I5IV7M6\SQLEXPRESS ERPSDatabas
            //System.Diagnostics.Debugger.Launch();
            var resultBaseEntity = _sqlProvider.ManagementUpdateEmployee(employee);

            var resultStream = new MemoryStream();
            //bf.Serialize(resultStream, employee);
            bf.Serialize(resultStream, resultBaseEntity);

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

            // TOREPLACE with this
            //return Utils.Functionals.ManagementStreamHandler.SendEntityBaseResponse(resultEntityBase, bf);
        }

        internal HttpResponseMessage ManagementUpdateEmployeeOld(Task<Stream> request)
		{
			var taskStream = request;
			taskStream.Wait();
			BinaryFormatter bf = new BinaryFormatter();
			var employee = (ManagementTeam.Employee)bf.Deserialize(taskStream.Result);

			// Do some stuff
			// Call SP
			// DB DESKTOP-I5IV7M6\SQLEXPRESS ERPSDatabas
			//System.Diagnostics.Debugger.Launch();
			_sqlProvider.ManagementAddCommonEmployee(employee);

			var resultStream = new MemoryStream();
			bf.Serialize(resultStream, employee);

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

        internal byte[] ManagementGetEntitiesSerialized<T>(Func<ManagementTeam.Collection<T>> getEntities)
            where T : EntityBase, new()
        {
            var bf = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                // Using dapper
                //var employees = this._sqlProvider.GetCollection<Employee>("dbo.umsp_GetEmployees");
                // Using ADO.NET
                //var employees = this._sqlProvider.ManagementGetListEntities<DataModel.Entities.Common.Employee>("dbo.umsp_GetEmployees");
                var employees = getEntities.Invoke();
                //var employeesOld = this._sqlProvider.ManagementGetEmployees<T>(storedProcedure);
                //System.Diagnostics.Debugger.Launch();
                bf.Serialize(stream, employees);
                stream.Seek(0, SeekOrigin.Begin);
                stream.Position = 0;
                return stream.ToArray();
            }
        }

		//public Stream GetSerializedObject()
		internal byte[] GetSerializedObject()
		{
			var stream = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stream, GetEmployee2());
			//byte[] data = stream.ToArray();
			stream.Close();
			//return data;
			return stream.ToArray();
		}

		public ManagementTeam.Employee GetEmployee() => new ManagementTeam.Employee() { Id = 5, /*Age = 32,*/ Name = "Unemployeed" };


		public string GetEntity(int entity)
		{
			return this._provider.ManagementGetData();
		}

		private ManagementTeam.Employee GetEmployee2()
		{
			return new ManagementTeam.Employee() { Id = 5, /*Age = 32,*/ Name = "Unemployeed" };
		}
		public string DeleteEmployeeManagement(byte[] bytes)
		{
			MemoryStream stream = new MemoryStream(bytes);
			ManagementTeam.EmployeeForGetAndDelete employee =
				Communicator.ParseResponse<ManagementTeam.EmployeeForGetAndDelete>(stream);
			_sqlProvider.DeleteEmployeeManagement(employee);
			return "Deleted.";
		}
        #endregion
        #region HRTeam
        /// <summary>
        /// function call GetHREmployee from sqlprovider 
        /// serialized and returned HREntities object
        /// </summary>
        /// <returns></returns>
        public byte[] HRDALManagerGetEmployee()
		{
		
			HREntities employees = new HREntities();
			employees = _sqlProvider.GetHREmployee();
            return Utils.Functionals.Formatter.GetBinary<HREntities>(employees);

		}
        /// <summary>
        /// function received HREntityId entity ,deserialized it 
        /// call GetHREmployeeByid function from sqlprovider
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns> serialized HREntity object</returns>
		public byte[] HRGetEmployeeById(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            BinaryFormatter bf = new BinaryFormatter();
            HREntityID HRId = (HREntityID)bf.Deserialize(stream);	
            HREntity employee = new HREntity();
            employee = _sqlProvider.GetHREmployeeById(HRId.Id);
            return Utils.Functionals.Formatter.GetBinary<HREntity>(employee);

		}

		#endregion
		#region FinanceTeam
		public byte[] GetEmployeesFinance()
		{
			EmployeesFinance employees = new EmployeesFinance();
			employees = this._sqlProvider.GetEmployeeFinance();
			MemoryStream memorystream = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(memorystream, employees);
			memorystream.Flush();
			memorystream.Position = 0;
			byte[] Bytes = memorystream.ToArray();
			return Bytes;
		}

		public string UpdateEmployeeFinance(byte[] bytes)
		{
			MemoryStream stream = new MemoryStream(bytes);
			EmployeeFinance employee = Communicator.ParseResponse<EmployeeFinance>(stream);
			_sqlProvider.AddEmployeeFinance(employee);
			return "Added.";
		}

		public string DeleteEmployeeFinance(byte[] bytes)
		{
			MemoryStream stream = new MemoryStream(bytes);
			EmployeeFinanceForGetAndDelete employee =
				Communicator.ParseResponse<EmployeeFinanceForGetAndDelete>(stream);
			_sqlProvider.DeleteEmployeeFinance(employee);
			return "Deleted.";
		}
		public byte[] GetEmployeeFinanceById(byte[] bytes)
		{
			MemoryStream stream = new MemoryStream(bytes);
			EmployeeFinanceForGetAndDelete employee =
			Communicator.ParseResponse<EmployeeFinanceForGetAndDelete>(stream);
			EmployeeFinance Employee = new EmployeeFinance();
			Employee = this._sqlProvider.GetEmployeeFinanceById(employee);
			MemoryStream memorystream = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(memorystream, Employee);
			memorystream.Flush();
			memorystream.Position = 0;
			byte[] Bytes = memorystream.ToArray();
			return Bytes;
		}
		#endregion
		#region Notification
		public byte[] GetNotifications()
		{
			Notifications notifications = new Notifications();
			notifications = _sqlProvider.GetNotifications();
			if (notifications == null)
				return null;
			BinaryFormatter bf = new BinaryFormatter();
			using (MemoryStream ms = new MemoryStream())
			{
				bf.Serialize(ms, notifications);
				return ms.ToArray();
			}
		}
        #endregion
        #region Task
        public string AddTask(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            ManagementTeam.Task task = Communicator.ParseResponse<ManagementTeam.Task>(stream);
            _sqlProvider.AddTask(task);
            return "Added.";
        }
        public byte[] GetProjects()
        {
            List<Project> projects = new List<Project>();
            projects = this._sqlProvider.GetProjects();
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, projects);
            memorystream.Flush();
            memorystream.Position = 0;
            byte[] Bytes = memorystream.ToArray();
            return Bytes;
        }
        public byte[] GetStates()
        {
            List<TaskState> states = new List<TaskState>();
            states = this._sqlProvider.GetStates();
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, states);
            memorystream.Flush();
            memorystream.Position = 0;
            byte[] Bytes = memorystream.ToArray();
            return Bytes;
        }
        public byte[] GetEmployees()
        {
            List<DataModel.Entities.Common.Employee> employees = new List<DataModel.Entities.Common.Employee>();
            employees = this._sqlProvider.GetEmployees();
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, employees);
            memorystream.Flush();
            memorystream.Position = 0;
            byte[] Bytes = memorystream.ToArray();
            return Bytes;
        }
        public byte[] GetSeverities()
        {
            List<Severity> s = new List<Severity>();
            s = this._sqlProvider.GetSeverities();
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, s);
            memorystream.Flush();
            memorystream.Position = 0;
            byte[] Bytes = memorystream.ToArray();
            return Bytes;
        }
        public string UploadAttachment(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            Attachment attachment = Communicator.ParseResponse<Attachment>(stream);
            System.IO.FileStream file = System.IO.File.Create(HttpContext.Current.Server.MapPath("~/Attachments/" + attachment.FileName + ".jpg "));
            file.Write(attachment.File, 0, attachment.File.Length);
            file.Close();
            /*HttpPostedFileBase objFile = (HttpPostedFileBase)new MemoryPostedFile(attachment.File);
            Path.GetFileName(objFile.FileName);
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Attachments/"), attachment.FileName + ".Jpg");
            objFile.SaveAs(path);*/
            _sqlProvider.AddAttachmentId(attachment);
            return "Added.";
        }
        public byte[] DownloadAttachment(int Id)
        {
            MemoryStream memoryStream = new MemoryStream();
            byte[] fileContents;
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Attachments/"), Id + ".Jpg");
            fileContents = File.ReadAllBytes(path);
            return fileContents;
        }
        public byte[] GetTasks()
        {
            TasksCollection tasks = new TasksCollection();
            SQLProvider sqlp = new SQLProvider();
            tasks = sqlp.GetAllTasks();
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, tasks);
            memorystream.Flush();
            memorystream.Position = 0;
            byte[] Bytes = memorystream.ToArray();
            return Bytes;
        }
        public string DeleteTask(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            TaskForDelete task =
                Communicator.ParseResponse<TaskForDelete>(stream);
            _sqlProvider.DeleteTask(task);
            return "Deleted.";
        }
        #endregion
    }
    
}