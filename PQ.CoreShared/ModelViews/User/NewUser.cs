using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.CoreShared.ModelViews.Usuario
{
    public class NewUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<ReferenceRole> Roles { get; set; }
    }
}
