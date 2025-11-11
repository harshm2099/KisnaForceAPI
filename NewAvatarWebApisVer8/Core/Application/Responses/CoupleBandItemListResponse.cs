namespace NewAvatarWebApis.Core.Application.Responses
{
    public class CoupleBandItemListResponse
    {
        public string? itemId { get; set; }
        public string? categoryId { get; set; }
        public string? subCategoryId { get; set; }
        public string? itemCode { get; set; }
        public string? itemName { get; set; }
        public string? itemDescription { get; set; }
        public string? itemGenderCommonID { get; set; }
        public string? itemNosePinScrewSts { get; set; }
        public string? itemMrp { get; set; }
        public string? itemIsSrp { get; set; }
        public string? itemPrice { get; set; }
        public string? priceFlag { get; set; }
        public string? isStockFilter { get; set; }
        public string? maxQtySold { get; set; }
        public string? mostOrder_1 { get; set; }
        public string? mostOrder_2 { get; set; }
        public string? isInFranchiseStore { get; set; }
        public string? imagePath { get; set; }
        public object? productTags { get; set; }
        public string? approxDeliveryDate { get; set; }
        public string? inStockDeliveryDate { get; set; }
    }
}
