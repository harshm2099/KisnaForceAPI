using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ItemListingController : ControllerBase
    {
        private readonly IItemListingService _itemListingService;

        public ItemListingController(IItemListingService itemListingService)
        {
            _itemListingService = itemListingService;
        }

        [HttpPost("get-item-listing")]
        public ServiceResponse<IList<ItemListing>> GetItemsListing(ItemListingParams item)
        {
            var result = _itemListingService.GetItemsListing(item);
            return result;
        }
    }
}
