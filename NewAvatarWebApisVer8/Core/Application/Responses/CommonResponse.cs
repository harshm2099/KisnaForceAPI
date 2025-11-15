namespace NewAvatarWebApis.Core.Application.Responses
{
    public class CommonResponse
    {
        public string? status { get; set; }
        public string? status_code { get; set; }
        public bool? success { get; set; }
        public string? message { get; set; }
        public string? error { get; set; }
        public string? changePasswordRequired { get; set; }
        public string? changePinRequired { get; set; }
        public string? token { get; set; }
        public string? loader_img { get; set; }
        public object? data { get; set; }
        public object? audiodata { get; set; }
        public string? holdmsg { get; set; }
        public string? timeid { get; set; }
        public string? smsmsg { get; set; }
        public string? OTP { get; set; }
        public string? emailmsg { get; set; }
        public string? emailsubject { get; set; }
    }

    public class FranchiseWiseStockResponse
    {
        public string? AvailableStock { get; set; }
        public string? BranchCode { get; set; }
        public string? FranchiseName { get; set; }
        public string? FranchiseAddress { get; set; }
        public object? Details { get; set; }
    }

    public class FranchiseWiseStockDetails
    {
        public string? ItemName { get; set; }
        public string? ItemImagePath { get; set; }
        public string? ItemSize { get; set; }
        public string? ItemColor { get; set; }
        public string? ItemAvailableStock { get; set; }
        public string? ItemBrand { get; set; }
    }

    public class StockWiseItemDataResponse
    {
        public object? Data { get; set; }
    }

    public class PopularItemsResponse
    {
        public string? ItemId { get; set; }
        public string? CategoryId { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemGenderCommonId { get; set; }
        public string? ItemNosePinScrewSts { get; set; }
        public string? SubCategoryId { get; set; }
        public string? PriceFlag { get; set; }
        public string? ItemMrp { get; set; }
        public string? MaxQtySold { get; set; }
        public string? ImagePath { get; set; }
        public string? MostOrder { get; set; }
        public string? TagName { get; set; }
        public string? TagColor { get; set; }
        public string? FranchiseStore { get; set; }
        public string? CurrentPage { get; set; }
        public string? LastPage { get; set; }
        public string? TotalItems { get; set; }
    }

    public class TopRecommandedItemsResponse
    {
        public string? ItemId { get; set; }
        public string? CategoryId { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemGenderCommonId { get; set; }
        public string? ItemNosePinScrewSts { get; set; }
        public string? PlaingoldStatus { get; set; }
        public string? SubCategoryId { get; set; }
        public string? PriceFlag { get; set; }
        public string? ItemMrp { get; set; }
        public string? MaxQtySold { get; set; }
        public string? ImagePath { get; set; }
        public string? MostOrder { get; set; }
        public object? ProductTags { get; set; }
        public string? IsInFranchiseStore { get; set; }
    }

        public class PopularItemsFilterList
        {
            public IList<FilterCategoryList> categoryList { get; set; }
            public IList<FilterDsgKtList> dsgKt { get; set; }
            public IList<FilterAmountList> dsgAmount { get; set; }
            public IList<FilterDsgMetalWtList> dsgMetalWeight { get; set; }
            public IList<FilterDsgDiamondWtList> dsgDiamondWeight { get; set; }
            public IList<FilterProductTagList> productTags { get; set; }
            public IList<FilterBrandList> brand { get; set; }
            public IList<FilterGenderList> gender { get; set; }
            public IList<FilterApproxDeliveryList> approxDelivery { get; set; }
            public IList<FilterStockList> stockFilter { get; set; }
            public IList<FilterSubCategoryList> itemSubCategory { get; set; }
            public IList<FilterSubSubCategoryList> itemSubSubCategory { get; set; }
            public IList<FilterFamilyProductList> familyProduct { get; set; }
            public IList<FilterExcludeDiscontinueList> excludeDiscontinue { get; set; }
            public IList<FilterWearViewList> wearView { get; set; }
            public IList<FilterTryViewList> tryView { get; set; }
        }

        public class FilterCategoryList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? categoryId { get; set; }
            public string? subCategoryId { get; set; }
            public string? subCategoryName { get; set; }
            public string? subCategoryCount { get; set; }
        }

        public class FilterDsgKtList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? KT { get; set; }
            public string? KtCount { get; set; }
        }

        public class FilterAmountList
        {
            public string? filterName { get; set; }
            public string? minAmount { get; set; }
            public string? maxAmount { get; set; }
        }

        public class FilterDsgMetalWtList
        {
            public string? filterName { get; set; }
            public string? minMetalweight { get; set; }
            public string? maxMetalWeight { get; set; }
        }

        public class FilterDsgDiamondWtList
        {
            public string? filterName { get; set; }
            public string? minDiamondWeight { get; set; }
            public string? maxDiamondWeight { get; set; }
        }

        public class FilterProductTagList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? tagName { get; set; }
            public string? tagCount { get; set; }
        }

        public class FilterBrandList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? brandId { get; set; }
            public string? brandName { get; set; }
            public string? brandCount { get; set; }
    }

        public class FilterGenderList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? genderId { get; set; }
            public string? genderName { get; set; }
            public string? genderCount { get; set; }
        }

        public class FilterApproxDeliveryList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? ItemAproxDays { get; set; }
            public string? ItemAproxDayCount { get; set; }
        }

        public class FilterStockList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? stockName { get; set; }
            public string? stockId { get; set; }
        }

        public class FilterSubCategoryList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? itemSubCategoryId { get; set; }
            public string? itemSubCategoryName { get; set; }
            public string? itemSubCategorycounts { get; set; }
        }

        public class FilterSubSubCategoryList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? itemSubSubCategoryId { get; set; }
            public string? itemSubSubCategoryName { get; set; }
            public string? itemSubSubCategorycounts { get; set; }
        }

        public class FilterFamilyProductList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? familyproductName { get; set; }
            public string? familyproductId { get; set; }
        }

        public class FilterExcludeDiscontinueList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? excludediscontinueName { get; set; }
            public string? excludediscontinueId { get; set; }
        }

        public class FilterWearViewList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? viewName { get; set; }
            public string? viewId { get; set; }
        }

        public class FilterTryViewList
        {
            public string? filterName { get; set; }
            public string? name { get; set; }
            public string? viewName { get; set; }
            public string? viewId { get; set; }
        }

        public class PieceVerifyResponse
        {
            public string? Status { get; set; }
            public string? Message { get; set; }
            public object? Data { get; set; }
        }

        public class PieceVerifyExcelResponse
        {
            public string? Date { get; set; }
            public string? ItemName { get; set; }
            public string? ItemDesc { get; set; }
            public string? BagDiamondQuality { get; set; }
            public string? CurrentDiamondQuality { get; set; }
            public string? BagMRP { get; set; }
            public string? CurrentMRP { get; set; }
            public string? BagBrand { get; set; }
            public string? CurrentBrand { get; set; }
            public string? BagQuantity { get; set; }
            public string? CurrentQuantity { get; set; }
            public string? BagTotalWeight { get; set; }
            public string? CurrentTotalWeight { get; set; }
            public string? BagDiamondWeight { get; set; }
            public string? CurrentDiamondWeight { get; set; }
            public string? IGINo { get; set; }
            public string? Bagno { get; set; }
            public string? HUID { get; set; }
            public string? Lab { get; set; }
            public string? ItemIsSRP { get; set; }
            public string? COCD { get; set; }
        }

        public class FileResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public string FilePath { get; set; } = string.Empty;
            public string FileName { get; set; } = string.Empty;
        }

        public class CartItemResponse
        {
            public string? CartAutoId { get; set; }
            public string? LabourPer { get; set; }
            public string? DislabourPer { get; set; }
            public string? Plaingoldstatus { get; set; }
            public string? ExtraGoldWeight { get; set; }
            public string? ExtraGoldPrice { get; set; }
            public string? CartId { get; set; }
            public string? ItemDAproxDay { get; set; }
            public string? DataId { get; set; }
            public string? DsgKt { get; set; }
            public string? ItemId { get; set; }
            public string? EntDate { get; set; }
            public string? ItemCode { get; set; }
            public string? ItemName { get; set; }
            public string? ItemSku { get; set; }
            public string? ItemDescription { get; set; }
            public string? ItemSoliter { get; set; }
            public string? IsStock { get; set; }
            public string? ProductItemCustomRemarksStatus { get; set; }
            public string? ItemBrandCommonID { get; set; }
            public string? ItemPlainGold { get; set; }
            public string? ItemSoliterSts { get; set; }
            public string? ItemMrp { get; set; }
            public string? ItemPrice { get; set; }
            public string? DistPrice { get; set; }
            public string? PriceText { get; set; }
            public string? CartPrice { get; set; }
            public string? ImagePath { get; set; } 
            public string? DsgSfx { get; set; }
            public string? DsgSize { get; set; }
            public string? DsgColor { get; set; } 
            public string? Star { get; set; }
            public string? CartImg { get; set; }
            public string? ImgCartTitle { get; set; } 
            public string? WatchImg { get; set; } 
            public string? ImgWatchTitle { get; set; } 
            public string? WishCount { get; set; }
            public string? ImgWatchTitle2 { get; set; } 
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
            public string? itemReview { get; set; }
            public string? ItemSize { get; set; } 
            public string? ItemKt { get; set; } 
            public string? ItemColor { get; set; } 
            public string? ItemMetal { get; set; } 
            public string? ItemWt { get; set; }
            public string? ItemStone { get; set; } 
            public string? ItemStoneWt { get; set; }
            public string? ItemStoneQty { get; set; }
            public string? starColor { get; set; } 
            public string? ItemColorId { get; set; }
            public string? ItemDetails { get; set; } 
            public string? ItemDiamondDetails { get; set; }
            public string? ItemText { get; set; }
            public string? MoreItemDetails { get; set; } 
            public string? ItemStock { get; set; }
            public string? CartItemQty { get; set; }
            public string? ItemMetalCommonID { get; set; }
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
            public string? ItemStockColorsizeQty { get; set; }
            public string? VariantCount { get; set; }
            public string? CartCancelQty { get; set; }
            public string? CartCancelDate { get; set; }
            public string? CartCancelBy { get; set; }
            public string? CartCancelSts { get; set; } 
            public string? CartCancelName { get; set; } 
            public string? ItemTypeCommonID { get; set; }
            public string? ItemNosePinScrewSts { get; set; }
            public string? PriceFlag { get; set; } 
            public string? SNMCCFlag { get; set; }
            public string? ItemFranchiseSts { get; set; } 
            public string? ItemAproxDayCommonID { get; set; }
            public string? ItemAproxDay { get; set; } 
            public string? CartItemID { get; set; }
            public string? IndentNumber { get; set; } 
            public string? ItemIllumine { get; set; } 
            public string? ItemIsSRP { get; set; } 
            public string? IsAutoInserted { get; set; }
            public string? IsCOOInserted { get; set; } 
            public string? ItemValidSts { get; set; }
            public string? ItemSizeAvailable { get; set; }
            public string? ItemSubCtgCommonID { get; set; }
            public string? IsConsumerOrder { get; set; }
            public string? IsSolitaireAvailable { get; set; } 
            public string? Weight { get; set; } 
            public string? TotalLabourPer { get; set; }
            public string? FranDiamondPrice { get; set; }
            public string? Fran_GoldPrice { get; set; }
            public string? FranPlatinumPrice { get; set; }
            public string? FranLabourPrice { get; set; }
            public string? FranMetalPrice { get; set; }
            public string? FranOtherPrice { get; set; }
            public string? FranStonePrice { get; set; }
            public string? FieldName { get; set; } 
            public string? SelectedSize { get; set; }
            public string? DefaultSizeName { get; set; }
            public object? SizeList { get; set; }
            public object? ColorList { get; set; } 
            public object? ItemsColorSizeList { get; set; }
            public object? ItemOrderCustomInstructionList { get; set; } 
            public object? ItemOrderInstructionList { get; set; } 
            public object? ItemImagesColor { get; set; } 
            public object? ProductTags { get; set; } 
            public object? ApproxDays { get; set; } 
            public string? CartSoliStkNo { get; set; } 
            public string? IsEarPiercing { get; set; } 
            public string? IsSolitaireCombo { get; set; } 
            public string? SelectedColor { get; set; }
            public string? SelectedColor1 { get; set; } 
            public string? SelectedSize1 { get; set; } 
            public string? ColorName { get; set; } 
            public string? DefaultColorName { get; set; }
            public string? DefaultColorCode { get; set; }
        }
}
