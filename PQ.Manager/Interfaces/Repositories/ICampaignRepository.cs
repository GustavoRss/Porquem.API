using PQ.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces.Repositories
{
   public interface ICampaignRepository
    {
        Task DeleteCampaignAsync(Guid id);
        Task<Campaign> GetCampaignAsync(Guid id);
        Task<IEnumerable<Campaign>> GetCampaignsAsync();
        Task<Campaign> InsertCampaignAsync(Campaign campaign);
        Task<Campaign> UpdateCampaignAsync(Campaign campaign);
    }
}
