using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPS.DataModel.Entities.HR
{[Serializable]
   public  class Candidate
    {
    public List<CandidateEmployee> ListofCandidate { get; set; }

        public Candidate()
        {
            ListofCandidate=new List<CandidateEmployee>();
        }
    }
    [Serializable]
   public class CandidateEmployee
    {
        public string Name { get; set; }
        public string LastName { get; set; }
         public string Address { get; set;}
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
