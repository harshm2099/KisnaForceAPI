namespace NewAvatarWebApis.Core.Application.DTOs
{
        public class CollectionItemMapping
        {
            public string ItemCode { get; set; }
            public string ItemOdDmCd { get; set; }
            public string ItemOdSfx { get; set; }
            public Int64 InsertedBy { get; set; }
            public Int32 CollectionId { get; set; }
        }

        public class ClsEditCollectionItemMapping
        {
            public string ItemCode { get; set; }
            public string ItemOdDmCd { get; set; }
            public string ItemOdSfx { get; set; }
            public string UpdatedBy { get; set; }
            public string CollectionId { get; set; }
        }

        public class Shoplistitems
        {
            public string bacode { get; set; }
            public string shopName { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string area { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string contactnumber { get; set; }
        }
        public class CustomCollectionList
        {
            public string dataId { get; set; }
            public string dataLoginType { get; set; }
        }

        public class CollectionCategoryRequest
        {
            public string dataId { get; set; }
            public string dataLoginType { get; set; }
            public string collectionId { get; set; }
        }

        public class CustomCollectionSortByRequest
        {
            public string? dataId { get; set; }
            public string? dataLoginType { get; set; }
            public string? collectionId { get; set; }
            public string? categoryId { get; set; }
        }

        public class CustomCollectionFilter
        {
            public string? dataId { get; set; }
            public string? dataLoginType { get; set; }
            public string? collectionId { get; set; }
            public string? categoryId { get; set; }
        }

        public class CustomCollectionItemListRequest
        {
            public string? dataId { get; set; }
            public string? dataLoginType { get; set; }
            public string? collectionId { get; set; }
            public string? categoryId { get; set; }
            public string? page { get; set; }
            public string? limit { get; set; }
        }
}
