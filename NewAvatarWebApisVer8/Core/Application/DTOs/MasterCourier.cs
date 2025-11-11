namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterCourier
    {
        public int CourierID { get; set; }
        public string CourierName { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}