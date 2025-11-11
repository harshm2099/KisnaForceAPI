namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class SearchAllItemsListParams
    {
        public int data_id { get; set; }
        public string item_name { get; set; }
        public string display { get; set; }
        public int page { get; set; }
    }

    public class SearchAllItemsListing
    {
        public string category_id { get; set; }
        public string item_id { get; set; }
        public string item_code { get; set; }
        public string ItemName { get; set; }
        public string brand { get; set; }
        public string item_name { get; set; }
        public string item_decription { get; set; }
        public string item_type_common_id { get; set; }
        public string ItemFranchiseSts { get; set; }
        public string priceflag { get; set; }
        public string ItemPlainGold { get; set; }
        public string ItemIsSRP { get; set; }
        public string mrp_withtax { get; set; }
    }
}
