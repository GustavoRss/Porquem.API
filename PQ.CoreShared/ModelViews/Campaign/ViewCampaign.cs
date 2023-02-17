using PQ.CoreShared.ModelViews.HelpItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.CoreShared.ModelViews.Campaign
{
    public class ViewCampaign
    {
        public Guid Id { get; set; }
        public string Slogan { get; set; }
        public string Wallpaper { get; set; }
        public string Logo { get; set; }
        public string Objective { get; set; }
        public string HowHelp { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // helpItems: icollection [{"id": 1, "helpType": "food"}] (implementar crud de help items)
        public string FeedBack { get; set; }
        public string Status { get; set; }
        public ICollection<ViewHelpItem> HelpItems { get; set; }
    }
}
