using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.CoreShared.ModelViews.PhilanthropicEntity
{
    public class PhilanthropicEntityCampaign
    {
        public string FantasyName { get; set; }
        public string Telephone { get; set; }
        public string ContactEmail { get; set; }
        public NewAddress Address { get; set; }
    }
}
