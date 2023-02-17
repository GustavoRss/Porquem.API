using Microsoft.EntityFrameworkCore;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.Data.Context;
using PQ.Manager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Data.Repository
{
    public class DataAdmRepository : IDataAdmRepository
    {
        private readonly PQContext context;
        public DataAdmRepository(PQContext context)
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
            var consultedEntity = await context.PhilanthropicEntities.SingleOrDefaultAsync(p => p.Id == id);
            if (consultedEntity != null)
            {
                return await context.PhilanthropicEntities
                    .Include(p => p.Address)
                    .Include(p => p.Documents)
                    .SingleOrDefaultAsync(p => p.Id == id);
            }
            else
            {
                return null;
            }
        }
        public async Task<PhilanthropicEntity> UpdateEntityAsync(PhilanthropicEntity entity)
        {
            var consultedEntity = await context.PhilanthropicEntities.FindAsync(entity.Id);
            if (consultedEntity != null)
            {
                var role = await context.Roles.SingleOrDefaultAsync(r => r.Description == entity.Status) ;

                var newRolsIds = new List<int> { role.Id == 4 ? 1 : role.Id };
                context.PhilanthropicEntities.Attach(consultedEntity);

                var user = await context.Users.Include("Roles").SingleOrDefaultAsync(u => u.Id == consultedEntity.UserId);

                var newRoles = context.Roles.Where(r => newRolsIds.Contains(r.Id)).ToList();

                user.Roles.Clear();
                foreach (var newRole in newRoles)
                    user.Roles.Add(newRole);

                consultedEntity.Status = entity.Status == "AD" ? "AT" : entity.Status;
            }
            else
            {
                return null;
            }
            await context.SaveChangesAsync();
            return consultedEntity;
        }
    }
}
