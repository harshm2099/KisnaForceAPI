namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterDesignStone
    {
        public int StoneID { get; set; }
        public string StoneCode { get; set; }
        public string StoneName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}