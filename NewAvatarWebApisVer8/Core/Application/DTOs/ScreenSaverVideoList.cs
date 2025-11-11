namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class ScreenSaverVideoParams
    {
        public int data_id { get; set; }
    }

    public class ScreenSaverVideoResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public string minute { get; set; }
        public List<ScreenSaverVideo> data { get; set; }
    }

    public class ScreenSaverVideo
    {
        public string ScreenId { get; set; }
        public string Name { get; set; }
        public string Selection { get; set; }
        public string ImagePath { get; set; }
        public string Remarks { get; set; }
        public string ScreenType { get; set; }
        public string FileType { get; set; }
        public string Screencounter { get; set; }
        public string select_status { get; set; }
    }

}
