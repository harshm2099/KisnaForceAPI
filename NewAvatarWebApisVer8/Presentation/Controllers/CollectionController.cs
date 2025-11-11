using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpPost("add-collection-item")]
        public ServiceResponse<bool> AddCollectionMapping(CollectionItemMapping CollectionMap)
        {
            var result =  _collectionService.AddCollectionMapping(CollectionMap);
            return result;
        }

        [HttpGet("get-collection-items-by-id")]
        public ServiceResponse<IList<MasterItemsrequest>> GetCollectionItemsById(string collectionid)
        {
            var result = _collectionService.GetCollectionItemsById(collectionid);
            return result;
        }

        [HttpPost("edit-collection-mapping")]
        public ServiceResponse<bool> EditCollectionMapping(ClsEditCollectionItemMapping CollectionMap)
        {
            var result = _collectionService.EditCollectionMapping(CollectionMap);
            return result;
        }

        [HttpPost("get-custom-collection-list")]
        public ServiceResponse<IList<CustomCollectionResponse>> GetCustomCollectionList([FromBody] CustomCollectionList param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = _collectionService.GetCustomCollectionList(param, commonHeader);
            return result;
        }

        [HttpPost("get-custom-collection-category-list")]
        public ServiceResponse<IList<CustomCollectionCategoriesResposne>> GetCustomCollectionCategoryList([FromBody]CollectionCategoryRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = _collectionService.GetCustomCollectionCategoryList(param, commonHeader);
            return result;
        }

        [HttpPost("custom-collection-filter")]
        public async Task<ResponseDetails> CustomCollectionFilter([FromBody] CustomCollectionFilter param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _collectionService.CustomCollectionFilter(param, commonHeader);
            return result;
        }

        [HttpPost("custom-collection-sub-category-list")]
        public async Task<ResponseDetails> CustomCollectionSubCategoryList([FromBody] CustomCollectionFilter param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _collectionService.CustomCollectionSubCategoryList(param, commonHeader);
            return result;
        }

        [HttpPost("custom-collection-item-list")]
        public async Task<ResponseDetails> CustomCollectionItemList([FromBody] CustomCollectionItemListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _collectionService.CustomCollectionItemList(param, commonHeader);
            return result;
        }
    }
}
