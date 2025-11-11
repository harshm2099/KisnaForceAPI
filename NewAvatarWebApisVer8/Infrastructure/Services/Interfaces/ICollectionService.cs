using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Models;


namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface ICollectionService
    {
        public ServiceResponse<bool> AddCollectionMapping(CollectionItemMapping CollectionMap);
        public ServiceResponse<IList<MasterItemsrequest>> GetCollectionItemsById(string collectionid);
        public ServiceResponse<bool> EditCollectionMapping(ClsEditCollectionItemMapping CollectionMap);
        public ServiceResponse<IList<CustomCollectionResponse>> GetCustomCollectionList([FromBody]CustomCollectionList param, [FromHeader] CommonHeader header);
        public ServiceResponse<IList<CustomCollectionCategoriesResposne>> GetCustomCollectionCategoryList([FromBody]CollectionCategoryRequest param, [FromHeader] CommonHeader header);
        public Task<ResponseDetails> CustomCollectionFilter([FromBody] CustomCollectionFilter param, [FromHeader] CommonHeader header);
        public Task<ResponseDetails> CustomCollectionSubCategoryList([FromBody] CustomCollectionFilter param, [FromHeader] CommonHeader header);
        public Task<ResponseDetails> CustomCollectionItemList(CustomCollectionItemListRequest param, [FromHeader] CommonHeader header);
    }
}
