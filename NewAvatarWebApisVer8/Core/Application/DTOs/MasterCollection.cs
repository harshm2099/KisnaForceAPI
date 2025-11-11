namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterCollection
    {
        public int CollectionID { get; set; }
        public string CollectionCode { get; set; }
        public string CollectionName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}