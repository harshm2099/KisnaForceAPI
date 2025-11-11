namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class HomeDetailList
    {
        public class HomeDataListParams
        {
            //public int data_id { get; set; }
            //public string type { get; set; }
        }

        public class HomeDataListResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
            public string loader_img { get; set; }
            public List<HomeDataItem> data { get; set; }
        }

        public class HomeDataItem
        {
            public string home_id { get; set; }
            public string item_id { get; set; }
            public string cat_menu_id { get; set; }
            public string link_url { get; set; }
            public string HomeRef { get; set; }
            public string image_path { get; set; }
            public string home_mstname { get; set; }
            public string home_mstid { get; set; }
            public string loader_img { get; set; }
        }

        public class HomeListRequest
        {
            public string dataId { get; set; }
            public string dataLoginType { get; set; }
        }
    }
}
