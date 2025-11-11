namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterSubCategory
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryCode { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}