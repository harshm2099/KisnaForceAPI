using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpPost("get-item")]
        public ServiceResponse<IList<MasterItems>> GetAllItems()
        {
            var result = _itemsService.GetAllItems();
            return result;
        }

        [HttpPost("add-item")]
        public Task<CommonResponse> AddItems(MasterItems Items)
        {
            var result = _itemsService.AddItems(Items);
            return result;
        }

        [HttpPost("edit-item")]
        public Task<CommonResponse> EditItem(MasterItems Item)
        {
            var result = _itemsService.EditItem(Item);
            return result;
        }

        [HttpPost("disable-item")]
        public Task<CommonResponse> DisableItem(MasterItems item)
        {
            var result = _itemsService.DisableItem(item);
            return result;
        }

        [HttpPost("get-item-count")]
        public ServiceResponse<IList<ItemCount>> GetAllItemsCount()
        {
            var result = _itemsService.GetAllItemsCount();
            return result;
        }
    }
}
