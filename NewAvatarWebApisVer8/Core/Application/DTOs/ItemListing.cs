namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class ItemListingParams
    {
        public int UserId { get; set; }
        public int DataId { get; set; }
        public int UserTypeId { get; set; }
        public string Mode {  get; set; }
        public int CategoryId { get; set; }
        public int SortId { get; set; }
        public string Variant { get; set; }
        public string Item_Name { get; set; }
        public string SubCategoryID { get; set; }
        public string RingSize { get; set; }
        public string DsgKt { get; set; }
        public string DsgColor { get; set; }
        public decimal price  {  get; set; }
        public decimal metalwt { get; set; }
        public decimal diawt { get; set; }
        public int Item_ID { get; set; }
        public string Stock_Av { get; set; }
        public string Family_Av { get; set; }
        public string Regular_Av { get; set; }
        public string wearit { get; set; }
        public string tryon { get; set; }
        public string gender { get; set; }
        public string TagWiseFilter { get; set; }
        public string BrandWiseFilter { get; set; }
        public string delivery_days { get; set; }
        public string ItemSubCtgIDs { get; set; }
        public string ItemSubSubCtgIDs { get; set; }
        public string salesLocation { get; set; }
        public string desginTimeLine { get; set; }

    }

    public class ItemListing
    {
        public int item_id { get; set; }
        public int category_id { get; set; }
        public string item_code { get; set; }
        public string ItemAproxDay { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public decimal item_mrp { get; set; }
        public decimal item_price { get; set; }
        public decimal dist_price { get; set; }
        public string dsg_size { get; set; }
        public string dsg_kt { get; set; }
        public string dsg_color { get; set; }
        public string item_kt { get; set; }
        public string item_color { get; set; }
        public string item_metal { get; set; }
        public string item_stone { get; set; }
        public decimal item_stone_wt { get; set; }
        public Int32 item_stone_qty { get; set; }
        public string star_color { get; set; }
        public int ItemMetalCommonID { get; set; }
        public string ItemNosePinScrewSts { get; set; }
        public int ItemAproxDayCommonID { get; set; }
        public string priceflag { get; set; }
        public string ItemPlainGold { get; set; }
        public string ItemSoliterSts { get; set; }
        public int ItemGenderCommonID { get; set; }
        public int sub_category_id { get; set; }
        public string max_qty_sold { get; set; }
        public string cart_img { get; set; }
        public string image_path { get; set; }

    }
}
