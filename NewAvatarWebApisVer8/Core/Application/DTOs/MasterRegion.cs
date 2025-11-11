namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterRegion
    {
        public int RegionID { get; set; }
        public int ZoneID { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string Description { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
