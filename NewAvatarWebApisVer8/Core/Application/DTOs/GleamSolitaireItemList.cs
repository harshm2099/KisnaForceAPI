using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class GleamSolitaireItemListingParams
    {
        public int page { get; set; }
        public int default_limit_app_page { get; set; }
        public int data_id { get; set; }
        public int data_login_type { get; set; }
        public int category_id { get; set; }
        public string sort_id { get; set; }
        public string variant { get; set; }
        public string item_name { get; set; }
        public string sub_category_id { get; set; }
        public string dsg_size { get; set; }
        public string dsg_kt { get; set; }
        public string dsg_color { get; set; }
        public string amount { get; set; }
        public string metalwt { get; set; }
        public string diawt { get; set; }
        public int Item_ID { get; set; }
        public string Stock_Av { get; set; }
        public string Family_Av { get; set; }
        public string Regular_Av { get; set; }
        public string wearit { get; set; }
        public string tryon { get; set; }
        public string gender_id { get; set; }
        public string item_tag { get; set; }
        public string brand { get; set; }
        public string delivery_days { get; set; }
        public string ItemSubCtgIDs { get; set; }
        public string ItemSubSubCtgIDs { get; set; }
        public string sales_location { get; set; }
        public int design_timeline { get; set; }
        public int master_common_id { get; set; }
        public string mode { get; set; }
    }
    public class GleamSolitaireItemListing
    {
        public string item_id { get; set; }
        public string category_id { get; set; }
        public string item_description { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string ItemGenderCommonID { get; set; }
        public string ItemNosePinScrewSts { get; set; }
        public string item_kt { get; set; }
        public string sub_category_id { get; set; }
        public string ItemIsSRP { get; set; }
        public string priceflag { get; set; }
        public string item_mrp { get; set; }
        public string max_qty_sold { get; set; }
        public string image_path { get; set; }
        public string item_color { get; set; }
        public string ItemMetalCommonID { get; set; }
        public string item_soliter { get; set; }
        public string plaingold_status { get; set; }
        public string item_price { get; set; }
        public string dist_price { get; set; }
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
}
