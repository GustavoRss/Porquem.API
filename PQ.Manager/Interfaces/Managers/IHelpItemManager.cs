using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.HelpItem;
using PQ.CoreShared.ModelViews.PhilanthropicEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces.Managers
{
    public interface IHelpItemManager
    {
        Task<IEnumerable<ViewHelpItem>> GetHelpItemsAsync();
        Task<ViewHelpItem> GetHelpItemAsync(int id);
        Task DeleteHelpItemAsync(int id);
        Task<HelpItem> InsertHelpItemAsync(NewHelpItem campaign);
    }
}
