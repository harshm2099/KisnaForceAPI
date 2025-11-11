namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterCity
    {
        public int CityID { get; set; }
        public int DistrictID { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string Description { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public int TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public string DistrictName { get; set; }
        
    }
}