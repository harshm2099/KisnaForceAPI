namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class OrderTrackingDataListParams
    {
        public string? TrackCartMstId { get; set; }
    }

    public class OrderTrackingSingleDataListParams
    {
        public string? CartItemId { get; set; }
        public string? CartId { get; set; }
        public string? CartStatus { get; set; }
        public string? DataId { get; set; }
    }

    public class OrderTrackingItemDetailDataRequest
    {
        public string? TrackCartMstId { get; set; }
        public string? TrackCartCommonId { get; set; }
        public string? DataLoginType { get; set; }
        public string? DataId { get; set; }
        public string? MasterCommonId { get; set; }
        public string? CartStatus { get; set; }
    }
}
