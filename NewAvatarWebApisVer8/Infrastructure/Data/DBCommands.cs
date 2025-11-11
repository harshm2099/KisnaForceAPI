using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NewAvatarWebApis.Infrastructure.Data
{
    public class DBCommands
    {
        public static string CONNECTION_STRING = "Data Source=13.203.163.138;Initial Catalog=hkdb_data_bkp;User Id=admin;Password=$Developer$123;TrustServerCertificate=True;";
        public static string CustomerID = "b19f6709-deb6-4da6-8c74-6f2ea9e49f3f";

        public const string LOGIN_MST = "LOGIN_PROC";
        public const string LOGIN_CHECK = "LOGIN_CHECK";
        public const string LOGIN_DATA = "LOGIN_DATA";
        public const string LOGIN_WITHMOBILEOTP = "LOGIN_WITHMOBILEOTP";
        public const string LOGIN_WITHEMAILOTP = "LOGIN_WITHEMAILOTP";
        public const string LOGIN_WITHUSERID = "LOGIN_WITHUSERID";
        public static string LOGIN_WITHEMAILMOBILE = "LOGIN_WITHEMAILMOBILE";
        public const string GETUSERBY_EMAIL_MOBILE = "GETUSERBY_EMAIL_MOBILE";
        public const string SENDOTP = "SENDOTP";
        public const string SENDOTPME = "SENDOTPME";
        public const string SENDEMAILOTP = "SENDEMAILOTP";
        public const string VERIFY_EMAIL_MOBILE = "VERIFY_EMAIL_MOBILE";
        public const string CHANGE_PASSWORD = "CHANGE_PASSWORD";

        public const string BRAND_MST = "CRUD_BRAND";
        public const string ZONE_MST = "CRUD_ZONE";
        public const string CATEGORY_MST = "CRUD_CATEGORY";
        public const string REGION_MST = "CRUD_REGION";
        public const string STATE_MST = "CRUD_STATE";
        public const string DISTRICT_MST = "CRUD_DISTRICT";
        public const string TALUKA_MST = "CRUD_TALUKA";
        public const string CITY_MST = "CRUD_CITY";
        public const string AREA_MST = "CRUD_AREA";
        public const string ANTIQUE_MST = "CRUD_ANTIQUE";
        public const string COLOR_MST = "CRUD_COLOR";
        public const string CADDESIGNER_MST = "CRUD_CADDESIGNER";
        public const string CARTBIFURCATION_MST = "CRUD_CartBifurCation";
        public const string COLLECTIONS_MST = "CRUD_Collection";
        public const string UOM_MST = "CRUD_UOM";
        public const string METAL_MST = "CRUD_Metal";
        public const string STONE_MST = "CRUD_Stone";
        public const string STONECOLOR_MST = "CRUD_StoneColor";
        public const string STONESHAPE_MST = "CRUD_Shapes";
        public const string PPTAG_MST = "CRUD_PPTag";
        public const string GENDER_MST = "CRUD_Gender";
        public const string DESIGNER_MST = "CRUD_DESIGNER";
        public const string STARCOLOR_MST = "CRUD_STARTCOLOR";
        public const string STONEQUALITY_MST = "CRUD_STONEQUALITY";
        public const string ITEMDAYS_MST = "CRUD_Days";
        public const string SUBCAT_MST = "CRUD_SUBCATEGORY";
        public const string USERTYPE_MST = "CRUD_UserType";
        public const string COURIER_MST = "CRUD_Courier";
        public const string CUSTOMERCARE_MST = "CRUD_CustomerCare";
        public const string DESIGNSTONE_MST = "CRUD_DesignStone";
        public const string DIAMONDQUALITY_MST = "CRUD_DIAMONDQUALITY";
        public const string ITEMS_MST = "CRUD_ITEMS";
        public const string ITEM_LISTING = "ITEM_LISTING";
        public const string DISTRIBUTORTYPE_MST = "CRUD_DISTRIBUTORTYPE";
        public const string EMAIL_MST = "CRUD_Email";
        public const string EMAILLIST_MST = "CRUD_EMAILIDLIST";
        public const string EMAILSUBJECT_MST = "CRUD_EmailSubject";
        public const string NAMETITLE_MST = "CRUD_NAMETITLE";
        public const string PART_MST = "CRUD_PART";
        public const string LANGUAGES_MST = "CRUD_LANGUAGES";
        public const string RELIGION_MST = "CRUD_Religion";
        public const string PRODUCTTYPE_MST = "CRUD_ProductType";
       // public const string INFORMATION_MST = "CRUD_Information";
        public const string COMMON_MST = "CRUD_Comman";
        public const string NOTIFICATION_MST = "CRUD_Notification";
        public const string SPLASH_MST = "CRUD_Splash";
        public const string SolitaireParameters_MST = "CRUD_SolitaireParameters";
        public const string OutletCategory = "CRUD_OutletCategory";
        public const string DiscontnueItems = "DISCONTINUE_ITEMS";
        public const string Ecatelog = "ECATELOGE";
        public const string MENUMASTER = "GETMENUS";
        public const string MENU = "CRUD_Menu";
        public const string USER_MENU_PERMISSION = "USER_MENU_PERMISSION";
        public const string USER_MENU_PERMISSION_CRUD = "USER_MENU_PERMISSION_CRUD";
        public const string VIEWITEMDETAILS = "ItemDetailsShow";
        public const string VIEWSOLITERITEMDETAILS = "SoliterDetail";
        public const string VIEWGOLDITEMDETAILS = "plaingold_itemdetail";
        public const string GETTEAMUSERS = "GETTEAMUSERS";
        public const string DiamondCertificatesPriceFilter = "DiamondCertificatesPriceFilter";
        public const string SoliterPriceBreakup = "usp_SoliterPriceBreakup";
        public const string FinalMRPNew = "finalMrpNew";
        public const string GetGoldPriceRateWise = "usp_GetGoldPriceRateWise";
        public const string GetPriceWiseList = "usp_GetPriceWiseList";
        public const string CategoryButtonListNew = "usp_CategoryButtonListNew";
        public const string SplashScreenList = "usp_SplashScreenList";
        public const string PriceWiseItemCategory = "usp_PriceWiseItemCategoryList";
        public const string SolitaireSortBy = "usp_GetSoliterSortBy";
        public const string CustomCollectionFilter = "usp_CustomCollectionFilter";
        public const string CustomCollectionSubCategoryList = "usp_CustomCollectionSubCategoryList";
        public const string AllSubCategoryList = "usp_GetAllSubCategoryList";
        public const string PlainGoldItemListFranSIS = "usp_PlainGoldItemListFranOrSIS";
        public const string ColorStoneItemListFranSIS = "usp_ColorStoneItemListFranOrSIS";
        public const string PlatinumItemListFranSIS = "usp_PlatinumItemListFranOrSIS";
        public const string IllumineItemListFranSIS = "usp_IllumineItemListFranOrSIS";
        public const string GlemSolitaireItemListSIS = "usp_GlemSolitaireItemListSIS";
        public const string RareSolitaireItemListFran = "usp_RareSolitaireItemList";
        public const string KisnaItemListFran = "usp_KisnaItemListFranOrSIS";
        public const string CoupleBandItemListFranSIS = "usp_CoupleBandItemListFranOrSIS";
        public const string PlainGoldFilterFranSIS = "usp_PlainGoldItemFilterFranOrSIS";
        public const string SolitaireSubCategoryNewFranSIS = "usp_SoliterSubCategoryNewFranOrSIS";
        public const string CommonItemFilterListFranSIS = "usp_CommonItemFilterFranOrSIS";
        public const string BannerWishList = "usp_BannerWishList";
        public const string CustomCollectionItemList = "usp_CustomCollectionItemList";
        public const string SolitaireDetailsFranSIS = "usp_SoliterDetailFranOrSIS";
        public const string PlainGoldItemDetailsFran = "usp_PlainGoldItemDetailFranOrSIS";
        public const string ItemDetailsFran = "usp_ItemDetailShowFranOrSIS";
        public const string TotalGoldDiaaWeight = "usp_TotalGoldDiaaWeight";
        public const string ChatCustomerCare = "usp_ChatCustomerCare";
        public const string FranchiseWiseStock = "usp_FranchiseWiseStock";
        public const string CappingSortBy = "usp_CappingSortBy";
        public const string CappingFilter = "usp_CappingFilter";
        public const string ConsumerFromStore = "usp_ConsumerFormStore";
        public const string ExtraGoldRateWiseRate = "usp_ExtraGoldRateWiseRate";
        public const string StockWiseItemData = "usp_StockWiseItemData";
        public const string PopularItems = "usp_PopularItems";
        public const string PopularItemsFilter = "usp_PopularItemFilter";
        public const string PieceVerify = "usp_PieceVerify";
        public const string CappingItemList = "usp_CappingItemList";
        public const string TopRecommandedItems = "usp_TopRecommendedItems";
        public const string ItemViewList = "usp_ItemList";

        //WISHLIST
        public const string Wishlist_Fetch = "WISHLIST_Fetch";
        public const string Wishlist_Add = "WISHLIST_Insert";
        public const string Wishlist_Delete = "WISHLIST_Delete";
        

        public const string REGION_MST_BY_ZONEID = "REGION_MST_BY_ZONEID";
        public const string STATE_MST_BY_REGIONID = "STATE_MST_BY_REGIONID";
        public const string STATE_MST_BY_ZONEID = "STATE_MST_BY_ZONEID";
        public const string DISTRICT_MST_BY_STATEID = "DISTRICT_MST_BY_STATEID";
        public const string TERRITORY_MST_BY_STATEID = "TERRITORY_MST_BY_STATEID";
        public const string TALUKA_MST_BY_DISTRICTID = "TALUKA_MST_BY_DISTRICTID";
        public const string CITY_MST_BY_DISTRICTID = "CITY_MST_BY_DISTRICTID";
        public const string CITY_MST_BY_TERRITORYID = "CITY_MST_BY_TERRITORYID";
        public const string AREA_MST_BY_CITYID = "AREA_MST_BY_CITYID";
        public const string CRUD_Territory = "CRUD_Territory";
        public const string CRUD_ImageView = "CRUD_ImageView";

        //ITEMMAPPING       
        public const string ITEMS_MAPPING = "ITEMS_MAPPING";
        public const string GET_USERS_ITEMS_MAPPING = "GET_USERS_ITEMS_MAPPING";
        public const string USERS = "CRUD_USERS";
        //

        //COLLECTIONMAPPING
        public const string COLLECTION_MAPPING = "COLLECTION_MAPPING";
        public const string GET_COLLECTION_BY_ID = "GET_COLLECTION_BY_ID";
        public const string COLLECTIONS_USER_MAPPING = "COLLECTIONS_USER_MAPPING";
        public const string GETCUSTOMCOLLECTION = "usp_GetCustomCollectionList";
        public const string GETCUSTOMCOLLECTIONCATEGORIES = "usp_CustomCollectionCategories";
        public const string GETCUSTOMCOLLECTIONSORTBY = "usp_GetCustomCollectionSortBy";
        public const string GETHOSTOCKCATEGORY = "usp_HoStockCategories";
        public const string GETHOSTOCKSORTBY = "usp_GetHoStockSortBy";
        public const string GETHOSTOCKITEMLIST = "usp_HoStockItemList";
        //

        //CART
        public const string DIAMOND_CERTIFICATES_PRICE_FILTER_DIST = "Diamond_Certificates_price_filter_dist";
        //

        //SOLI CATE
        public const string SOLITAIRECATEGORYLIST = "SolitaireCategoryList";
        //

        //DIAMOND DETAILS
        public const string VIEWDIAMONDDETAIL = "usp_ViewDiamondDetail";
        public const string DIAMONDFILTERSLIST = "DIAMONDFILTERSLIST";
        public const string DiamondGoldCapping = "usp_DiamondGoldCappingGet";
        //

        //TEAMS
        public const string CRUD_TEAM = "CRUD_TEAMS";

        //CART-CHECKOUT
        public const string CART_COUNT_NEW = "usp_CartCount";
        public const string CART_BILLINGFORTYPE = "usp_CartBillingForType";
        public const string CART_BILLINGUSERLIST = "usp_CartBillingUserList";
        public const string CART_ORDERBILLINGFORTYPE = "usp_CartOrderBillingForType";
        public const string CART_ORDERBILLINGUSERLIST = "usp_CartOrderBillingUserList";
        public const string CART_ORDERTYPELIST = "usp_OrderTypeList";
        public const string CheckItemSizeRange = "usp_CheckItemSizeRange";
        public const string CheckoutVerifyOtp = "usp_checkoutVerifyOtp";

        //Other API
        public const string VerifyProduct = "VerifyProduct";
        public const string Shoplist = "shoplist";
        public const string GoldParams = "GoldValues";
        public const string FranchisePrice = "FranchiseRMPrice";
        public const string FranchiseCSPrice = "FranchiseRmColorStone";
        public const string FranchiseDiamond = "FranchiseRmDiamond";
        public const string FranchiseExtra = "FranchiseRmExtra";
        public const string DistributorPrice = "DistributorRMPrice";
        public const string DistributorExtra = "DistributorRmExtra";
        public const string DistributorCSPrice = "DistributorRMColorStone";
        public const string DistributorDiamond = "DistributorRMDiamond";
        public const string Geometric = "geometricrpt";
        public const string FranchiseLOC = "FranchiseLabourOnComplexity";
        public const string FranchiseLOCC = "FranchiseLabourOnCoinComplexity";
        public const string DistributorLOC = "DistributorLabourOnComplexity";
        public const string DistributorLOCC = "DistributorLabourOnCoinComplexity";
        public const string FranchiseGoldValues = "FranchiseGoldValues";
        public const string banner = "Banner";
        public const string ScreenSaver = "ScreenSaver";
        public const string outlet = "NewOutlet";
        public const string INFORMATION_MST = "CRUD_Information";
        public const string SoliterParameter = "Soliter_Parameter";
        public const string DataMaster = "GetUserData";
        public const string Shoplistupload = "Shoplistupload";
        //

        //ITEM_LISTING
        public const string CART_ITEMLISTING = "CART_ITEM_LISTING";
        public const string CALCULATE_WEIGHT_PRICE_FROMSIZE = "CALCULATE_WEIGHT_PRICE_FROMSIZE";
        public const string DP_RP_CALCULATION = "DP_RP_CALCULATION";
        public const string CARTITEMS_SIZELIST = "CART_ITEMS_SIZELIST";
        public const string CART_ITEM_PRODUCTSIZELIST = "CART_ITEM_PRODUCTSIZELIST";
        public const string CARTITEM_COLORSIZELIST = "CART_ITEM_COLORSIZELIST";
        public const string ITEM_ORDERINSTRUCTIONLIST = "ITEM_ORDERINSTRUCTIONLIST";
        public const string ITEM_ORDERCUSTOMINSTRUCTIONLIST = "ITEM_ORDERCUSTOMINSTRUCTIONLIST";
        public const string CARTITEM_COLORLIST = "CART_ITEM_COLORLIST";
        public const string ITEM_TAGS = "ITEM_TAGS";
        public const string CARTITEM_IMAGECOLORS = "CART_ITEM_IMAGECOLORS";
        public const string HOLIDAYSLIST_BYDATES = "HOLIDAYSLIST_BYDATES";
        public const string CHECKITEM_ISSOLITAIRECOMBO = "CHECKITEM_ISSOLITAIRECOMBO";
        public const string CHECKITEM_ISNEWPREMIUMCOLLECTION = "CHECKITEM_ISNEWPREMIUMCOLLECTION";
        public const string ITEM_IMAGECOLORS = "ITEM_IMAGECOLORS";
        public const string ITEM_SIZELIST = "ITEM_SIZELIST";
        public const string ITEMCATEGORYMAPPINGS_LIST = "ITEMCATEGORYMAPPINGS_LIST";
        public const string ITEMCATEGORYMAPPINGS_KISNAPREMIUM_LIST = "ITEMCATEGORYMAPPINGS_KISNAPREMIUM_LIST";
        public const string GETSELECTEDSIZE_BYCOLLECTIONS = "GETSELECTEDSIZE_BYCOLLECTIONS";
        public const string ITEM_COLORSIZELIST = "ITEM_COLORSIZELIST";
        public const string IsInFranchiseStock = "IsInFranchiseStock";
        public const string ISINFRANCHISESTOCK_NEW = "ISINFRANCHISESTOCK_NEW";
        public const string ISINFRANCHISESTOCK_KISNAPREMIUM = "ISINFRANCHISESTOCK_KISNAPREMIUM";
        public const string KISNA_ITEMLIST = "KISNA_ITEMLIST";
        public const string PLAINGOLD_ITEMLIST = "PLAINGOLD_ITEMLIST";
        public const string GLEAMSOLITAIRE_ITEMLIST = "GLEAMSOLITAIRE_ITEMLIST";
        public const string COLORSTONE_ITEMLIST = "COLORSTONE_ITEMLIST";
        public const string PLATINUM_ITEMLIST = "PLATINUM_ITEMLIST";
        public const string COUPLEBAND_ITEMLIST = "COUPLEBAND_ITEMLIST";
        public const string RARESOLITAIRE_ITEMLIST = "RARESOLITAIRE_ITEMLIST";
        public const string ILLUMINE_ITEMLIST = "ILLUMINE_ITEMLIST";
        public const string NEWKISNAPREMIUM_ITEMLIST = "NEWKISNAPREMIUM_ITEMLIST";
        public const string ESMECOLLECTION_ITEMLIST = "ESMECOLLECTION_ITEMLIST";
        public const string AKSHAYAGOLD_ITEMLIST = "AKSHAYAGOLD_ITEMLIST";
        public const string NEWDEVELOPMENT_ITEMLIST = "NEWDEVELOPMENT_ITEMLIST";
        public const string TINNYWONDERS_ITEMLIST = "TINNYWONDERS_ITEMLIST";
        public const string ALLCATEGORYLIST = "ALLCATEGORYLIST";
        public const string ALLCATEGORYLISTNEW = "usp_GetAllCategoryListNew";
        public const string ALLCATEGORYLISTVI = "ALLCATEGORYLISTVI";
        public const string FAMILYPRODUCT = "FAMILYPRODUCT";
        public const string WISH_ITEMLIST_ON = "WISH_ITEMLIST_ON";
        public const string WATCH_ITEMLIST_ON = "WATCH_ITEMLIST_ON";
        public const string COMMONITEMFILTER_SUBCATEGORY = "COMMONITEMFILTER_SUBCATEGORY";
        public const string COMMONITEMFILTER_DSGKT = "COMMONITEMFILTER_DSGKT";
        public const string COMMONITEMFILTER_DSGAMOUNT = "COMMONITEMFILTER_DSGAMOUNT";
        public const string COMMONITEMFILTER_DSGMETALWT = "COMMONITEMFILTER_DSGMETALWT";
        public const string COMMONITEMFILTER_DSGDIAMONDWT = "COMMONITEMFILTER_DSGDIAMONDWT";
        public const string COMMONITEMFILTER_PRODUCTTAGS = "COMMONITEMFILTER_PRODUCTTAGS";
        public const string COMMONITEMFILTER_BRAND = "COMMONITEMFILTER_BRAND";
        public const string COMMONITEMFILTER_GENDER = "COMMONITEMFILTER_GENDER";
        public const string COMMONITEMFILTER_APPROXDELIVERY = "COMMONITEMFILTER_APPROXDELIVERY";
        public const string SOLITER_SUBCATEGORYNEW = "SOLITER_SUBCATEGORYNEW";
        public const string PLAINGOLDITEMFILTER_NEW = "PLAINGOLDITEMFILTER_NEW";
        public const string CATEGORYBUTTONLIST = "CATEGORYBUTTONLIST";
        public const string GET_ITEMDETAILS_BYITEMID = "GET_ITEMDETAILS_BYITEMID";
        public const string CART_INSERT = "CART_INSERT";
        public const string CARTSTORE = "CARTSTORE";
        public const string SOLICARTSTORE = "SOLICARTSTORE";
        public const string GETUSERTYPES_FORCUSTOMERMARGINS = "GETUSERTYPES_FORCUSTOMERMARGINS";
        public const string GETPPTAGS_FORCUSTOMERMARGINS = "GETPPTAGS_FORCUSTOMERMARGINS";
        public const string GET_ISDISPLAYNEWMARGIN_FORCUSTOMERMARGINS = "GET_ISDISPLAYNEWMARGIN_FORCUSTOMERMARGINS";
        public const string GETUSERSFROMUSERTYPE = "GETUSERSFROMUSERTYPE";
        public const string GET_CUSTOMERMARGINDISTDATA_DATAID = "GET_CUSTOMERMARGINDISTDATA_DATAID";
        public const string CUSTOMERMARGINDIST_ADDUPDATE = "CUSTOMERMARGINDIST_ADDUPDATE";
        public const string GET_FRANCHISELIST = "GET_FRANCHISELIST";
        public const string GET_CUSTOMERMARGINDATA_DATAID = "GET_CUSTOMERMARGINDATA_DATAID";
        public const string CUSTOMERMARGIN_ADDUPDATE = "CUSTOMERMARGIN_ADDUPDATE";
        public const string CARTCHECKOUTALLOTNEW = "CARTCHECKOUTALLOTNEW";
        public const string GOLDDYNAMICPRICECART = "GOLDDYNAMICPRICECART";
        public const string CARTCHECKOUTNOALLOTNEW = "CARTCHECKOUTNOALLOTNEW";
        public const string CARTCHECKOUTNOALLOTNEW_SAVEDIABOOKRESPONSE = "CARTCHECKOUTNOALLOTNEW_SAVEDIABOOKRESPONSE";
        public const string CARTCHECKOUTNOALLOTNEW_SAVECARTMST = "CARTCHECKOUTNOALLOTNEW_SAVECARTMST";
        public const string CARTCHECKOUTNOALLOTNEW_UPDATE_CARTMSTITEM_PRICES = "CARTCHECKOUTNOALLOTNEW_UPDATE_CARTMSTITEM_PRICES";
        public const string CARTITEM_DELETE = "usp_CartItemDelete";
        public const string GET_DETAILS_FOR_UPDATE_CARTITEMS = "GET_DETAILS_FOR_UPDATE_CARTITEMS";
        public const string CART_UPDATE_ITEMS = "CART_UPDATE_ITEMS";
        public const string HOMEBUTTON = "HOMEBUTTON";
        public const string SOLI_CATEGORY_LIST = "SOLI_CATEGORY_LIST";
        public const string ITEMSSORTBY = "ITEMSSORTBY";
        public const string SOLITER_SORTBY = "SOLITER_SORTBY";
        public const string PLAINGOLDSORTBYLIST = "PLAINGOLDSORTBYLIST";
        public const string EXTRAGOLDRATEWISEPRICE = "ExtraGoldRateWisePrice";
        public const string HOMESCREENMASTER = "HOMESCREENMASTER";
        public const string USERWATCHLIST = "USERWATCHLIST";
        public const string ADDTOWATCHLIST_NEW = "ADDTOWATCHLIST_NEW";
        public const string WATCHLISTDELETE_NEW = "WATCHLISTDELETE_NEW";
        public const string WATCHLISTITEMDELETE_NEW = "WATCHLISTITEMDELETE_NEW";
        public const string CARTCHILDLIST_NEW = "usp_CartChildList";
        public const string HOMEDATA_LIST = "HOMEDATA_LIST";
        public const string VIEWDIAMONDDETAIL_NEW = "VIEWDIAMONDDETAIL_NEW";
        public const string SEARCHALL_ITEMLIST = "SEARCHALL_ITEMLIST";
        public const string HOMELISTNEW = "usp_HomeList";

        //ORDERS
        public const string GET_MYORDERS = "GET_MYORDERS";
        public const string GETMYORDERITEMS = "GETMYORDERITEMS";
        public const string ORDERASSIGNLIST = "OrderAssignList";
        public const string CARTCANCEL = "CartCancel";
        public const string CARTSINGLECANCEL = "CartSingleCancel";
        public const string GETCOLORBULK = "GETCOLORBULK";
        public const string GETCATEGORYSIZEBULK = "GETCATEGORYSIZEBULK";
        public const string GETITEMSIZEBULKBY_SIZEID = "GETITEMSIZEBULKBY_SIZEID";
        public const string BULKIMPORTITEMS_INITIALCHECK = "BULKIMPORTITEMS_INITIALCHECK";
        public const string BULKIMPORTITEMS_INSERT_ITEMWISE_DATA = "BULKIMPORTITEMS_INSERT_ITEMWISE_DATA";
        public const string BULKIMPORTITEMS_GETDATA = "BULKIMPORTITEMS_GETDATA";
        public const string BULKIMPORTITEMS_DELETE_TEMP_AND_INSERT_LOG = "BULKIMPORTITEMS_DELETE_TEMP_AND_INSERT_LOG";
        public const string BULKUPLOADDELETE = "BULKUPLOADDELETE";
        public const string BULKUPLOAD_VERIFYDATA = "BULKUPLOAD_VERIFYDATA";
        public const string BULKUPLOAD_ORDERTYPEVALUE = "BULKUPLOAD_ORDERTYPEVALUE";
        public const string CHECKOUTBULKUPLOAD_NEW = "CHECKOUTBULKUPLOAD_NEW";
        public const string CHECKOUTBULKUPLOAD_NEW_INSERTCARTMSTITEM = "CHECKOUTBULKUPLOAD_NEW_INSERTCARTMSTITEM";
        public const string CHECKOUTBULKUPLOAD_NEW_INSERTGOLDCARTITEM = "CHECKOUTBULKUPLOAD_NEW_INSERTGOLDCARTITEM";
        public const string CHECKOUTBULKUPLOAD_NEW_INSERTLOG = "CHECKOUTBULKUPLOAD_NEW_INSERTLOG";
        public const string CHECKOUTBULKUPLOAD_NEW_TEMPTABLELOG = "CHECKOUTBULKUPLOAD_NEW_TEMPTABLELOG";
        public const string CARTCHECKOUTNOALLOTWEB = "CARTCHECKOUTNOALLOTWEB";
        public const string CARTCHECKOUTNOALLOTWEB_SAVEDIABOOKRESPONSE = "CARTCHECKOUTNOALLOTWEB_SAVEDIABOOKRESPONSE";
        public const string CARTCHECKOUTNOALLOTWEB_SAVECARTMST = "CARTCHECKOUTNOALLOTWEB_SAVECARTMST";
        public const string CARTCHECKOUTNOALLOTWEB_SAVE_SOLITAIRESTATUS = "CARTCHECKOUTNOALLOTWEB_SAVE_SOLITAIRESTATUS";
        public const string CARTCHECKOUTNOALLOTWEB_UPDATE_CARTMST = "CARTCHECKOUTNOALLOTWEB_UPDATE_CARTMST";
        public const string CARTCHECKOUTNOALLOTWEB_SAVE_CARTSTATUSMST = "CARTCHECKOUTNOALLOTWEB_SAVE_CARTSTATUSMST";
        public const string CARTCHECKOUTNOALLOTWEB_DELETEANDREASSIGNCART = "CARTCHECKOUTNOALLOTWEB_DELETEANDREASSIGNCART";
        public const string CARTCHECKOUTNOALLOTWEB_DELETECARTMST = "CARTCHECKOUTNOALLOTWEB_DELETECARTMST";
        public const string ITEMDYNAMICPRICECART = "ITEMDYNAMICPRICECART";

        //WISHLIST
        public const string WISHLISTINSERTITEM = "WISHLISTINSERTITEM";

        //Order Tracking
        public const string TotalOrderTrackingData = "usp_GetTotalOrderTrackingData";
        public const string GetTrackSingleDataList = "usp_GetTrackSingleDataList";
        public const string GetTrackingItemDetailData = "usp_GetTrackItemDetailData";
    }
}
