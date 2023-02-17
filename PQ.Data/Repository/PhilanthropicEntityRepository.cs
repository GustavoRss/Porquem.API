using Microsoft.EntityFrameworkCore;
using PQ.Core.Domain;
using PQ.Data.Context;
using PQ.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Data.Repository
{
    public class PhilanthropicEntityRepository : IPhilanthropicEntityRepository
    {
        private readonly PQContext context;
        public PhilanthropicEntityRepository(PQContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<PhilanthropicEntity>> GetEntitiesAsync()
        {
            return await context.PhilanthropicEntities
                .Include(p => p.Address)
                .Include(p => p.Documents)
                .AsNoTracking().ToListAsync();
        }
        public async Task<PhilanthropicEntity> GetEntityAsync(int id)
        {
            var consultedEntity = await context.PhilanthropicEntities.SingleOrDefaultAsync(p => p.UserId == id);
            if (consultedEntity != null)
            {
                return await context.PhilanthropicEntities
                    .Include(p => p.Address)
                    .Include(p => p.Campaigns)
                    .Include(tm => tm.Campaigns)
                    .ThenInclude(m => m.HelpItems) 
                    .SingleOrDefaultAsync(p => p.UserId == id);
            }
            else
            {
                return null;
            }
        }

        public async Task<PhilanthropicEntity> InsertEntityAsync(PhilanthropicEntity entity)
        {
            await context.PhilanthropicEntities.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<PhilanthropicEntity> UpdateEntityAsync(PhilanthropicEntity entity)
        {
            var consultedEntity = await context.PhilanthropicEntities.FindAsync(entity.Id);
            if (consultedEntity != null)
            {
                context.PhilanthropicEntities.Attach(consultedEntity);

                context.Entry(consultedEntity).CurrentValues.SetValues(entity);
                
                var address = await context.Adresses.FindAsync(entity.Address.PhilanthropicEntityId);
                context.Entry(address).CurrentValues.SetValues(entity.Address);

                context.Entry(consultedEntity).Property(p => p.CorporateName).IsModified =
                false;

                context.Entry(consultedEntity).Property(p => p.StateRegistration).IsModified =
                false;

                context.Entry(consultedEntity).Property(p => p.Cnpj).IsModified =
                false;

                context.Entry(consultedEntity).Property(p => p.CreatedAt).IsModified =
                false;

                context.Entry(consultedEntity).Property(p => p.Status).IsModified =
                false;

                context.Entry(consultedEntity).Property(p => p.Logo).IsModified =
                entity.Logo?.Length > 0;


            }
            else
            {
                return null;
            }
            await context.SaveChangesAsync();
            return consultedEntity;
        }
        
        public async Task DeleteEntityAsync(int id)
        {
            var entidadeConsultada = await context.PhilanthropicEntities.FindAsync(id);
            context.PhilanthropicEntities.Remove(entidadeConsultada);
            await context.SaveChangesAsync();
        }
    }
}
