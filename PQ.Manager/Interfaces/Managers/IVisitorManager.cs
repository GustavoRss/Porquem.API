using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.Visitor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces.Managers
{
    public interface IVisitorManager
    {
        Task<IEnumerable<ViewVisitorCampaigns>> GetCampaignsAsync();
        Task<ViewVisitorCampaigns> GetCampaignAsync(Guid id);
        Task<ViewProfileEntity> GetEntityAsync(int id);
    }
}
