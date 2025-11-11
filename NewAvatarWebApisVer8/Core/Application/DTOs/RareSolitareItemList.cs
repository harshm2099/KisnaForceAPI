using NewAvatarWebApis.Models;
using System.Collections.Generic;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class RareSolitareItemListingParams
    {
        public string page { get; set; }
        public string default_limit_app_page { get; set; }
        public string data_id { get; set; }
        public string data_login_type { get; set; }
        public string master_common_id { get; set; }
        public string category_id { get; set; }
        public string sub_category_id { get; set; }
        public string sort_id { get; set; }
        public string dsg_color { get; set; }
        public string amount { get; set; }
        public string metalwt { get; set; }
        public string diawt { get; set; }
        public string gender_id { get; set; }
        public string brand { get; set; }
        public string button_cd { get; set; }
        public string mode { get; set; }
    }
    public class RareSolitareItemListing
    {
        public string itemId { get; set; }
        public string itemMrp { get; set; }
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        public string categoryId { get; set; }
        public string itemKt { get; set; }
        public string isNewCollection { get; set; }
        public string rupySymbol { get; set; }
        public string imgWatchTitle { get; set; }
        public string imgWishTitle { get; set; }
        public string wishCount { get; set; }
        public string imagePath { get; set; }
        public string mostOrder { get; set; }
        public List<Item_TagsListing> productTags { get; set; }
    }

}
