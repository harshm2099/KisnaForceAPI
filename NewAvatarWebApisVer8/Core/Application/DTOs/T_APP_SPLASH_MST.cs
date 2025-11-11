namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class T_APP_SPLASH_MST
    {
        public int AppSplashID { get; set; }
        public string AppSplashImgPath { get; set; }
        public int AppSplashDay { get; set; }
        public char AppSplashActive { get; set; }
        public int AppSplashUsrId { get; set; }
        public DateTime AppSplashEntDt { get; set; }
        public DateTime AppSplashCngDt { get; set; }
        public int AppSplashDayCommonID { get; set; }
        public string InsertedBy { get; set; }
    }
}