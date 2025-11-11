namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class AllCategoryListingParams
    {
        public string type { get; set; }
        public string device_type { get; set; }
        public int data_login_type { get; set; }
        public int categoryid { get; set; }
        public int dataid { get; set; }
    }

    public class AllCategoryListing
    {
        public string category_id { get; set; }
        public string category_name { get; set; }
        public string master_common_id { get; set; }
        public string image_path { get; set; }
        public string count { get; set; }
    }

    public class CategoryButtonListing
    {
        public string button_name { get; set; }
        public string btn_cd { get; set; }
    }

    public class AllCategoryListingParamsNew
    {
        public int dataid { get; set; }
        public int data_login_type { get; set; }
    }

    public class GoldPriceRateWise
    {
        public int dataid { get; set; }
    }

    public class GoldPriceRateWiseResponse
    {
        public string name { get; set; }
        public string price { get; set; }
    }

    public class PriceRateWiseResponse
    {
        public string range { get; set; }
        public string imagePath { get; set; }
    }

    public class CategoryButtonListRequest
    {
        public string dataId  { get; set; }
        public string dataLoginId { get; set; }
        public string categoryId { get; set; }
    }

    public class SplashScreenListRequest
    {
        public int dataid { get; set; }
    }
}
