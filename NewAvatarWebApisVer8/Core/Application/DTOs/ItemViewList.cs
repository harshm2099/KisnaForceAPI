namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class ItemViewItemListParams
    {
        public string data_id { get; set; }
        public string data_login_type { get; set; }
        public string page { get; set; }
    }

    public class ItemViewItemListResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int current_page { get; set; }
        public int last_page { get; set; }
        public int total_items { get; set; }
        public List<ItemViewItemData> data { get; set; }
    }

    public class ItemViewItemData
    {
        public string view_id { get; set; }
        public string item_soliter { get; set; }
        public string ItemDisLabourPer { get; set; }
        public string ItemAproxDay { get; set; }
        public string labour_per { get; set; }
        public string plaingold_status { get; set; }
        public string cat_id { get; set; }
        public string data_id { get; set; }
        public string item_id { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string item_sku { get; set; }
        public string item_description { get; set; }
        public string item_mrp { get; set; }
        public string item_price { get; set; }
        public string dist_price { get; set; }
        public string image_path { get; set; }
        public string image_thumb_path { get; set; }
        public string dsg_sfx { get; set; }
        public string dsg_size { get; set; }
        public string dsg_kt { get; set; }
        public string dsg_color { get; set; }
        public string star { get; set; }
        public string cart_img { get; set; }
        public string img_cart_title { get; set; }
        public string watch_img { get; set; }
        public string img_watch_title { get; set; }
        public string wish_count { get; set; }
        public string wearit_count { get; set; }
        public string wearit_status { get; set; }
        public string wearit_img { get; set; }
        public string wearit_none_img { get; set; }
        public string wearit_color { get; set; }
        public string wearit_text { get; set; }
        public string img_wearit_title { get; set; }
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
        public string price_text { get; set; }
        public string cart_price { get; set; }
        public string item_color_id { get; set; }
        public string item_details { get; set; }
        public string item_diamond_details { get; set; }
        public string item_text { get; set; }
        public string more_item_details { get; set; }
        public string item_stock { get; set; }
        public string item_removecart_img { get; set; }
        public string item_removecard_title { get; set; }
        public string rupy_symbol { get; set; }
        public string recent_view { get; set; }
        public string category_id { get; set; }
        public string cart_id { get; set; }
        public string ItemGenderCommonID { get; set; }
        public string item_stock_qty { get; set; }
        public string item_stock_colorsize_qty { get; set; }
        public string variantCount { get; set; }
        public string ItemTypeCommonID { get; set; }
        public string ItemNosePinScrewSts { get; set; }
        public string ItemFranchiseSts { get; set; }
        public string item_illumine { get; set; }
        public string isSolitaireOtherCollection { get; set; }
        public List<ItemViewProductTag> productTags { get; set; }
        public string weight { get; set; }
        public string totalLabourPer { get; set; }
        public int selectedColor { get; set; }
        public int selectedSize { get; set; }
        public string selectedColor1 { get; set; }
        public string selectedSize1 { get; set; }
        public string field_name { get; set; }
        public string color_name { get; set; }
        public string default_color_name { get; set; }
        public List<ItemViewSizeList> sizeList { get; set; }
        public List<ItemViewColorList> colorList { get; set; }
        public List<ItemViewColorSizeList> itemsColorSizeList { get; set; }
        public List<ItemViewItemInstruction> itemOrderInstructionList { get; set; }
        public List<ItemViewItemInstruction> itemOrderCustomInstructionList { get; set; }
        public List<ItemViewItemImagesColor> item_images_color { get; set; }
    }

    public class ItemViewProductTag
    {
        public string tag_name { get; set; }
        public string tag_color { get; set; }
    }

    public class ItemViewSizeList
    {
        public string product_size_mst_id { get; set; }
        public string product_size_mst_code { get; set; }
        public string product_size_mst_name { get; set; }
        public string product_size_mst_desc { get; set; }
    }

    public class ItemViewColorList
    {
        public string product_color_mst_id { get; set; }
        public string product_color_mst_code { get; set; }
        public string product_color_mst_name { get; set; }
    }

    public class ItemViewColorSizeList
    {
        public string cart_item_detail_id { get; set; }
        public string cart_mst_id { get; set; }
        public string cart_item_id { get; set; }
        public string cart_qty { get; set; }
        public string cart_color_id { get; set; }
        public string cart_size_id { get; set; }
        public string cart_item_remarks { get; set; }
        public string cart_item_remarks_ids { get; set; }
    }

    public class ItemViewItemInstruction
    {
        public string item_instruction_mst_id { get; set; }
        public string item_instruction_mst_code { get; set; }
        public string item_instruction_mst_name { get; set; }
    }

    public class ItemViewItemImagesColor
    {
        public string color_id { get; set; }
        public List<ItemViewColorImageDetails> color_image_details { get; set; }
    }

    public class ItemViewColorImageDetails
    {
        public string image_view_name { get; set; }
        public string image_path { get; set; }
        public string image_thumb_path { get; set; }
        public string color_id { get; set; }
    }


}
