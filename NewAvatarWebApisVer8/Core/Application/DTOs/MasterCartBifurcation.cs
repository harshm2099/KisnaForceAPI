namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterCartBifurcation
    {
        public int DaysID { get; set; }
        public string DaysCode { get; set; }
        public string DaysName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}