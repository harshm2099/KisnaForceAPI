namespace NewAvatarWebApis.Core.Application.Responses
{
    public class OrderTrackingDataListResponse
    {
        public string? TrackingType { get; set; }
        public string? TrackCartMstId { get; set; }
        public string? TrackCartLocalID { get; set; }
        public string? TotalRec { get; set; }
        public string? MstSortBY { get; set; }
    }

    public class OrderTrackingSingleDataItemResponse
    {
        public string? TrackType { get; set; }
        public string? TrackId { get; set; }
        public string? CartItemId { get; set; }
        public string? CartId { get; set; }
        public string? TrackCartCommonId { get; set; }
        public string? TrackDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? IsLinkOption { get; set; }
        public string? IconImage { get; set; }
        public string? ParcelId { get; set; }
        public string? CouriorName { get; set; }
        public string? DocketNo { get; set; }
        public string? ParcelCustomerName { get; set; }
        public string? ParcelLink { get; set; }
        public string? AcknowledgmentUrl { get; set; }
        public string? OrderNo { get; set; }
        public string? EmrOrderNo { get; set; }
        public string? OrderDate { get; set; }
    }

    public class OrderTrackingItemDetailDataResponse
    {
        public string? CartAutoId { get; set; }
        public string? CartId { get; set; }
        public string? DataId { get; set; }
        public string? ItemId { get; set; }
        public string? EntDt { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemAproxDay { get; set; }
        public string? ItemSku { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemSoliter { get; set; }
        public string? PlaingoldStatus { get; set; }
        public string? ItemMrp { get; set; }
        public string? ItemPrice { get; set; }
        public string? DistPrice { get; set; }
        public string? DsgSfx { get; set; }
        public string? DsgSize { get; set; }
        public string? DsgKt { get; set; }
        public string? DsgColor { get; set; }
        public string? Star { get; set; }
        public string? CartImg { get; set; }
        public string? ImgCartTitle { get; set; }
        public string? WatchImg { get; set; }
        public string? ImgWatchTitle { get; set; }
        public string? WishCount { get; set; }
        public string? WearitCount { get; set; }
        public string? WearitStatus { get; set; }
        public string? WearitImg { get; set; }
        public string? WearitNoneImg { get; set; }
        public string? WearitColor { get; set; }
        public string? WearitText { get; set; }
        public string? ImgWearitTitle { get; set; }
        public string? WishDefaultImg { get; set; }
        public string? WishFillImg { get; set; }
        public string? ImgWishTitle { get; set; }
        public string? ItemReview { get; set; }
        public string? ItemSize { get; set; }
        public string? ItemKt { get; set; }
        public string? ItemColor { get; set; }
        public string? ItemMetal { get; set; }
        public string? ItemWt { get; set; }
        public string? ItemStone { get; set; }
        public string? ItemStoneWt { get; set; }
        public string? ItemStoneQty { get; set; }
        public string? StarColor { get; set; }
        public string? PriceText { get; set; }
        public string? CartPrice { get; set; }
        public string? ItemColorId { get; set; }
        public string? ItemDetails { get; set; }
        public string? ItemDiamondDetails { get; set; }
        public string? ItemText { get; set; }
        public string? MoreItemDetails { get; set; }
        public string? ItemStock { get; set; }
        public string? CartItemQty { get; set; }
        public string? ItemRemovecartImg { get; set; }
        public string? ItemRemovecardTitle { get; set; }
        public string? RupySymbol { get; set; }
        public string? CategoryId { get; set; }
        public string? ColorCommonId { get; set; }
        public string? SizeCommonId { get; set; }
        public string? ColorCommonName { get; set; }
        public string? SizeCommonName { get; set; }
        public string? ColorCommonName1 { get; set; }
        public string? SizeCommonName1 { get; set; }
        public string? ItemGenderCommonID { get; set; }
        public string? ItemStockQty { get; set; }
        public string? ItemStockColorSizeQty { get; set; }
        public string? VariantCount { get; set; }
        public string? CartCancelQty { get; set; }
        public string? CartCancelDate { get; set; }
        public string? CartCancelBy { get; set; }
        public string? CartCancelSts { get; set; }
        public string? CartCancelName { get; set; }
        public string? ItemTypeCommonID { get; set; }
        public string? ItemNosePinScrewSts { get; set; }
        public string? ImagePath { get; set; }
        public string? Weight { get; set; }
        public string? SelectedColor { get; set; }
        public string? SelectedSize { get; set; }
        public string? SelectedColor1 { get; set; }
        public string? SelectedSize1 { get; set; }
        public string? FieldName { get; set; }
        public string? ColorName { get; set; }
        public string? DefaultColorName { get; set; }
        public string? DefaultColorCode { get; set; }
        public string?  DefaultSizeName { get; set; }
        public object? SizeList { get; set; }
        public object? ColorList { get; set; }
        public object? ItemsColorSizeList { get; set; }
        public object? ItemOrderInstructionList { get; set; }
        public object? ItemOrderCustomInstructionList { get; set; }
        public object? ItemImagesColor { get; set; }
    }
}
