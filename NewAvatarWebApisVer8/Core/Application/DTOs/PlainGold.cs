using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class PayloadGoldDetails
    {
        public int category_id { get; set; }
        public int data_id { get; set; }
        public int data_login_type { get; set; }
        public int item_id { get; set; }
        public int master_common_id { get; set; }


    }

    //new
    public class data1
    {
        public PlainGoldDetails item_detail { get; set; }
        //public IList<Item_Image_Color> item_images_color { get; set; }
        public IList<Item_Images_Gold> color_image_details { get; set; }
    }
    //

    public class GoldView_Static
    {
        public string success { get; set; }
        public string message { get; set; }
        //public PlainGoldDetails data { get; set; }
        public data1 data { get; set; }

    }

    public class SizeListGold
    {

        public string product_size_mst_id { get; set; }
        public string product_size_mst_code { get; set; }
        public string product_size_mst_name { get; set; }
        public string product_size_mst_desc { get; set; }
        public decimal SortBy { get; set; }

    }

    public class ColorListGold
    {

        public string product_color_mst_id { get; set; }
        public string product_color_mst_code { get; set; }
        public string product_color_mst_name { get; set; }
        public string IsDefault { get; set; }
    }

    public class ItemOrderInstructionListGold
    {
        public string item_instruction_mst_id { get; set; }
        public string item_instruction_mst_code { get; set; }
        public string item_instruction_mst_name { get; set; }
    }

    public class ItemOrderCustomInstructionListGold
    {
        public string item_instruction_mst_id { get; set; }
        public string item_instruction_mst_code { get; set; }
        public string item_instruction_mst_name { get; set; }
    }

    public class Item_Image_Color_Gold
    {
        public string color_id { get; set; }
        public IList<Item_Images_Gold> color_image_details { get; set; } // to do

    }

    public class Item_Images_Gold
    {
        public string image_view_name { get; set; }
        public string image_path { get; set; }
        public string filetype { get; set; }
    }


    public class PlainGoldDetails
    {
        public string item_id { get; set; }
        public decimal ItemDisLabourPer { get; set; }
        public string ItemCtgCommonID { get; set; }
        public string labour_per { get; set; }
        public string item_soliter {  get; set; }
        public string ItemAproxDay { get; set; }
        public string ItemDAproxDay {  get; set; }  
        public string plaingold_status { get; set; }
        public string item_name { get; set; }
        public string item_sku { get; set; }
        public string item_description { get; set; }
        public string dsg_kt { get; set; }
        //public string item_ { get; set; }
        public string item_discount { get; set; }
        public string item_price { get; set; }
        public string retail_price { get; set; }
        public string dist_price { get; set; }
        //public string item_mrp { get; set; }
       
        //public string uom { get; set; }
        public string ItemBrandCommonID { get; set; }

        public string star { get; set; }
        public string cart_img { get; set; }
        public string img_cart_title { get; set; }
        public string watch_img { get; set; }
        public string img_watch_title { get; set; }
        public string wearit_count { get; set; }
        public string wearit_status { get; set; }
        public string wearit_img { get; set; }
        public string wearit_none_img { get; set; }
        public string wearit_color { get; set; }
        public string wearit_text { get; set; }
        public string img_wearit_title { get; set; }
        public string tryon_count { get; set; }
        public string tryon_status { get; set; }
        public string tryon_img { get; set; }
        public string tryon_none_img { get; set; }
        public string tryon_text { get; set; }
        public string tryon_title { get; set; }
        public string tryon_android_path { get; set; }
        public string tryon_ios_path { get; set; }
        public string wish_count { get; set; }
        public string wish_default_img { get; set; }
        public string wish_fill_img { get; set; }
        public string img_wish_title { get; set; }
        public string item_review { get; set; }
        public string item_size { get; set; }
        public string item_kt { get; set; }
        public string item_color { get; set; }
        public string item_metal { get; set; }
        public string item_wt { get; set; }
        public string item_stone { get; set; }
        public string item_stone_wt { get; set; }
        public string item_stone_qty { get; set; }
        //public string star_color { get; set; }
        public string ItemMetalCommonID { get; set; }
        //public string price_text { get; set; }
        public string cart_price { get; set; }
        public string item_color_id { get; set; }
        public string item_details { get; set; }
        public string item_diamond_details { get; set; } //*//
        public string item_text { get; set; }
        public string more_item_details { get; set; }
        public string item_stock { get; set; }
        public string cart_item_qty { get; set; }
        public string rupy_symbol { get; set; }
        public string variantCount { get; set; }
        public string cart_id { get; set; }
        public string ItemGenderCommonID { get; set; }
        //public string cart_auto_id { get; set; }
        public string item_stock_qty { get; set; }
        public string item_stock_colorsize_qty { get; set; }
        public string category_id { get; set; }
        public string ItemNosePinScrewSts { get; set; }
        
        public string brand { get; set; }
        public string category { get; set; }
        public string ItemAproxDayCommonID { get; set; }
        public string ItemPlainGold { get; set; }
        public string ItemSoliterSts { get; set; }
        //public string ItemIsSRP { get; set; }
        public string ItemSubCtgName { get; set; }
        public string ItemSubSubCtgName { get; set; }
        public string next { get; set; }
        public string prev { get; set; }
        public string data_collection1 { get; set; }
        public string Collections { get; set; }
        public IList<ProductTags> productTags { get; set; }
        public string weight { get; set; }
        public string selectedColor { get; set; }
        public string selectedSize { get; set; }

        public string selectedColor1 { get; set; }
        public string field_name { get; set; }
        public string color_name { get; set; }
        public string default_color_name { get; set; }
        public string default_color_code { get; set; }
        public string default_size_name { get; set; }
        public decimal GrossWt { get; set; }
        public string FinalMrp { get; set; }
        public string FinalRMrp { get; set; }
        public string fix_lab_sts { get; set; }
        public string fix_lab_val { get; set; }
        public string ITEM_MRP { get; set; }
        public string tem_mrp_without_gst { get; set; }
        public string FinalDMrp { get; set; }
        public string goldprice { get; set; }
        public string goldprice_new { get; set; }
        public string Approx_Delivery { get; set; }
        public string sub_collection { get; set; }
        public string totalLabourPer { get; set; }
        public string gst_priceDist { get; set; }
        public string metal_priceDist { get; set; }
        public string totalLabourPerDist { get; set; }
        public IList<SizeListGold> sizeList { get; set; }
        public IList<ColorListGold> colorList { get; set; }
        public IList<ItemOrderInstructionListGold> itemOrderInstructionList { get; set; }
        public IList<ItemOrderCustomInstructionListGold> itemOrderCustomInstructionList { get; set; }
        public IList<string> itemsColorSizeList { get; set; }
        public IList<Item_Image_Color_Gold> item_images_color { get; set; }
        public IList<Item_Images_Gold> item_images { get; set; }
        public string item_illumine { get; set; }
    }

    public class PlainGoldFilter
    {
        public string? category_id { get; set; }
        public string? data_id { get; set; }
        public string? button_code { get; set; }
        public string? master_common_id { get; set; }
        public string? data_login_type { get; set; }
    }

    public class PlainGoldItemDetailsRequest
    {
        public string dataId { get; set; }
        public string dataLoginType { get; set; }
        public string categoryId { get; set; }
        public string itemId { get; set; }
        public string variant { get; set; }
        public string itemName { get; set; }
        public string masterCommonId { get; set; }
    }

    public class TotalGoldDiaaWeightRequest
    {
        public string? cartId { get; set; }
    }

    public class ExtraGoldRateWiseRateRequest
    {
        public string? goldWeight { get; set; }
        public string? dataId { get; set; }
        public string? dataLogicType { get; set; }
        public string? designKt { get; set; }
    }
}