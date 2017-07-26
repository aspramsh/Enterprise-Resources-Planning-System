using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logger
{
    public class Logger
    {/// <summary>
    /// function for save exceptions in file 
    /// </summary>
    /// <param name="exception"></param>
        public static void Addlog(string exception)
        {  
        // var path = "C:\\Users\\Arman\\Documents\\Visual Studio 2013\\ERPS T14\\Logger\\LogFile.txt";
            var path = System.Configuration.ConfigurationManager.ConnectionStrings["LogPath"].ConnectionString;
            if (File.Exists(path))
            {
                // Create a file to write to.
                string createText = exception + " - "+DateTime.Now.ToString()+Environment.NewLine;
                File.AppendAllText(path, createText);
            }
        }
    }
}
