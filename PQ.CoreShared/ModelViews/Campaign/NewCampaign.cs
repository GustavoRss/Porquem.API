using PQ.CoreShared.ModelViews.HelpItem;
using PQ.CoreShared.ModelViews.PhilanthropicEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.CoreShared.ModelViews.Campaign
{
    public class NewCampaign
    {
        public int PhilanthropicEntityId { get; set; }
        public string Slogan { get; set; }
        public PhilanthropicEntityCampaign PhilanthropicEntity { get; set; }
        public string Wallpaper { get; set; }
        public string Logo { get; set; }
        public string Objective { get; set; }
        public string HowHelp { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FeedBack { get; set; }
        public string Status { get; set; }
        public ICollection<ViewHelpItem> HelpItems { get; set; }

    }
}
