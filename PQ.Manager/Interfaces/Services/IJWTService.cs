using PQ.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Manager.Interfaces
{
    public interface IJWTService
    {
        string GerarToken(User user);
    }
}
