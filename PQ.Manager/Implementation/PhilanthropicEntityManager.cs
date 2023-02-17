using AutoMapper;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Implementation
{
    public class PhilanthropicEntityManager : IPhilanthropicEntityManager
    {
        private readonly IPhilanthropicEntityRepository PhilanthropicEntityRepository;
        private readonly IMapper mapper;

        public PhilanthropicEntityManager(IPhilanthropicEntityRepository philanthropicEntityRepository, IMapper mapper)
        {
            this.PhilanthropicEntityRepository = philanthropicEntityRepository;
            this.mapper = mapper;
        }

        public async Task DeleteEntityAsync(int id)
        {
            await PhilanthropicEntityRepository.DeleteEntityAsync(id);
        }

        public async Task<IEnumerable<ViewPhilanthropicEntity>> GetEntitiesAsync()
        {
            return mapper.Map<IEnumerable<PhilanthropicEntity>, IEnumerable<ViewPhilanthropicEntity>>(await PhilanthropicEntityRepository.GetEntitiesAsync());
        }
        public async Task<ViewPhilanthropicEntity> GetEntityAsync(int id)
        {
            return mapper.Map<ViewPhilanthropicEntity>(await PhilanthropicEntityRepository.GetEntityAsync(id));
        }

        public async Task<PhilanthropicEntity> InsertEntityAsync(NewPhilanthropicEntity newPhilanthropicEntity)
        {
            var philanthropicEntity = mapper.Map<PhilanthropicEntity>(newPhilanthropicEntity);
            return await PhilanthropicEntityRepository.InsertEntityAsync(philanthropicEntity);
        }

        public async Task<PhilanthropicEntity> UpdateEntityAsync(UpdatePhilanthropicEntity updatePhilanthropicEntity)
        {
            var philanthropicEntity = mapper.Map<PhilanthropicEntity>(updatePhilanthropicEntity);
            return await PhilanthropicEntityRepository.UpdateEntityAsync(philanthropicEntity);
        }
    }
}
