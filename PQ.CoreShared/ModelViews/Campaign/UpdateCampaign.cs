using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PQ.CoreShared.ModelViews.Campaign
{
    public class UpdateCampaign : NewCampaign
    {
        public Guid Id { get; set; }
        public string Campaign { get; set; }
    }
}
