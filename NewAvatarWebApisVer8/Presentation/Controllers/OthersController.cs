using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class OthersController : ControllerBase
    {
        private readonly IOthersService _othersService;
        public OthersController(IOthersService othersService)
        {
            _othersService = othersService;
        }

        [HttpGet("verify-product")]
        public ServiceResponse<IList<T_PIECE_VERIFY_LOG>> VerifyProduct(string numType, string numValue)
        {
            var result = _othersService.VerifyProduct(numType, numValue);
            return result;
        }


        [HttpGet("shop-list")]
        public ServiceResponse<IList<T_NEW_ROUTE_USER_LIST>> GetShopList(string State, string FromDt, string ToDt)
        {
            var result = _othersService.GetShopList(State, FromDt, ToDt);
            return result;
        }

        [HttpPost("add-shop-list")]
        public ServiceResponse<bool> AddShoplist(Shoplistitems Shoplistitems)
        {
            var result = _othersService.AddShoplist(Shoplistitems);
            return result;
        }

        [HttpGet("get-gold-params")]
        public ServiceResponse<IList<GoldParameter>> FetchGoldParameters(int paramID)
        {
            var result = _othersService.FetchGoldParameters(0);
            return result;
        }

        [HttpPost("save-gold-params")]
        public ServiceResponse<bool> UpdateGoldValue(GoldParameter gold)
        {
            var result = _othersService.UpdateGoldValue(gold);
            return result;
        }

        [HttpGet("get-franchise-rm-values")]
        public ServiceResponse<IList<RmPrice>> FetchRMValuesForFranchise(string rmType)
        {
            var result = _othersService.FetchRMValuesForFranchise("L");
            return result;
        }

        [HttpPost("save-rm-prices")]
        public ServiceResponse<bool> UpdateFranchiseRmPrices(RmPrice rmprice)
        {
            var result = _othersService.UpdateFranchiseRmPrices(rmprice);
            return result;
        }

        [HttpGet("get-franchise-color-stones")]
        public ServiceResponse<IList<RmPrice>> FetchColorStonesForFranchise(string stone)
        {
            var result = _othersService.FetchColorStonesForFranchise("Bugguete");
            return result;
        }

        [HttpPost("save-color-stone-prices")]
        public ServiceResponse<bool> UpdateFranchiseColorStonePrices(RmPrice rmprice)
        {
            var result = _othersService.UpdateFranchiseColorStonePrices(rmprice);
            return result;
        }

        [HttpGet("get-franchise-diamond-rates")]
        public ServiceResponse<IList<RmPrice>> FetchDiamondRatesForFranchise(string quality, string shape)
        {
            var result = _othersService.FetchDiamondRatesForFranchise("SI", "RND");
            return result;
        }

        [HttpPost("save-diamond-prices")]
        public ServiceResponse<bool> UpdateFranchiseDiamondPrices(RmPrice rmprice)
        {
            var result = _othersService.UpdateFranchiseDiamondPrices(rmprice);
            return result;
        }

        [HttpGet("get-franchise-extra-material-rates")]
        public ServiceResponse<IList<RmPrice>> FetchExtraMaterialRatesForFranchise()
        {
            var result = _othersService.FetchExtraMaterialRatesForFranchise();
            return result;
        }

        [HttpPost("save-extra-material-prices")]
        public ServiceResponse<bool> UpdateFranchiseExtraMaterialPrices(RmPrice rmprice)
        {
            var result = _othersService.UpdateFranchiseExtraMaterialPrices(rmprice);
            return result;
        }

        [HttpGet("get-distributor-rm-values")]
        public ServiceResponse<IList<RmPrice>> FetchRMValuesForDistributor(string rmType, string ppTag)
        {
            var result = _othersService.FetchRMValuesForDistributor("L", "Promo");
            return result;
        }

        [HttpPost("save-distributor-rm-prices")]
        public ServiceResponse<bool> UpdateDistributorRmPrices(RmPrice rmprice)
        {
            var result = _othersService.UpdateDistributorRmPrices(rmprice);
            return result;
        }

        [HttpGet("get-distributor-extra-material-rates")]
        public ServiceResponse<IList<RmPrice>> FetchExtraMaterialRatesForDistributor(string pptag)
        {
            var result = _othersService.FetchExtraMaterialRatesForDistributor(pptag);
            return result;
        }

        [HttpPost("save-extra-material-prices-distributor")]
        public ServiceResponse<bool> UpdateDistributorExtraMaterialPrices(RmPrice rmprice)
        {
            var result = _othersService.UpdateDistributorExtraMaterialPrices(rmprice);
            return result;
        }

        [HttpGet("get-distributor-cs-rates")]
        public ServiceResponse<IList<RmPrice>> FetchColorStoneRatesForDistributor(string stone, string pptag)
        {
            var result = _othersService.FetchColorStoneRatesForDistributor("Bugguete", "Premium");
            return result;
        }

        [HttpPost("save-cs-prices-distributor")]
        public ServiceResponse<bool> UpdateDistributorColorStonePrices(RmPrice rmprice)
        {
            var result = _othersService.UpdateDistributorColorStonePrices(rmprice);
            return result;
        }

        [HttpGet("get-distributor-diamond-rates")]
        public ServiceResponse<IList<RmPrice>> FetchDiamondRatesForDistributor(string pptag, string quality, string shape)
        {
            var result = _othersService.FetchDiamondRatesForDistributor("Premium", "SI", "RND");
            return result;
        }

        [HttpPost("save-diamond-prices-distributor")]
        public ServiceResponse<bool> UpdateDistributorDiamondPrices(RmPrice rmprice)
        {
            var result = _othersService.UpdateDistributorDiamondPrices(rmprice);
            return result;
        }

        [HttpGet("get-geo-metric-report")]
        public ServiceResponse<IList<GeoData>> FetchGeometricReport()
        {
            var result = _othersService.FetchGeometricReport();
            return result;
        }

        [HttpGet("get-loc-values")]
        public ServiceResponse<IList<LOC>> FetchLOCValuesForFranchise(int rmUserID, int rmCatID, int rmComplexityID)
        {
            var result = _othersService.FetchLOCValuesForFranchise(rmUserID, rmCatID, rmComplexityID);
            return result;
        }

        [HttpPost("save-loc")]
        public ServiceResponse<bool> UpdateFranchiseLOC(LOC loc)
        {
            var result = _othersService.UpdateFranchiseLOC(loc);
            return result;
        }

        [HttpGet("get-locc-values")]
        public ServiceResponse<IList<LOCC>> FetchLOCCValuesForFranchise(int rmUserID, int rmComplexityID)
        {
            var result = _othersService.FetchLOCCValuesForFranchise(rmUserID, rmComplexityID);
            return result;
        }

        [HttpPost("save-locc")]
        public ServiceResponse<bool> UpdateFranchiseLOCC(LOCC loc)
        {
            var result = _othersService.UpdateFranchiseLOCC(loc);
            return result;
        }

        [HttpGet("get-loc-values-dist")]
        public ServiceResponse<IList<LOC>> FetchLOCValuesForDistributor(int rmUserTypeID, int rmUserID, int rmComplexityID)
        {
            var result = _othersService.FetchLOCValuesForDistributor(rmUserTypeID, rmUserID, rmComplexityID);
            return result;
        }

        [HttpPost("save-loc-dist")]
        public ServiceResponse<bool> UpdateDistributorLOC(LOC loc)
        {
            var result = _othersService.UpdateDistributorLOC(loc);
            return result;
        }

        [HttpGet("get-locc-values-dist")]
        public ServiceResponse<IList<LOCC>> FetchLOCCValuesForDistributor(int rmUserID, int rmUserTypeID, int rmComplexityID)
        {
            var result = _othersService.FetchLOCCValuesForDistributor(rmUserID, rmUserTypeID, rmComplexityID);
            return result;
        }

        [HttpPost("save-locc-dist")]
        public ServiceResponse<bool> UpdateDistributorLOCC(LOCC loc)
        {
            var result = _othersService.UpdateDistributorLOCC(loc);
            return result;
        }

        [HttpGet("get-gold-params-franchise")]
        public ServiceResponse<IList<GoldParameter>> FetchGoldParametersForFranchise(int paramID)
        {
            var result = _othersService.FetchGoldParametersForFranchise(0);
            return result;
        }

        [HttpGet("get-gold-params-franchise-details")]
        public ServiceResponse<IList<GoldParameter>> FetchGoldParametersForFranchiseDetails(int paramID)
        {
            var result = _othersService.FetchGoldParametersForFranchiseDetails(0);
            return result;
        }

        [HttpPost("save-gold-params-franchise")]
        public ServiceResponse<bool> UpdateGoldValueForFranchise(GoldParameter gold)
        {
            var result = _othersService.UpdateGoldValueForFranchise(gold);
            return result;
        }

        [HttpGet("get-surveys")]
        public ServiceResponse<IList<T_INFO_SRC_MST>> GetAllSurveys()
        {
            var result = _othersService.GetAllSurveys();
            return result;
        }

        [HttpPost("add-survey")]
        public ServiceResponse<bool> AddInformation(T_INFO_SRC_MST info)
        {
            var result = _othersService.AddInformation(info);
            return result;
        }

        [HttpPost("edit-survey")]
        public ServiceResponse<bool> EditInformation(T_INFO_SRC_MST info)
        {
            var result = _othersService.EditInformation(info);
            return result;
        }

        [HttpPost("disable-survey")]
        public ServiceResponse<bool> DisableInformation(T_INFO_SRC_MST info)
        {
            var result = _othersService.DisableInformation(info);
            return result;
        }

        [HttpGet("get-banners")]
        public ServiceResponse<IList<Banner>> GetAllBanners()
        {
            var result = _othersService.GetAllBanners();
            return result;
        }

        [HttpPost("add-banner")]
        public ServiceResponse<bool> AddBanner(Banner info)
        {
            var result = _othersService.AddBanner(info);
            return result;
        }

        [HttpPost("edit-banner")]
        public ServiceResponse<bool> EditBanner(Banner info)
        {
            var result = _othersService.EditBanner(info);
            return result;
        }

        [HttpPost("disable-banner")]
        public ServiceResponse<bool> DisableBanner(Banner info)
        {
            var result = _othersService.DisableBanner(info);
            return result;
        }

        [HttpGet("get-screen-savers")]
        public ServiceResponse<IList<ScreenSaver>> GetAllScreenSavers()
        {
            var result = _othersService.GetAllScreenSavers();
            return result;
        }

        [HttpPost("add-screen-saver")]
        public ServiceResponse<bool> AddScreenSaver(ScreenSaver saver)
        {
            var result = _othersService.AddScreenSaver(saver);
            return result;
        }

        [HttpPost("edit-screen-saver")]
        public ServiceResponse<bool> EditScreenSaver(ScreenSaver saver)
        {
            var result = _othersService.EditScreenSaver(saver);
            return result;
        }

        [HttpPost("disable-screen-saver")]
        public ServiceResponse<bool> DisableScreenSaver(ScreenSaver saver)
        {
            var result = _othersService.DisableScreenSaver(saver);
            return result;
        }

        [HttpGet("get-new-outlets")]
        public ServiceResponse<IList<NewOutlet>> GetAllNewOutlets()
        {
            var result = _othersService.GetAllNewOutlets();
            return result;
        }

        [HttpPost("add-new-outlet")]
        public ServiceResponse<bool> AddNewOutlet(NewOutlet outlet)
        {
            var result = _othersService.AddNewOutlet(outlet);
            return result;
        }

        [HttpPost("edit-new-outlet")]
        public ServiceResponse<bool> EditNewOutlet(NewOutlet outlet)
        {
            var result = _othersService.EditNewOutlet(outlet);
            return result;
        }

        [HttpPost("disable-new-outlet")]
        public ServiceResponse<bool> DisableNewOutlet(NewOutlet outlet)
        {
            var result = _othersService.DisableNewOutlet(outlet);
            return result;
        }

        [HttpGet("get-solitaire-parameter")]
        public ServiceResponse<IList<SoliterParameter>> GetAllSoliterParameter(int FieldId)
        {
            var result = _othersService.GetAllSoliterParameter(FieldId);
            return result;
        }

        [HttpPost("add-solitaire-parameter")]
        public ServiceResponse<bool> AddSoliterParameter(SoliterParameter info)
        {
            var result = _othersService.AddSoliterParameter(info);
            return result;
        }

        [HttpPost("edit-solitaire-parameter")]
        public ServiceResponse<bool> EditSoliterParameter(SoliterParameter info)
        {
            var result = _othersService.EditSoliterParameter(info);
            return result;
        }

        [HttpPost("disable-solitaire-parameter")]
        public ServiceResponse<bool> DisableSoliterParameter(SoliterParameter info)
        {
            var result = _othersService.DisableSoliterParameter(info);
            return result;
        }

        [HttpPost("delete-solitaire-parameter")]
        public ServiceResponse<bool> DeleteSoliterParameter(SoliterParameter info)
        {
            var result = _othersService.DeleteSoliterParameter(info);
            return result;
        }

        [HttpGet("get-user-data")]
        public ServiceResponse<IList<DataMaster>> GetUserData(int DataId)
        {
            var result = _othersService.GetUserData(DataId);
            return result;
        }

        [HttpGet("get-item-price-data")]
        public ServiceResponse<IList<DiscontinueItems>> GetItemPriceData(Int16 Source, int DataId, string ItemName)
        {
            var result = _othersService.GetItemPriceData(Source, DataId, ItemName);
            return result;
        }

        [HttpGet("get-ecatelog")]
        public ServiceResponse<IList<Ecatelog>> GetEcatelog()
        {
            var result = _othersService.GetEcatelog();
            return result;
        }

        [HttpPost("add-new-ecatelog")]
        public ServiceResponse<bool> AddNewCatelog(Ecatelog ecatelog)
        {
            var result = _othersService.AddNewCatelog(ecatelog);
            return result;
        }

        [HttpPost("update-catelog")]
        public ServiceResponse<bool> UpdateCatelog(Ecatelog ecatelog)
        {
            var result = _othersService.UpdateCatelog(ecatelog);
            return result;
        }

        [HttpPost("delete-ecatelog")]
        public ServiceResponse<bool> DeleteCatelog(Ecatelog ecatelog)
        {
            var result = _othersService.DeleteCatelog(ecatelog);
            return result;
        }
    }
}
