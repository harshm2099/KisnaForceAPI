using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IWishListService
    {
        public Task<ResponseDetails> SaveWishlistItem(WishlistInsertPayload wishlistinsert_params);

        public Task<ResponseDetails> GetWishItemList(WishItemListingParams wishitemlistparams);
    }
}
