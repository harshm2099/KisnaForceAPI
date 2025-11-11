namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterPart
    {
        public int PartID { get; set; }
        public string PartName { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}