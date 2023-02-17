namespace PQ.CoreShared.ModelViews
{
    public class NewAddress
    {
        public int PhilanthropicEntityId { get; set; }
        /// <example>00000-000</example>
        public string CEP { get; set; }
        /// <example>São Paulo</example>
        public string State { get; set; }
        /// <example>Birigui</example>
        public string City { get; set; }
        /// <example>10</example>
        public string Number { get; set; }
        /// <example>Rua X</example>
        public string PublicPlace { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
    }
}