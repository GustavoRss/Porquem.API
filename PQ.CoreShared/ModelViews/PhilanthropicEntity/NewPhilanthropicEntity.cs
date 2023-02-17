
using PQ.CoreShared.ModelViews.Campaign;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.CoreShared.ModelViews
{
    /// <summary>
    /// Objeto utilizado para inserção de uma nova entidade filantrópica
    /// </summary>
    public class NewPhilanthropicEntity
    {
        public string FantasyName { get; set; }
        public DateTime DtOpening { get; set; }
        public string Telephone { get; set; }
        public string CorporateName { get; set; }
        public string Email { get; set; }
        public string ContactEmail { get; set; }
        public string Cnpj { get; set; }
        public string StateRegistration { get; set; }
        public int UserId { get; set; }
        public NewAddress Address { get; set; }
        public NewDocument Documents { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string Logo { get; set; }
        public string History { get; set; }
        public string Cause { get; set; }
    }
}
