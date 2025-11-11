using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _wishListService;
        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        [HttpPost("insert")]
        public async Task<ResponseDetails> SaveWishlistItem(WishlistInsertPayload wishlistinsert_params)
        {
            var result = await _wishListService.SaveWishlistItem(wishlistinsert_params);
            return result;
        }

        [HttpPost("wish-item-list-on")]
        public async Task<ResponseDetails> GetWishItemList(WishItemListingParams wishitemlistparams)
        {
            var result = await _wishListService.GetWishItemList(wishitemlistparams);
            return result;
        }
    }
}
