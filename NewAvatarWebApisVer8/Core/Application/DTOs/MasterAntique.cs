namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterAntique
    {
        public int PartID { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}