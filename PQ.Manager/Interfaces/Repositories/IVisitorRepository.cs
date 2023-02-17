using PQ.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces.Repositories
{
    public interface IVisitorRepository
    {
        Task<IEnumerable<Campaign>> GetCampaignsAsync();
        Task<Campaign> GetCampaignAsync(Guid id);
        Task<PhilanthropicEntity> GetEntityAsync(int id);
    }
}
