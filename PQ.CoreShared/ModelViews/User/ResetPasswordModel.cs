using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.CoreShared.ModelViews.User
{
    public class ResetPasswordModel
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
