using ERPS.DataModel.Entities.Finance;
using ERPS.Utils.Functionals;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace ERPS.Finance.Functionals
{
    public class Manager
    {
        public static Manager instance { get; }

        public static Manager Instance()
        {
            return instance != null ? instance : new Manager();
        }
        
        public EmployeesFinance GetEmployeesFinance()
        {
            EmployeesFinance deserialized = new EmployeesFinance();
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetEmployees;
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
                deserialized = bfd.Deserialize(memStr) as EmployeesFinance;
            }
            return deserialized;
        }
        
        public byte[] SerializeEmployeeFinance()
        {
            EmployeesFinance employees = this.GetEmployeesFinance();
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, employees);
            memorystream.Flush();
            memorystream.Position = 0;
            byte[] Bytes = memorystream.ToArray();
            return Bytes;
        }
        
        public string UpdateEmployee(byte[] bytes)
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.UpdateEmployee;
            EmployeeFinance deserialized = Communicator.SendRequest<EmployeeFinance>
                (bytes, url, "POST");
            return "Success";
        }
        public string DeleteEmployee(byte[] bytes)
        {
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.DeleteEmployee;
            EmployeeFinanceForGetAndDelete deserialized = Communicator.SendRequest<EmployeeFinanceForGetAndDelete>
                (bytes, url, "POST");
            return "Success";
        }
		public byte[] GetEmployeeById(byte[] bytes)
		{
            EmployeeFinance deserialized = null;
            BinaryFormatter bfd = new BinaryFormatter();
            string baseUrl = ConfigurationManager.ConnectionStrings["DALServerName"].ConnectionString;
            string url = baseUrl + DestinationNames.GetEmployeeById;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/octet-stream";
            using (Stream strRequest = request.GetRequestStream())
            {
                strRequest.Write(bytes, 0, bytes.Length);
            }
            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())

            {
                MemoryStream memStr = new MemoryStream();
                stream.CopyTo(memStr);
                memStr.Flush();
                memStr.Position = 0;
                deserialized = bfd.Deserialize(memStr) as EmployeeFinance;
            }
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memorystream, deserialized);
            memorystream.Flush();
            memorystream.Position = 0;
            byte[] Bytes = memorystream.ToArray();
            return Bytes;
        }
	}
}