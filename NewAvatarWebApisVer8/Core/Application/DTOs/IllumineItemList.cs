using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class IllumineItemListingParams
    {
        public int page { get; set; }
        public int default_limit_app_page { get; set; }
        public int data_id { get; set; }
        public int category_id { get; set; }
        public string sub_category_id { get; set; }
        public string sort_id { get; set; }
        public string dsg_color { get; set; }
        public string amount { get; set; }
        public string metalwt { get; set; }
        public string diawt { get; set; }
        public string gender_id { get; set; }
        public string brand { get; set; }
        public string item_sub_category_id { get; set; }
    }

    public class IllumineItemListing
    {
        public string item_id { get; set; }
        public string item_mrp { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public string category_id { get; set; }
        public string item_kt { get; set; }
        public string rupy_symbol { get; set; }
        public string img_watch_title { get; set; }
        public string img_wish_title { get; set; }
        public string wish_count { get; set; }
        public string image_path { get; set; }
        public string mostOrder { get; set; }
        public List<Item_TagsListing> productTags { get; set; }
    }


}
