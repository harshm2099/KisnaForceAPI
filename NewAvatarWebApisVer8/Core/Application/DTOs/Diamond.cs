namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class Diamond
    {
        public string solietr_stoke_no { get; set; }
    }

    public class DiamondFiltersList
    {
        public string cart_quantity { get; set; }
        public string category_id { get; set; }
        public string color_id { get; set; }
        public string item_id { get; set; }
        public string size_id { get; set; }
    }

    //public class Diamond_Static
    //{
    //    public string success { get; set; }
    //    public string message { get; set; }
    //    public IList<Diamond_Records> data { get; set; }
    //}

    public class DiamondDetailsResponse
    {
        public string ViewDetail { get; set; }
        public string ViewDiamondImage { get; set; }
        public string ViewDiamondCertificate { get; set; }
        public string ViewDiamondVideo { get; set; }
    }

    public class DiamondFilter_Static
    {
        public string success { get; set; }
        public string message { get; set; }
        public DiamondFilter_Records data { get; set; }
    }
    public class DiamondFilter_Records
    {
        public IList<Diamond_Shapelist> Diamond_Shapelist { get; set; }
        public IList<Diamond_Colorlist> Diamond_Colorlist { get; set; }
        public IList<Diamond_Qualitylist> Diamond_Qualitylist { get; set; }
        public IList<Diamond_Cutlist> Diamond_Cutlist { get; set; }
        public IList<Diamond_Polishlis> Diamond_Polishlis { get; set; }
        public IList<Diamond_Symmetrylist> Diamond_Symmetrylist { get; set; }
        public IList<Diamond_Fluorlist> Diamond_Fluorlist { get; set; }
        public IList<Diamond_Lablist> Diamond_Lablist { get; set; }
        public Diamond_Caratlist Diamond_Caratlist { get; set; } 
    }
    public class Diamond_Shapelist
    {
        public string DiaStkShape { get; set; }
    }
    public class Diamond_Colorlist
    {
        public string DiaStkColor { get; set; }
    }
    public class Diamond_Qualitylist
    {
        public string DiaStkClarity { get; set; }
    }
    public class Diamond_Cutlist
    {
        public string DiaStkCut { get; set; }
    }
    public class Diamond_Polishlis
    {
        public string DiaStkPolish { get; set; }
        public string DiaStkPolishVal { get; set; }
    }
    public class Diamond_Symmetrylist
    {
        public string DiaStkSymmetry { get; set; }
        public string DiaStkSymmetryfull { get; set; }
    }
    public class Diamond_Fluorlist
    {
        public string DiaStkFluorescent { get; set; }
    }
    public class Diamond_Lablist
    {
        public string DiaStkLab { get; set; }
    }
    public class Diamond_Caratlist
    {
        public IList<Diamond_Caratlist_detail> caratlist { get; set; }
    }

    public class Diamond_Caratlist_detail
    {
        public string ID { get; set; }
        public string DiaStkCarat {  get; set; }
        public string itemDiaWtFr { get; set; }
        public string itemDiaWtTo { get; set; }
        public string itemGoldWt { get; set; }
        public string itemId { get; set; }
        public string itemSize { get; set; }
        public string fromto_carat { get; set; }
        public string RowNum { get; set; }
    }

    public class DiamondGoldCappingRequest
    {
        public string? dataId { get; set; }
        public string? dataLoginType { get; set; }
    }
}