using AutoMapper;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.PhilanthropicEntity;
using PQ.Manager.Interfaces;
using PQ.Manager.Interfaces.Managers;
using PQ.Manager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Implementation
{
    public class DataAdmManager : IDataAdmManager
    {
        private readonly IDataAdmRepository DataAdmRepository;
        private readonly IMapper mapper;

        public DataAdmManager(IDataAdmRepository dataAdmRepository, IMapper mapper)
        {
            DataAdmRepository = dataAdmRepository;
            this.mapper = mapper;
        }

  
        public async Task<IEnumerable<ViewPhilanthropicEntity>> GetEntitiesAsync()
        {
            return mapper.Map<IEnumerable<PhilanthropicEntity>, IEnumerable<ViewPhilanthropicEntity>>(await DataAdmRepository.GetEntitiesAsync());
        }

        public async Task<ViewPhilanthropicEntity> GetEntityAsync(int id)
        {
            return mapper.Map<ViewPhilanthropicEntity>(await DataAdmRepository.GetEntityAsync(id));
        }

        public async Task<PhilanthropicEntity> UpdateEntityAsync(ChangeStatus updatePhilanthropicEntity)
        {
            var philanthropicEntity = mapper.Map<PhilanthropicEntity>(updatePhilanthropicEntity);
            return await DataAdmRepository.UpdateEntityAsync(philanthropicEntity);
        }
    }
}
