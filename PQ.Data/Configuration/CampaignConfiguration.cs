using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PQ.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Data.Configuration
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.HasKey(p => new { p.PhilanthropicEntityId, p.Id });
        }
    }
}
