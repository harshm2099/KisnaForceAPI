namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class PayloadsSoliterCertificate
    {
       
       public string diaquality_id { get; set; }
        public string sort_type { get; set; }
        public string diacolor_id { get; set; }
        public string diacut_id { get; set; }
        public string diafrom_carat { get; set; }
        public string diato_carat { get; set; }
        public string diaflurocense_id { get; set; }
        public string diasymmentry_id { get; set; }
        public string diapolish_id { get; set; }
        public string diacertificate_id { get; set; }
        public string category_id { get; set; }
        public string color_id { get; set; }
        public string cart_quantity { get; set; }
        public string item_id { get; set; }
        public string design_kt { get; set; }
        public string size_id { get; set; }
        public string diashap_id { get; set; }
        public string shape { get; set; }
        public string diaprice { get; set; }

    }

    public class SoliCertificateMaster
    {
        public string success { get; set; }
        public string message { get; set; }
        public IList<SoliCertificateDetail> data { get; set; }

    }

    public class SoliCertificateDetail
    {
        public string RowNumber { get; set; }
        public string price { get; set; }
        public string goldwt { get; set; }
        public string stock_no { get; set; }
        public string certificate { get; set; }
        public string certificate_link { get; set; }
        public string stock_image { get; set; }
        public string stock_video { get; set; }
        public string dia_carat { get; set; }
        public string tab_per { get; set; }
        public string diamond_cut { get; set; }
        public string diamond_polish { get; set; }
        public string diamond_symmentry { get; set; }
        public string diamond_flurence { get; set; }
        public string diamond_maindetail { get; set; }
        public string diamond_extradetail { get; set; }
        public string TotalCount { get; set; }
        public string price_text { get; set; }
    }

    


}
