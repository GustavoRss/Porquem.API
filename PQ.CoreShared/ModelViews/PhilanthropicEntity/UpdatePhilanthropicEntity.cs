using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PQ.CoreShared.ModelViews
{
   public class UpdatePhilanthropicEntity : NewPhilanthropicEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string PhilanthropicEntity { get; set; }
    }
}
