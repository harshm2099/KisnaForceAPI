namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterItemSize
    {
        public int MstID { get; set; }
        public string MstCd { get; set; }
        public string MstName { get; set; }
        public string MstDesc { get; set; }
        public string MstValidSts { get; set; }
        public int MstFlagID { get; set; }
        public int MstImgID { get; set; }
        public int MstSortBy { get; set; }
        public string MstIconImgPath { get; set; }
        public string MstTyp { get; set; }
        public string SyncFlg { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
