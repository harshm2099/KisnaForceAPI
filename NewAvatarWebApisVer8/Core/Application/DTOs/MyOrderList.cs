namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MyOrderListingParams
    {
        public int data_id { get; set; }
    }
    public class MyOrderListing
    {
        public string CartID { get; set; }
        public string CartNo { get; set; }
        public string CartDataID { get; set; }
        public string DataLoginTypeID { get; set; }
        public string PONo { get; set; }
        public string UserName { get; set; }
        public string CartChkOutDt { get; set; }
        public string CartRemarks { get; set; }
        public string Cart_Status { get; set; }
        public string CartQty { get; set; }
        public string Total_Items_Price { get; set; }
        public string CartStatus { get; set; }
    }

    public class CartSingleCancelListingParams
    {
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int DataID { get; set; }
    }

    public class OrderItemCancelRequest
    {
        public string? CartId { get; set; }
        public string? CartItemId { get; set; }
        public string? ItemId { get; set; }
        public string? CartQty { get; set; }
        public string? DataId { get; set; }
        public string? Email { get; set; }
    }

    // Models/DataMaster.cs
    //public class DataMaster
    //{
    //    public int DataID { get; set; }
    //    public string DataEmail { get; set; }
    //    public int DataLoginTypeID { get; set; }
    //    public string DataValidSts { get; set; } = "Y";
    //}

    // Models/CommonMaster.cs
    //public class CommonMaster
    //{
    //    public int MstID { get; set; }
    //    public string MstName { get; set; }
    //    public string MstTyp { get; set; }
    //    public string MstCd { get; set; }
    //    public string MstValidSts { get; set; } = "Y";
    //    public int MstFlagID { get; set; }
    //}

    // Models/EmailData.cs
    public class EmailData
    {
        public string EmailIds { get; set; }
    }

    // Models/CartItem.cs
    public class CartItem
    {
        // Map from SQL columns to C# properties
        public int ordertyid { get; set; }
        public string? title { get; set; }
        public string? BillName { get; set; }
        public string? orderName { get; set; }
        public string? orderType { get; set; }
        public string? OrderCartName { get; set; }
        public string? userName { get; set; }
        public string? BillAddress { get; set; }
        public string? CartNo { get; set; }
        public string? cart_chkout_date { get; set; }
        public string? itemName { get; set; }
        public string? item_id { get; set; }
        public string? item_soliter { get; set; }
        public decimal CartQty { get; set; }
        public decimal CartDPrice { get; set; }
        public decimal CartRPrice { get; set; }
        public decimal CartMRPrice { get; set; }
        public string? colorCode { get; set; }
        public string? colorName { get; set; }
        public string? sizeCode { get; set; }
        public string? sizeName { get; set; }
        public string? CartItemInstruction { get; set; }
        public string? cart_item_remarks { get; set; }
        public int CartItemID { get; set; }
        public int CartMstID { get; set; }
        public int CartItemMstID { get; set; }
        public decimal CartPrice { get; set; }
        public DateTime CartItemEntDt { get; set; }
        public int CartColorCommonID { get; set; }
        public int CartConfCommonID { get; set; }
        public int CartBillingDataID { get; set; }
        public string? BillType { get; set; }
        public string? subject_old { get; set; }
        public string? subject { get; set; }
        public string? item_category { get; set; }
        public string? ItemOdSfx { get; set; }
        public string? cart_cancel_sts { get; set; }
        public decimal? cart_cancel_qty { get; set; }
        public string? cart_cancel_date { get; set; }
        public int CartCreatedDataID { get; set; }
        public int CartDataID { get; set; }
        public string? CartCancelRemark { get; set; }
        public string? item_illumine { get; set; }
    }

    // Models/SolitaireStock.cs
    //public class SolitaireStock
    //{
    //    public string IsAvailableStk { get; set; }
    //    public string CartSoliStkNo { get; set; }
    //}

    // Models/EmailRequest.cs
    public class EmailRequest
    {
        public int CartId { get; set; }
        public string K { get; set; } = "";
        public string YourEmail { get; set; } = "";
        public List<int> SelectedCartItemIDs { get; set; } = new List<int>();
    }

    // Models/ApiResponse.cs
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public static class AppConstants
    {
        public const int LOGIN_TYPE_COMMON_ID = 61;
        public const int ILLUMINE_COLLECTION = 20169;
    }
}
