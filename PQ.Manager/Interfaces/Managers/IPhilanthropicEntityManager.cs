using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces
{
    public interface IPhilanthropicEntityManager
    {
        Task DeleteEntityAsync(int id);
        Task<IEnumerable<ViewPhilanthropicEntity>> GetEntitiesAsync();
        Task<ViewPhilanthropicEntity> GetEntityAsync(int id);
        Task<PhilanthropicEntity> InsertEntityAsync(NewPhilanthropicEntity entity);
        Task<PhilanthropicEntity> UpdateEntityAsync(UpdatePhilanthropicEntity entity);
    }
}
