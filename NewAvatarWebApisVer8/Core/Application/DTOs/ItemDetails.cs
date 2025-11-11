using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public  class PayloadsItemDetails
    {
        public int category_id { get; set; }
        public int data_id { get; set; }
        public int data_login_type { get; set; }
        public int item_id { get; set; }
        public int master_common_id { get; set; }
    }

    public class data
    {
        public ItemDetails item_detail { get; set; }
        //public IList<Item_Image_Color> item_images_color { get; set; }
        public IList<Item_Images> color_image_details { get; set; }
    }

    public class ItemDetails_Static
    {
        public string success { get; set; }
        public string message { get; set; }
        //public ItemDetails data { get; set; }
        public data data { get; set; }
        
    }

    public class ProductTags
    {
        public string tag_name { get; set; }        
        public string tag_color { get; set; }
        public string StruItemCommonID { get; set; }
    }

    public class SizeList
    {
        public string product_size_mst_id { get; set; }
        public string product_size_mst_code { get; set; }
        public string product_size_mst_name { get; set; }
        public string product_size_mst_desc { get; set; }
    }

    public class ItemsColorSizeList
    {
        public string cart_item_detail_id { get; set; }
        public string ExtraGoldWeight { get; set; }
        public string ExtraGoldPrice { get; set; }
        public string cart_mst_id { get; set; }
        public string cart_item_id { get; set; }
        public string cart_qty { get; set; }
        public string cart_color_id { get; set; }
        public string cart_size_id { get; set; }
        public string cart_item_remarks { get; set; }
        public string cart_item_remarks_ids { get; set; }
        public string cart_item_custom_remarks { get; set; }
        public string cart_item_custom_remarks_ids { get; set; }
        public string cart_item_custom_remarks_status { get; set; }
    }

    public class ColorList
    {
        public string product_color_mst_id { get; set; }
        public string product_color_mst_code { get; set; }
        public string product_color_mst_name { get; set; }
        public string IsDefault { get; set; }
    }

    public class ItemOrderInstructionList
    {
        public string item_instruction_mst_id { get; set; }
        public string item_instruction_mst_code { get; set; }
        public string item_instruction_mst_name { get; set; }
    }

    public class ItemOrderCustomInstructionList
    {
        public string item_instruction_mst_id { get; set; }
        public string item_instruction_mst_code { get; set; }
        public string item_instruction_mst_name { get; set; }
    }

    public class Item_Image_Color
    {
        public string color_id { get; set; }
        public IList<Item_Images> color_image_details { get; set; } // to do
       
    }

    public class ApproxDays
    {
        public string manufactureStartDate { get; set; }
        public string manufactureEndDate { get; set; }
        public string deliveryStartDate { get; set; }
        public string deliveryEndDate { get; set; }
        public string deliveryInDays { get; set; }
    }

    public class DiamondData
    {
        public string diamond_price { get; set; }
        public string diamond_wt { get; set; }
        public string diamond_qty { get; set; }
        public string diamond_shape { get; set; }
    }

    public class Item_Images
    {
        public string image_view_name { get; set; }
        public string image_path { get; set; }
        public string filetype { get; set; }
    }

    public class ItemDetails
    {
        public string item_detail { get; set; }
        public string item_id { get; set; }
        public string item_soliter { get; set; }
        public string ItemCtgCommonID { get; set; }
        public string ItemAproxDay { get; set; }
        public string ItemDAproxDay { get; set; }
        public string plaingold_status { get; set; }
        public string item_name { get; set; }
        public string item_sku { get; set; }
        public string item_description { get; set; }
        public string item_mrp { get; set; }
        public string item_discount { get; set; }
        public string item_price { get; set; }
        public string retail_price { get; set; }
        public string dist_price { get; set; }
        public string uom { get; set; }
        public string star { get; set; }
        public string cart_img { get; set; }
        public string img_cart_title { get; set; }
        public string watch_img { get; set; }
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
        public string star_color { get; set; }
        public string ItemMetalCommonID { get; set; }
        public string price_text { get; set; }
        public string cart_price { get; set; }
        public string item_color_id { get; set; }
        public string item_details { get; set; }
        public string item_diamond_details { get; set; }
        public string item_text { get; set; }
        public string more_item_details { get; set; }
        public string item_stock { get; set; }
        public string cart_item_qty { get; set; }
        public string rupy_symbol { get; set; }
        public string variantCount { get; set; }
        public string cart_id { get; set; }
        public string ItemGenderCommonID { get; set; }
        public string category_id { get; set; }
        public string ItemNosePinScrewSts { get; set; }
        public string metal { get; set; }
        public string kt { get; set; }
        public string quality { get; set; }
        public string shape { get; set; }
        public string brand { get; set; }
        public string stone { get; set; }
        public string diamondcolor { get; set; }
        public string category { get; set; }
        public string ItemAproxDayCommonID { get; set; }
        public string GrossWt { get; set; }
        public string ItemFranchiseSts { get; set; }
        public string priceflag { get; set; }
        public string ItemPlainGold { get; set; }
        public string ItemSoliterSts { get; set; }
        public string ItemSubCtgName { get; set; }
        public string ItemSubSubCtgName { get; set; }
        public string ItemIsSRP { get; set; }
        public string ItemSizeAvailable { get; set; }
        public IList<ProductTags> productTags { get; set; }
        public IList<string> sub_collection { get; set; }
        public IList<string> gems_stone { get; set; }
        public IList<string> data_collection1 { get; set; }
        public string next { get; set; }
        public string prev { get; set; }
        public IList<string> weight { get; set; }
        public string metalweight { get; set; }
        public string diamondweight { get; set; }
        public string approxdelivery { get; set; }
        public string collections { get; set; }
        public string item_stock_qty { get; set; }  
        public string item_stock_colorsize_qty {  get; set; }   
        public string selectedColor {  get; set; }
        public string selectedSize { get; set; }
        public string selectedColor1 { get; set; }
        public string selectedSize1 { get; set; }
        public string field_name { get; set; }
        public string color_name { get; set; }
        public string default_color_name { get; set; }
        public string default_color_code { get; set; }
        public string default_size_name { get; set; }
        public string fran_diamond_price { get; set; }
        public string diamond_wt { get; set; }
        public string fran_gold_price { get; set; }
        public string gold_wt { get; set; }
        public string gold_ktprice { get; set; }
        public string fran_platinum_price { get; set; }
        public string platinum_wt { get; set; }
        public string platinum_ktprice { get; set; }
        public string fran_labour_price { get; set; }
        public string fran_metal_price { get; set; }
        public string fran_other_price { get; set; }
        public string fran_stone_price { get; set; }
        public string stone_qty { get; set; }
        public string stone_wt { get; set; }
        public string fran_mrp_gst { get; set; }
        public IList<SizeList> sizeList {  get; set; }
        public IList<ColorList> colorList { get; set; }
        public IList<ItemsColorSizeList> itemsColorSizeList {  get; set; }
        public IList<ItemOrderInstructionList> itemOrderInstructionList { get; set; }
        public IList<ItemOrderCustomInstructionList> itemOrderCustomInstructionList { get; set; }
        //public IList<Item_Image_Color> item_images_color { get; set; }
        public IList<ApproxDays> approxDays { get; set; }
        public IList<DiamondData> diamondData { get; set; }
        public IList<Item_Images> item_images { get; set; }
    }

    public class ItemDetailsRquest
    {
        public string dataId { get; set; }
        public string dataLoginType { get; set; }
        public string categoryId { get; set; }
        public string itemId { get; set; }
        public string masterCommonId { get; set; }
    }
}

