using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace NewAvatarWebApis.Core.Application.Responses
{
    public class CustomCollectionCategoriesResposne
    {
        public string categoryId { get; set; }
        public string categoryName { get; set; }
        public string imagePath { get; set; }
        public string mstSortBy { get; set; }
    }
}
