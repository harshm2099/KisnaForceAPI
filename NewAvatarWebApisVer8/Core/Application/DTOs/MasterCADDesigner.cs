namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterCADDesigner
    {
        public int DsgID { get; set; }
        public string DsgCode { get; set; }
        public string DsgName { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}