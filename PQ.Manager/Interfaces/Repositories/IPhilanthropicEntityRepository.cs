using PQ.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces
{
    public interface IPhilanthropicEntityRepository
    {
        Task DeleteEntityAsync(int id);
        Task<PhilanthropicEntity> GetEntityAsync(int id);
        Task<IEnumerable<PhilanthropicEntity>> GetEntitiesAsync();
        Task<PhilanthropicEntity> InsertEntityAsync(PhilanthropicEntity philanthropicEntity);
        Task<PhilanthropicEntity> UpdateEntityAsync(PhilanthropicEntity philanthropicEntity);
    }
}
