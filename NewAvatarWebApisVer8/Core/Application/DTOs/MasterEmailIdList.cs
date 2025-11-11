namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterEmailIdList
    {
        public int ID { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}