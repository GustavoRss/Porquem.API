using System.Collections.Generic;

namespace PQ.Core.Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}