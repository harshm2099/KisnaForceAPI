using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IItemListingService
    {
        public ServiceResponse<IList<ItemListing>> GetItemsListing(ItemListingParams item);
    }
}
