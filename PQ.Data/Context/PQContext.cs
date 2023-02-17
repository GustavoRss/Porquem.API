using Microsoft.EntityFrameworkCore;
using PQ.Core.Domain;
using PQ.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Data.Context
{
    public class PQContext : DbContext
    {
        public DbSet<PhilanthropicEntity> PhilanthropicEntities { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<HelpItem> HelpItems { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public PQContext(DbContextOptions<PQContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PhilanthropicEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CampaignConfiguration());

        }

    }
}
