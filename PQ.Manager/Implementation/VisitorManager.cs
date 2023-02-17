using AutoMapper;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.Visitor;
using PQ.Manager.Interfaces.Managers;
using PQ.Manager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Implementation
{
    public class VisitorManager : IVisitorManager
    {
        private readonly IVisitorRepository VisitorRepository;
        private readonly IMapper mapper;

        public VisitorManager(IVisitorRepository visitorRepository, IMapper mapper)
        {
            this.VisitorRepository = visitorRepository;
            this.mapper = mapper;
        }

        public async Task<ViewVisitorCampaigns> GetCampaignAsync(Guid id)
        {
            return mapper.Map<ViewVisitorCampaigns>(await VisitorRepository.GetCampaignAsync(id));
        }

        public async Task<IEnumerable<ViewVisitorCampaigns>> GetCampaignsAsync()
        {
            return mapper.Map<IEnumerable<Campaign>, IEnumerable<ViewVisitorCampaigns>>(await VisitorRepository.GetCampaignsAsync());
        }

        public async Task<ViewProfileEntity> GetEntityAsync(int id)
        {
            return mapper.Map<ViewProfileEntity>(await VisitorRepository.GetEntityAsync(id));
        }
    }
}
