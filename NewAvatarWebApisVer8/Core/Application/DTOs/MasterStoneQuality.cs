namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterStoneQuality
    {
        public int StoneQualityID { get; set; }
        public string StoneQuality { get; set; }
        public string Description { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}