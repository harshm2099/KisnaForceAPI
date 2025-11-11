using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IItemsService
    {
        public ServiceResponse<IList<MasterItems>> GetAllItems();

        public Task<CommonResponse> AddItems(MasterItems Items);

        public Task<CommonResponse> EditItem(MasterItems Item);

        public Task<CommonResponse> DisableItem(MasterItems item);

        public ServiceResponse<IList<ItemCount>> GetAllItemsCount();
    }
}
