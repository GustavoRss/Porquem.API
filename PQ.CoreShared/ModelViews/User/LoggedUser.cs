using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.CoreShared.ModelViews.Usuario
{
    public class LoggedUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public ICollection<RoleView> Roles { get; set; }
        public string Token { get; set; }
    }
}
