using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class WatchItemListingParams
    {
        public int data_id { get; set; }
        public int data_login_type { get; set; }
        public int watchlist_id { get; set; }
        public int category_id { get; set; }
        public string variant { get; set; }
        public string item_name { get; set; }
        public string sort_id { get; set; }
        public string sub_category_id { get; set; }
        public string dsg_size { get; set; }
        public string dsg_kt { get; set; }
        public string dsg_color { get; set; }
        public string amount { get; set; }
        public string metalwt { get; set; }
        public string diawt { get; set; }
        public string gender_id { get; set; }
        public string item_tag { get; set; }
        public string brand { get; set; }
        public string delivery_days { get; set; }
        public int Item_ID { get; set; }
        public string Stock_Av { get; set; }
        public string Family_Av { get; set; }
        public string Regular_Av { get; set; }
        public string wearit { get; set; }
        public string tryon { get; set; }
        public int default_limit_app_page { get; set; }
        public string ItemSubCtgIDs { get; set; }
        public string ItemSubSubCtgIDs { get; set; }
        public int master_common_id { get; set; }
        public int page { get; set; }
    }
    public class WatchItemListing
    {
        public string watch_list_id { get; set; }
        public string item_id { get; set; }
        public string item_code { get; set; }
        public string ItemAproxDay { get; set; }
        public string item_sku { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public string item_mrp { get; set; }
        public string dsg_sfx { get; set; }
        public string dsg_size { get; set; }
        public string dsg_kt { get; set; }
        public string dsg_color { get; set; }
        public string ItemDisLabourPer { get; set; }
        public string ApproxDeliveryDate { get; set; }
        public string sub_category_id { get; set; }
        public string item_price { get; set; }
        public string dist_price { get; set; }
        public string item_soliter { get; set; }
        public string image_path { get; set; }
        public string star { get; set; }
        public string mostOrder { get; set; }
        public string ItemAproxDayCommonID { get; set; }
        public string plaingold_status { get; set; }
        public string ItemPlainGold { get; set; }
        public string labour_per { get; set; }
        public string item_wt { get; set; }
        public string watchcart_flag { get; set; }
        public string category_id { get; set; }
        public string data_id { get; set; }
        public string selectedColor1 { get; set; }
        public string selectedSize1 { get; set; }
        public string valid_status { get; set; }
        public string ent_dt { get; set; }
        public string item_valid_status { get; set; }
        public string item_ent_dt { get; set; }
        public string ItemFranchiseSts { get; set; }
        public string priceflag { get; set; }
        public string ItemIsSRP { get; set; }
        public string mrp_withtax { get; set; } 
        public string diamond_price { get; set; }
        public string gold_price { get; set; }
        public string platinum_price { get; set; }
        public string labour_price { get; set; }
        public string metal_price { get; set; }
        public string other_price { get; set; }
        public string stone_price { get; set; }
        public string item_stock_qty { get; set; }
        public string item_stock_colorsize_qty { get; set; }
        public List<Item_TagsListing> productTags {  get; set; }
        public string fran_diamond_price { get; set; }  
        public string fran_gold_price { get; set; }
        public string fran_platinum_price { get; set; }
        public string fran_labour_price { get; set;}
        public string fran_metal_price { get; set; }
        public string fran_other_price { get; set; }
        public string fran_stone_price { get; set; }
    }

    public class WatchlistDeleteParams
    {
        public int watchlist_id { get; set; }
    }

    public class WatchlistItemDeleteParams
    {
        public int watchlist_id { get; set; }
        public string itemlist_id { get; set; }
    }

    public class WatchListDownloadPdfParams
    {
        public string data_id { get; set; }
        public string watchlist_id { get; set; }
    }

    public class WatchListPricewisePdfParams
    {
        public string data_id { get; set; }
        public string watchlist_id { get; set; }
    }

    public class WatchlistPricewisedetailPDFParams
    {
        public string data_id { get; set; }
        public string watchlist_id { get; set; }
    }

    public class WatchlistDownloadExcelParams
    {
        public string watchlist_id { get; set; }
    }

    public class WatchlistImagewiseExcelParams
    {
        public string watchlist_id { get; set; }
    }
}
