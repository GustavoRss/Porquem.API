using AutoMapper;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews.Campaign;
using PQ.Manager.Interfaces.Managers;
using PQ.Manager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Implementation
{
    public class CampaignManager : ICampaignManager
    {
        private readonly ICampaignRepository CampaignRepository;
        private readonly IMapper mapper;

        public CampaignManager(ICampaignRepository campaignRepository, IMapper mapper)
        {
            this.CampaignRepository = campaignRepository;
            this.mapper = mapper;
        }

        public async Task DeleteCampaignAsync(Guid id)
        {
            await CampaignRepository.DeleteCampaignAsync(id);
        }

        public async Task<NewCampaign> GetCampaignAsync(Guid id)
        {
            return mapper.Map<NewCampaign>(await CampaignRepository.GetCampaignAsync(id));
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsAsync()
        {
            return mapper.Map<IEnumerable<Campaign>, IEnumerable<Campaign>>(await CampaignRepository.GetCampaignsAsync());
        }

        public async Task<Campaign> InsertCampaignAsync(NewCampaign newCampaign)
        {
            var campaign = mapper.Map<Campaign>(newCampaign);
            return await CampaignRepository.InsertCampaignAsync(campaign);
        }

        public async Task<Campaign> UpdateCampaignAsync(UpdateCampaign updateCampaign)
        {
            var campaign = mapper.Map<Campaign>(updateCampaign);
            return await CampaignRepository.UpdateCampaignAsync(campaign);
        }
    }
}
