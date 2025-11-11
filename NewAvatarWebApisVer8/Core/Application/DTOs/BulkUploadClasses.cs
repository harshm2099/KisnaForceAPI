namespace NewAvatarWebApis.Core.Application.DTOs
{
        public class CartCancelListingParams
        {
            public int CartID { get; set; }
            public int DataID { get; set; }
        }

        public class ItemSizeListingParams
        {
            public int SizeID { get; set; }
        }

        public class ItemRequest
        {
            public string Style { get; set; }
            public string Brand { get; set; }
            public string Size { get; set; }
            public string Color { get; set; }
            public int Qty { get; set; }
            public string Remarks { get; set; }
        }

        public class BulkImportItemsParams
        {
            public int DataID { get; set; }
            public int BillingForDataID { get; set; }
            public List<ItemRequest> items;
        }

        public class BulkImportInsertItemParams
        {
            public int DataID { get; set; }
            public int DiscontinueItemAllowUsers_Sts { get; set; }
            public string ItemFranchiseSts { get; set; }
            public int Premium_collection_check_flag { get; set; }
            public string style { get; set; }
            public string brand { get; set; }
            public string size { get; set; }
            public string color { get; set; }
            public int qty { get; set; }
            public string Remarks { get; set; }
            public string insdat { get; set; }
        }

        public class BulkImportGetDataParams
        {
            public int DataID { get; set; }
        }

        public class BulkImportGetDataListing
        {
            public string id { get; set; }
            public string style { get; set; }
            public string brand { get; set; }
            public string size { get; set; }
            public string color { get; set; }
            public string qty { get; set; }
            public string remarks { get; set; }
            public string ItemName { get; set; }
            public string ItemSfx { get; set; }
            public string ItemColor { get; set; }
            public string ItemSize { get; set; }
            public string ItemQty { get; set; }
            public string ItemDate { get; set; }
            public string ItemUserId { get; set; }
            public string ItemRemarks { get; set; }
            public string ItemError { get; set; }
        }

        public class BulkUploadDeleteParams
        {
            public string temps_id { get; set; }
            public int DataID { get; set; }
        }

        public class BulkUploadVerifyDataParams
        {
            public int DataID { get; set; }

        }

        public class OrderTypeListingParams
        {
            public int DataID { get; set; }
        }

        public class OrderTypeListing
        {
            public int ordertype_mst_id { get; set; }
            public string ordertype_mst_code { get; set; }
            public string ordertype_mst_name { get; set; }
        }

        public class CheckOutBulkUploadnewParams
        {
            public int data_id { get; set; }
            public int cart_order_data_id { get; set; }
            public int cart_billing_data_id { get; set; }
            public int cart_order_login_type_id { get; set; }
            public int cart_billing_login_type_id { get; set; }
            public int ordertypeid { get; set; }
            public string cart_remarks { get; set; }
            public string cart_delivery_date { get; set; }
            public string netip { get; set; }
        }

        public class GoldCartInsertParams
        {
            public int DataId { get; set; }
            public int CartMstId { get; set; }
            public int CartItemId { get; set; }
            public int ItemId { get; set; }
            public string design_kt { get; set; }
            public decimal pure_gold { get; set; }
            public decimal gold_ktprice { get; set; }
            public decimal gold_price { get; set; }
            public decimal labour_price { get; set; }
            public decimal item_price { get; set; }
            public decimal gst_price { get; set; }
            public decimal total_price { get; set; }
            public int CartQty { get; set; }
            public string making_per_gram { get; set; }
            public string ItemOdSfx { get; set; }

        }

        public class LogInsertParams
        {
            public int DataId { get; set; }
            public int CartMstId { get; set; }
            public int CartItemId { get; set; }
            public int ItemId { get; set; }
            public int CartQty { get; set; }
            public int CartOldQty { get; set; }
            public int CartColorCommonID { get; set; }
            public int CartConfCommonID { get; set; }
            public decimal CartMRPrice { get; set; }
            public decimal CartRPrice { get; set; }
            public decimal CartDPrice { get; set; }

        }

        public class TempTableLogInsertParams
        {
            public int DataId { get; set; }
            public string OperationName { get; set; }
        }

        public class BrandDataResponse
        {
            public IllumineData IllumineData { get; set; }
            public KisnaGoldData KisnaGoldData { get; set; }
            public SilverData SilverData { get; set; }
            public OroData OroData { get; set; }
            public KisnaData KisnaData { get; set; }
        }

        public class IllumineData
        {
            public List<string> SevenDay { get; set; } = new List<string>();
            public List<string> Hours { get; set; } = new List<string>();
            public List<string> FifteenDay { get; set; } = new List<string>();
            public List<string> TwentyOneDay { get; set; } = new List<string>();
            public List<string> FiveDay { get; set; } = new List<string>();
        }

        public class KisnaGoldData
        {
            public List<string> SevenDay { get; set; } = new List<string>();
            public List<string> Hours { get; set; } = new List<string>();
            public List<string> FifteenDay { get; set; } = new List<string>();
            public List<string> TwentyOneDay { get; set; } = new List<string>();
            public List<string> FiveDay { get; set; } = new List<string>();
        }

        public class SilverData
        {
            public List<string> SevenDay { get; set; } = new List<string>();
            public List<string> Hours { get; set; } = new List<string>();
            public List<string> FifteenDay { get; set; } = new List<string>();
            public List<string> TwentyOneDay { get; set; } = new List<string>();
            public List<string> FiveDay { get; set; } = new List<string>();
        }

        public class OroData
        {
            public List<string> SevenDay { get; set; } = new List<string>();
            public List<string> Hours { get; set; } = new List<string>();
            public List<string> FifteenDay { get; set; } = new List<string>();
            public List<string> TwentyOneDay { get; set; } = new List<string>();
            public List<string> FiveDay { get; set; } = new List<string>();
        }

        public class KisnaData
        {
            public List<string> SevenDay { get; set; } = new List<string>();
            public List<string> Hours { get; set; } = new List<string>();
            public List<string> FifteenDay { get; set; } = new List<string>();
            public List<string> TwentyOneDay { get; set; } = new List<string>();
            public List<string> FiveDay { get; set; } = new List<string>();
        }

        public class CartCheckoutNoAllotWebParams
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
            public BrandDataResponse brandDataResponse { get; set; }
            public string devicetype { get; set; }
            public string devicename { get; set; }
            public string appversion { get; set; }
            public string SourceType { get; set; }
        }

        public class CartCheckoutNoAllotWebSaveCartStatusMstParams
        {
            public int DataID { get; set; }
            public int CartID { get; set; }
            public string Stage { get; set; }
            public string SourceType { get; set; }
            public string Data { get; set; }
        }

        public class CartCheckoutNoAllotWebSaveSolitaireStatusParams
        {
            public int DataID { get; set; }
            public int CartID { get; set; }
            public int CartItemID { get; set; }
            public string Stage { get; set; }
            public string DiaStkNo { get; set; }
            public string SourceType { get; set; }

        }

        public class CartCheckoutNoAllotWebSaveCartMstParams
        {
            public int DataID { get; set; }
            public int CartID { get; set; }
            public int OrderTypeID { get; set; }
            public int cart_billing_login_type_id { get; set; }
            public int cart_order_data_id { get; set; }
            public int cart_billing_data_id { get; set; }
            public string cart_remarks { get; set; }
            public string cart_delivery_date { get; set; }

            public string devicetype { get; set; }
            public string devicename { get; set; }
            public string appversion { get; set; }
            public string SourceType { get; set; }
        }

        public class CartCheckoutNoAllotWebSaveNewCartMstParams
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

        }

        public class CartCheckoutNoAllotWebUpdateCartMstParams
        {
            public int cart_id { get; set; }
            public int cart_order_data_id { get; set; }
            public int cart_billing_data_id { get; set; }
            public string cart_remarks { get; set; }
            public int data_id { get; set; }
            public string cart_delivery_date { get; set; }
            public string cartno { get; set; }
            public int count { get; set; }
            public int total { get; set; }
            public int ordertypeid { get; set; }
            public int goldcnt { get; set; }
            public int goldvalue { get; set; }
            public int goldpremium { get; set; }

        }

        public class CartCheckoutNoAllotWebDeleteAndReassignCartParams
        {
            public int DataID { get; set; }
            public int CartID { get; set; }
            public string SourceType { get; set; }
            public string MsgDetail { get; set; }
        }

        public class CartCheckoutNoAllotWebDeleteCartMstParams
        {
            public int CartID { get; set; }
        }
}
