using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Core.Application.DTOs
{
        public class CartCountListingParams
        {
            public int data_id { get; set; }
        }

        public class CartCountListing
        {
            public string cart_id { get; set; }
            public string item_count { get; set; }
            public string cart_color { get; set; }
            public string cart_img { get; set; }
        }

        public class CartBillingForTypeListingParams
        {
            public int data_login_type_dtldata_id { get; set; }
        }

        public class CartBillingForTypeListing
        {
            public string parent_type_id { get; set; }
            public string parent_type_code { get; set; }
            public string parent_type_name { get; set; }
            public string DataLoginTypeDtlSeqNo { get; set; }
            public string DataLoginTypeDtlCommonID { get; set; }
            public string billing_data_id { get; set; }
        }
        public class CartBillingUserListingParams
        {
            public int data_login_type_dtldata_id { get; set; }
        }

        public class CartBillingUserListing
        {
            public string data_id { get; set; }
            public string data_shop_name { get; set; }
            public string data_contact_name { get; set; }
            public string data_contact_no { get; set; }
            public string user_data_id { get; set; }
            public string DataLoginTypeDtlSeqNo { get; set; }
            public string DataLoginTypeDtlCommonID { get; set; }
            public string DataLoginTypeDtlDataID { get; set; }
            public string DataLoginTypeOrgCommonID { get; set; }
            public string DataLoginTypeCommonID { get; set; }
            public string DataLoginTypeSeqNo { get; set; }
            public string DataLoginTypePart { get; set; }
            public string DataLoginTypeValidSts { get; set; }
            public string DataAddr1 { get; set; }
            public string DataAddr2 { get; set; }
            public string DataAddrState { get; set; }
            public string DataAddrCity { get; set; }
            public string DataAddrPinCode { get; set; }
        }

        public class CartOrderBillingForTypeListingParams
        {
            public int data_login_type_dtldata_id { get; set; }
            public string fill_type { get; set; }
            public int parent_type_id { get; set; }
        }

        public class CartOrderBillingForTypeListing
        {
            public string parent_type_id { get; set; }
            public string parent_type_code { get; set; }
            public string parent_type_name { get; set; }
            public string DataLoginTypeDtlDataID { get; set; }
            public string DataLoginTypePart { get; set; }
            public string DataLoginTypeValidSts { get; set; }
        }

        public class CartOrderBillingUserListingParams
        {
            public int? data_id { get; set; }
            public int? data_login_type_dtldata_id { get; set; }
            public int? parent_type_id { get; set; }
        }

        public class CartOrderBillingUserListing
        {
            public string data_id { get; set; }
            public string parent_type_id { get; set; }
            public string parent_type_code { get; set; }
            public string parent_type_name { get; set; }
            public string data_code { get; set; }
            public string data_shop_name { get; set; }
            public string data_latitude { get; set; }
            public string data_longitude { get; set; }
            public string data_alt_contact_no { get; set; }
            public string data_alt_email { get; set; }
            public string data_tel_no { get; set; }
            public string data_gstno { get; set; }
            public string data_remarks { get; set; }
            public string data_name { get; set; }
            public string data_contact_no { get; set; }
            public string data_email { get; set; }
            public string profile_image { get; set; }
            public string login_type_id { get; set; }
            public string area_id { get; set; }
            public string img_id { get; set; }
            public string DataAddr1 { get; set; }
            public string DataAddr2 { get; set; }
            public string DataAddrState { get; set; }
            public string DataAddrCity { get; set; }
            public string DataAddrPinCode { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string data_alt_name { get; set; }
        }

        public class CartOrderTypeListingParams
        {
            public int datalogintypeid { get; set; }
            public int data_id { get; set; }
        }

        public class CartOrderTypeListing
        {
            public string ordertype_mst_id { get; set; }
            public string ordertype_mst_code { get; set; }
            public string ordertype_mst_name { get; set; }
            public string ordertype_mst_color { get; set; }
        }

        public class CartItemListingParams
        {
            public int data_id { get; set; }
            public int cart_id { get; set; }
            public int data_login_type { get; set; }
            public int page { get; set; }
            public string cart_sts { get; set; }
        }

        public class CartItemListing
        {
            public string cart_auto_id { get; set; }
            public string labour_per { get; set; }
            public string dislabour_per { get; set; }
            public string plaingold_status { get; set; }
            public string ExtraGoldWeight { get; set; }
            public string ExtraGoldPrice { get; set; }
            public string cart_id { get; set; }
            public string ItemAproxDay { get; set; }
            public string ItemDAproxDay { get; set; }
            public string data_id { get; set; }
            public string dsg_kt { get; set; }
            public string item_id { get; set; }
            public DateTime ent_dt { get; set; }
            public string item_code { get; set; }
            public string item_name { get; set; }
            public string item_sku { get; set; }
            public string item_description { get; set; }
            public string item_soliter { get; set; }
            public string is_stock { get; set; }
            public string product_item_custom_remarks_status { get; set; }
            public string ItemBrandCommonID { get; set; }
            public string ItemPlainGold { get; set; }
            public string ItemSoliterSts { get; set; }
            public string item_mrp { get; set; }
            public string item_price { get; set; }
            public string dist_price { get; set; }
            public string price_text { get; set; }
            public string cart_price { get; set; }
            public string image_path { get; set; }
            public string dsg_sfx { get; set; }
            public string dsg_size { get; set; }
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
            public string star_color { get; set; }
            public string item_color_id { get; set; }
            public string item_details { get; set; }
            public string item_diamond_details { get; set; }
            public string item_text { get; set; }
            public string more_item_details { get; set; }
            public string item_stock { get; set; }
            public string cart_item_qty { get; set; }
            public string ItemMetalCommonID { get; set; }
            public string item_removecart_img { get; set; }
            public string item_removecard_title { get; set; }
            public string rupy_symbol { get; set; }
            public string category_id { get; set; }
            public string color_common_id { get; set; }
            public string size_common_id { get; set; }
            public string color_common_name { get; set; }
            public string size_common_name { get; set; }
            public string color_common_name1 { get; set; }
            public string size_common_name1 { get; set; }
            public string ItemGenderCommonID { get; set; }
            public string item_stock_qty { get; set; }
            public string item_stock_colorsize_qty { get; set; }
            public string variantCount { get; set; }
            public string cart_cancel_qty { get; set; }
            public DateTime cart_cancel_date { get; set; }
            public string cart_cancel_by { get; set; }
            public string cart_cancel_sts { get; set; }
            public string cart_cancel_name { get; set; }
            public string ItemTypeCommonID { get; set; }
            public string ItemNosePinScrewSts { get; set; }
            public string priceflag { get; set; }
            public string SNMCCFlag { get; set; }
            public string ItemFranchiseSts { get; set; }
            public string ItemAproxDayCommonID { get; set; }
            public string CartItemID { get; set; }
            public string ItemValidSts { get; set; }
            public string indentNumber { get; set; }
            public string item_illumine { get; set; }
            public string ItemIsSRP { get; set; }
            public string isAutoInserted { get; set; }
            public string isCOOInserted { get; set; }
            public string ItemSizeAvailable { get; set; }
            public string weight { get; set; }
            public string totalLabourPer { get; set; }
            public string IsSolitaireCombo { get; set; }
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

            // public List<CartItem_sizeListing> sizeList { get; set; }
            public List<CartProduct_sizeListing> sizeList { get; set; }
            public List<CartItem_colorListing> colorList { get; set; }
            public List<CartItem_itemsColorSizeListing> itemsColorSizeList { get; set; }
            public List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList { get; set; }
            public List<Item_itemOrderInstructionListing> itemOrderInstructionList { get; set; }
            public List<CartItem_item_images_colorListing> item_images_color { get; set; }
            public List<Item_TagsListing> productTags { get; set; }
            public CartItem_approxDaysListing approxDays { get; set; }
            public string CartSoliStkNo { get; set; }
        }

        public class CartStoreParams
        {
            public int data_id { get; set; }
            public string net_ip { get; set; }
            public string cart_remark { get; set; }
            public int cart_id { get; set; }
            public int item_id { get; set; }
            public decimal cart_price { get; set; }
            public int cart_qty { get; set; }
            public decimal cart_mrprice { get; set; }
            public decimal cart_rprice { get; set; }
            public decimal cart_dprice { get; set; }
            public int product_color_mst_id { get; set; }
            public int product_size_mst_id { get; set; }
            public string product_item_remarks { get; set; }
            public string product_item_custom_remarks_status { get; set; }
            public string product_item_remarks_ids { get; set; }
            public decimal extra_gold { get; set; }
            public decimal extra_gold_price { get; set; }
            public int ItemAproxDayCommonID { get; set; }
            public string devicetype { get; set; }
            public string devicename { get; set; }
            public string appversion { get; set; }
        }
        public class CartStore
        {
            public int cart_auto_id { get; set; }
        }

        public class CartInsertParams
        {
            public int data_id { get; set; }
            public string net_ip { get; set; }
            public string cart_remark { get; set; }
            public int cart_id { get; set; }
            public int item_id { get; set; }
            public decimal cart_price { get; set; }
            public int cart_qty { get; set; }
            public int is_stock { get; set; }
            public decimal cart_mrprice { get; set; }
            public decimal cart_rprice { get; set; }
            public decimal cart_dprice { get; set; }
            public int product_color_mst_id { get; set; }
            public int product_size_mst_id { get; set; }
            public string product_item_remarks { get; set; }
            public string product_item_remarks_ids { get; set; }
            public string product_item_custom_remarks { get; set; }
            public string product_item_custom_remarks_ids { get; set; }
            public int product_item_custom_remarks_status { get; set; }
            public decimal extra_gold { get; set; }
            public decimal extra_gold_price { get; set; }
            public int ItemAproxDayCommonID { get; set; }
            public string devicetype { get; set; }
            public string devicename { get; set; }
            public string appversion { get; set; }
        }
        public class CartInsert
        {
            public int cart_auto_id { get; set; }
        }

        public class CheckItemIsSolitaireComboListing
        {
            public int is_valid { get; set; }
            public int min_amount { get; set; }
            public decimal cart_price { get; set; }
            public string collection_name { get; set; }
        }
        public class CheckItemIsSolitaireComboParams
        {
            public int cart_id { get; set; }
            public int cart_billing_data_id { get; set; }
            public int ordertypeid { get; set; }
            public string implodeCartMstItemIds { get; set; }
        }

        public class GoldDynamicPriceCartParams
        {
            public int DataId { get; set; }
            public int ItemId { get; set; }
            public int CartItemId { get; set; }
            public int CartMstId { get; set; }
            public int CartQty { get; set; }
            public decimal gold_price { get; set; }
            public string making_per_gram { get; set; }
            public decimal total_goldvalue { get; set; }
            public decimal total_labour { get; set; }
            public decimal finalGoldValue { get; set; }
            public decimal item_price { get; set; }
            public decimal total_price { get; set; }
            public decimal gst_price { get; set; }
            public decimal dp_final_price { get; set; }
            public string design_kt { get; set; }
            public string labour { get; set; }
        }

        public class CartCheckoutAllotNewParams
        {
            public int cart_id { get; set; }
            public int data_id { get; set; }
            public int cart_order_data_id { get; set; }
            public int cart_billing_data_id { get; set; }
            public int cart_order_login_type_id { get; set; }
            public int cart_billing_login_type_id { get; set; }
            public string cart_remarks { get; set; }
            public DateTime cart_delivery_date { get; set; }
            public string cart_string { get; set; }
            public int ordertypeid { get; set; }
            public string devicetype { get; set; }
            public string devicename { get; set; }
            public string appversion { get; set; }
            public int? consumer_form_id { get; set; }
    }

        public class CheckItemIsNewPremiumCollectionListing
        {
            public int is_valid { get; set; }
            public int min_amount { get; set; }
            public decimal cart_price { get; set; }
            public string collection_name { get; set; }
        }
        public class CheckItemIsNewPremiumCollectionParams
        {
            public int cart_id { get; set; }
            public int cart_billing_data_id { get; set; }
            public int ordertypeid { get; set; }
            public string implodeCartMstItemIds { get; set; }
        }

        public class CartCheckoutAllotNew
        {
            public int cart_auto_id { get; set; }
        }

        public class CartCheckoutNoAllotNewParams
        {
            public int cart_id { get; set; }
            public int data_id { get; set; }
            public int cart_order_data_id { get; set; }
            public int cart_billing_data_id { get; set; }
            public int cart_order_login_type_id { get; set; }
            public int cart_billing_login_type_id { get; set; }
            public string cart_remarks { get; set; }
            public string cart_delivery_date { get; set; }
            public string itemall { get; set; }
            public int ordertypeid { get; set; }
            public string devicetype { get; set; }
            public string devicename { get; set; }
            public string appversion { get; set; }
            public string SourceType { get; set; }
            public brand_data data { get; set; }
        }

        public class CartCheckoutNoAllotNew
        {
            public int cart_auto_id { get; set; }
        }

        public class CartCheckoutNoAllotNewParamsPart2
        {
            public int cart_id { get; set; }
            public int data_id { get; set; }
            public string CartSoliStkNoData { get; set; }
            public string DiaBookRespose { get; set; }
            public int item_id { get; set; }
            public int cart_item_id { get; set; }
        }

        public class CARTCHECKOUTNOALLOTNEW_UPDATE_CARTMSTITEM_PRICESParams
        {
            public int CartItemID { get; set; }
            public decimal CartMRPrice { get; set; }
            public decimal CartPrice { get; set; }
            public decimal CartDPrice { get; set; }
            public decimal FranMarginCurDP { get; set; }
            public decimal CartItemMetalWt { get; set; }
            public int FlagValue { get; set; }
        }

        public class CartCheckoutNoAllotNewSaveNewCartMstParams
        {
            public int DataID { get; set; }
            public int CartID { get; set; }
            public int OrderTypeID { get; set; }
            public int cart_order_login_type_id { get; set; }
            public int cart_billing_login_type_id { get; set; }
            public int cart_order_data_id { get; set; }
            public int cart_billing_data_id { get; set; }
            public string cart_remarks { get; set; }
            public string cart_delivery_date { get; set; }
            public string itemall { get; set; }

            public string devicetype { get; set; }
            public string devicename { get; set; }
            public string appversion { get; set; }
            public string SourceType { get; set; }
            public string APP_ENV { get; set; }

            public int oroseven_cnt { get; set; }
            public string orosevenString { get; set; }
            public int orofifty_cnt { get; set; }
            public string orofiftyString { get; set; }
            public int orohours_cnt { get; set; }
            public string orohoursString { get; set; }
            public int orotwentyone_cnt { get; set; }
            public string orotwentyoneString { get; set; }
            public int orofive_cnt { get; set; }
            public string orofiveString { get; set; }

            public int kisnaseven_cnt { get; set; }
            public string kisnasevenString { get; set; }
            public int kisnafifty_cnt { get; set; }
            public string kisnafiftyString { get; set; }
            public int kisnahours_cnt { get; set; }
            public string kisnahoursString { get; set; }
            public int kisnatwentyone_cnt { get; set; }
            public string kisnatwentyoneString { get; set; }
            public int kisnafive_cnt { get; set; }
            public string kisnafiveString { get; set; }

            public int kgseven_cnt { get; set; }
            public string kgsevenString { get; set; }
            public int kgfifty_cnt { get; set; }
            public string kgfiftyString { get; set; }
            public int kghours_cnt { get; set; }
            public string kghoursString { get; set; }
            public int kgtwentyone_cnt { get; set; }
            public string kgtwentyoneString { get; set; }
            public int kgfive_cnt { get; set; }
            public string kgfiveString { get; set; }

            public int silverseven_cnt { get; set; }
            public string silversevenString { get; set; }
            public int silverfifty_cnt { get; set; }
            public string silverfiftyString { get; set; }
            public int silverhours_cnt { get; set; }
            public string silverhoursString { get; set; }
            public int silvertwentyone_cnt { get; set; }
            public string silvertwentyoneString { get; set; }
            public int silverfive_cnt { get; set; }
            public string silverfiveString { get; set; }

            public int illumineseven_cnt { get; set; }
            public string illuminesevenString { get; set; }
            public int illuminefifty_cnt { get; set; }
            public string illuminefiftyString { get; set; }
            public int illuminehours_cnt { get; set; }
            public string illuminehoursString { get; set; }
            public int illuminetwentyone_cnt { get; set; }
            public string illuminetwentyoneString { get; set; }
            public int illuminefive_cnt { get; set; }
            public string illuminefiveString { get; set; }
        }

        public class brand_data
        {
            public Kisna_Data kisna_data { get; set; }
            public Kisna_Gold_Data kisna_gold_data { get; set; }
            public Oro_Data oro_data { get; set; }
            public Illumine_Data illumine_data { get; set; }
            public Silver_Data silver_data { get; set; }
        }

        public class Kisna_Data
        {
            public List<string> fifteen_day { get; set; } = new List<string>();
            public List<string> hours { get; set; } = new List<string>();
            public List<string> seven_day { get; set; } = new List<string>();
            public List<string> twentyone_day { get; set; } = new List<string>();
            public List<string> five_day { get; set; } = new List<string>();
        }

        public class Kisna_Gold_Data
        {
            public List<string> fifteen_day { get; set; } = new List<string>();
            public List<string> hours { get; set; } = new List<string>();
            public List<string> seven_day { get; set; } = new List<string>();
            public List<string> twentyone_day { get; set; } = new List<string>();
            public List<string> five_day { get; set; } = new List<string>();
        }

        public class Oro_Data
        {
            public List<string> fifteen_day { get; set; } = new List<string>();
            public List<string> hours { get; set; } = new List<string>();
            public List<string> seven_day { get; set; } = new List<string>();
            public List<string> twentyone_day { get; set; } = new List<string>();
            public List<string> five_day { get; set; } = new List<string>();
        }

        public class Illumine_Data
        {
            public List<string> fifteen_day { get; set; } = new List<string>();
            public List<string> hours { get; set; } = new List<string>();
            public List<string> seven_day { get; set; } = new List<string>();
            public List<string> twentyone_day { get; set; } = new List<string>();
            public List<string> five_day { get; set; } = new List<string>();
        }

        public class Silver_Data
        {
            public List<string> fifteen_day { get; set; } = new List<string>();
            public List<string> hours { get; set; } = new List<string>();
            public List<string> seven_day { get; set; } = new List<string>();
            public List<string> twentyone_day { get; set; } = new List<string>();
            public List<string> five_day { get; set; } = new List<string>();
        }

        public class CartItemDeleteParams
        {
            public string cart_auto_id { get; set; }
        }

        public class CartUpdateItemParams
        {
            public int cart_auto_id { get; set; }
            public int cart_id { get; set; }
            public int item_id { get; set; }
            public int size_common_id { get; set; }
            public int color_common_id { get; set; }
        }

    public class CartItemPriceDetailListingParams
    {
        public int DataID { get; set; }
        public int ItemID { get; set; }
        public int SizeID { get; set; }
        public int CategoryID { get; set; }
        public int ItemBrandCommonID { get; set; }
        public decimal ItemGrossWt { get; set; }
        public decimal ItemMetalWt { get; set; }
        public int IsWeightCalcRequired { get; set; }
        public int ItemGenderCommonID { get; set; }
    }

    public class CartItemPriceDetailListing
    {
        // Design
        public string ItemOdSfx { get; set; }
        public string design { get; set; }
        public string design_kt { get; set; }

        // Gold
        public decimal pure_gold { get; set; }
        public decimal gold_wt { get; set; }
        public decimal gold_ktprice { get; set; }
        public decimal gold_price { get; set; }

        // Platinum
        public decimal platinum { get; set; }
        public decimal platinum_wt { get; set; }
        public decimal platinum_ktprice { get; set; }
        public decimal platinum_price { get; set; }

        // Diamond
        public decimal diamond_qty { get; set; }
        public decimal diamond_wt { get; set; }
        public decimal diamond_price { get; set; }

        // Stone
        public decimal stone_wt { get; set; }
        public decimal stone_qty { get; set; }
        public decimal stone_price { get; set; }

        // Metal
        public decimal metal_price { get; set; }

        // Other
        public decimal other_price { get; set; }

        // Labour
        public string labour { get; set; }
        public decimal labour_percentage { get; set; }
        public decimal labour_price { get; set; }

        // ItemPrice without GST
        public decimal item_price { get; set; }
        public string gst_percent { get; set; }
        public decimal GST { get; set; }

        // ItemPrice with GST
        public decimal total_price { get; set; }

        // DP
        public string dp_labour_Per { get; set; }
        public string dp_labour_percentage { get; set; }
        public decimal dp_labour_price { get; set; }
        public decimal dp_price { get; set; }
        public decimal DP_GST { get; set; }
        public decimal dp_final_price { get; set; }
        public decimal dp_maring_percent { get; set; }
        public string dp_is_labour { get; set; }
        public decimal dp_gold_price { get; set; }
        public decimal dp_platinum_price { get; set; }
        public decimal dp_metal_price { get; set; }
        public decimal dp_diamond_price { get; set; }
        public decimal dp_stone_price { get; set; }
    }

    public class CartItemDPRPCALCListingParams
    {
        public int DataID { get; set; }
        public decimal MRP { get; set; }
    }

    public class CartItemDPRPCALCListing
    {
        public decimal D_Price { get; set; }
        public decimal R_Price { get; set; }
        public decimal D_M_Percentage { get; set; }
    }

    public class CartChildListParams
    {
        public int data_id { get; set; }
    }

    public class CartChildListing
    {
        public string data_id { get; set; }
        public string parent_type_id { get; set; }
        public string parent_type_code { get; set; }
        public string parent_type_name { get; set; }
        public string data_code { get; set; }
        public string data_shop_name { get; set; }
        public string data_latitude { get; set; }
        public string data_longitude { get; set; }
        public string data_alt_contact_no { get; set; }
        public string data_alt_email { get; set; }
        public string data_tel_no { get; set; }
        public string data_gstno { get; set; }
        public string data_remarks { get; set; }
        public string data_name { get; set; }
        public string data_contact_no { get; set; }
        public string data_email { get; set; }
        public string profile_image { get; set; }
        public string login_type_id { get; set; }
        public string area_id { get; set; }
        public string img_id { get; set; }
        public string address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string data_alt_name { get; set; }
        public string CartID { get; set; }
    }

    public class HomeScreenMasterParams
    {
        public int data_id { get; set; }
        public string type { get; set; }
    }

    public class HomeScreenMasterListing
    {
        public string home_id { get; set; }
        public string item_id { get; set; }
        public string cat_menu_id { get; set; }
        public string link_url { get; set; }
        public string HomeRef { get; set; }
        public string image_path { get; set; }
        public string home_mstname { get; set; }
        public string home_mstid { get; set; }
        public string loader_img { get; set; }
    }

    public class OrderListParams
    {
        public string data_id { get; set; }
        public string order_track_type { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string search_field { get; set; }
    }

    public class OrderListResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int current_page { get; set; }
        public int last_page { get; set; }
        public int total_items { get; set; }
        public List<OrderListData> data { get; set; }
        public List<OrderListColor> orderColorList { get; set; }
    }

    public class OrderListData
    {
        public string cart_id { get; set; }
        public string order_no { get; set; }
        public string PONo { get; set; }
        public string cart_date { get; set; }
        public string status { get; set; }
        public string user_name { get; set; }
        public string cart_creater_name { get; set; }
        public string orderColor { get; set; }
        public string amount { get; set; }
        public string CartChkOutDt { get; set; }
        public string cart_total_qty { get; set; }
        public string CartBillingDataID { get; set; }
        public string DeliveryStatus { get; set; }
        public OrderListApproxDays approxDays { get; set; }
        public string gold_rate { get; set; }
    }

    public class OrderListApproxDays
    {
        public string manufactureStartDate { get; set; }
        public string manufactureEndDate { get; set; }
        public string deliveryStartDate { get; set; }
        public string deliveryEndDate { get; set; }
        public string deliveryInDays { get; set; }
    }

    public class OrderListColor
    {
        public string ordertype_mst_id { get; set; }
        public string ordertype_mst_code { get; set; }
        public string ordertype_mst_name { get; set; }
        public string ordertype_mst_color { get; set; }
    }

    public class CheckItemSizeRangeRequest
    {
        public string? dataId { get; set; }
        public string? dataLoginType { get; set; }
        public string? categoryId { get; set; }
        public string? itemId { get; set; }
        public string? sizeId { get; set; }
    }

    public class CheckoutVerifyOtpRequest
    {
        public string? dataId { get; set; }
        public string? otp { get; set; }
    }
}
