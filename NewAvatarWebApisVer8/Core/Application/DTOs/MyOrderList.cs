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

}
