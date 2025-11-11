using NewAvatarWebApis.Core.Application.DTOs;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IItemViewService
    {
        public Task<ItemViewItemListResponse> ItemList(ItemViewItemListParams request);
    }
}
