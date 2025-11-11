using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IMasterService
    {
        public ServiceResponse<IList<MasterZone>> GetAllZones();

        public ServiceResponse<bool> AddZones(MasterZone zone);

        public ServiceResponse<bool> EditZones(MasterZone zone);

        public ServiceResponse<bool> DisableZones(MasterZone zone);

        public ServiceResponse<IList<MasterZone>> GetZoneByID(int id);

        public ServiceResponse<IList<MasterBrand>> GetAllBrands();

        public ServiceResponse<bool> AddBrands(MasterBrand brand);

        public ServiceResponse<bool> EditBrands(MasterBrand brand);

        public ServiceResponse<bool> DisableBrands(MasterBrand brand);

        public ServiceResponse<IList<MasterBrand>> GetBrandByID(int id);

        public ServiceResponse<IList<MasterBrand>> GetBrandByName(string name);

        public ServiceResponse<IList<MasterCategory>> GetAllCategories();

        public ServiceResponse<bool> AddCategory(MasterCategory category);

        public ServiceResponse<bool> EditCategory(MasterCategory category);

        public ServiceResponse<bool> DisableCategory(MasterCategory category);

        public ServiceResponse<IList<MasterCategory>> GetCategoryByID(int id);

        public ServiceResponse<IList<MasterCategory>> GetCategoryByName(string name);

        public ServiceResponse<IList<MasterRegion>> GetAllRegions();

        public ServiceResponse<IList<MasterRegion>> GetAllRegionByZoneId(string zoneid);

        public ServiceResponse<bool> AddRegion(MasterRegion region);

        public ServiceResponse<bool> EditRegion(MasterRegion region);

        public ServiceResponse<bool> DisableRegion(MasterRegion region);

        public ServiceResponse<IList<MasterRegion>> GetRegionByID(int id);

        public ServiceResponse<IList<MasterRegion>> GetRegionByName(MasterRegion region);

        public ServiceResponse<IList<MasterState>> GetAllStates();

        public ServiceResponse<IList<MasterState>> GetAllStatesByRegionId(string regionid);

        public ServiceResponse<IList<MasterState>> GetAllStatesByzoneId(string zoneid);

        public ServiceResponse<bool> AddState(MasterState state);

        public ServiceResponse<bool> EditState(MasterState state);

        public ServiceResponse<bool> DisableState(MasterState state);

        public ServiceResponse<IList<MasterDistrict>> GetAllDistricts();

        public ServiceResponse<IList<MasterDistrict>> GetAllDistrictsByStateId(string stateid);

        public ServiceResponse<IList<MasterTerritory>> GetAllTerritorysByStateId(string stateid);

        public ServiceResponse<bool> AddDistrict(MasterDistrict district);

        public ServiceResponse<bool> EditDistrict(MasterDistrict district);

        public ServiceResponse<bool> DisableDistrict(MasterDistrict district);

        public ServiceResponse<IList<MasterTaluka>> GetAllTalukas();

        public ServiceResponse<IList<MasterTaluka>> GetAllTalukasByDistrictId(string districtid);

        public ServiceResponse<bool> AddTaluka(MasterTaluka taluka);

        public ServiceResponse<bool> EditTaluka(MasterTaluka taluka);

        public ServiceResponse<bool> DisableTaluka(MasterTaluka taluka);

        public ServiceResponse<IList<MasterCity>> GetAllCities();

        public ServiceResponse<IList<MasterCity>> GetCityByID(int id);

        public ServiceResponse<IList<MasterCity>> GetAllCityByDistrictId(string districtid);

        public ServiceResponse<IList<MasterCity>> GetAllCityByterritoryId(string territoryid);

        public ServiceResponse<bool> AddCity(MasterCity city);

        public ServiceResponse<bool> EditCity(MasterCity city);

        public ServiceResponse<bool> DisableCity(MasterCity city);

        public ServiceResponse<IList<MasterArea>> GetAllAreas();

        public ServiceResponse<IList<MasterArea>> GetAllAreasByCityId(string cityid);

        public ServiceResponse<bool> AddArea(MasterArea area);

        public ServiceResponse<bool> EditArea(MasterArea area);

        public ServiceResponse<bool> DisableArea(MasterArea area);

        public ServiceResponse<IList<MasterAntique>> GetAllAntiques();

        public ServiceResponse<bool> AddAntique(MasterAntique antique);

        public ServiceResponse<bool> EditAntique(MasterAntique antique);

        public ServiceResponse<bool> DisableAntique(MasterAntique antique);

        public ServiceResponse<IList<MasterColor>> GetAllColors();

        public ServiceResponse<bool> AddColor(MasterColor color);

        public ServiceResponse<bool> EditColor(MasterColor color);

        public ServiceResponse<bool> DisableColor(MasterColor color);

        public ServiceResponse<IList<MasterCADDesigner>> GetAllDesigners();

        public ServiceResponse<bool> AddDesigner(MasterCADDesigner designer);

        public ServiceResponse<bool> EditDesigner(MasterCADDesigner designer);

        public ServiceResponse<bool> DisableDesigner(MasterCADDesigner designer);

        public ServiceResponse<IList<MasterCartBifurcation>> GetAllCartDays();

        public ServiceResponse<bool> AddCartDays(MasterCartBifurcation cart);

        public ServiceResponse<bool> EditCartDays(MasterCartBifurcation cart);

        public ServiceResponse<bool> DisableCartDays(MasterCartBifurcation cart);

        public ServiceResponse<IList<MasterUserType>> GetAllUserTypes();

        public ServiceResponse<bool> AddUserType(MasterUserType userType);

        public ServiceResponse<bool> EditUserType(MasterUserType userType);

        public ServiceResponse<bool> DisableUserType(MasterUserType userType);

        public ServiceResponse<IList<MasterCollection>> GetAllCollections();

        public ServiceResponse<bool> AddCollection(MasterCollection collection);

        public ServiceResponse<bool> EditCollection(MasterCollection collection);

        public ServiceResponse<bool> DisableCollection(MasterCollection collection);

        public ServiceResponse<IList<MasterCourier>> GetAllCourierNames();

        public ServiceResponse<bool> AddCourier(MasterCourier courier);

        public ServiceResponse<bool> EditCourier(MasterCourier courier);

        public ServiceResponse<bool> DisableCourier(MasterCourier courier);

        public ServiceResponse<IList<MasterCustomerCare>> GetAllCustomerCares();

        public ServiceResponse<bool> AddCare(MasterCustomerCare care);

        public ServiceResponse<bool> EditCare(MasterCustomerCare care);

        public ServiceResponse<bool> DisableCare(MasterCustomerCare care);

        public ServiceResponse<IList<MasterItemDesigner>> GetAllItemDesigners();

        public ServiceResponse<bool> AddItemDesigner(MasterItemDesigner designer);

        public ServiceResponse<bool> EditItemDesigner(MasterItemDesigner designer);

        public ServiceResponse<bool> DisableItemDesigner(MasterItemDesigner designer);

        public ServiceResponse<IList<MasterStone>> GetAllStones();

        public ServiceResponse<bool> AddStone(MasterStone stone);

        public ServiceResponse<bool> EditStone(MasterStone stone);

        public ServiceResponse<bool> DisableStone(MasterStone stone);

        public ServiceResponse<IList<MasterDesignStone>> GetAllDesignStones();

        public ServiceResponse<bool> AddDesignStone(MasterDesignStone stone);

        public ServiceResponse<bool> EditDesignStone(MasterDesignStone stone);

        public ServiceResponse<bool> DisableDesignStone(MasterDesignStone stone);

        public ServiceResponse<IList<MasterDiamondQuality>> GetAllDiamondqualities();

        public ServiceResponse<bool> AddDiamondQuality(MasterDiamondQuality quality);

        public ServiceResponse<bool> EditDiamondQuality(MasterDiamondQuality quality);

        public ServiceResponse<bool> DisableDiamondQuality(MasterDiamondQuality quality);

        public ServiceResponse<IList<MasterDistributorType>> GetAllDistributorTypes();

        public ServiceResponse<bool> AddDistributortype(MasterDistributorType distributorType);

        public ServiceResponse<bool> EditDistributortype(MasterDistributorType distributorType);

        public ServiceResponse<bool> DisableDistributortype(MasterDistributorType distributorType);

        public ServiceResponse<IList<MasterEmail>> GetAllEmail();

        public ServiceResponse<bool> AddEmail(MasterEmail email);

        public ServiceResponse<bool> EditEmail(MasterEmail email);

        public ServiceResponse<bool> DisableEmail(MasterEmail email);

        public ServiceResponse<IList<MasterEmailIdList>> GetAllEmailList();

        public ServiceResponse<bool> AddEmailIDToList(MasterEmailIdList email);

        public ServiceResponse<bool> EditEmailList(MasterEmailIdList email);

        public ServiceResponse<bool> DisableEmaillist(MasterEmailIdList email);

        public ServiceResponse<IList<MasterEmailSubject>> GetAllEmailSubjects();

        public ServiceResponse<bool> AddEmailSubject(MasterEmailSubject subject);

        public ServiceResponse<bool> EditEmailSubject(MasterEmailSubject subject);

        public ServiceResponse<bool> DisableEmailsubject(MasterEmailSubject subject);

        public ServiceResponse<IList<MasterUom>> GetAllUom();

        public ServiceResponse<bool> AddUOM(MasterUom uom);

        public ServiceResponse<bool> EditUOM(MasterUom uom);

        public ServiceResponse<bool> DisableUom(MasterUom uom);

        public ServiceResponse<IList<MasterMetal>> GetAllMetals();

        public ServiceResponse<bool> AddMetal(MasterMetal metal);

        public ServiceResponse<bool> EditMetal(MasterMetal metal);

        public ServiceResponse<bool> DisableMetal(MasterMetal metal);

        public ServiceResponse<IList<MasterStoneColor>> GetAllStoneColors();

        public ServiceResponse<bool> AddStoneColor(MasterStoneColor scolor);

        public ServiceResponse<bool> EditStoneColor(MasterStoneColor scolor);

        public ServiceResponse<bool> DisableStoneColor(MasterStoneColor scolor);

        public ServiceResponse<IList<MasterShapes>> GetAllStoneShapes();

        public ServiceResponse<bool> AddStoneShape(MasterShapes shape);

        public ServiceResponse<bool> EditStoneShape(MasterShapes shape);

        public ServiceResponse<bool> DisableStoneShape(MasterShapes shape);

        public ServiceResponse<IList<MasterPPTag>> GetAllPPTags();

        public ServiceResponse<bool> AddPPTag(MasterPPTag pptag);

        public ServiceResponse<bool> EditPPTag(MasterPPTag pptag);

        public ServiceResponse<bool> DisablePPTag(MasterPPTag pptag);

        public ServiceResponse<IList<MasterGender>> GetAllGenders();

        public ServiceResponse<bool> AddGender(MasterGender gender);

        public ServiceResponse<bool> EditGender(MasterGender gender);

        public ServiceResponse<bool> DisableGender(MasterGender gender);

        public ServiceResponse<IList<MasterStartColor>> GetAllItemStars();

        public ServiceResponse<bool> AddStarColor(MasterStartColor scolor);

        public ServiceResponse<bool> EditStarColor(MasterStartColor scolor);

        public ServiceResponse<bool> DisableStarColor(MasterStartColor scolor);

        public ServiceResponse<IList<MasterStoneQuality>> GetStoneQuality();

        public ServiceResponse<bool> AddStoneQuality(MasterStoneQuality squality);

        public ServiceResponse<bool> EditStoneQuality(MasterStoneQuality squality);

        public ServiceResponse<bool> DisableStoneQuality(MasterStoneQuality squality);

        public ServiceResponse<IList<MasterDays>> GetItemDays();

        public ServiceResponse<bool> AddItemDays(MasterDays days);

        public ServiceResponse<bool> EditItemDay(MasterDays days);

        public ServiceResponse<bool> DisableItemDay(MasterDays days);

        public ServiceResponse<IList<MasterSubCategory>> GetSubCategories();

        public ServiceResponse<bool> AddSubCategory(MasterSubCategory scat);

        public ServiceResponse<bool> EditSubCategory(MasterSubCategory scat);

        public ServiceResponse<bool> DisableSubCategory(MasterSubCategory scat);

        public ServiceResponse<IList<MasterNameTitle>> GetTitles();

        public ServiceResponse<bool> AddTitle(MasterNameTitle title);

        public ServiceResponse<bool> EditTitle(MasterNameTitle title);

        public ServiceResponse<bool> DisableTitle(MasterNameTitle title);

        public ServiceResponse<IList<MasterPart>> GetParts();

        public ServiceResponse<bool> AddPart(MasterPart part);

        public ServiceResponse<bool> EditPart(MasterPart part);

        public ServiceResponse<bool> DisablePart(MasterPart part);

        public ServiceResponse<IList<MasterLanguages>> GetLanguages();

        public ServiceResponse<bool> AddLanguage(MasterLanguages language);

        public ServiceResponse<bool> EditLanguage(MasterLanguages language);

        public ServiceResponse<bool> DisableLanguage(MasterLanguages language);

        public ServiceResponse<IList<MasterReligion>> GetReligions();

        public ServiceResponse<bool> AddReligion(MasterReligion religion);

        public ServiceResponse<bool> EditReligion(MasterReligion religion);

        public ServiceResponse<bool> DisableReligion(MasterReligion religion);

        public ServiceResponse<IList<MasterProductType>> GetProductTypes();

        public ServiceResponse<bool> AddProductType(MasterProductType ptype);

        public ServiceResponse<bool> EditProductType(MasterProductType ptype);

        public ServiceResponse<bool> DisableProductType(MasterProductType ptype);

        public ServiceResponse<IList<MasterCommon>> GetCommonMasters();

        public ServiceResponse<bool> AddCommon(MasterCommon common);

        public ServiceResponse<bool> EditCommon(MasterCommon common);

        public ServiceResponse<bool> DisableCommon(MasterCommon common);

        public ServiceResponse<IList<T_NOTIFICATION_MST>> GetNotifications();

        public ServiceResponse<bool> AddNotification(T_NOTIFICATION_MST notification);

        public ServiceResponse<bool> EditNotification(T_NOTIFICATION_MST notification);

        public ServiceResponse<bool> DisableNotification(T_NOTIFICATION_MST notification);

        public ServiceResponse<IList<T_APP_SPLASH_MST>> GetSplashScreens();

        public ServiceResponse<bool> AddSplashScreen(T_APP_SPLASH_MST splash);

        public ServiceResponse<bool> EditSplashScreen(T_APP_SPLASH_MST splash);

        public ServiceResponse<bool> DisableSplashScreen(T_APP_SPLASH_MST splash);

        public ServiceResponse<IList<T_DIA_COMMON_MST>> GetSolitaireParameters();

        public ServiceResponse<bool> AddSolitairepParameter(T_DIA_COMMON_MST SOLITAIREPARAM);

        public ServiceResponse<bool> Editsolitaireparameter(T_DIA_COMMON_MST SOLITAIREPARAM);

        public ServiceResponse<bool> Disablesolitaireparameter(T_DIA_COMMON_MST solitaireparameter);

        public ServiceResponse<bool> AddItemsMapping(ItemMapping ItemsMap);

        public ServiceResponse<bool> AddCollectionUserMapping(CollectionUserMapping CollectionUserMap);

        public ServiceResponse<IList<Users>> GetItemsMappingUsers(string ZoneID, string StateID, string TerritoryID, string CityID, string AreaID, string UsertypeID);

        public ServiceResponse<IList<MasterOutletCategory>> GetOutLetCategory();

        public ServiceResponse<IList<MasterTerritory>> GetAllTerritory(Int32 MstId);

        public ServiceResponse<IList<User>> GetAllUsers();

        public ServiceResponse<IList<ImageView>> GetAllImageView();

        public ServiceResponse<bool> AddTerritory(MasterTerritory territory);

        public ServiceResponse<bool> EditTerritory(MasterTerritory territory);

        public ServiceResponse<bool> DisableTerritory(MasterTerritory territory);

        public ServiceResponse<IList<MenuMaster>> GetMenu();

        public ServiceResponse<IList<MenuMaster>> GetMenuMasterOld();

        public ServiceResponse<IList<MenuMaster>> GetMenuMaster();

        //public ServiceResponse<IList<MenuMaster>> GetMenuMasterData(int dataid);
        public ServiceResponse<IList<MenuItem>> GetMenuMasterData(int dataid);

        public ServiceResponse<bool> AddUserMenuPermission(UserMenuPermission UserMenuPermission);

        public ServiceResponse<IList<UserMenuPermission>> GetUserMenuPermission(int dataid);

        public ServiceResponse<IList<UserMenuPermissionCRUD>> GetUserMenuPermissionCRUD(int menupermissionid);

        public ServiceResponse<bool> AddUserMenuPermissionCRUD(UserMenuPermissionCRUD UserMenuPermissionCRUD);

        public ServiceResponse<IList<UserMenuPermissionCRUD>> GetUserMenuPermissionCRUDByDataId(int dataid, int menumasterid);

        public ServiceResponse<IList<MasterTeams>> GetTeamUser(int Flag);
    }
}
