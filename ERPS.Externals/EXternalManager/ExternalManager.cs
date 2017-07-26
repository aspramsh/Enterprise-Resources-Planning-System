using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using ERPS.DataModel.Entities.HR;
using System.Xml.Serialization;

namespace ERPS.Externals.EXternalManager
{
    public class ExternalManager
    {
        private static ExternalManager instance = null;

        public static ExternalManager Instance()
        {
            if (null == instance)
            {
                instance = new ExternalManager();
            }
            return instance;
        }


      
        public byte[] GetCandidate()
        {
            CandidateEmployee employe =new CandidateEmployee();
            employe.Name = "John";
            employe.LastName = "Smith";
            employe.Address = "Erevan";
            employe.Email = "Mail.am";
            employe.Phone = "0778878787";
            CandidateEmployee employe1 = new CandidateEmployee();
            employe1.Name = "Narek";
            employe1.LastName = "Narekyan";
            employe1.Address = "New York";
            employe1.Email = "Mail.com";
            employe1.Phone = "0778878787";

            Candidate candidate = new Candidate();
            candidate.ListofCandidate.Add(employe);
            candidate.ListofCandidate.Add(employe1);
            if (candidate == null)
                return null;
            XmlSerializer XmlFormatter = new XmlSerializer(typeof(Candidate));
            using (MemoryStream ms = new MemoryStream())
            {
                XmlFormatter.Serialize(ms, candidate);
                return ms.ToArray();
            }
        }
    }
}