using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataManager
{
    public class DataManager
    {
        #region Singleton
        private static DataManager _instance; 

        public static DataManager Instance
        {
            get
            {
                return _instance == null ? new DataManager() : _instance;
            }
        }

        private DataManager() { }
        #endregion

        public void GetManagementTest(string s)
        {
            Console.WriteLine($"Got following string {s}");
            // HttpWebRequest request = new HttpWebRequest(); 
            string commandUrl = "http://localhost:53654/api/TestWAPIManager/GetTestWAPI?id=8";

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

                Console.WriteLine(str);
            }
        }
    }
}
