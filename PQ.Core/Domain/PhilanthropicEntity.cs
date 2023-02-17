using System;
using System.Collections.Generic;


namespace PQ.Core.Domain
{
    public class PhilanthropicEntity
    {
        public int Id { get; set; }
        public string FantasyName { get; set; }
        public string CorporateName { get; set; }
        public string ContactEmail { get; set; }
        public string Cnpj { get; set; }
        public string StateRegistration { get; set; }
        public DateTime DtOpening { get; set; }
        public string Telephone { get; set; }
        public Document Documents { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Address Address { get; set; }
        public string Status { get; set; }
        public string Logo { get; set; }
        public string History { get; set; }
        public string Cause { get; set; }
    }

}
