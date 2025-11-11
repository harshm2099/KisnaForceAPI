namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class OrderTrackingDataListParams
    {
        public string? TrackCartMstId { get; set; }
    }

    public class OrderTrackingDataListResponse
    {
        public string? TrackingType { get; set; }
        public string? TrackCartMstId { get; set; }
        public string? TrackCartLocalID { get; set; }
        public string? TotalRec { get; set; }
        public string? MstSortBY { get; set; }
    }

    public class OrderTrackingSingleDataListParams
    {
        public string? CartItemId { get; set; }
        public string? CartId { get; set; }
        public string? CartStatus { get; set; }
        public string? DataId { get; set; }
    }

    public class OrderTrackingSingleDataItemResponse
    {
        public string? TrackType { get; set; }
        public string? TrackId { get; set; }
        public string? CartItemId { get; set; }
        public string? CartId { get; set; }
        public string? TrackCartCommonId { get; set; }
        public string? TrackDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? IsLinkOption { get; set; }
        public string? IconImage { get; set; }
        public string? ParcelId { get; set; }
        public string? CouriorName { get; set; }
        public string? DocketNo { get; set; }
        public string? ParcelCustomerName { get; set; }
        public string? ParcelLink { get; set; }
        public string? AcknowledgmentUrl { get; set; }
        public string? OrderNo { get; set; }
        public string? EmrOrderNo { get; set; }
        public string? OrderDate { get; set; }
    }
}
