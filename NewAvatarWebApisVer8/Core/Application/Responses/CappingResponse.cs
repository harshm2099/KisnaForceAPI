namespace NewAvatarWebApis.Core.Application.Responses
{
    public class CappingSortByResponse
    {
        public string? SortId { get; set; }
        public string? SortCode { get; set; }
        public string? SortName { get; set; }
        public string? SortMasterId { get; set; }
    }

    public class CappingItemListResponse
    {
        public string? ItemId { get; set; }
        public string? CategoryId { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemGenderCommonID { get; set; }
        public string? ItemNosePinScrewSts { get; set; }
        public string? ItemKt { get; set; }
        public string? SizeId { get; set; }
        public string? ColorId { get; set; }
        public string? IsStockFilter { get; set; }
        public string? PlainGoldStatus { get; set; }
        public string? ApproxDeliveryDate { get; set; }
        public string? ItemBrandText { get; set; }
        public string? ItemIsSRP { get; set; }
        public string? SubCategoryId { get; set; }
        public string? PriceFlag { get; set; }
        public string? ItemMrp { get; set; }
        public string? ImagePath { get; set; }
        public string? MostOrder { get; set; }
        public object? ProductTags { get; set; }
        public string? IsInFranchiseStore { get; set; }
    }
}
