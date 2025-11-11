using Newtonsoft.Json;

namespace NewAvatarWebApis.Core.Application.Responses
{
        public class SolitaireDetailList
        {
            public IList<DetailList> details { get; set; }
            public IList<ImageList> images { get; set; }
        }

        public class DetailList
        {
            public string? ItemId { get; set; }
            public string? CategoryName { get; set; }
            public string? ItemSoliter { get; set; }
            public string? PlaingoldStatus { get; set; }
            public string? ItemName { get; set; }
            public string? ItemSku { get; set; }
            public string? ItemDescription { get; set; }
            public string? ItemMrp { get; set; }
            public string? ItemDiscount { get; set; }
            public string? ItemPrice { get; set; }
            public string? RetailPrice { get; set; }
            public string? DistPrice { get; set; }
            public string? Uom { get; set; }
            public string? Star { get; set; }
            public string? CartImg { get; set; }
            public string? ImgCartTitle { get; set; }
            public string? WatchImg { get; set; }
            public string? ImgWatchTitle { get; set; }
            public string? WearitCount { get; set; }
            public string? WearitStatus { get; set; }
            public string? WearitImg { get; set; }
            public string? WearitNoneImg { get; set; }
            public string? WearitColor { get; set; }
            public string? WearitText { get; set; }
            public string? ImgWearitTitle { get; set; }
            public string? TryonCount { get; set; }
            public string? TryonStatus { get; set; }
            public string? TryonImg { get; set; }
            public string? TryonNoneImg { get; set; }
            public string? TryonText { get; set; }
            public string? TryonTitle { get; set; }
            public string? TryonAndroidPath { get; set; }
            public string? TryonIosPath { get; set; }
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
            public string? ItemMetalCommonId { get; set; }
            public string? PriceText { get; set; }
            public string? CartPrice { get; set; }
            public string? ItemColorId { get; set; }
            public string? ItemDetails { get; set; }
            public string? ItemText { get; set; }
            public string? MoreItemDetails { get; set; }
            public string? ItemStock { get; set; }
            public string? CartItemQty { get; set; }
            public string? RupySymbol { get; set; }
            public string? VariantCount { get; set; }
            public string? CartId { get; set; }
            public string? ItemGenderCommonId { get; set; }
            public string? CartAutoId { get; set; }
            public string? ItemStockQty { get; set; }
            public string? ItemStockColorsizeQty { get; set; }
            public string? CategoryId { get; set; }
            public string? ItemNosePinScrewSts { get; set; }
            public string? ItemAproxDayCommonId { get; set; }
            public string? ItemBrandCommonId { get; set; }
            public string? ItemIllumine { get; set; }
            public string? CustomNote { get; set; }
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
            public object? SizeList { get; set; }
            public object? ColorList { get; set; }
            public object? ItemsColorSizeList { get; set; }
            public object? ItemOrderInstructionList { get; set; }
            public object? ItemOrderCustomInstructionList { get; set; }
            public object? ItemImagesColor { get; set; }
        }

        public class ImageList
        {
            public string? imageViewName { get; set; }
            public string? imagePath { get; set; }
        }

        public class DiamondGoldCappingResponse
        {
            public string? Type { get; set; }
            public string? CategoryID { get; set; }
            public string? CategoryName { get; set; }
            public string? SubCategoryID { get; set; }
            public string? SubCategoryName { get; set; }
            public string? ComplexityID { get; set; }
            public string? ComplexityName { get; set; }
            public string? SizeID { get; set; }
            public string? SizeName { get; set; }
            public string? ColorID { get; set; }
            public string? ColorName { get; set; }
            public string? MinQuantity { get; set; }
            public string? Quantity { get; set; }
            public string? FromGrams { get; set; }
            public string? ToGrams { get; set; }
            public string? FromPrice { get; set; }
            public string? ToPrice { get; set; }
            public string? Amount { get; set; }
            public string? StockQtyFlag { get; set; }
            public string? AvailableQty { get; set; }
            public string? RemainCapping { get; set; }
    }
}
