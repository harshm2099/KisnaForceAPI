namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class T_NOTIFICATION_MST
    {
        public int NotificationID { get; set; }
        public int NotificationFromDataID { get; set; }
        public string NotificationImgIconPath { get; set; }
        public string NotificationImgBigPath { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationText { get; set; }
        public DateTime NotificationSendScheduleOn { get; set; }
        public char NotificationStatus { get; set; }
        public DateTime NotificationSendOn { get; set; }
        public char NotificationValidSts { get; set; }
        public DateTime NotificationEntDt { get; set; }
        public int NotificationTypeCommonID { get; set; }
        public string NotificationTypeParam { get; set; }
        public int NotificationFileTypeCommonID { get; set; }
        
    }
}