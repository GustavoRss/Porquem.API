using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.PhilanthropicEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces.Managers
{
    public interface IDataAdmManager
    {
        Task<IEnumerable<ViewPhilanthropicEntity>> GetEntitiesAsync();
        Task<ViewPhilanthropicEntity> GetEntityAsync(int id);
        Task<PhilanthropicEntity> UpdateEntityAsync(ChangeStatus entity);
    }
}
