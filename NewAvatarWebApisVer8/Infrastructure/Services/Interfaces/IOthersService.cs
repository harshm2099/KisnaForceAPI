using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IOthersService
    {
        public ServiceResponse<IList<T_PIECE_VERIFY_LOG>> VerifyProduct(string numType, string numValue);

        public ServiceResponse<IList<T_NEW_ROUTE_USER_LIST>> GetShopList(string State, string FromDt, string ToDt);

        public ServiceResponse<bool> AddShoplist(Shoplistitems Shoplistitems);

        public ServiceResponse<IList<GoldParameter>> FetchGoldParameters(int paramID);

        public ServiceResponse<bool> UpdateGoldValue(GoldParameter gold);

        public ServiceResponse<IList<RmPrice>> FetchRMValuesForFranchise(string rmType);

        public ServiceResponse<bool> UpdateFranchiseRmPrices(RmPrice rmprice);

        public ServiceResponse<IList<RmPrice>> FetchColorStonesForFranchise(string stone = "Bugguete");

        public ServiceResponse<bool> UpdateFranchiseColorStonePrices(RmPrice rmprice);

        public ServiceResponse<IList<RmPrice>> FetchDiamondRatesForFranchise(string quality = "SI", string shape = "RND");

        public ServiceResponse<bool> UpdateFranchiseDiamondPrices(RmPrice rmprice);

        public ServiceResponse<IList<RmPrice>> FetchExtraMaterialRatesForFranchise();

        public ServiceResponse<bool> UpdateFranchiseExtraMaterialPrices(RmPrice rmprice);

        public ServiceResponse<IList<RmPrice>> FetchRMValuesForDistributor(string rmType = "L", string ppTag = "Promo");

        public ServiceResponse<bool> UpdateDistributorRmPrices(RmPrice rmprice);

        public ServiceResponse<IList<RmPrice>> FetchExtraMaterialRatesForDistributor(string pptag);

        public ServiceResponse<bool> UpdateDistributorExtraMaterialPrices(RmPrice rmprice);

        public ServiceResponse<IList<RmPrice>> FetchColorStoneRatesForDistributor(string stone = "Bugguete", string pptag = "Premium");

        public ServiceResponse<bool> UpdateDistributorColorStonePrices(RmPrice rmprice);

        public ServiceResponse<IList<RmPrice>> FetchDiamondRatesForDistributor(string pptag = "Premium", string quality = "SI", string shape = "RND");

        public ServiceResponse<bool> UpdateDistributorDiamondPrices(RmPrice rmprice);

        public ServiceResponse<IList<GeoData>> FetchGeometricReport();

        public ServiceResponse<IList<LOC>> FetchLOCValuesForFranchise(int rmUserID, int rmCatID, int rmComplexityID);

        public ServiceResponse<bool> UpdateFranchiseLOC(LOC loc);

        public ServiceResponse<IList<LOCC>> FetchLOCCValuesForFranchise(int rmUserID, int rmComplexityID);

        public ServiceResponse<bool> UpdateFranchiseLOCC(LOCC loc);

        public ServiceResponse<IList<LOC>> FetchLOCValuesForDistributor(int rmUserTypeID, int rmUserID, int rmComplexityID);

        public ServiceResponse<bool> UpdateDistributorLOC(LOC loc);

        public ServiceResponse<IList<LOCC>> FetchLOCCValuesForDistributor(int rmUserID, int rmUserTypeID, int rmComplexityID);

        public ServiceResponse<bool> UpdateDistributorLOCC(LOCC loc);

        public ServiceResponse<IList<GoldParameter>> FetchGoldParametersForFranchise(int paramID = 0);

        public ServiceResponse<IList<GoldParameter>> FetchGoldParametersForFranchiseDetails(int paramID = 0);

        public ServiceResponse<bool> UpdateGoldValueForFranchise(GoldParameter gold);

        public ServiceResponse<IList<T_INFO_SRC_MST>> GetAllSurveys();

        public ServiceResponse<bool> AddInformation(T_INFO_SRC_MST info);

        public ServiceResponse<bool> EditInformation(T_INFO_SRC_MST info);

        public ServiceResponse<bool> DisableInformation(T_INFO_SRC_MST info);

        public ServiceResponse<IList<Banner>> GetAllBanners();

        public ServiceResponse<bool> AddBanner(Banner info);

        public ServiceResponse<bool> EditBanner(Banner info);

        public ServiceResponse<bool> DisableBanner(Banner info);

        public ServiceResponse<IList<ScreenSaver>> GetAllScreenSavers();

        public ServiceResponse<bool> AddScreenSaver(ScreenSaver saver);

        public ServiceResponse<bool> EditScreenSaver(ScreenSaver saver);

        public ServiceResponse<bool> DisableScreenSaver(ScreenSaver saver);

        public ServiceResponse<IList<NewOutlet>> GetAllNewOutlets();

        public ServiceResponse<bool> AddNewOutlet(NewOutlet outlet);

        public ServiceResponse<bool> EditNewOutlet(NewOutlet outlet);

        public ServiceResponse<bool> DisableNewOutlet(NewOutlet outlet);

        public ServiceResponse<IList<SoliterParameter>> GetAllSoliterParameter(int FieldId);

        public ServiceResponse<bool> AddSoliterParameter(SoliterParameter info);

        public ServiceResponse<bool> EditSoliterParameter(SoliterParameter info);

        public ServiceResponse<bool> DisableSoliterParameter(SoliterParameter info);

        public ServiceResponse<bool> DeleteSoliterParameter(SoliterParameter info);

        public ServiceResponse<IList<DataMaster>> GetUserData(int DataId);

        public ServiceResponse<IList<DiscontinueItems>> GetItemPriceData(Int16 Source, int DataId, string ItemName);

        public ServiceResponse<IList<Ecatelog>> GetEcatelog();

        public ServiceResponse<bool> AddNewCatelog(Ecatelog ecatelog);

        public ServiceResponse<bool> UpdateCatelog(Ecatelog ecatelog);

        public ServiceResponse<bool> DeleteCatelog(Ecatelog ecatelog);
    }
}
