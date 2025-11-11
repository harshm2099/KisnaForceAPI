namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class ImageView
    {
        public int ImageViewID { get; set; }
        public string ImageViewCode { get; set; }
        public string ViewName { get; set; }
        public string Description { get; set; }
        public string SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}