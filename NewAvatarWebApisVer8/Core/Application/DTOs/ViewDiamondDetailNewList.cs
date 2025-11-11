namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class ViewDiamondDetailNewParams
    {
        public string solietr_stoke_no { get; set; }
    }

    public class ViewDiamondDetailNewResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<DiamondDetailNew> data { get; set; }
    }

    public class DiamondDetailNew
    {
        public string view_detail { get; set; }
        public string view_diamond_image { get; set; }
        public string view_diamond_certificate { get; set; }
        public string view_diamond_video { get; set; }
    }

}
