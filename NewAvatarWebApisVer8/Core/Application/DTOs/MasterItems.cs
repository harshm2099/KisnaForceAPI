using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class MasterItems
    {
        public int ItemID { get; set; }
        public string ItemCode  { get; set; }
        public string ItemName { get; set; }
        public string ItemDesc { get; set; }
        public string ItemValidSts  { get; set; }
        public int ItemImgID  { get; set; }
        public string ItemSKU  { get; set; }
        public decimal ItemMRP  { get; set; }
        public decimal  ItemDiscount  { get; set; }
        public decimal  ItemMargin  { get; set; }
        public decimal  ItemRPrice  { get; set; }
        public decimal  ItemMargin1  { get; set; }
        public decimal ItemDPrice  { get; set; }
        public int ItemUOMID  { get; set; }
        public decimal  ItemGST  { get; set; }
        public string ItemPPTag  { get; set; }
        public string ItemUsrID  { get; set; }
        //public DateTime ItemEntDt  { get; set; }
        //public DateTime ItemCngDt  { get; set; }
        public string ItemEntDt { get; set; }
        public string ItemCngDt { get; set; }
        public string ItemOdDmCd  { get; set; }
        public string ItemOdSfx  { get; set; }
        public string ItemOdDmSz  { get; set; }
        public string ItemOdKt  { get; set; }
        public string ItemOdDmCol { get; set; }
        public int ItemOdIdNo  { get; set; }
        public string ItemMainSts { get; set; }
        public int ItemStarNo  { get; set; }
        public int ItemMetalCommonID  { get; set; }
        public decimal ItemMetalWt  { get; set; }
        public int ItemStoneCommonID  { get; set; }
        public decimal ItemStoneWt  { get; set; }
        public int ItemStoneQty  { get; set; }
        public int ItemStoneQltyCommonID  { get; set; }
        public int ItemStoneColorCommonID  { get; set; }
        public int ItemStoneShapeCommonID  { get; set; }
        public int ItemBrandCommonID  { get; set; }
        public int ItemGenderCommonID  { get; set; }
        public int ItemDesignerCommonID  { get; set; }
        public int ItemCadDesignerCommonID  { get; set; }
        public decimal ItemGrossWt  { get; set; }
        public decimal ItemOtherWt  { get; set; }
        public int ItemCtgCommonID  { get; set; }
        public string ItemValidStsRemarks  { get; set; }
        public string ItemNosePinScrewSts  { get; set; }
        public string ItemSoliterSts  { get; set; }
        public string ItemPlainGold  { get; set; }
        public decimal ItemGoldLabourper  { get; set; }
        public string ItemAproxDay  { get; set; }
        public int ItemAproxDayCommonID  { get; set; }
        public decimal ItemDisLabourPer  { get; set; }
        public string ItemStatusRemark  { get; set; }
        public int ItemStatusUsrID  { get; set; }
        //public DateTime ItemStatusEntDt  { get; set; }
        public string ItemStatusEntDt { get; set; }
        public string ItemDiscontinueDate  { get; set; }
        public string ItemDiscontinueRemark { get; set; }
        public int ItemSubCollCommonID  { get; set; }
        public string ItemFranchiseSts  { get; set; }
        public string ItemIsMRP  { get; set; }
        public string ItemFixLabourSts  { get; set; }
        public decimal ItemFixLabourValue  { get; set; }
        public int ItemSubCtgCommonID  { get; set; }
        public int ItemSubSubCtgCommonID  { get; set; }
        public int ItemHeight  { get; set; }
        public int ItemWidth  { get; set; }
        public string ItemDAproxDay  { get; set; }
        public int ItemDAproxDayCommonID  { get; set; }
        public string ItemIsSRP  { get; set; }
        public string ItemSizeAvailable { get; set; }
        //public int MstID { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

    }

    public class ItemMapping
    {
        public string ItemId { get; set; }
        public string UserId { get; set; }
    }

    public class CollectionUserMapping
    {
        public string CollectionId { get; set; }
        public string UserId { get; set; }
    }
    public class ItemMappingUsers
    {
        public string ZoneID { get; set; }
        public string StateID { get; set; }
        public string DistrictID { get; set; }
        public string TerritoryID { get; set; }
        public string CityID { get; set; }
        public string AreaID { get; set; }
        public string UsertypeID { get; set; }

    }

    public class ItemCount
    {
        public Int64 ItemCountcnt { get; set; }
        
    }
}
