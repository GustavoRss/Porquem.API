using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PQ.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Data.Configuration
{
    class PhilanthropicEntityConfiguration : IEntityTypeConfiguration<PhilanthropicEntity>
    {
        public void Configure(EntityTypeBuilder<PhilanthropicEntity> builder)
        {

            //builder.Property(p => p.FantasyName).HasMaxLength(200).IsRequired();
            builder.HasOne(e => e.Address)
            .WithOne(e => e.PhilanthropicEntity)
            .HasForeignKey<Address>(p => p.PhilanthropicEntityId);
        }
    }
}
