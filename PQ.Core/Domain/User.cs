using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Core.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public PhilanthropicEntity PhilanthropicEntity { get; set; }
        public string Password { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public ICollection<Role> Roles { get; set; }
    
        public User()
        {
            Roles = new HashSet<Role>();
        }
    }
}
