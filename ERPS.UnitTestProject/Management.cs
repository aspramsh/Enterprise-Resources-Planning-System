using ERPS.DataModel.Entities.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using ERPS.Utils.Functionals;

namespace ERPS.UnitTestProject
{
    [TestClass]
    public class Management
    {
        //TestWAPIManagerController
        //[Route("api/TestWAPIManager/GetTestEmployee")]
        ////public Employee GetTestEmployee()
        //public HttpResponseMessage GetTestEmployee()
        // Server Url
        [TestMethod]
        public void GetTestEmployeeTest()
        {
            string commandUrl = "http://localhost:53654/api/TestWAPIManager/GetTestEmployee";
            // Initializing request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(commandUrl);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/octet-stream";
            // Binary serialization
            BinaryFormatter bf = new BinaryFormatter();
            Employee employee = new Employee() { Id = 4 };
            using (Stream requestStream = request.GetRequestStream())
            {
                bf.Serialize(requestStream, employee);
                //request.ContentLength = requestStream.Length;
                //requestStream.Write(requestStream, 0, request.ContentLength);
            }

            Debug.Assert(employee.Id > 0);
        }

        [TestMethod]
        public void SqlProviderDapper()
        {
            var employees = new List<Employee>();
            using (IDbConnection connection = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=ERPSDataBase;Integrated Security=True"))
            {
                employees = connection.Query<Employee>("dbo.umsp_GetEmployees", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        [TestMethod]
        public void SqlProviderManagementGetListEntities()
        {
            var employees = new Collection<Employee>();
            employees.Entities = new List<Employee>();
            using (SqlConnection connection = new SqlConnection
              (@"Data Source=(local)\SQLEXPRESS;Initial Catalog=ERPSDataBase;Integrated Security=True"))
            {
                var cmd = new SqlCommand("dbo.umsp_GetEmployees", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var employee = new Employee();
                    employee.Id = Int32.Parse(reader[0].ToString());
                    employee.Name = reader[1].ToString();
                    employee.Surname = reader[2].ToString();
                    employees.Entities.Add(employee);
                    //employees.Entities = (T)employees.Entities.AddToIEnumerable(employee);
                    //employees.Entities.Add<Employee>(employee);
                }
            }
            return;
        }

        [TestMethod]
        public void GetTestManagementWAPI()
        {

            var employee = new Employee() { Id = 4 };
            // Requesting server:
            //"http://localhost:53654/api/AddEmployee/AddEmployeeToTeam"
            //update "http://localhost:53654/api/UpdateEmployee/UpdateEmployee"
            var response = ERPS.Utils.Functionals.ManagementStreamHandler.SendObject<Employee>(employee, "http://localhost:53654/api/UpdateEmployee/UpdateEmployee", "POST");
            var recievedEntity = ERPS.Utils.Functionals.ManagementStreamHandler.RecieveObject<Employee>(response);
            Debug.Assert(employee.Id > 0);
        }

        [TestMethod]
        public void TestAddToIEumberable()
        {
            //IEnumerable<int> list = new List<int>();
            //var result = list.AddToIEnumerable(4);
            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}
            ////list.Concat(Enumerable.Repeat(4, 1));
            //Console.WriteLine(result.First());
            IEnumerable<Employee> list = new List<Employee>();
            //list.AddToIEnumerable(new Employee() { Id = 1, Name = "name" });
            var result = list;
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            int count = result.Count();
            Console.WriteLine(result.First());
        }
    }

}

