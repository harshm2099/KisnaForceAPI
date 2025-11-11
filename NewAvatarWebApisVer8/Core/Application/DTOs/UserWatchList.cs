namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class UserWatchParams
    {
        public int data_id { get; set; }
    }

    public class UserWatchListing
    {
        public string watchlist_id { get; set; }
        public string watchlist_name { get; set; }
        public string created_name { get; set; }
        public string shared_name { get; set; }
        public string create_data_id { get; set; }
        public string watch_flag { get; set; }
        public string watchCount { get; set; }
    }

}
