using PQ.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces.Repositories
{
    public interface IHelpItemRepository
    {
        Task<IEnumerable<HelpItem>> GetHelpItemsAsync();
        Task<HelpItem> GetHelpItemAsync(int id);
        Task<HelpItem> InsertHelpItemAsync(HelpItem helpItem);
        Task DeleteHelpItemAsync(int id);
    }
}
