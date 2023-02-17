using AutoMapper;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.HelpItem;
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
    public class HelpItemManager : IHelpItemManager
    {
        private readonly IHelpItemRepository HelpItemRepository;
        private readonly IMapper mapper;

        public HelpItemManager(IHelpItemRepository helpItemRepository, IMapper mapper)
        {
            HelpItemRepository = helpItemRepository;
            this.mapper = mapper;
        }

        public async Task DeleteHelpItemAsync(int id)
        {
            await HelpItemRepository.DeleteHelpItemAsync(id);
        }

        public async Task<ViewHelpItem> GetHelpItemAsync(int id)
        {
            return mapper.Map<ViewHelpItem>(await HelpItemRepository.GetHelpItemAsync(id));
        }

        public async Task<IEnumerable<ViewHelpItem>> GetHelpItemsAsync()
        {
            return mapper.Map<IEnumerable<HelpItem>, IEnumerable<ViewHelpItem>>(await HelpItemRepository.GetHelpItemsAsync());
        }

        public async Task<HelpItem> InsertHelpItemAsync(NewHelpItem newHelpItem)
        {
            var helpItem = mapper.Map<HelpItem>(newHelpItem);
            return await HelpItemRepository.InsertHelpItemAsync(helpItem);
        }
    }
}
