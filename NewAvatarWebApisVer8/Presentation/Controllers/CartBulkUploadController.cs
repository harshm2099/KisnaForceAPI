using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Services;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CartBulkUploadController
    {
        private readonly ICartBulkUploadService _cartBulkUploadService;

        public CartBulkUploadController(ICartBulkUploadService cartBulkUploadService)
        {
            _cartBulkUploadService = cartBulkUploadService;
        }

        // C# method name follows PascalCase.
        // The route template uses kebab-case for the URL.

        [HttpPost("get-color-bulk")]
        public Task<ResponseDetails> GetColourBulk()
        {
            var result = _cartBulkUploadService.GetColourBulk();
            return result;
        }

        [HttpPost("get-category-size-bulk")]
        public Task<ResponseDetails> GetCategorySizeBulk()
        {
            var result = _cartBulkUploadService.GetCategorySizeBulk();
            return result;
        }

        [HttpPost("get-size-bulk")]
        public Task<ResponseDetails> GetSizeBulk(ItemSizeListingParams itemsizelistparams)
        {
            var result = _cartBulkUploadService.GetSizeBulk(itemsizelistparams);
            return result;
        }

        [HttpPost("get-order-type-value")]
        public Task<ResponseDetails> GetOrderTypevalue(OrderTypeListingParams ordertypelistparams)
        {
            var result = _cartBulkUploadService.GetOrderTypevalue(ordertypelistparams);
            return result;
        }

        [HttpPost("bulk-upload-verify-data")]
        public Task<ResponseDetails> BulkUploadVerifyData(BulkUploadVerifyDataParams bulkuploadverifydata_params)
        {
            var result = _cartBulkUploadService.BulkUploadVerifyData(bulkuploadverifydata_params);
            return result;
        }

        [HttpPost("bulk-upload-delete")]
        public Task<ResponseDetails> BulkUploadDelete(BulkUploadDeleteParams bulkuploaddelete_params)
        {
            var result = _cartBulkUploadService.BulkUploadDelete(bulkuploaddelete_params);
            return result;
        }

        [HttpPost("bulk-import-items")]
        public Task<ResponseDetails> BulkImportItems(BulkImportItemsParams bulkimportitems_params)
        {
            var result = _cartBulkUploadService.BulkImportItems(bulkimportitems_params);
            return result;
        }

        [HttpPost("bulk-import-get-item-data")]
        public Task<ResponseDetails> BulkImportGetItemData(BulkImportGetDataParams bulkimportgetdata_params)
        {
            var result = _cartBulkUploadService.BulkImportGetItemData(bulkimportgetdata_params);
            return result;
        }

        [HttpPost("checkout-bulk-upload-new")]
        public Task<ResponseDetails> CheckOutBulkUploadNew(CheckOutBulkUploadnewParams checkoutbulkuploadnew_params)
        {
            var result = _cartBulkUploadService.CheckOutBulkUploadNew(checkoutbulkuploadnew_params);
            return result;
        }
    }
}
