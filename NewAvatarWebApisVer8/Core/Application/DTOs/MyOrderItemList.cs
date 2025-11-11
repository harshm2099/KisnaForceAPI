namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MyOrderItemListingParams
    {
        public int cart_id { get; set; }
        public int data_id { get; set; }
    }
    public class MyOrderItemListing
    {
        public string cart_auto_id { get; set; }
        public string data_id { get; set; }
        public string item_id { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string item_sku { get; set; }
        public string item_description { get; set; }
        public string image_path { get; set; }
        public string item_text { get; set; }
        public string indentNumber { get; set; }
        public string more_item_details { get; set; }
        public string cart_item_qty { get; set; }
        public string cancel_quantity { get; set; }
        public string Cart_Price { get; set; }
        public string total_amount { get; set; }
        public string more_item_details_new { get; set; }

        public string CartPriceDetails { get; set; }
        public string CartPriceWithQtyDetails { get; set; }
        public string FormattedCartPrice { get; set; }
        public string Formattedtotal_amount { get; set; }
    }

}
