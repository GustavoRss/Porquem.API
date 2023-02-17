using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Core.Domain
{
    public class Campaign
    {
        public Guid Id  { get; set; }
        public int PhilanthropicEntityId { get; set; }
        public PhilanthropicEntity PhilanthropicEntity { get; set; }
        public string Slogan { get; set; }
        public string Wallpaper { get; set; }
        public string Logo { get; set; }
        public string Objective { get; set; }
        public string HowHelp { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public string FeedBack { get; set; }
        public string Status { get; set; }

        public ICollection<HelpItem> HelpItems{ get; set; }

        public Campaign()
        {
            HelpItems = new HashSet<HelpItem>();
        }
    }
}
