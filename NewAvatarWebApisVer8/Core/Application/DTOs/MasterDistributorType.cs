namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterDistributorType
    {
        public int DistTypeID { get; set; }
        public string DistType { get; set; }
        public string Remarks { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}