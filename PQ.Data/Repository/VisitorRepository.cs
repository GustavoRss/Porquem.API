using Microsoft.EntityFrameworkCore;
using PQ.Core.Domain;
using PQ.Data.Context;
using PQ.Manager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Data.Repository
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly PQContext context;
        public VisitorRepository(PQContext context)
        {
            this.context = context;
        }

        public async Task<Campaign> GetCampaignAsync(Guid id)
        {
            var baselineDate = DateTime.Now.AddHours(-24);
            var consultedEntity = await context.Campaign.SingleOrDefaultAsync(p => p.Id == id);
            if (consultedEntity != null)
            {
                return await context.Campaign.Where(p => p.EndDate >= baselineDate).Include(p => p.PhilanthropicEntity).Include(p => p.PhilanthropicEntity.Address).Include(p => p.HelpItems)
                    .SingleOrDefaultAsync(p => p.Id == id);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsAsync()
        {
            var baselineDate = DateTime.Now.AddHours(-24);
            return await context.Campaign.Where(p => p.EndDate >= baselineDate).Include(p => p.PhilanthropicEntity).Include(p => p.PhilanthropicEntity.Address).Include(p => p.HelpItems)
                 .AsNoTracking().ToListAsync();
        }

        public async Task<PhilanthropicEntity> GetEntityAsync(int id)
        {
            var baselineDate = DateTime.Now.AddHours(-24);
            var consultedEntity = await context.PhilanthropicEntities.SingleOrDefaultAsync(p => p.Id == id);
            if (consultedEntity != null)
            {
                return await context.PhilanthropicEntities
                    .Include(p => p.Campaigns.Where(p => p.EndDate >= baselineDate))
                    .ThenInclude(m => m.HelpItems)
                    .Include(p => p.Address)
                    .SingleOrDefaultAsync(p => p.Id == id);
            }
            else
            {
                return null;
            }
        }
    }
}
