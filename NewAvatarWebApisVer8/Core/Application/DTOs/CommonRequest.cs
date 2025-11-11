using Microsoft.Data.SqlClient;
using System.Data;

namespace NewAvatarWebApis.Models
{
    public static class SqlReaderExtensions
    {
        public static int GetSafeInt(this SqlDataReader reader, string column) =>
            reader[column] != DBNull.Value ? Convert.ToInt32(reader[column]) : 0;

        public static decimal GetSafeDecimal(this SqlDataReader reader, string column) =>
            reader[column] != DBNull.Value ? Convert.ToDecimal(reader[column]) : 0;

        public static string GetSafeString(this SqlDataReader reader, string column) =>
            reader[column] != DBNull.Value ? Convert.ToString(reader[column]) : string.Empty;
    }

    public static class DataRowExtensions
    {
        public static string GetSafeString(this DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToString(row[columnName]) : string.Empty;
        }

        public static int GetSafeInt(this DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToInt32(row[columnName]) : 0;
        }

        public static decimal GetSafeDecimal(this DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToDecimal(row[columnName]) : 0;
        }

        public static bool GetSafeBool(this DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value && Convert.ToBoolean(row[columnName]);
        }
    }

    public class ResponseDetails
    {
        public bool? success { get; set; }
        public string? message { get; set; }
        public string? status { get; set; }
        public string? current_page { get; set; }
        public string? last_page { get; set; }
        public string? total_items { get; set; }
        public object? data { get; set; }
        public object? data1 { get; set; }
        public string? cart_auto_id { get; set; }
        public object? button { get; set; }
        public object? Buttons { get; set; }
        public string? image { get; set; }
        public string? loader_img { get; set; }
        public string? file_path { get; set; }
    }

    public class CartItems_sizeListingParams
    {
        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public int BranID { get; set; }
        public string ItemNosePinScrewSts { get; set; }
        public int ItemGenderCommonID { get; set; }
        public string pfield_name { get; set; }
        public int pselectedSize { get; set; }
        public int pdefault_size_name { get; set; }


    }
    public class CartItem_sizeListing
    {
        public int product_size_mst_id { get; set; }
        public string product_size_mst_code { get; set; }
        public string product_size_mst_name { get; set; }
        public string product_size_mst_desc { get; set; }
        public string field_name { get; set; }
        public int selectedSize { get; set; }
        public int default_size_name { get; set; }

    }

    public class CartProduct_sizeListingParams
    {
        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public int BranID { get; set; }
        public string ItemNosePinScrewSts { get; set; }
        public int ItemGenderCommonID { get; set; }
        public string field_name { get; set; }
        public string selectedSize { get; set; }
        public int default_size_name { get; set; }
    }
    public class CartProduct_sizeListing
    {
        public int product_size_mst_id { get; set; }
        public string product_size_mst_code { get; set; }
        public string product_size_mst_name { get; set; }
        public string product_size_mst_desc { get; set; }
        public string field_name { get; set; }
        public string selectedSize { get; set; }
        public int default_size_name { get; set; }
    }

    public class CartItem_colorListingParams
    {
        public int item_id { get; set; }
        public string default_color_code { get; set; }
        public int metalid { get; set; }
    }
    public class CartItem_colorListing
    {
        public string product_color_mst_id { get; set; }
        public string product_color_mst_code { get; set; }
        public string product_color_mst_name { get; set; }
        public string IsDefault { get; set; }
    }

    public class CartItem_itemsColorSizeListingParams
    {
        public int itemid { get; set; }
        public int cart_auto_id { get; set; }
        public int CartID { get; set; }
    }
    public class CartItem_itemsColorSizeListing
    {
        public int cart_item_detail_id { get; set; }
        public int cart_mst_id { get; set; }
        public int cart_item_id { get; set; }
        public int cart_qty { get; set; }
        public int cart_color_id { get; set; }
        public int cart_size_id { get; set; }
        public string cart_item_remarks { get; set; }
        public string cart_item_remarks_ids { get; set; }
        public string cart_item_custom_remarks { get; set; }
        public string cart_item_custom_remarks_ids { get; set; }
        public int cart_item_custom_status { get; set; }
    }

    public class Item_itemOrderCustomInstructionListing
    {
        public string item_instruction_mst_id { get; set; }
        public string item_instruction_mst_code { get; set; }
        public string item_instruction_mst_name { get; set; }
    }

    public class Item_itemOrderInstructionListing
    {
        public string item_instruction_mst_id { get; set; }
        public string item_instruction_mst_code { get; set; }
        public string item_instruction_mst_name { get; set; }
    }

    public class CartItem_item_images_color_detailsListing
    {
        public string image_view_name { get; set; }
        public string image_path { get; set; }
        public int color_id { get; set; }
    }

