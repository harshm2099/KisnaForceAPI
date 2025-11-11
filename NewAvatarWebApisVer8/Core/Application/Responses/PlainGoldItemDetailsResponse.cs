using NewAvatarWebApis.Core.Application.DTOs;
using System.Text.Json.Serialization;

namespace NewAvatarWebApis.Core.Application.Responses
{
    public class PlainGoldItemDetailList
    {
        public IList<PlainGoldDetailList> details { get; set; }
        public IList<PlainGoldImageList> images { get; set; }
    }

    public class PlainGoldDetailList
    {
        public string? ItemId { get; set; }
        public string? ItemDisLabourPer { get; set; }
        public string? ItemCtgCommonID { get; set; }
        public string? ItemCd { get; set; }
        public string? LabourPer { get; set; }
        public string? ItemSoliter { get; set; }
        public string? ItemAproxDay { get; set; }
        public string? ItemDAproxDay { get; set; }
        public string? PlainGoldStatus { get; set; }
        public string? ItemName { get; set; }
        public string? ItemSku { get; set; }
        public string? ItemDescription { get; set; }
        public string? DsgKt { get; set; }
        public string? ItemDiscount { get; set; }
        public string? ItemPrice { get; set; }
        public string? RetailPrice { get; set; }
        public string? DistPrice { get; set; }
        public string? Uom { get; set; }
        public string? ItemBrandCommonID { get; set; }
        public string? Star { get; set; }
        public string? CartImg { get; set; }
        public string? ImgCartTitle { get; set; }
        public string? WatchImg { get; set; }
        public string? ImgWatchTitle { get; set; }
        public string? WearItCount { get; set; }
        public string? WearItStatus { get; set; }
        public string? WearItImg { get; set; }
        public string? WearItNoneImg { get; set; }
        public string? WearItColor { get; set; }
        public string? WearItText { get; set; }
        public string? ImgWearItTitle { get; set; }
        public string? TryOnCount { get; set; }
        public string? TryOnStatus { get; set; }
        public string? TryOnImg { get; set; }
        public string? TryOnNoneImg { get; set; }
        public string? TryOnText { get; set; }
        public string? TryOnTitle { get; set; }
        public string? TryOnAndroidPath { get; set; }
        public string? TryOnIosPath { get; set; }
        public string? WishCount { get; set; }
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
        public string? ItemMetalCommonID { get; set; }
        public string? CartPrice { get; set; }
        public string? ItemDPrice { get; set; }
        public string? ItemColorId { get; set; }
        public string? ItemDetails { get; set; }
        public string? ItemDiamondDetails { get; set; }
        public string? ItemText { get; set; }
        public string? MoreItemDetails { get; set; }
        public string? ItemStock { get; set; }
        public string? CartItemQty { get; set; }
        public string? RupySymbol { get; set; }
        public string? VariantCount { get; set; }
        public string? CartId { get; set; }
        public string? ItemGenderCommonID { get; set; }
        public string? ItemStockQty { get; set; }
        public string? ItemStockColorSizeQty { get; set; }
        public string? CategoryId { get; set; }
        public string? ItemNosePinScrewSts { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public string? ItemAproxDayCommonID { get; set; }
        public string? ItemPlainGold { get; set; }
        public string? ItemSoliterSts { get; set; }
        public string? ItemSubCtgName { get; set; }
        public string? ItemSubCtgId { get; set; }
        public string? ItemSubCtgNm { get; set; }
        public string? ItemSubSubCtgName { get; set; }
        public string? Prev { get; set; }
        public string? Next { get; set; }
        public string? DataCollection1 { get; set; }
        public string? ProductTags { get; set; }
        public string? SelectedColor { get; set; }
        public string? SelectedSize { get; set; }
        public string? SelectedColor1 { get; set; }
        public string? SelectedSize1 { get; set; }
        public string? FieldName { get; set; }
        public string? ColorName { get; set; }
        public string? DefaultColorName { get; set; }
        public string? DefaultColorCode { get; set; }
        public string? DefaultSizeName { get; set; }
        public string? FinalDMrp { get; set; }
        public string? ItemMrp { get; set; }
        public string? ItemMrpWithoutGst { get; set; }
        public string? GoldPrice { get; set; }
        public string? GoldPriceNew { get; set; }
        public string? Collections { get; set; }
        public string? ApproxDelivery { get; set; }
        public string? Weight { get; set; }
        public string? TotalLabourPer { get; set; }
        public string? GstPrice { get; set; }
        public string? MetalPrice { get; set; }
        public string? TotalLabourPerDist { get; set; }
        public string? GstPriceDist { get; set; }
        public string? MetalPriceDist { get; set; }
        public object? SizeList { get; set; }
        public object? ColorList { get; set; }
        public object? ItemsColorSizeList { get; set; }
        public object? ItemOrderInstructionList { get; set; }
        public object? ItemOrderCustomInstructionList { get; set; }
        public object? ItemImagesColor { get; set; }
        public object? ApproxDays { get; set; }
        public string? StockColorId { get; set; }
        public string? StockSizeId { get; set; }
        public string? SubCollection { get; set; }
        public string? FranchiseWiseStock { get; set; }

    }

    public class PlainGoldImageList
    {
        public string? imageViewName { get; set; }
        public string? imagePath { get; set; }
    }

    public class ExtraGoldRateWiseRateResponse
    {
        public string? Data { get; set; }
    }
}
