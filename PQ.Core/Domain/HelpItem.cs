using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Core.Domain
{
    public class HelpItem
    {
        public int Id { get; set; }
        public string HelpType { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }
    }
}
