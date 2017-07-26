using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPS.ServiceLayer.Models
{
    public class HRCandidate
    {
      
        public string Name { get; set; }
        public string LastName { get; set; }
       public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Valuation { get; set; }
    }

    public class HRCandidates
    {
        public List<HRCandidate> ListOfCandidate { get; set; }
       public  HRCandidates()
        {
            ListOfCandidate = new List<Models.HRCandidate>();
        }
       
    }
}