    public class CartItem_item_images_colorListingParams
    {
        public int itemid { get; set; }
        public int master_common_id { get; set; }
    }
    public class CartItem_item_images_colorListing
    {
        public int color_id { get; set; }
        public List<CartItem_item_images_color_detailsListing> color_image_details { get; set; }
    }

    public class Item_TagsListingParams
    {
        public int item_id { get; set; }
    }
    public class Item_TagsListing
    {
        public string tag_name { get; set; }
        public string tag_color { get; set; }
        public string StruItemID { get; set; }
    }

    public class CartItem_approxDaysListing
    {
        public string manufactureStartDate { get; set; }
        public string manufactureEndDate { get; set; }
        public string deliveryStartDate { get; set; }
        public string deliveryEndDate { get; set; }
        public string deliveryInDays { get; set; }
    }
    public class Item_images_color_detailsListing
    {
        public string image_view_name { get; set; }
        public string image_path { get; set; }
        public int color_id { get; set; }
    }
    public class Item_images_colorListingParams
    {
        public int itemid { get; set; }
    }
    public class Item_images_colorListing
    {
        public int color_id { get; set; }
        public List<Item_images_color_detailsListing> color_image_details { get; set; }
    }

    public class Items_sizeListingParams
    {
        public int ItemID { get; set; }
        public string CategoryCommonID { get; set; }
        public string ExcludeSizes { get; set; }
    }
    public class Item_sizeListing
    {
        public string product_size_mst_id { get; set; }
        public string product_size_mst_code { get; set; }
        public string product_size_mst_name { get; set; }
        public string product_size_mst_desc { get; set; }
    }

    public class ItemCategoryMappingList
    {
        public string category_id { get; set; }
        public string size_common_id { get; set; }
        public string field_name { get; set; }
        public int default_size { get; set; }
        public string default_size_female { get; set; }
        public string exclude_sizes { get; set; }
    }

    public class Item_ColorSizeListingParams
    {
        public int itemid { get; set; }
        public int CartID { get; set; }
    }
    public class Item_ColorSizeListing
    {
        public string cart_item_detail_id { get; set; }
        public string cart_mst_id { get; set; }
        public string cart_item_id { get; set; }
        public string cart_qty { get; set; }
        public string cart_color_id { get; set; }
        public string cart_size_id { get; set; }
        public string cart_item_remarks { get; set; }
        public string cart_item_remarks_ids { get; set; }
        public string cart_item_custom_remarks { get; set; }
        public string cart_item_custom_remarks_ids { get; set; }
        public string cart_item_custom_status { get; set; }
    }

    public class ItemDynamicPriceCartParams
    {
        public int DataID { get; set; }
        public int ItemID { get; set; }
        public int CartID { get; set; }
        public int CartItemID { get; set; }
        public decimal diamond_price { get; set; }
        public decimal gold_wt { get; set; }
        public decimal pure_gold { get; set; }
        public decimal gold_ktprice { get; set; }
        public decimal gold_price { get; set; }
        public decimal platinum_wt { get; set; }
        public decimal platinum { get; set; }
        public decimal platinum_price { get; set; }
        public decimal labour_price { get; set; }
    }

    public class ItemSelectedSizeList
    {
        public int selectedSize { get; set; }
        public int flag_exclude_sizes { get; set; }
        public string exclude_sizes { get; set; }
    }

    public class PriceWiseCategoryListRequest
    {
        public string? dataId { get; set; }
        public string? dataLoginType { get; set; }
        public string? priceRange { get; set; }
    }

    public class HOStockCategoriesRequest
    {
        public string? dataId { get; set; }
        public string? dataLoginType { get; set; }
    }
    public class HOStockSortByRequest
    {
        public string? dataId { get; set; }
        public string? dataLoginType { get; set; }
    }

    public class HOStockItemListRequest
    {
        public string? dataId { get; set; }
        public string? dataLoginType { get; set; }
        public string? categoryId { get; set; }
        public string? itemName { get; set; }
        public string? subCategoryId { get; set; }
        public string? dsgKt { get; set; }
        public string? amount { get; set; }
        public string? sortId { get; set; }
        public string? itemTag { get; set; }
        public string? brand { get; set; }
        public string? genderId { get; set; }
        public string? deliveryDays { get; set; }
        public string? limit { get; set; }
        public string? itemId { get; set; }
        public string? metalWt { get; set; }
        public string? diaWt { get; set; }
        public string? page { get; set; }
        public string? itemSubCategoryId { get; set; }
        public string? itemSubSubCategoryId { get; set; }
    }

    public class AllSubCategoryListRequest
    {
        public string? dataId { get; set; }
        public string? dataLoginType { get; set; }
        public string? buttonCode { get; set; }
        public string? categoryId { get; set; }
    }

    public class ChatCustomerCareRequest
    {
        public string? dataId { get; set; }
    }

