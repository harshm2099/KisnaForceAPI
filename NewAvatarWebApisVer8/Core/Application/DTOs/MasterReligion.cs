namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterReligion
    {
        public int ReligionID { get; set; }
        public string ReligionName { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}