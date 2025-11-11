namespace NewAvatarWebApis.Core.Application.Responses
{
    public class CommonResponse
    {
        public string? status { get; set; }
        public string? status_code { get; set; }
        public bool? success { get; set; }
        public string? message { get; set; }
        public string? error { get; set; }
        public string? changePasswordRequired { get; set; }
        public string? changePinRequired { get; set; }
        public string? token { get; set; }
        public string? loader_img { get; set; }
        public object? data { get; set; }
        public object? audiodata { get; set; }
        public string? holdmsg { get; set; }
        public string? timeid { get; set; }
        public string? smsmsg { get; set; }
        public string? OTP { get; set; }
        public string? emailmsg { get; set; }
        public string? emailsubject { get; set; }
    }

    public class FranchiseWiseStockResponse
    {
        public string? AvailableStock { get; set; }
        public string? BranchCode { get; set; }
        public string? FranchiseName { get; set; }
        public string? FranchiseAddress { get; set; }
        public object? Details { get; set; }
    }

    public class FranchiseWiseStockDetails
    {
        public string? ItemName { get; set; }
        public string? ItemImagePath { get; set; }
        public string? ItemSize { get; set; }
        public string? ItemColor { get; set; }
        public string? ItemAvailableStock { get; set; }
        public string? ItemBrand { get; set; }
    }

    public class StockWiseItemDataResponse
    {
        public object? Data { get; set; }
    }

    public class PopularItemsResponse
    {
        public string? ItemId { get; set; }
        public string? CategoryId { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemGenderCommonId { get; set; }
        public string? ItemNosePinScrewSts { get; set; }
        public string? SubCategoryId { get; set; }
        public string? PriceFlag { get; set; }
        public string? ItemMrp { get; set; }
        public string? MaxQtySold { get; set; }
        public string? ImagePath { get; set; }
        public string? MostOrder { get; set; }
        public string? TagName { get; set; }
        public string? TagColor { get; set; }
        public string? FranchiseStore { get; set; }
        public string? CurrentPage { get; set; }
        public string? LastPage { get; set; }
        public string? TotalItems { get; set; }
    }

    public class TopRecommandedItemsResponse
    {
        public string? ItemId { get; set; }
        public string? CategoryId { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemGenderCommonId { get; set; }
        public string? ItemNosePinScrewSts { get; set; }
        public string? PlaingoldStatus { get; set; }
        public string? SubCategoryId { get; set; }
        public string? PriceFlag { get; set; }
        public string? ItemMrp { get; set; }
        public string? MaxQtySold { get; set; }
        public string? ImagePath { get; set; }
        public string? MostOrder { get; set; }
        public object? ProductTags { get; set; }
        public string? IsInFranchiseStore { get; set; }
    }
}
