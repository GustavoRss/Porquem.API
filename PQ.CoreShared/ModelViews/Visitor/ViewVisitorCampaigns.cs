using PQ.CoreShared.ModelViews.HelpItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.CoreShared.ModelViews.Visitor
{
    public class ViewVisitorCampaigns
    {
        public Guid Id { get; set; }
        public int PhilanthropicEntityId { get; set; }
        public ViewVisitorEntities PhilanthropicEntity { get; set; }
        public string Slogan { get; set; }
        public string Wallpaper { get; set; }
        public string Logo { get; set; }
        public string Objective { get; set; }
        public string HowHelp { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FeedBack { get; set; }

        public ICollection<ViewHelpItem> HelpItems { get; set; }

        public ViewVisitorCampaigns()
        {
            HelpItems = new HashSet<ViewHelpItem>();
        }
    }
}
