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
    public class HelpItemRepository : IHelpItemRepository
    {
        private readonly PQContext context;
        public HelpItemRepository(PQContext context)
        {
            this.context = context;
        }

        public async Task DeleteHelpItemAsync(int id)
        {
            var consultedEntity = await context.HelpItems.SingleOrDefaultAsync(p => p.Id == id);
            context.HelpItems.Remove(consultedEntity);
            await context.SaveChangesAsync();
        }
        
        public async Task<HelpItem> GetHelpItemAsync(int id)
        {
            var consultedEntity = await context.HelpItems.SingleOrDefaultAsync(p => p.Id == id);
            if (consultedEntity != null)
            {
                return consultedEntity;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<HelpItem>> GetHelpItemsAsync()
        {
            return await context.HelpItems
                 .AsNoTracking().ToListAsync();
        }

        public async Task<HelpItem> InsertHelpItemAsync(HelpItem helpItem)
        {
            var alreadyExist = await context.HelpItems.SingleOrDefaultAsync(p=> p.HelpType == helpItem.HelpType);
            if (alreadyExist == null)
            {
                await context.HelpItems.AddAsync(helpItem);
                await context.SaveChangesAsync();
                return helpItem;
            } else
            {
                return alreadyExist;
            }
        }
    }
}
