namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterProductType
    {
        public int PTypeID { get; set; }
        public string PTypeName { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}