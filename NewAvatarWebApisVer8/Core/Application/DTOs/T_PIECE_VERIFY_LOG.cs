namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class T_PIECE_VERIFY_LOG
    {
        public int PVId { get; set; }
        public int DataID { get; set; }
        public string PVType { get; set; }
        public string PVNumber { get; set; }
        public string RequestType { get; set; }
        public string Status { get; set; }
        public DateTime Entdt { get; set; }
        public string TicketNo { get; set; }
        public string fileType { get; set; }
    }
}