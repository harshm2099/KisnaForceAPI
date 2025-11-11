namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterArea
    {
        public int AreaID { get; set; }
        public int CityID { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string Description { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}