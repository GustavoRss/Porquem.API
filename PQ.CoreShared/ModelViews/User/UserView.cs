using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.CoreShared.ModelViews
{
    public class UserView
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public ICollection<RoleView> Roles { get; set; }
    }
}
