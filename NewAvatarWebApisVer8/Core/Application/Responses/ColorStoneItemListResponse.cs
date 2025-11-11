using Microsoft.Exchange.WebServices.Data;

namespace NewAvatarWebApis.Core.Application.Responses
{
    public class ColorStoneItemListResponse
    {
        public string? itemId { get; set; }
        public string? categoryId { get; set; }
        public string? itemDescription { get; set; }
        public string? itemCode { get; set; }
        public string? itemName { get; set; }
        public string? itemGenderCommonId { get; set; }
        public string? itemNosePinScrewSts { get; set; }
        public string? subCategoryId { get; set; }
        public string? itemIsSRP { get; set; }
        public string? itemPrice { get; set; }
        public string? approxDeliveryDate { get; set; }
        public string? priceFlag { get; set; }
        public string? itemMrp { get; set; }
        public string? mostOrder_1 { get; set; }
        public string? maxQtySold { get; set; }
        public string? imagePath { get; set; }
        public object? productTags { get; set; }
        public string? mostOrder_2 { get; set; }
        public string? productTagsJson { get; set; }
        public string? stockFilter { get; set; }
        public string? franchiseStore { get; set; }

    }
}
