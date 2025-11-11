namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterState
    {
        public int StateID { get; set; }
        public int RegionID { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string Description { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public int ZoneID { get; set; }
    }
}