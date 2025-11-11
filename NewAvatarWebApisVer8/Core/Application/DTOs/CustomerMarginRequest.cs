namespace NewAvatarWebApis.Core.Application.DTOs
{
        public class FranchiseData
        {
            public int DataID { get; set; }
            public string DataName { get; set; }
        }

        public class CustomerMarginData
        {
            public int MarginBrandID { get; set; }
            public int MarginId { get; set; }
            public string MarginBrandName { get; set; }
            public decimal MarginCurrentDP { get; set; }
            public decimal MarginNewDP { get; set; }
            public string OnLabour { get; set; }
            public decimal Gold { get; set; }
            public decimal GoldNew { get; set; }
            public decimal Platinum { get; set; }
            public decimal PlatinumNew { get; set; }
            public decimal Other { get; set; }
            public decimal OtherNew { get; set; }
            public decimal Diamond { get; set; }
            public decimal DiamondNew { get; set; }
            public decimal Color { get; set; }
            public decimal ColorNew { get; set; }
            public decimal Labour { get; set; }
            public decimal LabourNew { get; set; }
            public decimal Silver { get; set; }
            public decimal SilverNew { get; set; }
            public decimal SNMCCPer { get; set; }
            public decimal SNMCCPerNew { get; set; }
        }

        public class CustomerMarginAddUpdateParams
        {
            public int MarginId { get; set; }
            public int LoginDataId { get; set; }
            public int MarginDataID { get; set; }
            public int MarginBrandID { get; set; }
            public decimal GoldNew { get; set; }
            public decimal PlatinumNew { get; set; }
            public decimal OtherNew { get; set; }
            public decimal DiamondNew { get; set; }
            public decimal ColorNew { get; set; }
            public decimal LabourNew { get; set; }
            public decimal SilverNew { get; set; }
            public decimal SnmccPerNew { get; set; }
            public string OnLabour { get; set; }
        }

        public class IsDisplayNewMarginListing
        {
            public int is_display_new_margin { get; set; }
        }

        public class CustomerMarginDistData
        {
            public int MarginId { get; set; }
            public int MarginPPTagID { get; set; }
            public string MarginPPTag { get; set; }
            public decimal MarginCurrentDP { get; set; }
            public decimal MarginNewDP { get; set; }
            public string OnLabour { get; set; }
            public decimal Gold { get; set; }
            public decimal GoldNew { get; set; }
            public decimal Platinum { get; set; }
            public decimal PlatinumNew { get; set; }
            public decimal Other { get; set; }
            public decimal OtherNew { get; set; }
            public decimal Diamond { get; set; }
            public decimal DiamondNew { get; set; }
            public decimal Color { get; set; }
            public decimal ColorNew { get; set; }
            public decimal Labour { get; set; }
            public decimal LabourNew { get; set; }
            public decimal Silver { get; set; }
            public decimal SilverNew { get; set; }
            public decimal SNMCCPer { get; set; }
            public decimal SNMCCPerNew { get; set; }
        }

        public class CustomerMarginDistAddUpdateParams
        {
            public int MarginId { get; set; }
            public int LoginDataId { get; set; }
            public int MarginDataID { get; set; }
            public int MarginPPTagID { get; set; }
            public decimal GoldNew { get; set; }
            public decimal PlatinumNew { get; set; }
            public decimal OtherNew { get; set; }
            public decimal DiamondNew { get; set; }
            public decimal ColorNew { get; set; }
            public decimal LabourNew { get; set; }
            public decimal SilverNew { get; set; }
            public decimal SnmccPerNew { get; set; }
            public string OnLabour { get; set; }
        }

        public class MasterUserType
        {
            public int UserTypeID { get; set; }
            public string UTCode { get; set; }
            public string UTName { get; set; }
            public string Remarks { get; set; }
            public char UTChar { get; set; }
            public decimal SortOrder { get; set; }
            public bool IsActive { get; set; }
            public DateTime InsertedOn { get; set; }
            public string InsertedBy { get; set; }
            public DateTime UpdatedOn { get; set; }
            public string UpdatedBy { get; set; }
        }

        public class MasterPPTag
        {
            public int PPTagID { get; set; }
            public string PPTag { get; set; }
            public string Description { get; set; }
            public decimal SortOrder { get; set; }
            public bool IsActive { get; set; }
            public DateTime InsertedOn { get; set; }
            public string InsertedBy { get; set; }
            public DateTime UpdatedOn { get; set; }
            public string UpdatedBy { get; set; }
        }

        public class User
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
}
