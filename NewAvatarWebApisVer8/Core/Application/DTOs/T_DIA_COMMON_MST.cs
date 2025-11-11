namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class T_DIA_COMMON_MST
    {
        public int FieldID { get; set; }
        public string Field { get; set; }
        public decimal Value { get; set; }
        public char ValidSts { get; set; }        
        public DateTime EntDt { get; set; }
        public int UserId { get; set; }
        public DateTime ChgDt { get; set; }
        public string InsertedBy { get; set; }

        public string UpdatedBy { get; set; }

    }
}