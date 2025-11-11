namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class CategoryButtonListingParams
    {
        public int data_id { get; set; }
        public int data_login_type { get; set; }
        public int category_id { get; set; }
    }

    public class CategoryButtonListingNew
    {
        public string button_name { get; set; }
        public string btn_cd { get; set; }
        public string btn_type { get; set; }
        public string btn_image { get; set; }
    }
}
