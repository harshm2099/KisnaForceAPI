namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterTaluka
    {
        public int TalukaID { get; set; }
        public int DistrictID { get; set; }
        public string TalukaCode { get; set; }
        public string TalukaName { get; set; }
        public string Description { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}