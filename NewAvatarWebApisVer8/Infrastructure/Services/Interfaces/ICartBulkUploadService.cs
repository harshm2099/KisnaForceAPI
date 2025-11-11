using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface ICartBulkUploadService
    {
        public Task<ResponseDetails> GetColourBulk();

        public Task<ResponseDetails> GetCategorySizeBulk();
        
        public Task<ResponseDetails> GetSizeBulk(ItemSizeListingParams itemsizelistparams);

        public Task<ResponseDetails> GetOrderTypevalue(OrderTypeListingParams ordertypelistparams);

        public Task<ResponseDetails> BulkUploadVerifyData(BulkUploadVerifyDataParams bulkuploadverifydata_params);

        public Task<ResponseDetails> BulkUploadDelete(BulkUploadDeleteParams bulkuploaddelete_params);

        public Task<ResponseDetails> BulkImportItems(BulkImportItemsParams bulkimportitems_params);

        public Task<ResponseDetails> BulkImportGetItemData(BulkImportGetDataParams bulkimportgetdata_params);

        public Task<ResponseDetails> CheckOutBulkUploadNew(CheckOutBulkUploadnewParams checkoutbulkuploadnew_params);
    }
}
