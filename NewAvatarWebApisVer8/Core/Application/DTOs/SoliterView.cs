using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class PayloadsSoliterDetails
    {
        public int category_id { get; set; }
        public int data_id { get; set; }
        public int data_login_type { get; set; }
        public int item_id { get; set; }
        public int master_common_id { get; set; }


    }

    //new
    public class data2
    {
        public SoliterView item_detail { get; set; }
        //public IList<Item_Image_Color> item_images_color { get; set; }
        public IList<Item_Images_Soliter> color_image_details { get; set; }
    }
    //

    public class SoliterView_Static
    {
        public string success { get; set; }
        public string message { get; set; }
        //public SoliterView data { get; set; }
        public data2 data { get; set; }
    }

    public class SizeListSoliter
    {

        public string product_size_mst_id { get; set; }
        public string product_size_mst_code { get; set; }
        public string product_size_mst_name { get; set; }
        public string product_size_mst_desc { get; set; }
        public decimal SortBy { get; set; }  

    }

    public class ColorListSoliter
    {

        public string product_color_mst_id { get; set; }
        public string product_color_mst_code { get; set; }
        public string product_color_mst_name { get; set; }
        public string IsDefault { get; set; }
    }

    public class ItemOrderInstructionListSoliter
    {
        public string item_instruction_mst_id { get; set; }
        public string item_instruction_mst_code { get; set; }
        public string item_instruction_mst_name { get; set; }
    }

    public class ItemOrderCustomInstructionListSoliter
    {
        public string item_instruction_mst_id { get; set; }
        public string item_instruction_mst_code { get; set; }
        public string item_instruction_mst_name { get; set; }
    }

    public class Item_Image_Color_Soliter
    {
        public string color_id { get; set; }
        public IList<Item_Images_Soliter> color_image_details { get; set; } // to do

    }

    public class Item_Images_Soliter
    {
        public string image_view_name { get; set; }
        public string image_path { get; set; }
        public string filetype { get; set; }
    }

    public class SoliterView
    {
        public string item_id { get; set; }
        public string Category_name { get; set; }
        public string item_soliter { get; set; }
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
        public string star_color { get; set; }
        public string ItemMetalCommonID { get; set; }
        public string price_text { get; set; }
        public string cart_price { get; set; }
        public string item_color_id { get; set; }
        public string item_details { get; set; }
        public string item_text { get; set; }
        public string more_item_details { get; set; }
        public string item_stock { get; set; }
        public string cart_item_qty { get; set; }
        public string rupy_symbol { get; set; }
        public string variantCount { get; set; }
        public string cart_id { get; set; }
        public string ItemGenderCommonID { get; set; }
        public string cart_auto_id { get; set; }
        public string item_stock_qty { get; set; }
        public string item_stock_colorsize_qty { get; set; }
        public string category_id { get; set; }
        public string ItemNosePinScrewSts { get; set; }
        public string ItemAproxDayCommonID { get; set; }
        public string ItemBrandCommonID { get; set; }
        public string item_illumine { get; set; }
        public IList<ProductTags> productTags { get; set; }
        public string selectedColor { get; set; }
        public string selectedSize { get; set; }
        public string selectedColor1 { get; set; }
        public string selectedSize1 { get; set; }
        public string field_name { get; set; }
        public string color_name { get; set; }
        public string default_color_name { get; set; }
        public string default_color_code { get; set; }
        public string default_size_name { get; set; }
        public IList<SizeListSoliter> sizeList { get; set; }
        public IList<ColorListSoliter> colorList { get; set; }
        public IList<string> itemsColorSizeList { get; set; }
        public IList<ItemOrderInstructionListSoliter> itemOrderInstructionList { get; set; }
        public IList<ItemOrderCustomInstructionListSoliter> itemOrderCustomInstructionList { get; set; }
        public IList<Item_Image_Color_Soliter> item_images_color { get; set; }
        public IList<Item_Images_Soliter> item_images { get; set; }
        

    }

    public class SoliterPriceBreakupPayload
    {
        public string gold_wt { get; set; }
        public string cart_quantity { get; set; }
        public string category_id { get; set; }
        public string design_kt { get; set; }
        public string data_id { get; set; }
        public string item_id { get; set; }
        public string Stoke_no_arr { get; set; }

    }

    public class SoliterPriceBreakupResponse
    {

        public string SolitaireWt_Price { get; set; }
        public string GoldWt_Price { get; set; }
        public string Labour_Price { get; set; }
        public string GST { get; set; }
        public string Total { get; set; }


    }

    public class SoliterPriceBreakup_Static
    {
        public string success { get; set; }
        public string message { get; set; }
        public IList<SoliterPriceBreakupResponse> data { get; set; }
    }

    public class FinalMrpNewPayload
    {
        public string gold_wt { get; set; }
        public string cart_quantity { get; set; }
        public string category_id { get; set; }
        public string design_kt { get; set; }
        public string data_id { get; set; }
        public string item_id { get; set; }
        public string Stoke_no_arr { get; set; }

    }

    public class FinalMRPNewResponse
    {

        public string gold_wt { get; set; }
        public string gold_price { get; set; }
        public string dia_wt { get; set; }
        public string dia_price { get; set; }
        public string labour { get; set; }
        public string tax { get; set; }
        public string other { get; set; }
        public string Final_Mrp { get; set; }
        public string Final_Mrp_text { get; set; }
        public string Final_DMrp { get; set; }
        public string Final_RMrp { get; set; }
        public string Soli_refrence_id { get; set; }
        public string soliter_stock_no { get; set; }
      
    }

    public class FinalMRPNew_Static
    {
        public string success { get; set; }
        public string message { get; set; }
        public IList<FinalMRPNewResponse> data { get; set; }
    }

}