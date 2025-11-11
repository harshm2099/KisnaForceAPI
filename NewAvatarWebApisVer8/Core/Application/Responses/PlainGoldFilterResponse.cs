using NewAvatarWebApis.Core.Application.DTOs;

namespace NewAvatarWebApis.Core.Application.Responses
{
    public class PlaingoldItemFilterList
    {
        public IList<FilterCategoryList> categoryList { get; set; }
        public IList<FilterDsgKtList> dsgKt { get; set; }
        public IList<FilterProductTagList> productTag { get; set; }
        public IList<FilterBrandList> brand { get; set; }
        public IList<FilterGenderList> gender { get; set; }
        public IList<FilterApproxDeliveryList> approxDelivery { get; set; }
        public IList<FilterPriceList> price { get; set; }
        public IList<FilterDsgWeightList> dsgWeight { get; set; }
        public IList<FilterStockList> hoStock { get; set; }
        public IList<FilterFamilyProductList> familyProduct { get; set; }
        public IList<FilterExcludeDiscontinueList> excludeDiscontinue { get; set; }
        public IList<FilterWearItList> wearIt { get; set; }
        public IList<FilterTryOnList> tryOn { get; set; }
        public IList<FilterImageAvailList> imageAvailable { get; set; }
        public IList<FilterSubCategoryList> subCategory { get; set; }
        public IList<FilterSubSubCategoryList> subSubCategory { get; set; }
        public IList<FilterDsgMetalWtList> metalWt { get; set; }
        public IList<FilterDsgDiamondWtList> diamondWt { get; set; }
        public IList<FilterBestSellerList> bestSeller { get; set; }
        public IList<FilterLatestDesignList> latestDesign { get; set; }
    }

    public class FilterCategoryList
    {
        public string category_id { get; set; }
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string category_count { get; set; }
    }

    public class FilterDsgKtList
    {
        public string kt { get; set; }
        public string kt_count { get; set; }
    }

    public class FilterProductTagList
    {
        public string tag_name { get; set; }
        public string tag_count { get; set; }
    }

    public class FilterBrandList
    {
        public string ItemBrandCommonID { get; set; }
        public string brand_id { get; set; }
        public string brand_name { get; set; }
        public string brand_count { get; set; }
    }

    public class FilterGenderList
    {
        public string gender_id { get; set; }
        public string gender_name { get; set; }
        public string gender_count { get; set; }
    }

    public class FilterApproxDeliveryList
    {
        public string ItemAproxDay { get; set; }
        public string ItemAproxDay_count { get; set; }
    }

    public class FilterPriceList
    {
        public string minprice { get; set; }
        public string maxprice { get; set; }
    }

    public class FilterDsgWeightList
    {
        public string minWt { get; set; }
        public string maxWt { get; set; }
    }

    public class FilterStockList
    {
        public string stock_name { get; set; }
        public string stock_id { get; set; }
    }

    public class FilterFamilyProductList
    {
        public string familyproduct_name { get; set; }
        public string familyproduct_id { get; set; }
    }

    public class FilterExcludeDiscontinueList
    {
        public string excludediscontinue_name { get; set; }
        public string excludediscontinue_id { get; set; }
    }

    public class FilterWearItList
    {
        public string view_name { get; set; }
        public string view_id { get; set; }
    }

    public class FilterTryOnList
    {
        public string view_name { get; set; }
        public string view_id { get; set; }
    }

    public class FilterImageAvailList
    {
        public string imageavail_name { get; set; }
        public string imageavail_id { get; set; }
    }

    public class FilterSubCategoryList
    {
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string sub_category_count { get; set; }
    }

    public class FilterSubSubCategoryList
    {
        public string data { get; set; }
    }

    public class FilterDsgMetalWtList
    {
        public string metalwt { get; set; }
    }

    public class FilterDsgDiamondWtList
    {
        public string diamondwt { get; set; }
    }

    public class FilterBestSellerList
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class FilterLatestDesignList
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
