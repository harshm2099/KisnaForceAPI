namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class SoliCartStoreParams
    {
        public int data_id { get; set; }
        public string net_ip { get; set; }
        public string cart_remark { get; set; }
        public int cart_id {  get; set; }
        public int item_id {  get; set; }
        public decimal cart_price { get; set; }
        public int cart_qty {  get; set; }
        public int soli_refrence_id { get; set; }
        public decimal cart_mrprice { get; set; }
        public decimal cart_rprice { get; set; }
        public decimal cart_dprice { get; set; }
        public int product_color_mst_id {  get; set; }
        public int product_size_mst_id { get; set; }
        public string product_item_remarks {  get; set; }
        public string product_item_remarks_ids {  get; set; }
        public decimal extra_gold { get; set; }
        public decimal extra_gold_price { get; set; }
        public int ItemAproxDayCommonID { get; set; }

    }

    public class SoliCartStore
    {
        public int cart_auto_id { get; set; }
    }
}
