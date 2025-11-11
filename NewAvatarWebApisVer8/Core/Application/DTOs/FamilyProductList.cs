using NewAvatarWebApis.Models;
using System.Collections.Generic;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class FamilyProductListingParams
    {
        public int data_id { get; set; }
        public int item_id { get; set; }
        public int category_id { get; set; }
        public int master_common_id { get; set; }
        public int default_limit_app_page { get; set; }
    }

    public class FamilyProductListing
    {
        public string item_id { get; set; }
        public string item_gold { get; set; }
        public string ItemDisLabourPer { get; set; }
        public string labour_per { get; set; }
        public string product_itemid { get; set; }
        public string ItemAproxDay { get; set; }
        public string item_code { get; set; }
        public string category_id { get; set; }
        public string item_name { get; set; }
        public string item_sku { get; set; }
        public string item_description { get; set; }
        public string item_mrp { get; set; }
        public string ItemFranchiseSts { get; set; }
        public string sub_category_id { get; set; }
        public string item_price { get; set; }
        public string dist_price { get; set; }
        public string FinalDMrp { get; set; }
        public string image_path { get; set; }
        public string dsg_sfx { get; set; }
        public string dsg_size { get; set; }
        public string dsg_kt { get; set; }
        public string dsg_color { get; set; }
        public string item_soliter { get; set; }
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
        public string item_brand_text { get; set; }
        public string more_item_details { get; set; }
        public string item_stock { get; set; }
        public string cart_item_qty { get; set; }
        public string rupy_symbol { get; set; }
        public string variantCount { get; set; }
        public string cart_id { get; set; }
        public string ItemGenderCommonID { get; set; }
        public string item_gender { get; set; }
        public string ItemTypeCommonID { get; set; }
        public string ItemNosePinScrewSts { get; set; }
        public string ItemAproxDayCommonID { get; set; }
        public string priceflag { get; set; }
        public string ItemPlainGold { get; set; }
        public string plaingold_status { get; set; }
        public string ItemBrandCommonID { get; set; }
        public string ItemIsSRP { get; set; }
        public string mrp_withtax { get; set; }
        public string diamond_price { get; set; }
        public string gold_price { get; set; }
        public string platinum_price { get; set; }
        public string labour_price { get; set; }
        public string metal_price { get; set; }
        public string other_price { get; set; }
        public string stone_price { get; set; }
        public string ItemSizeAvailable { get; set; }
        public string item_stock_qty { get; set; }
        public string item_stock_colorsize_qty { get; set; }
        public string selectedColor { get; set; }
        public string selectedSize { get; set; }
        public string selectedColor1 { get; set; }
        public string selectedSize1 { get; set; }
        public string field_name { get; set; }
        public string color_name { get; set; }
        public string default_color_name { get; set; }
        public string default_color_code { get; set; }
        public string default_size_name { get; set; }
        public string fran_diamond_price { get; set; }
        public string fran_gold_price { get; set; }
        public string fran_platinum_price { get; set; }
        public string fran_labour_price { get; set; }
        public string fran_metal_price { get; set; }
        public string fran_other_price { get; set; }
        public string fran_stone_price { get; set; }
        public string weight { get; set; }
        public string totalLabourPer { get; set; }

        public List<Item_TagsListing> productTags { get; set; }
        public List<CartProduct_sizeListing> sizeList { get; set; }
        public List<CartItem_colorListing> colorList { get; set; }
        public List<CartItem_itemsColorSizeListing> itemsColorSizeList { get; set; }
        public List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList { get; set; }
        public List<Item_itemOrderInstructionListing> itemOrderInstructionList { get; set; }
        public List<CartItem_item_images_colorListing> item_images_color { get; set; }
    }

}
