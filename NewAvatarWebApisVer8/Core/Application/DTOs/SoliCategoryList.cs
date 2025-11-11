namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class SoliCategoryListingParams
    {
        public string display { get; set; }
        public string devicetype { get; set; }
    }

    public class SoliCategoryListing
    {
        public string category_id { get; set; }
        public string category_name { get; set; }
        public string master_common_id { get; set; }
        public string image_path { get; set; }
        public string count { get; set; }
    }

    public class SolitaireFilterRequest
    {
        public string category_id { get; set; }
        public string button_code { get; set; }
        public string master_common_id { get; set; }
        public string data_login_type { get; set; }
    }

    public class SolitaireDetailsRequest
    {
        public string dataId { get; set; }
        public string dataLoginType { get; set; }
        public string categoryId { get; set; }
        public string itemId { get; set; }
        public string variant { get; set; }
        public string itemName { get; set; }
        public string masterCommonId { get; set; }
    }
}
