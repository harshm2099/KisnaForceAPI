namespace NewAvatarWebApis.Core.Application.Responses
{
    public class SolitaireItemFilterList
    {
        public IList<FilterSubCategory> subCategory { get; set; }
        public IList<FilterColor> color { get; set; }
        public IList<FilterBrand> brand { get; set; }
        public IList<FilterGender> gender { get; set; }
    }

    public class FilterSubCategory
    {
        public string categoryId { get; set; }
        public string subCategoryId { get; set; }
        public string subCategoryName { get; set; }
        public string categoryCount { get; set; }
    }

    public class FilterColor
    {
        public string color { get; set; }
    }

    public class FilterBrand
    {
        public string brandId { get; set; }
        public string brandName { get; set; }
        public string brandCount { get; set; }
    }

    public class FilterGender
    {
        public string genderId { get; set; }
        public string genderName { get; set; }
        public string genderCount { get; set; }
    }
}
