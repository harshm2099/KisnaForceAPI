using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ItemViewController
    {
        private readonly IItemViewService _itemViewService;

        public ItemViewController(IItemViewService itemViewService)
        {
            _itemViewService = itemViewService;
        }

        [HttpPost("item-list")]
        public Task<ItemViewItemListResponse> ItemList(ItemViewItemListParams request)
        {
            var result = _itemViewService.ItemList(request);
            return result;
        }
    }
}
