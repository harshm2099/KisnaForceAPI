
namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class SoliCatList
    {
        public string data_id { get; set; }
        public string data_login_type { get; set; }
        public string display { get; set; }
        
        public string CATEGORY_DEFAULT_COMMON_ID { get; set; }
        public string AWS_IMAGE_DEFAULT_COMMON_ID { get; set; }
        public string STATE_COMMON_ID { get; set; }
        public string ILLUMINE_COLLECTION { get; set; }
        public string device_type { get; set; }

        public SoliCatList()
        {
            CATEGORY_DEFAULT_COMMON_ID = "56";
            AWS_IMAGE_DEFAULT_COMMON_ID = "15716";
            STATE_COMMON_ID = "275";
            ILLUMINE_COLLECTION = "20169";
            device_type = "0";
        }

        public class SoliCatList_Static
        {
            public string success { get; set; }
            public string message { get; set; }
            public IList<SoliCatList_Records> data { get; set; }
        }

        public class SoliCatList_Records
        {
            public string category_id { get; set; }
            public string category_name { get; set; }
            public string master_common_id { get; set; }
            public string image_path { get; set; }
            public string count { get; set; }
        }
    }
}