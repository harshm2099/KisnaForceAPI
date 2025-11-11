namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterCustomerCare
    {
        public int CcID { get; set; }
        public string CCName { get; set; }
        public string CCEmail { get; set; }
        public string CCMobile { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}