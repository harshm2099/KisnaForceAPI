namespace NewAvatarWebApis.Core.Application.Responses
{
    public class CommonItemFilterResponse
    {
            public IList<CommonFilterCategoryList> categoryList { get; set; }
            public IList<CommonFilterDsgKtList> dsgKt { get; set; }
            public IList<CommonFilterProductTagList> productTag { get; set; }
            public IList<CommonFilterBrandList> brand { get; set; }
            public IList<CommonFilterGenderList> gender { get; set; }
            public IList<CommonFilterApproxDeliveryList> approxDelivery { get; set; }
            public IList<CommonFilterPriceList> price { get; set; }
            public IList<CommonFilterHOStockList> hoStock { get; set; }
            public IList<CommonFilterFranchiseStockList> franchiseStock { get; set; }
            public IList<CommonFilterFamilyProductList> familyProduct { get; set; }
            public IList<CommonFilterExcludeDiscontinueList> excludeDiscontinue { get; set; }
            public IList<CommonFilterWearItList> wearIt { get; set; }
            public IList<CommonFilterTryOnList> tryOn { get; set; }
            public IList<CommonFilterDsgMetalWtList> metalWt { get; set; }
            public IList<CommonFilterDsgDiamondWtList> diamondWt { get; set; }
            public IList<CommonFilterBestSellerList> bestSeller { get; set; }
            public IList<CommonFilterLatestDesignList> latestDesign { get; set; }
        }

        public class CommonFilterCategoryList
        {
            public string category_id { get; set; }
            public string sub_category_id { get; set; }
            public string sub_category_name { get; set; }
            public string category_count { get; set; }
        }

        public class CommonFilterDsgKtList
        {
            public string kt { get; set; }
            public string Kt_count { get; set; }
        }

        public class CommonFilterPriceList
        {
            public string amount { get; set; }
        }

        public class CommonFilterDsgMetalWtList
        {
            public string metalwt { get; set; }
        }

        public class CommonFilterDsgDiamondWtList
        {
            public string diamondwt { get; set; }
        }

        public class CommonFilterProductTagList
        {
            public string tag_name { get; set; }
            public string tag_count { get; set; }
        }

        public class CommonFilterBrandList
        {
            public string brand_id { get; set; }
            public string brand_name { get; set; }
            public string brand_count { get; set; }
        }

        public class CommonFilterGenderList
        {
            public string gender_id { get; set; }
            public string gender_name { get; set; }
            public string gender_count { get; set; }
        }

        public class CommonFilterApproxDeliveryList
        {
            public string ItemAproxDay { get; set; }
            public string ItemAproxDay_count { get; set; }
        }

        public class CommonFilterHOStockList
        {
            public string stock_name { get; set; }
            public string stock_id { get; set; }
        }

        public class CommonFilterFranchiseStockList
        {
            public string stock_name { get; set; }
            public string stock_id { get; set; }
        }

        public class CommonFilterBestSellerList
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public class CommonFilterLatestDesignList
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public class CommonFilterFamilyProductList
        {
            public string familyproduct_name { get; set; }
            public string familyproduct_id { get; set; }
        }

        public class CommonFilterExcludeDiscontinueList
        {
            public string excludediscontinue_name { get; set; }
            public string excludediscontinue_id { get; set; }
        }

        public class CommonFilterWearItList
        {
            public string view_name { get; set; }
            public string view_id { get; set; }
        }

        public class CommonFilterTryOnList
        {
            public string view_name { get; set; }
            public string view_id { get; set; }
        }   
}
