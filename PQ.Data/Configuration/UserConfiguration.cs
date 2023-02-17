using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PQ.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        { 
            builder.HasOne(e => e.PhilanthropicEntity)
            .WithOne(e => e.User)
            .HasForeignKey<PhilanthropicEntity>(p => p.UserId);
        }
    }
}
