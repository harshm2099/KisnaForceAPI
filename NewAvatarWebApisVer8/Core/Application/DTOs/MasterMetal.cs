namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterMetal
    {
        public int MetalID { get; set; }
        public string MetalCode { get; set; }
        public string MetalName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}