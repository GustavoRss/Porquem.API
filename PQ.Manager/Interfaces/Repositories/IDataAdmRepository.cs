using PQ.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces.Repositories
{
    public interface IDataAdmRepository
    {
     
        Task<PhilanthropicEntity> GetEntityAsync(int id);
        Task<IEnumerable<PhilanthropicEntity>> GetEntitiesAsync();
        Task<PhilanthropicEntity> UpdateEntityAsync(PhilanthropicEntity philanthropicEntity);
    }
}
