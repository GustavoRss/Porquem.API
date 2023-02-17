using PQ.Core.Domain;
using PQ.CoreShared.ModelViews.Campaign;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces.Managers
{
    public interface ICampaignManager
    {
        Task DeleteCampaignAsync(Guid id);
        Task<IEnumerable<Campaign>> GetCampaignsAsync();
        Task<NewCampaign> GetCampaignAsync(Guid id);
        Task<Campaign> InsertCampaignAsync(NewCampaign campaign);
        Task<Campaign> UpdateCampaignAsync(UpdateCampaign campaign);
    }
}
