using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class PlainGoldItemListingParams
    {
        public string amount { get; set; }
        public string brand { get; set; }
        public int category_id { get; set; }
        public int data_id { get; set; }
        public int data_login_type { get; set; }
        public int default_limit_app_page { get; set; }
        public string delivery_days { get; set; }
        public int design_timeline { get; set; }
        public string dsg_color { get; set; }
        public string dsg_kt { get; set; }
        public string dsg_size { get; set; }
        public string dsg_westage { get; set; }
        public string dsg_weight { get; set; }
        public string Family_Av { get; set; }
        public string Regular_Av { get; set; }
        public string gender_id { get; set; }
        public int Item_ID { get; set; }
        public string item_name { get; set; }
        public string item_tag { get; set; }
        public string ItemSubCtgIDs { get; set; }
        public string ItemSubSubCtgIDs { get; set; }
        public int master_common_id { get; set; }
        public int page { get; set; }
        public string sales_location { get; set; }
        public string sort_id { get; set; }
        public string Stock_Av { get; set; }
        public string sub_category_id { get; set; }
        public string tryon { get; set; }
        public string variant { get; set; }
        public string wearit { get; set; }
        public string mode { get; set; }
    }
    public class PlainGoldItemListing
    {
        public string item_id { get; set; }
        public string category_id { get; set; }
        public string item_description { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string ItemGenderCommonID { get; set; }
        public string ItemNosePinScrewSts { get; set; }
        public string plaingold_status { get; set; }
        public string item_kt { get; set; }
        public string sub_category_id { get; set; }
        public string priceflag { get; set; }
        public string item_mrp { get; set; }
        public string max_qty_sold { get; set; }
        public string image_path { get; set; }
        public string item_color { get; set; }
        public string ItemMetalCommonID { get; set; }
        public string item_soliter { get; set; }

        public string item_text { get; set; }
        public string item_brand_text { get; set; }
        public string rupy_symbol { get; set; }
        public string img_watch_title { get; set; }
        public string img_wearit_title { get; set; }
        public string img_wish_title { get; set; }
        public string wearit_text { get; set; }
        public string cart_price { get; set; }
        public string variantCount { get; set; }
        public string item_color_id { get; set; }
        public string cart_id { get; set; }
        public string cart_item_qty { get; set; }
        public string wish_count { get; set; }
        public string wearit_count { get; set; }
        public string field_name { get; set; }
        public int selectedSize { get; set; }
        public List<Item_sizeListing> sizeList { get; set; }
        public string color_name { get; set; }
        public List<Item_ColorSizeListing> itemsColorSizeList { get; set; }
        public List<Item_itemOrderInstructionListing> itemOrderInstructionList { get; set; }
        public List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList { get; set; }
        public string selectedColor1 { get; set; }
        public string selectedSize1 { get; set; }
        public string default_color_name { get; set; }
        public string default_color_code { get; set; }
        public List<CartItem_colorListing> colorList { get; set; }
        public List<Item_TagsListing> productTags { get; set; }
        public string isInFranchiseStore { get; set; }
    }

    public class PlainGoldItemListRequest
    {
        public string? page { get; set; }
        public string? limit { get; set; }
        public string? data_id { get; set; }
        public string? data_login_type { get; set; }
        public string? master_common_id { get; set; }
        public string? category_id { get; set; }
        public string? sort_id { get; set; }
        public string? variant { get; set; }
        public string? item_name { get; set; } 
        public string? size { get; set; }
        public string? color { get; set; }
        public string? item_id { get; set; } 
        public string? stock_av { get; set; }
        public string? family_av { get; set; }
        public string? regular_av { get; set; }
        public string? wearit { get; set; }
        public string? tryon { get; set; }
        public string? fran_store_av { get; set; }
        public string? price { get; set; }
        public string? dsg_weight { get; set; }
        public string? dsg_westage { get; set; }
        public string? diawt { get; set; }
        public string? dsgKt { get; set; }
        public string? brand { get; set; }
        public string? approx_days { get; set; }
        public string? gender { get; set; }
        public string? tags { get; set; }
        public string? subCategoryId { get; set; }
        public string? itemSubCtgIDs { get; set; }
        public string? itemSubSubCtgIDs { get; set; }
        public string? sales_location { get; set; }
        public string? design_time_line { get; set; }
        public string? item_sub_category_id { get; set; }
    }

    public class ColorStoneItemListRequest
    {
        public string? page { get; set; }
        public string? limit { get; set; }
        public string? data_id { get; set; }
        public string? data_login_type { get; set; }
        public string? master_common_id { get; set; }
        public string? category_id { get; set; }
        public string? sort_id { get; set; }
        public string? variant { get; set; }
        public string? item_name { get; set; }
        public string? subCategoryId { get; set; }
        public string? size { get; set; }
        public string? dsgKt { get; set; }
        public string? color { get; set; }
        public string? price { get; set; }
        public string? metalwt { get; set; }
        public string? diawt { get; set; }
        public string? item_id { get; set; }
        public string? stock_av { get; set; }
        public string? family_av { get; set; }
        public string? regular_av { get; set; }
        public string? wearit { get; set; }
        public string? tryon { get; set; }
        public string? gender { get; set; }
        public string? tags { get; set; }
        public string? brand { get; set; }
        public string? approx_days { get; set; }
        public string? itemSubCtgIDs { get; set; }
        public string? itemSubSubCtgIDs { get; set; }
        public string? sales_location { get; set; }
        public string? design_time_line { get; set; }
        public string? item_sub_category_id { get; set; }
        public string? fran_store_av { get; set; }
    }

    public class PlatinumItemListRequest
    {
        public string? page { get; set; }
        public string? limit { get; set; }
        public string? data_id { get; set; }
        public string? data_login_type { get; set; }
        public string? master_common_id { get; set; }
        public string? category_id { get; set; }
        public string? sort_id { get; set; }
        public string? variant { get; set; }
        public string? item_name { get; set; }
        public string? subCategoryId { get; set; }
        public string? size { get; set; }
        public string? dsgKt { get; set; }
        public string? color { get; set; }
        public string? price { get; set; }
        public string? metalwt { get; set; }
        public string? diawt { get; set; }
        public string? item_id { get; set; }
        public string? stock_av { get; set; }
        public string? family_av { get; set; }
        public string? regular_av { get; set; }
        public string? wearit { get; set; }
        public string? tryon { get; set; }
        public string? gender { get; set; }
        public string? tags { get; set; }
        public string? brand { get; set; }
        public string? approx_days { get; set; }
        public string? itemSubCtgIDs { get; set; }
        public string? itemSubSubCtgIDs { get; set; }
        public string? sales_location { get; set; }
        public string? design_time_line { get; set; }
        public string? item_sub_category_id { get; set; }
        public string? fran_store_av { get; set; }
    }

    public class CoupleBandItemListRequest
    {
        public string? page { get; set; }
        public string? limit { get; set; }
        public string? data_id { get; set; }
        public string? data_login_type { get; set; }
        public string? master_common_id { get; set; }
        public string? category_id { get; set; }
        public string? sort_id { get; set; }
        public string? variant { get; set; }
        public string? item_name { get; set; }
        public string? subCategoryId { get; set; }
        public string? size { get; set; }
        public string? dsgKt { get; set; }
        public string? color { get; set; }
        public string? price { get; set; }
        public string? metalwt { get; set; }
        public string? diawt { get; set; }
        public string? item_id { get; set; }
        public string? stock_av { get; set; }
        public string? family_av { get; set; }
        public string? regular_av { get; set; }
        public string? wearit { get; set; }
        public string? tryon { get; set; }
        public string? gender { get; set; }
        public string? tags { get; set; }
        public string? brand { get; set; }
        public string? approx_days { get; set; }
        public string? itemSubCtgIDs { get; set; }
        public string? itemSubSubCtgIDs { get; set; }
        public string? sales_location { get; set; }
        public string? design_time_line { get; set; }
        public string? item_sub_category_id { get; set; }
        public string? fran_store_av { get; set; }
    }

    public class IllumineItemListRequest
    {
        public string? page { get; set; }
        public string? limit { get; set; }
        public string? data_id { get; set; }
        public string? data_login_type { get; set; }
        public string? master_common_id { get; set; }
        public string? category_id { get; set; }
        public string? sort_id { get; set; }
        public string? subCategoryId { get; set; }
        public string? color { get; set; }
        public string? gender { get; set; }
        public string? brand { get; set; }
        public string? item_sub_category_id { get; set; }
    }

    public class GlemSolitaireItemListRequest
    {
        public string? page { get; set; }
        public string? limit { get; set; }
        public string? data_id { get; set; }
        public string? data_login_type { get; set; }
        public string? master_common_id { get; set; }
        public string? category_id { get; set; }
        public string? sort_id { get; set; }
        public string? variant { get; set; }
        public string? item_name { get; set; }
        public string? subCategoryId { get; set; }
        public string? size { get; set; }
        public string? dsgKt { get; set; }
        public string? color { get; set; }
        public string? price { get; set; }
        public string? metalwt { get; set; }
        public string? diawt { get; set; }
        public string? item_id { get; set; }
        public string? stock_av { get; set; }
        public string? family_av { get; set; }
        public string? regular_av { get; set; }
        public string? wearit { get; set; }
        public string? tryon { get; set; }
        public string? gender { get; set; }
        public string? tags { get; set; }
        public string? brand { get; set; }
        public string? approx_days { get; set; }
        public string? itemSubCtgIDs { get; set; }
        public string? itemSubSubCtgIDs { get; set; }
        public string? sales_location { get; set; }
        public string? design_time_line { get; set; }
    }

    public class RareSolitaireItemListRequest
    {
        public string? DataId { get; set; }
        public string? DataLoginType { get; set; }
        public string? Page { get; set; }
        public string? Limit { get; set; }
        public string? CategoryId { get; set; }
        public string? SortId { get; set; }
        public string? DsgKt { get; set; }
        public string? DsgSize { get; set; }
        public string? DsgColor { get; set; }
        public string? ItemName { get; set; }
        public string? Variant { get; set; }
        public string? MasterCommonId { get; set; }
        public string? ItemTag { get; set; }
        public string? DeliveryDays { get; set; }
        public string? Amount { get; set; }
        public string? MetalWt { get; set; }
        public string? DiaWt { get; set; }
        public string? GenderId { get; set; }
        public string? Brand { get; set; }
        public string? ButtonCode { get; set; }
        public string? SubCategoryId { get; set; }
    }

    public class KisnaItemListRequest
    {
        public string? DataID { get; set; }
        public string? DataLoginTypeID { get; set; }
        public string? MasterCommonID { get; set; }
        public string? CategoryID { get; set; }
        public string? SortIds { get; set; }
        public string? Variant { get; set; }
        public string? ItemName { get; set; }
        public string? SubCategoryIDs { get; set; }
        public string? Sizes { get; set; }
        public string? DsgKts { get; set; }
        public string? Colors { get; set; }
        public string? Price { get; set; }
        public string? MetalWt { get; set; }
        public string? DiaWt { get; set; }
        public string? ItemID { get; set; }
        public string? Stock_Av { get; set; }
        public string? Family_Av { get; set; }
        public string? Regular_Av { get; set; }
        public string? Wearit { get; set; }
        public string? Tryon { get; set; }
        public string? Genders { get; set; }
        public string? TagWiseFilters { get; set; }
        public string? BrandWiseFilters { get; set; }
        public string? DeliveryDays { get; set; }
        public string? ItemSubCtgIDs { get; set; }
        public string? ItemSubSubCtgIDs { get; set; }
        public string? SalesLocation { get; set; }
        public string? DesignTimeLine { get; set; }
        public string? ItemSubCategoryID { get; set; }
        public string? FranStore_AV { get; set; }
        public string? Page { get; set; }
        public string? Limit { get; set; }
        public string? OrgType { get; set; }
        public string? Mode { get; set; }
    }
}
