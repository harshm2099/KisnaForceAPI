namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class PlainGoldItemFilterParams
    {
        public string data_id { get; set; }
        public string category_id { get; set; }
        public string master_common_id { get; set; }
        public string button_code { get; set; }
        public string btn_cd { get; set; }
    }

    public class PlainGoldItemFilter_Data
    {
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_SubCategory>> sub_category { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_Kt>> dsg_kt { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_ProductTag>> productTags { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_Brand>> brand { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_Gender>> gender { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_ApproxDelivery>> approx_develivery { get; set; }
        public List<PlainGoldItemFilter_Westage> dsg_westage { get; set; }
        public List<PlainGoldItemFilter_Weight> dsg_weight { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_StockFilter>> stock_filter { get; set; }
        public object franchise_store_filter { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_FamilyProductFilter>> familyproduct_filter { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_ExcludeDiscontinueFilter>> excludediscontinue_filter { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_WearViewFilter>> wearview_filter { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_TryOnViewFilter>> tryonview_filter { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_ImageAvailFilter>> imageavail_filter { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_ItemSubCategory>> item_sub_category { get; set; }
        public PlainGoldItemFilter_NamedData<List<object>> item_sub_sub_category { get; set; }
        public object dsg_metalwt { get; set; }
        public object dsg_diamondwt { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_BestSellerData>> best_sellers_data { get; set; }
        public PlainGoldItemFilter_NamedData<List<PlainGoldItemFilter_DesignsData>> designs_data { get; set; }
    }

    public class PlainGoldItemFilter_NamedData<T>
    {
        public string name { get; set; }
        public T data { get; set; }
    }

    public class PlainGoldItemFilter_SubCategory
    {
        public string category_id { get; set; }
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string category_count { get; set; }
    }

    public class PlainGoldItemFilter_Kt
    {
        public string kt { get; set; }
        public string Kt_Count { get; set; }
    }

    public class PlainGoldItemFilter_ProductTag { }

    public class PlainGoldItemFilter_Brand
    {
        public string ItemBrandCommonID { get; set; }
        public string brand_id { get; set; }
        public string brand_name { get; set; }
        public string brand_count { get; set; }
    }

    public class PlainGoldItemFilter_Gender
    {
        public string gender_id { get; set; }
        public string gender_name { get; set; }
        public string gender_count { get; set; }
    }

    public class PlainGoldItemFilter_ApproxDelivery
    {
        public string ItemAproxDay { get; set; }
        public string ItemAproxDay_count { get; set; }
    }

    public class PlainGoldItemFilter_Westage
    {
        public string Westage { get; set; }
    }

    public class PlainGoldItemFilter_Weight
    {
        public string Weight { get; set; }
    }

    public class PlainGoldItemFilter_StockFilter
    {
        public string stock_name { get; set; }
        public string stock_id { get; set; }
    }

    public class PlainGoldItemFilter_FamilyProductFilter
    {
        public string familyproduct_name { get; set; }
        public string familyproduct_id { get; set; }
    }

    public class PlainGoldItemFilter_ExcludeDiscontinueFilter
    {
        public string excludediscontinue_name { get; set; }
        public string excludediscontinue_id { get; set; }
    }

    public class PlainGoldItemFilter_WearViewFilter
    {
        public string view_name { get; set; }
        public string view_id { get; set; }
    }

    public class PlainGoldItemFilter_TryOnViewFilter
    {
        public string view_name { get; set; }
        public string view_id { get; set; }
    }

    public class PlainGoldItemFilter_ImageAvailFilter
    {
        public string imageavail_name { get; set; }
        public string imageavail_id { get; set; }
    }

    public class PlainGoldItemFilter_ItemSubCategory
    {
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string sub_category_count { get; set; }
    }

    public class PlainGoldItemFilter_BestSellerData
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class PlainGoldItemFilter_DesignsData
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }


}
