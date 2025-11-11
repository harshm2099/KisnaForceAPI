namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterDistrict
    {
        public int DistrictID { get; set; }
        public int StateID { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string Description { get; set; }
        public decimal SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class MasterTerritory
    {
        public Int32 MstId { get; set; }
        public string MstCd { get; set; }
        public string MstName { get; set; }
        public string MstDesc { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public decimal SortOrder { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

    }
}
