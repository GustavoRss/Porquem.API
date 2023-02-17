using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PQ.CoreShared.ModelViews.FileUpload
{
    public class CampaignWithFileUpload
    {
        [NotMapped]
        public string Wallpaper { get; set; }
        [NotMapped]
        public string Logo { get; set; }
        public string Campaign { get; set; }
    }
}
