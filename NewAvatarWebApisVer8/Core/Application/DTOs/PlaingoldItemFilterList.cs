using System.Collections.Generic;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class PlaingoldItemFilterParams
    {
        public int data_id { get; set; }
        public int category_id { get; set; }
        public int master_common_id { get; set; }
        public string button_code { get; set; }
        public string btn_cd { get; set; }
    }

    public class PlaingoldItemFilterData
    {
        public Plaingold_SubCategoryFilter sub_category { get; set; } = new Plaingold_SubCategoryFilter();
        public Plaingold_DsgKtFilter dsg_kt { get; set; } = new Plaingold_DsgKtFilter();
        public Plaingold_ProductTagFilter productTags { get; set; } = new Plaingold_ProductTagFilter();
        public Plaingold_BrandFilter brand { get; set; } = new Plaingold_BrandFilter();
        public Plaingold_GenderFilter gender { get; set; } = new Plaingold_GenderFilter();
        public Plaingold_ApproxDeliveryFilter approx_develivery { get; set; } = new Plaingold_ApproxDeliveryFilter();
        public List<Plaingold_DsgWestageFilter> dsg_westage { get; set; } = new List<Plaingold_DsgWestageFilter>();
        public List<Plaingold_DsgWeightFilter> dsg_weight { get; set; } = new List<Plaingold_DsgWeightFilter>();
        public Plaingold_StockFilter stock_filter { get; set; } = new Plaingold_StockFilter();
        public Plaingold_FamilyProductFilter familyproduct_filter { get; set; } = new Plaingold_FamilyProductFilter();
        public Plaingold_ExcludeDiscontinueFilter excludediscontinue_filter { get; set; } = new Plaingold_ExcludeDiscontinueFilter();
        public Plaingold_WearViewFilter wearview_filter { get; set; } = new Plaingold_WearViewFilter();
        public Plaingold_TryOnViewFilter tryonview_filter { get; set; } = new Plaingold_TryOnViewFilter();
        public Plaingold_ImageAvailFilter imageavail_filter { get; set; } = new Plaingold_ImageAvailFilter();
        public PlaingoldItemSubCategoryFilter item_sub_category { get; set; } = new PlaingoldItemSubCategoryFilter();
        public PlaingoldItemSubSubCategoryFilter item_sub_sub_category { get; set; } = new PlaingoldItemSubSubCategoryFilter();
        public Plaingold_BestSellersFilter best_sellers_data { get; set; } = new Plaingold_BestSellersFilter();
        public Plaingold_DesignsFilter designs_data { get; set; } = new Plaingold_DesignsFilter();
        //public List<Plaingold_DsgAmountFilter> dsg_amount { get; set; } = new List<Plaingold_DsgAmountFilter>();
        public List<Plaingold_DsgMetalWtFilter> dsg_metalwt { get; set; } = new List<Plaingold_DsgMetalWtFilter>();
        public List<Plaingold_DsgDiamondWtFilter> dsg_diamondwt { get; set; } = new List<Plaingold_DsgDiamondWtFilter>();
    }

    public class Plaingold_SubCategoryFilter
    {
        public string name { get; set; } = "Collection";
        public List<PlaingoldItemFilterSubCategory> data { get; set; } = new List<PlaingoldItemFilterSubCategory>();
    }

    public class Plaingold_DsgKtFilter
    {
        public string name { get; set; } = "KT";
        public List<PlaingoldItemFilterDsgKt> data { get; set; } = new List<PlaingoldItemFilterDsgKt>();
    }

    public class Plaingold_DsgWestageFilter
    {
        public string Westage { get; set; }
    }

    public class Plaingold_DsgWeightFilter
    {
        public string Weight { get; set; }
    }

    public class Plaingold_DsgMetalWtFilter
    {
        public string metalwt { get; set; }
    }

    public class Plaingold_DsgDiamondWtFilter
    {
        public string diamondwt { get; set; }
    }

    public class Plaingold_ProductTagFilter
    {
        public string name { get; set; } = "Product Tag";
        public List<PlaingoldItemFilterProductTag> data { get; set; } = new List<PlaingoldItemFilterProductTag>();
    }

    public class Plaingold_BrandFilter
    {
        public string name { get; set; } = "Brand";
        public List<PlaingoldItemFilterBrand> data { get; set; } = new List<PlaingoldItemFilterBrand>();
    }

    public class Plaingold_GenderFilter
    {
        public string name { get; set; } = "Gender";
        public List<PlaingoldItemFilterGender> data { get; set; } = new List<PlaingoldItemFilterGender>();
    }

    public class Plaingold_ApproxDeliveryFilter
    {
        public string name { get; set; } = "Approx Delivery Days";
        public List<PlaingoldItemFilterApproxDelivery> data { get; set; } = new List<PlaingoldItemFilterApproxDelivery>();
    }

    public class Plaingold_StockFilter
    {
        public string name { get; set; } = "Stock";
        public List<PlaingoldItemFilterStock> data { get; set; } = new List<PlaingoldItemFilterStock>();
    }

    public class PlaingoldItemSubCategoryFilter
    {
        public string name { get; set; } = "Complexity";
        public List<PlaingoldItemSubCategory> data { get; set; } = new List<PlaingoldItemSubCategory>();
    }

    public class PlaingoldItemSubSubCategoryFilter
    {
        public string name { get; set; } = "Sub Complexity";
        public List<PlaingoldItemSubSubCategory> data { get; set; } = new List<PlaingoldItemSubSubCategory>();
    }

    public class Plaingold_BestSellersFilter
    {
        public string name { get; set; } = "Best Sellers";
        public List<PlaingoldItemFilterNameValue> data { get; set; } = new List<PlaingoldItemFilterNameValue>();
    }

    public class Plaingold_DesignsFilter
    {
        public string name { get; set; } = "The latest design released in";
        public List<PlaingoldItemFilterNameValue> data { get; set; } = new List<PlaingoldItemFilterNameValue>();
    }

    public class Plaingold_FamilyProductFilter
    {
        public string name { get; set; } = "Family Product";
        public List<PlaingoldItemFilterFamilyProduct> data { get; set; } = new List<PlaingoldItemFilterFamilyProduct>();
    }

    public class Plaingold_ExcludeDiscontinueFilter
    {
        public string name { get; set; } = "Exclude Discontinue";
        public List<PlaingoldItemFilterExcludeDiscontinue> data { get; set; } = new List<PlaingoldItemFilterExcludeDiscontinue>();
    }

    public class Plaingold_WearViewFilter
    {
        public string name { get; set; } = "Wear It";
        public List<PlaingoldItemFilterView> data { get; set; } = new List<PlaingoldItemFilterView>();
    }

    public class Plaingold_TryOnViewFilter
    {
        public string name { get; set; } = "Try On";
        public List<PlaingoldItemFilterView> data { get; set; } = new List<PlaingoldItemFilterView>();
    }

    public class Plaingold_ImageAvailFilter
    {
        public string name { get; set; } = "Image Available";
        public List<PlaingoldItemFilterImageAvail> data { get; set; } = new List<PlaingoldItemFilterImageAvail>();
    }

    public class PlaingoldItemFilterSubCategory
    {
        public string category_id { get; set; }
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string category_count { get; set; }
    }

    public class PlaingoldItemFilterDsgKt
    {
        public string kt { get; set; }
        public string Kt_Count { get; set; }
    }

    public class PlaingoldItemFilterWestage
    {
        public decimal minprice { get; set; }
        public decimal maxprice { get; set; }
    }

    public class PlaingoldItemFilterWeight
    {
        public decimal minWt { get; set; }
        public decimal maxWt { get; set; }
    }

    public class PlaingoldItemFilterDsgAmount
    {
        public string minprice { get; set; }
        public string maxprice { get; set; }
    }

    public class PlaingoldItemFilterDsgMetalWt
    {
        public string minmetalwt { get; set; }
        public string maxmetalwt { get; set; }
    }

    public class PlaingoldItemFilterDsgDiamondWt
    {
        public string mindiawt { get; set; }
        public string maxdiawt { get; set; }
    }

    public class PlaingoldItemFilterProductTag
    {
        public string tag_name { get; set; }
        public string tag_count { get; set; }
    }

    public class PlaingoldItemFilterBrand
    {
        public string ItemBrandCommonID { get; set; }
        public string brand_id { get; set; }
        public string brand_name { get; set; }
        public string brand_count { get; set; }
    }

    public class PlaingoldItemFilterGender
    {
        public string gender_id { get; set; }
        public string gender_name { get; set; }
        public string gender_count { get; set; }
    }

    public class PlaingoldItemFilterApproxDelivery
    {
        public string ItemAproxDay { get; set; }
        public string ItemAproxDay_count { get; set; }
    }

    public class PlaingoldItemFilterStock
    {
        public string stock_name { get; set; }
        public string stock_id { get; set; }
    }

    public class PlaingoldItemFilterNameValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class PlaingoldItemFilterFamilyProduct
    {
        public string familyproduct_name { get; set; }
        public string familyproduct_id { get; set; }
    }

    public class PlaingoldItemFilterExcludeDiscontinue
    {
        public string excludediscontinue_name { get; set; }
        public string excludediscontinue_id { get; set; }
    }

    public class PlaingoldItemFilterView
    {
        public string view_name { get; set; }
        public string view_id { get; set; }
    }

    public class PlaingoldItemFilterImageAvail
    {
        public string imageavail_name { get; set; }
        public string imageavail_id { get; set; }
    }

    public class PlaingoldItemSubCategory
    {
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string sub_category_count { get; set; }
    }

    public class PlaingoldItemSubSubCategory
    {
        public string sub_sub_category_id { get; set; }
        public string sub_sub_category_name { get; set; }
        public string sub_sub_category_count { get; set; }
    }

}
