namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class WatchListAddParams
    {
        public int data_id { get; set; }
        public int item_id { get; set; }
        public string watch_name { get; set; }
        public string watchlist_id { get; set; }
        public string product_item_remarks { get; set; }
        public string product_item_remarks_ids { get; set; }
        public int color_id { get; set; }
        public int size_id { get; set; }
    }
}
