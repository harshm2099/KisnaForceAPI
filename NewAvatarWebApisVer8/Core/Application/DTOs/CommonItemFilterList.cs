using System.Collections.Generic;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class CommonItemFilterParams
    {
        public int data_id { get; set; }
        public int category_id { get; set; }
        public int master_common_id { get; set; }
        public string button_code { get; set; }
        public string btn_cd { get; set; }
    }

    public class CommonItemFilterData
    {
        public SubCategoryFilter sub_category { get; set; } = new SubCategoryFilter();
        public DsgKtFilter dsg_kt { get; set; } = new DsgKtFilter();
        public List<DsgAmountFilter> dsg_amount { get; set; } = new List<DsgAmountFilter>();
        public List<DsgMetalWtFilter> dsg_metalwt { get; set; } = new List<DsgMetalWtFilter>();
        public List<DsgDiamondWtFilter> dsg_diamondwt { get; set; } = new List<DsgDiamondWtFilter>();
        public ProductTagFilter productTags { get; set; } = new ProductTagFilter();
        public BrandFilter brand { get; set; } = new BrandFilter();
        public GenderFilter gender { get; set; } = new GenderFilter();
        public ApproxDeliveryFilter approx_develivery { get; set; } = new ApproxDeliveryFilter();
        public StockFilter stock_filter { get; set; } = new StockFilter();
        public object item_sub_category { get; set; } = null;
        public object item_sub_sub_category { get; set; } = null; 
        public BestSellersFilter best_sellers_data { get; set; } = new BestSellersFilter();
        public DesignsFilter designs_data { get; set; } = new DesignsFilter();
        public FamilyProductFilter familyproduct_filter { get; set; } = new FamilyProductFilter();
        public ExcludeDiscontinueFilter excludediscontinue_filter { get; set; } = new ExcludeDiscontinueFilter();
        public WearViewFilter wearview_filter { get; set; } = new WearViewFilter();
        public TryOnViewFilter tryonview_filter { get; set; } = new TryOnViewFilter();
    }

    public class SubCategoryFilter
    {
        public string name { get; set; } = "Collection";
        public List<CommonItemFilterSubCategory> data { get; set; } = new List<CommonItemFilterSubCategory>();
    }

    public class DsgKtFilter
    {
        public string name { get; set; } = "KT";
        public List<CommonItemFilterDsgKt> data { get; set; } = new List<CommonItemFilterDsgKt>();
    }

    public class DsgAmountFilter
    {
        public string amount { get; set; }
    }

    public class DsgMetalWtFilter
    {
        public string metalwt { get; set; }
    }

    public class DsgDiamondWtFilter
    {
        public string diamondwt { get; set; }
    }

    public class ProductTagFilter
    {
        public string name { get; set; } = "Product Tag";
        public List<CommonItemFilterProductTag> data { get; set; } = new List<CommonItemFilterProductTag>();
    }

    public class BrandFilter
    {
        public string name { get; set; } = "Brand";
        public List<CommonItemFilterBrand> data { get; set; } = new List<CommonItemFilterBrand>();
    }

    public class GenderFilter
    {
        public string name { get; set; } = "Gender";
        public List<CommonItemFilterGender> data { get; set; } = new List<CommonItemFilterGender>();
    }

    public class ApproxDeliveryFilter
    {
        public string name { get; set; } = "Approx Delivery Days";
        public List<CommonItemFilterApproxDelivery> data { get; set; } = new List<CommonItemFilterApproxDelivery>();
    }

    public class StockFilter
    {
        public string name { get; set; } = "Stock";
        public List<CommonItemFilterStock> data { get; set; } = new List<CommonItemFilterStock>();
    }

    public class BestSellersFilter
    {
        public string name { get; set; } = "Best Sellers";
        public List<CommonItemFilterNameValue> data { get; set; } = new List<CommonItemFilterNameValue>();
    }

    public class DesignsFilter
    {
        public string name { get; set; } = "The latest design released in";
        public List<CommonItemFilterNameValue> data { get; set; } = new List<CommonItemFilterNameValue>();
    }

    public class FamilyProductFilter
    {
        public string name { get; set; } = "Family Product";
        public List<CommonItemFilterFamilyProduct> data { get; set; } = new List<CommonItemFilterFamilyProduct>();
    }

    public class ExcludeDiscontinueFilter
    {
        public string name { get; set; } = "Exclude Discontinue";
        public List<CommonItemFilterExcludeDiscontinue> data { get; set; } = new List<CommonItemFilterExcludeDiscontinue>();
    }

    public class WearViewFilter
    {
        public string name { get; set; } = "Wear It";
        public List<CommonItemFilterView> data { get; set; } = new List<CommonItemFilterView>();
    }

    public class TryOnViewFilter
    {
        public string name { get; set; } = "Try On";
        public List<CommonItemFilterView> data { get; set; } = new List<CommonItemFilterView>();
    }


    public class CommonItemFilterSubCategory
    {
        public string category_id { get; set; }
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string category_count { get; set; }
    }

    public class CommonItemFilterDsgKt
    {
        public string kt { get; set; }
        public string Kt_Count { get; set; }
    }

    public class CommonItemFilterDsgAmount
    {
        public string minprice { get; set; }
        public string maxprice { get; set; }
    }

    public class CommonItemFilterDsgMetalWt
    {
        public string minmetalwt { get; set; }
        public string maxmetalwt { get; set; }
    }

    public class CommonItemFilterDsgDiamondWt
    {
        public string mindiawt { get; set; }
        public string maxdiawt { get; set; }
    }

    public class CommonItemFilterProductTag
    {
        public string tag_name { get; set; }
        public string tag_count { get; set; }
    }

    public class CommonItemFilterBrand
    {
        public string brand_id { get; set; }
        public string brand_name { get; set; }
        public string brand_count { get; set; }
    }

    public class CommonItemFilterGender
    {
        public string gender_id { get; set; }
        public string gender_name { get; set; }
        public string gender_count { get; set; }
    }

    public class CommonItemFilterApproxDelivery
    {
        public string ItemAproxDay { get; set; }
        public string ItemAproxDay_count { get; set; }
    }

    public class CommonItemFilterStock
    {
        public string stock_name { get; set; }
        public string stock_id { get; set; }
    }

    public class CommonItemFilterNameValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class CommonItemFilterFamilyProduct
    {
        public string familyproduct_name { get; set; }
        public string familyproduct_id { get; set; }
    }

    public class CommonItemFilterExcludeDiscontinue
    {
        public string excludediscontinue_name { get; set; }
        public string excludediscontinue_id { get; set; }
    }

    public class CommonItemFilterView
    {
        public string view_name { get; set; }
        public string view_id { get; set; }
    }

}
