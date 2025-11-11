namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterStartColor
    {
        public int StarColorID { get; set; }
        public string StarCode { get; set; }
        public string StarName { get; set; }
        public string ColorCode { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}