    public class FranchiseWiseStockRequest
    {
        public string? ItemCode{ get; set; }
    }

    public class CappingSortByRequest
    {
        public string? DataId { get; set; }
        public string? DataLoginType { get; set; }
    }

    public class CappingFilterRequest
    {
        public string? DataId { get; set; }
        public string? DataLoginType { get; set; }
        public string? Type { get; set; }
        public string? CategoryId { get; set; }
        public string? MetalWt { get; set; }
    }

    public class CappingItemListRequest
    {
        public string? DataId { get; set; }
        public string? DataLoginType { get; set; }
        public string? Type { get; set; }
        public string? CategoryId { get; set; }
        public string? ItemSubCategoryId { get; set; }
        public string? SubCategoryId { get; set; }
        public string? SizeId { get; set; }
        public string? ColorCode { get; set; }
        public string? Amount { get; set; }
        public string? DeliveryDays { get; set; }
        public string? DsgKt { get; set; }
        public string? SortId { get; set; }
        public string? ItemTag { get; set; }
        public string? GenderId { get; set; }
        public string? Brand { get; set; }
        public string? FamilyAv { get; set; }
        public string? MetalWt { get; set; }
        public string? DiaWt { get; set; }
        public string? ItemSubCtgIDs { get; set; }
        public string? ItemSubSubCtgIDs { get; set; }
        public string? Page { get; set; }
        public string? DefaultLimitAppPage { get; set; }
    }

    public class ConsumerFormStoreRequest
    {
        public string? DataId { get; set; }
        public string? DataLoginType { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }

    public class StockWiseItemDataRequest
    {
        public string? ItemId { get; set; }
        public string? ColorId { get; set; }
        public string? SizeId { get; set; }
        public string? DataId { get; set; }
        public string? DataLoginType { get; set; }
    }

    public class PopularItemsRequest
    {
        public string? DataId { get; set; }
        public string? DataLoginType { get; set; }
        public string? MasterCommonId { get; set; }
        public string? Page { get; set; }
        public string? DefaultLimitAppPage { get; set; }
        public string? Variant { get; set; }
        public string? ItemName { get; set; }
        public string? SubCategoryId { get; set; }
        public string? DsgSize { get; set; }
        public string? DsgKt { get; set; }
        public string? DsgColor { get; set; }
        public string? Amount { get; set; }
        public string? MetalWt { get; set; }
        public string? DiaWt { get; set; }
        public string? ItemId { get; set; }
        public string? StockAv { get; set; }
        public string? FamilyAv { get; set; }
        public string? RegularAv { get; set; }
        public string? FranStoreAv { get; set; }
        public string? WearIt { get; set; }
        public string? TryOn { get; set; }
        public string? GenderId { get; set; }
        public string? ItemTag { get; set; }
        public string? Brand { get; set; }
        public string? DeliveryDays { get; set; }
        public string? ItemSubCtgIDs { get; set; }
        public string? ItemSubSubCtgIDs { get; set; }
        public string? DesignTimeline { get; set; }
        public string? ItemSubCategoryId { get; set; }
    }

    public class PieceVerifyRequest
    {
        public string? Type { get; set; }
        public string? Search { get; set; }
        public string? DataId { get; set; }
        public string? DataLoginType { get; set; }
    }

    public class TopRecommandedItemsRequest
    {
        public string? DataId { get; set; }
        public string? DataLoginType { get; set; }
        public string? MasterCommonId { get; set; }
        public string? Page { get; set; }
        public string? DefaultLimitAppPage { get; set; }
        public string? Variant { get; set; }
        public string? ItemName { get; set; }
        public string? SubCategoryId { get; set; }
        public string? DsgSize { get; set; }
        public string? DsgKt { get; set; }
        public string? DsgColor { get; set; }
        public string? Amount { get; set; }
        public string? MetalWt { get; set; }
        public string? DiaWt { get; set; }
        public string? ItemId { get; set; }
        public string? StockAv { get; set; }
        public string? FamilyAv { get; set; }
        public string? RegularAv { get; set; }
        public string? WearIt { get; set; }
        public string? TryOn { get; set; }
        public string? GenderId { get; set; }
        public string? ItemTag { get; set; }
        public string? Brand { get; set; }
        public string? DeliveryDays { get; set; }
        public string? ItemSubCtgIDs { get; set; }
        public string? ItemSubSubCtgIDs { get; set; }
        public string? SalesLocation { get; set; }
        public string? DesignTimeline { get; set; }
        public string? ItemSubCategoryId { get; set; }
    }

    public class PopularItemsFilterRequest
    {
        public string? DataId { get; set; }
        public string? ButtonCode { get; set; }
        public string? MasterCommonId { get; set; }
        public string? CategoryId { get; set; }
    }
}
