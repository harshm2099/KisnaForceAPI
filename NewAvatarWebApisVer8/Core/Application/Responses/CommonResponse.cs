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
}
