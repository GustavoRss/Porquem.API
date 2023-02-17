using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PQ.CoreShared.ModelViews.FileUpload
{
    public class EntityWithFileUpload
    {
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileBlob { get; set; }
        public string PhilanthropicEntity { get; set; }
    }
}
