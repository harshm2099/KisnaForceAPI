namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class T_NEW_ROUTE_USER_LIST
    {
        public int A_I { get; set; }
        public int NewRoutId { get; set; }
        public int DataID { get; set; }
        public string ShopName { get; set; }
        public string Contactnumber { get; set; }
        public string ContactPersonName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime EntDt { get; set; }
        public DateTime CngDt { get; set; }
        public string DocumentImage { get; set; }
        public string EmailId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string BirthDate { get; set; }
        public DateTime AnniversaryDate { get; set; }
        public string Remark { get; set; }
        public string CurrentBusiness { get; set; }
        public string OtherBusiness { get; set; }
        public string FirmName { get; set; }
        public string StoreLocation { get; set; }
        public string StoreSize { get; set; }
        public string InvestmentDiscussion { get; set; }
        public string NoofRetailers { get; set; }
        public string Amount { get; set; }
        public string ExistsRetailer { get; set; }
        public string InactiveKisnaRetailer { get; set; }
        public string LeadReference { get; set; }
        public string Comment { get; set; }
        public string TypeofBusiness { get; set; }
        public string TypeOtherBusiness { get; set; }
        public string BAId { get; set; }
        public string OutletCategory { get; set; }
        public string TargetValue { get; set; }
        public string MonthPlan { get; set; }
        public string DiamondSelling { get; set; }
        public string DataName { get; set; }
        public string UTName { get; set; }
        public string DataCd { get; set; }
    }
    public class ShopListRequest
    {
        public string State { get; set; }
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
    }
}