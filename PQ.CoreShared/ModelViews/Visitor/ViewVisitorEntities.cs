using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.CoreShared.ModelViews
{
   public class ViewVisitorEntities
    {
        public int Id { get; set; }
        public string FantasyName { get; set; }
        public DateTime DtOpening { get; set; }
        public string Telephone { get; set; }
       // public string CorporateName { get; set; }
        public string ContactEmail { get; set; }
       // public string Cnpj { get; set; }
       // public string StateRegistration { get; set; }
       // public int UserId { get; set; }
        public NewAddress Address { get; set; }
       // public NewDocument Documents { get; set; }
       // public string Status { get; set; }
        public string Logo { get; set; }
        public string History { get; set; }
        public string Cause { get; set; }
    }
}
