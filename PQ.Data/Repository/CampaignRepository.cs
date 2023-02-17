using Microsoft.EntityFrameworkCore;
using PQ.Core.Domain;
using PQ.Data.Context;
using PQ.Manager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Data.Repository
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly PQContext context;
        public CampaignRepository(PQContext context)
        {
            this.context = context;
        }

        public async Task DeleteCampaignAsync(Guid id)
        {
            var consultedEntity = await context.Campaign.SingleOrDefaultAsync(p => p.Id == id);
            context.Campaign.Remove(consultedEntity);
            await context.SaveChangesAsync();
        }

        public async Task<Campaign> GetCampaignAsync(Guid id)
        {
            var consultedEntity = await context.Campaign.SingleOrDefaultAsync(p => p.Id == id);
            if (consultedEntity != null)
            {
                return await context.Campaign.Include(p => p.PhilanthropicEntity).Include(p => p.PhilanthropicEntity.Address).Include(p => p.HelpItems)
                    .SingleOrDefaultAsync(p => p.Id == id);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsAsync()
        {
            return await context.Campaign.Include(p => p.HelpItems)
                 .AsNoTracking().ToListAsync();
        }

        public async Task<Campaign> InsertCampaignAsync(Campaign campaign)
        {
            campaign.Id = Guid.NewGuid();
            campaign.CreatedAt = DateTime.Now;
            campaign.Status = "AT";
            await InsertCampaignHelpItems(campaign);
            await context.Campaign.AddAsync(campaign);
            await context.SaveChangesAsync();
            return campaign;
        }

        public async Task<Campaign> UpdateCampaignAsync(Campaign campaign)
        {
            var consultedEntity = await context.Campaign.SingleOrDefaultAsync(p => p.Id == campaign.Id);
            if (consultedEntity == null)
            {
                return null;
            }

            await UpdateCampaignHelpItems(campaign, consultedEntity);
            context.Campaign.Attach(consultedEntity);
            context.Entry(consultedEntity).CurrentValues.SetValues(campaign);

            context.Entry(consultedEntity).Property(p => p.Logo).IsModified =
                campaign.Logo?.Length > 0;

            context.Entry(consultedEntity).Property(p => p.Wallpaper).IsModified =
                campaign.Wallpaper?.Length > 0;

            await context.SaveChangesAsync();
            return consultedEntity;
        }

        private async Task InsertCampaignHelpItems(Campaign campaign)
        {
            var consultedHelpitems= new List<HelpItem>();
            foreach (var helpitem in campaign.HelpItems)
            {
                var consultedHelpItem = await context.HelpItems.FindAsync(helpitem.Id);
                consultedHelpitems.Add(consultedHelpItem);
            }
            campaign.HelpItems = consultedHelpitems;
        }

        private async Task UpdateCampaignHelpItems(Campaign campaign, Campaign consultedCampaign)
        {
            consultedCampaign.HelpItems.Clear();
            foreach (var helpItem in campaign.HelpItems)
            {
                var especialidadeConsultada = await context.HelpItems.FindAsync(helpItem.Id);
                consultedCampaign.HelpItems.Add(especialidadeConsultada);
            }
        }
    }
}
