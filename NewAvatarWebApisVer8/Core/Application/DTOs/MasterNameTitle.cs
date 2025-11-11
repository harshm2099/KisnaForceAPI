namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterNameTitle
    {
        public int TitleID { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}