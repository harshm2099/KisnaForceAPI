namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class Others
    {
    }
    public class GoldParameter
    {
        public int ParamID { get; set; }
        public decimal GoldValue { get; set; }
        public string ParamName { get; set; }
        public DateTime UpdatedOn { get; set; }
        public char GoldParameterSts { get; set; }
        public string GoldParameterLive { get; set; }
        public decimal GoldParameterFranchise { get; set; }
        public decimal PremiumGoldParameterFranchise { get; set; }
        public decimal total { get; set; }
        public int UserID { get; set; }
        public string GoldParameterName { get; set; }   

        public float current_franchise_gold_value { get; set; }
        public float current_live_gold_value { get; set; }
        public float premium_franchise_gold_value { get; set; }
        public float totalfloat { get; set; }

        public float GoldParameterValue { get; set; }
        public float GoldParameterFranchiseFloat { get; set; }
        public float GoldParameterFranchise1 { get; set; }
        public float GoldDiamondParameterFranchise { get; set; }    
        public float GoldDiamondParameterDistributor { get; set; }  


    }
    public class  RmPrice
    {
        public int RmID { get; set; }
        public string RmType { get; set; }
        public string RmName { get; set; }
        public string FromSize { get; set; }
        public string ToSize { get; set; }
        public string shape { get; set; }
        public string quality { get; set; }
        public decimal RmValue { get; set; }
        public char BasedOnWt { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UserID { get; set; }
    }
    public class GeoData
    {
        public int SrNo { get; set; }
        public string State { get; set; }
        public int count { get; set; }
    }
    public class LOC //LABOUR ON COMPLEXITY
    {
        public int rmID { get; set; }
        public int RmCatID { get; set; }
        public int RmSubCatID { get; set; }
        public int RmUserID { get; set; }
        public int RmUserTypeID { get; set; }
        public decimal labourPercentage { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UserID { get; set; }
    }
    public class LOCC //LABOUR ON COIN COMPLEXITY
    {
        public int ID { get; set; }
        public int RmSubCatID { get; set; }
        public int RmUserID { get; set; }
        public int RmUserTypeID { get; set; }
        public string grams { get; set; }
        public decimal labour { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UserID { get; set; }
    }
    public class Banner
    {
        public int HomeID { get; set; }
        public int HomeImgID { get; set; }
        public int HomeItemID { get; set; }
        public int HomeMenuID { get; set; }
        public string HomeURL { get; set; }
        public char HomeValidSts { get; set; }
        public string HomeRemarks { get; set; }
        public decimal HomeSortBy { get; set; }
        public int HomeRefCommonID { get; set; }
        public string HomeImgPath { get; set; }
        public string SoliterStatus { get; set; } //char
        public string HomeRef { get; set; }
        public string HomeAppMenu { get; set; }
        public string  FilePath { get; set; }
        public int UserID { get; set; }
    }
    public class ScreenSaver 
    {
        public int ScreenId { get; set; }
        public string Name { get; set; }
        public string Selection { get; set; }
        public string ImagePath { get; set; }
        public string Remarks { get; set; }
        public string ScreenType { get; set; }
        public char ValidSts { get; set; }
        public int Screencounter { get; set; }
        public int ScreensaverType { get; set; }
    }

    public class NewOutlet
    {
        public int NewOutLetID { get; set; }
        public DateTime NewOutLetDate { get; set; }
        public string SrName { get; set; }
        public string Distributor { get; set; }
        public string ShopName { get; set; }
        public string GstNo { get; set; }
        public string Penetration_Details { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public int PinCodeNo { get; set; }
        public DateTime DOB { get; set; }
        public DateTime ADate { get; set; }
        public DateTime JoinDate { get; set; }
        public int Territory { get; set; }
        //public int SubTerritory { get; set; }
        public string SubTerritory { get; set; }
        public int Area { get; set; }
        public DateTime DateOfDeal { get; set; }
        public string DataImageId { get; set; }
        public char DataDeviceSecurityStatus { get; set; }
        public int DataOrgCommonID { get; set; }
        public int SelectType { get; set; }
        public int SelectTitle { get; set; }
        public string DataCd { get; set; }
        public string DataName { get; set; }
        public int DataContactTitle { get; set; }
        public int DisplayType { get; set; }
        public int Community { get; set; }
        public int NoOfVisit { get; set; }
        public int WeeklyOff { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string DistBillNo { get; set; }
        public int InitialQty { get; set; }
        public int InitialAmount { get; set; }
        public int CourierService { get; set; }
        public string StyleNo { get; set; }
        public string Image { get; set; }
        public string DataLongitude { get; set; }
        public string DataLatitude { get; set; }
        public string EmrCustCd { get; set; }
        public char ValidSts { get; set; }
        public char EditSts { get; set; }
        public int UsrId { get; set; }
        public DateTime EntDt { get; set; }        
    }

    public class SoliterParameter
    {
        public int SrNo { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public Single FieldValue { get; set; }
        public char ValidSts { get; set; }
        public int UserID { get; set; }
    }

    public class DataMaster
    {
        public int DataId { get; set; }
        public string DataName { get; set; }
       
    }

    public class DiscontinueItems
    {
        
        public string ItemName { get; set; }
        public string ItemOdSfx { get; set; }
        public string ItemMRP { get; set; }
        public decimal ItemDPrice { get; set; }
        public char ItemValidSts { get; set; }
        public Int16 Source {  get; set; }  

    }

    public class Ecatelog
    {
        public int AutoNumber { get; set; }
        public string InfoSrcId { get; set; }
        public string InfoSrcName { get; set; }
        public string InfoSrcUrl { get; set; }
        public string InfoSrcDesc { get; set; }
        public string InfoSrcCtg { get; set; }
        public string InfoSrcTyp { get; set; }
        public string PDFUrl { get; set; }
        public char InfoSrcStatus { get; set; }
        public int UsrId { get; set; }

    }

    public class UserMenuPermission
    {
        public int Id { get; set; }
        public string DataId { get; set; }
        public string MenuId { get; set; }
        public int UsrId { get; set; }
        public string DataName { get; set; }
        public string MenuName { get; set; }
        public string Ids { get; set; }

        public int ParentId { get; set; }
        public string strParentIds { get; set; }
    }

    public class UserMenuPermissionCRUD
    {
        public int Id { get; set; }
        public string DataId { get; set; }
        public int UserMenuPermissionID { get; set; }
        public bool IsRead { get; set; }
        public bool IsWrite { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public string DataName { get; set; }
        public string DataCode { get; set; }
        public string MenuName { get; set; }
        public int UsrId { get; set; }

    }

    public class MenuMaster
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string PageURL { get; set; }
      
        public string  JsonData {  get; set; }
        public int ParentId { get; set; }

    }

    #region MenuData
    public class MenuItem
    {
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public List<ChildItem> Children { get; set; }
    }

    public class ChildItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string UrlLink { get; set; }
    }

    #endregion
}










