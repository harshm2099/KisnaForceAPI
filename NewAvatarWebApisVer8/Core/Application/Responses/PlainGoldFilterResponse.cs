using NewAvatarWebApis.Core.Application.DTOs;

namespace NewAvatarWebApis.Core.Application.Responses
{
    public class PlaingoldItemFilterList
    {
        public IList<PlainGoldFilterCategoryList> categoryList { get; set; }
        public IList<PlainGoldFilterDsgKtList> dsgKt { get; set; }
        public IList<PlainGoldFilterProductTagList> productTag { get; set; }
        public IList<PlainGoldFilterBrandList> brand { get; set; }
        public IList<PlainGoldFilterGenderList> gender { get; set; }
        public IList<PlainGoldFilterApproxDeliveryList> approxDelivery { get; set; }
        public IList<PlainGoldFilterPriceList> price { get; set; }
        public IList<PlainGoldFilterDsgWeightList> dsgWeight { get; set; }
        public IList<PlainGoldFilterStockList> hoStock { get; set; }
        public IList<PlainGoldFilterFamilyProductList> familyProduct { get; set; }
        public IList<PlainGoldFilterExcludeDiscontinueList> excludeDiscontinue { get; set; }
        public IList<PlainGoldFilterWearItList> wearIt { get; set; }
        public IList<PlainGoldFilterTryOnList> tryOn { get; set; }
        public IList<PlainGoldFilterImageAvailList> imageAvailable { get; set; }
        public IList<PlainGoldFilterSubCategoryList> subCategory { get; set; }
        public IList<PlainGoldFilterSubSubCategoryList> subSubCategory { get; set; }
        public IList<PlainGoldFilterDsgMetalWtList> metalWt { get; set; }
        public IList<PlainGoldFilterDsgDiamondWtList> diamondWt { get; set; }
        public IList<PlainGoldFilterBestSellerList> bestSeller { get; set; }
        public IList<PlainGoldFilterLatestDesignList> latestDesign { get; set; }
    }

    public class PlainGoldFilterCategoryList
    {
        public string category_id { get; set; }
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string category_count { get; set; }
    }

    public class PlainGoldFilterDsgKtList
    {
        public string kt { get; set; }
        public string kt_count { get; set; }
    }

    public class PlainGoldFilterProductTagList
    {
        public string tag_name { get; set; }
        public string tag_count { get; set; }
    }

    public class PlainGoldFilterBrandList
    {
        public string ItemBrandCommonID { get; set; }
        public string brand_id { get; set; }
        public string brand_name { get; set; }
        public string brand_count { get; set; }
    }

    public class PlainGoldFilterGenderList
    {
        public string gender_id { get; set; }
        public string gender_name { get; set; }
        public string gender_count { get; set; }
    }

    public class PlainGoldFilterApproxDeliveryList
    {
        public string ItemAproxDay { get; set; }
        public string ItemAproxDay_count { get; set; }
    }

    public class PlainGoldFilterPriceList
    {
        public string minprice { get; set; }
        public string maxprice { get; set; }
    }

    public class PlainGoldFilterDsgWeightList
    {
        public string minWt { get; set; }
        public string maxWt { get; set; }
    }

    public class PlainGoldFilterStockList
    {
        public string stock_name { get; set; }
        public string stock_id { get; set; }
    }

    public class PlainGoldFilterFamilyProductList
    {
        public string familyproduct_name { get; set; }
        public string familyproduct_id { get; set; }
    }

    public class PlainGoldFilterExcludeDiscontinueList
    {
        public string excludediscontinue_name { get; set; }
        public string excludediscontinue_id { get; set; }
    }

    public class PlainGoldFilterWearItList
    {
        public string view_name { get; set; }
        public string view_id { get; set; }
    }

    public class PlainGoldFilterTryOnList
    {
        public string view_name { get; set; }
        public string view_id { get; set; }
    }

    public class PlainGoldFilterImageAvailList
    {
        public string imageavail_name { get; set; }
        public string imageavail_id { get; set; }
    }

    public class PlainGoldFilterSubCategoryList
    {
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string sub_category_count { get; set; }
    }

    public class PlainGoldFilterSubSubCategoryList
    {
        public string data { get; set; }
    }

    public class PlainGoldFilterDsgMetalWtList
    {
        public string metalwt { get; set; }
    }

    public class PlainGoldFilterDsgDiamondWtList
    {
        public string diamondwt { get; set; }
    }

    public class PlainGoldFilterBestSellerList
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class PlainGoldFilterLatestDesignList
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
