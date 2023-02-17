namespace PQ.Core.Domain
{
    public class Address
    {
        public int PhilanthropicEntityId { get; set; }
        public string CEP { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string PublicPlace { get; set; }
        public PhilanthropicEntity PhilanthropicEntity { get; set; }
    }
}