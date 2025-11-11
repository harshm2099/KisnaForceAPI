using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;

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
        public async Task<ResponseDetails> ItemList(ItemViewItemListParams request)
        {
            var result = await _itemViewService.ItemList(request);
            return result;
        }
    }
}
