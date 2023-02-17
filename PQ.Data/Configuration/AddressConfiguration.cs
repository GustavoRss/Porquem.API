using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PQ.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Data.Configuration
{
    class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(p => p.PhilanthropicEntityId);
        }
    }
}
