using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet("get-zones")]
        public ServiceResponse<IList<MasterZone>> GetAllZones()
        {
            var result = _masterService.GetAllZones();
            return result;
        }

        [HttpPost("add-zone")]
        public ServiceResponse<bool> AddZones(MasterZone zone)
        {
            var result = _masterService.AddZones(zone);
            return result;
        }

        [HttpPost("edit-zone")]
        public ServiceResponse<bool> EditZones(MasterZone zone)
        {
            var result = _masterService.EditZones(zone);
            return result;
        }

        [HttpPost("disable-zone")]
        public ServiceResponse<bool> DisableZones(MasterZone zone)
        {
            var result = _masterService.DisableZones(zone);
            return result;
        }

        [HttpGet("search-zone")]
        public ServiceResponse<IList<MasterZone>> GetZoneByID(int id)
        {
            var result = _masterService.GetZoneByID(id);
            return result;
        }

        [HttpGet("get-brands")]
        public ServiceResponse<IList<MasterBrand>> GetAllBrands()
        {
            var result = _masterService.GetAllBrands();
            return result;
        }

        [HttpPost("add-brand")]
        public ServiceResponse<bool> AddBrands(MasterBrand brand)
        {
            var result = _masterService.AddBrands(brand);
            return result;
        }

        [HttpPost("edit-brand")]
        public ServiceResponse<bool> EditBrands(MasterBrand brand)
        {
            var result = _masterService.EditBrands(brand);
            return result;
        }

        [HttpPost("disable-brand")]
        public ServiceResponse<bool> DisableBrands(MasterBrand brand)
        {
            var result = _masterService.DisableBrands(brand);
            return result;
        }

        [HttpGet("search-brand")]
        public ServiceResponse<IList<MasterBrand>> GetBrandByID(int id)
        {
            var result = _masterService.GetBrandByID(id);
            return result;
        }

        [HttpGet("search-brand-name")]
        public ServiceResponse<IList<MasterBrand>> GetBrandByName(string name)
        {
            var result = _masterService.GetBrandByName(name);
            return result;
        }

        [HttpGet("get-categories")]
        public ServiceResponse<IList<MasterCategory>> GetAllCategories()
        {
            var result = _masterService.GetAllCategories();
            return result;
        }

        [HttpPost("add-category")]
        public ServiceResponse<bool> AddCategory(MasterCategory category)
        {
            var result = _masterService.AddCategory(category);
            return result;
        }

        [HttpPost("edit-category")]
        public ServiceResponse<bool> EditCategory(MasterCategory category)
        {
            var result = _masterService.EditCategory(category);
            return result;
        }

        [HttpPost("disable-category")]
        public ServiceResponse<bool> DisableCategory(MasterCategory category)
        {
            var result = _masterService.DisableCategory(category);
            return result;
        }

        [HttpGet("search-category")]
        public ServiceResponse<IList<MasterCategory>> GetCategoryByID(int id)
        {
            var result = _masterService.GetCategoryByID(id);
            return result;
        }

        [HttpGet("search-category-name")]
        public ServiceResponse<IList<MasterCategory>> GetCategoryByName(string name)
        {
            var result = _masterService.GetCategoryByName(name);
            return result;
        }

        [HttpGet("get-regions")]
        public ServiceResponse<IList<MasterRegion>> GetAllRegions()
        {
            var result = _masterService.GetAllRegions();
            return result;
        }

        [HttpGet("get-all-region-by-zone-id")]
        public ServiceResponse<IList<MasterRegion>> GetAllRegionByZoneId(string zoneid)
        {
            var result = _masterService.GetAllRegionByZoneId(zoneid);
            return result;
        }

        [HttpPost("add-region")]
        public ServiceResponse<bool> AddRegion(MasterRegion region)
        {
            var result = _masterService.AddRegion(region);
            return result;
        }

        [HttpPost("edit-region")]
        public ServiceResponse<bool> EditRegion(MasterRegion region)
        {
            var result = _masterService.EditRegion(region);
            return result;
        }

        [HttpPost("disable-region")]
        public ServiceResponse<bool> DisableRegion(MasterRegion region)
        {
            var result = _masterService.DisableRegion(region);
            return result;
        }

        [HttpGet("search-region")]
        public ServiceResponse<IList<MasterRegion>> GetRegionByID(int id)
        {
            var result = _masterService.GetRegionByID(id);
            return result;
        }

        [HttpGet("search-region-by-name")]
        public ServiceResponse<IList<MasterRegion>> GetRegionByName(MasterRegion region)
        {
            var result = _masterService.GetRegionByName(region);
            return result;
        }

        [HttpGet("get-states")]
        public ServiceResponse<IList<MasterState>> GetAllStates()
        {
            var result = _masterService.GetAllStates();
            return result;
        }

        [HttpGet("get-all-states-by-region-id")]
        public ServiceResponse<IList<MasterState>> GetAllStatesByRegionId(string regionid)
        {
            var result = _masterService.GetAllStatesByRegionId(regionid);
            return result;
        }

        [HttpGet("get-all-states-by-zone-id")]
        public ServiceResponse<IList<MasterState>> GetAllStatesByzoneId(string zoneid)
        {
            var result = _masterService.GetAllStatesByzoneId(zoneid);
            return result;
        }

        [HttpPost("add-state")]
        public ServiceResponse<bool> AddState(MasterState state)
        {
            var result = _masterService.AddState(state);
            return result;
        }

        [HttpPost("edit-state")]
        public ServiceResponse<bool> EditState(MasterState state)
        {
            var result = _masterService.EditState(state);
            return result;
        }

        [HttpPost("disable-state")]
        public ServiceResponse<bool> DisableState(MasterState state)
        {
            var result = _masterService.DisableState(state);
            return result;
        }

        [HttpGet("get-district")]
        public ServiceResponse<IList<MasterDistrict>> GetAllDistricts()
        {
            var result = _masterService.GetAllDistricts();
            return result;
        }

        [HttpGet("get-all-district-by-state-id")]
        public ServiceResponse<IList<MasterDistrict>> GetAllDistrictsByStateId(string stateid)
        {
            var result = _masterService.GetAllDistrictsByStateId(stateid);
            return result;
        }

        [HttpGet("get-all-territory-by-state-id")]
        public ServiceResponse<IList<MasterTerritory>> GetAllTerritorysByStateId(string stateid)
        {
            var result = _masterService.GetAllTerritorysByStateId(stateid);
            return result;
        }

        [HttpPost("add-district")]
        public ServiceResponse<bool> AddDistrict(MasterDistrict district)
        {
            var result = _masterService.AddDistrict(district);
            return result;
        }

        [HttpPost("edit-district")]
        public ServiceResponse<bool> EditDistrict(MasterDistrict district)
        {
            var result = _masterService.EditDistrict(district);
            return result;
        }

        [HttpPost("disable-district")]
        public ServiceResponse<bool> DisableDistrict(MasterDistrict district)
        {
            var result = _masterService.DisableDistrict(district);
            return result;
        }

        [HttpGet("get-taluka")]
        public ServiceResponse<IList<MasterTaluka>> GetAllTalukas()
        {
            var result = _masterService.GetAllTalukas();
            return result;
        }

        [HttpGet("get-all-taluka-by-district-id")]
        public ServiceResponse<IList<MasterTaluka>> GetAllTalukasByDistrictId(string districtid)
        {
            var result = _masterService.GetAllTalukasByDistrictId(districtid);
            return result;
        }

        [HttpPost("add-taluka")]
        public ServiceResponse<bool> AddTaluka(MasterTaluka taluka)
        {
            var result = _masterService.AddTaluka(taluka);
            return result;
        }

        [HttpPost("edit-taluka")]
        public ServiceResponse<bool> EditTaluka(MasterTaluka taluka)
        {
            var result = _masterService.EditTaluka(taluka);
            return result;
        }

        [HttpPost("disable-taluka")]
        public ServiceResponse<bool> DisableTaluka(MasterTaluka taluka)
        {
            var result = _masterService.DisableTaluka(taluka);
            return result;
        }

        [HttpGet("get-city")]
        public ServiceResponse<IList<MasterCity>> GetAllCities()
        {
            var result = _masterService.GetAllCities();
            return result;
        }

        [HttpGet("search-city")]
        public ServiceResponse<IList<MasterCity>> GetCityByID(int id)
        {
            var result = _masterService.GetCityByID(id);
            return result;
        }

        [HttpGet("get-all-city-by-district-id")]
        public ServiceResponse<IList<MasterCity>> GetAllCityByDistrictId(string districtid)
        {
            var result = _masterService.GetAllCityByDistrictId(districtid);
            return result;
        }

        [HttpGet("get-all-city-by-territory-id")]
        public ServiceResponse<IList<MasterCity>> GetAllCityByterritoryId(string territoryid)
        {
            var result = _masterService.GetAllCityByterritoryId(territoryid);
            return result;
        }

        [HttpPost("add-city")]
        public ServiceResponse<bool> AddCity(MasterCity city)
        {
            var result = _masterService.AddCity(city);
            return result;
        }

        [HttpPost("edit-city")]
        public ServiceResponse<bool> EditCity(MasterCity city)
        {
            var result = _masterService.EditCity(city);
            return result;
        }

        [HttpPost("disable-city")]
        public ServiceResponse<bool> DisableCity(MasterCity city)
        {
            var result = _masterService.DisableCity(city);
            return result;
        }

        [HttpGet("get-areas")]
        public ServiceResponse<IList<MasterArea>> GetAllAreas()
        {
            var result = _masterService.GetAllAreas();
            return result;
        }

        [HttpGet("get-all-areas-by-city-id")]
        public ServiceResponse<IList<MasterArea>> GetAllAreasByCityId(string cityid)
        {
            var result = _masterService.GetAllAreasByCityId(cityid);
            return result;
        }

        [HttpPost("add-area")]
        public ServiceResponse<bool> AddArea(MasterArea area)
        {
            var result = _masterService.AddArea(area);
            return result;
        }

        [HttpPost("edit-area")]
        public ServiceResponse<bool> EditArea(MasterArea area)
        {
            var result = _masterService.EditArea(area);
            return result;
        }

        [HttpPost("disable-area")]
        public ServiceResponse<bool> DisableArea(MasterArea area)
        {
            var result = _masterService.DisableArea(area);
            return result;
        }

        [HttpGet("get-antique")]
        public ServiceResponse<IList<MasterAntique>> GetAllAntiques()
        {
            var result = _masterService.GetAllAntiques();
            return result;
        }

        [HttpPost("add-antique")]
        public ServiceResponse<bool> AddAntique(MasterAntique antique)
        {
            var result = _masterService.AddAntique(antique);
            return result;
        }

        [HttpPost("edit-antique")]
        public ServiceResponse<bool> EditAntique(MasterAntique antique)
        {
            var result = _masterService.EditAntique(antique);
            return result;
        }

        [HttpPost("disable-antique")]
        public ServiceResponse<bool> DisableAntique(MasterAntique antique)
        {
            var result = _masterService.DisableAntique(antique);
            return result;
        }

        [HttpGet("get-colors")]
        public ServiceResponse<IList<MasterColor>> GetAllColors()
        {
            var result = _masterService.GetAllColors();
            return result;
        }

        [HttpPost("add-color")]
        public ServiceResponse<bool> AddColor(MasterColor color)
        {
            var result = _masterService.AddColor(color);
            return result;
        }

        [HttpPost("edit-color")]
        public ServiceResponse<bool> EditColor(MasterColor color)
        {
            var result = _masterService.EditColor(color);
            return result;
        }

        [HttpPost("disable-color")]
        public ServiceResponse<bool> DisableColor(MasterColor color)
        {
            var result = _masterService.DisableColor(color);
            return result;
        }

        [HttpGet("get-designers")]
        public ServiceResponse<IList<MasterCADDesigner>> GetAllDesigners()
        {
            var result = _masterService.GetAllDesigners();
            return result;
        }

        [HttpPost("add-designer")]
        public ServiceResponse<bool> AddDesigner(MasterCADDesigner designer)
        {
            var result = _masterService.AddDesigner(designer);
            return result;
        }

        [HttpPost("edit-designer")]
        public ServiceResponse<bool> EditDesigner(MasterCADDesigner designer)
        {
            var result = _masterService.EditDesigner(designer);
            return result;
        }

        [HttpPost("disable-designer")]
        public ServiceResponse<bool> DisableDesigner(MasterCADDesigner designer)
        {
            var result = _masterService.DisableDesigner(designer);
            return result;
        }

        [HttpGet("get-cart-days")]
        public ServiceResponse<IList<MasterCartBifurcation>> GetAllCartDays()
        {
            var result = _masterService.GetAllCartDays();
            return result;
        }

        [HttpPost("add-cart-days")]
        public ServiceResponse<bool> AddCartDays(MasterCartBifurcation cart)
        {
            var result = _masterService.AddCartDays(cart);
            return result;
        }

        [HttpPost("edit-cart-days")]
        public ServiceResponse<bool> EditCartDays(MasterCartBifurcation cart)
        {
            var result = _masterService.EditCartDays(cart);
            return result;
        }

        [HttpPost("disable-cart-days")]
        public ServiceResponse<bool> DisableCartDays(MasterCartBifurcation cart)
        {
            var result = _masterService.DisableCartDays(cart);
            return result;
        }

        [HttpGet("get-user-types")]
        public ServiceResponse<IList<MasterUserType>> GetAllUserTypes()
        {
            var result = _masterService.GetAllUserTypes();
            return result;
        }

        [HttpPost("add-user-type")]
        public ServiceResponse<bool> AddUserType(MasterUserType userType)
        {
            var result = _masterService.AddUserType(userType);
            return result;
        }

        [HttpPost("edit-user-type")]
        public ServiceResponse<bool> EditUserType(MasterUserType userType)
        {
            var result = _masterService.EditUserType(userType);
            return result;
        }

        [HttpPost("disable-user-type")]
        public ServiceResponse<bool> DisableUserType(MasterUserType userType)
        {
            var result = _masterService.DisableUserType(userType);
            return result;
        }

        [HttpGet("get-collections")]
        public ServiceResponse<IList<MasterCollection>> GetAllCollections()
        {
            var result = _masterService.GetAllCollections();
            return result;
        }

        [HttpPost("add-collection")]
        public ServiceResponse<bool> AddCollection(MasterCollection collection)
        {
            var result = _masterService.AddCollection(collection);
            return result;
        }

        [HttpPost("edit-collection")]
        public ServiceResponse<bool> EditCollection(MasterCollection collection)
        {
            var result = _masterService.EditCollection(collection);
            return result;
        }

        [HttpPost("disable-collection")]
        public ServiceResponse<bool> DisableCollection(MasterCollection collection)
        {
            var result = _masterService.DisableCollection(collection);
            return result;
        }

        [HttpGet("get-couriers")]
        public ServiceResponse<IList<MasterCourier>> GetAllCourierNames()
        {
            var result = _masterService.GetAllCourierNames();
            return result;
        }

        [HttpPost("add-courier")]
        public ServiceResponse<bool> AddCourier(MasterCourier courier)
        {
            var result = _masterService.AddCourier(courier);
            return result;
        }

        [HttpPost("edit-courier")]
        public ServiceResponse<bool> EditCourier(MasterCourier courier)
        {
            var result = _masterService.EditCourier(courier);
            return result;
        }

        [HttpPost("disable-courier")]
        public ServiceResponse<bool> DisableCourier(MasterCourier courier)
        {
            var result = _masterService.DisableCourier(courier);
            return result;
        }

        [HttpGet("get-customer-care")]
        public ServiceResponse<IList<MasterCustomerCare>> GetAllCustomerCares()
        {
            var result = _masterService.GetAllCustomerCares();
            return result;
        }

        [HttpPost("add-customer-care")]
        public ServiceResponse<bool> AddCare(MasterCustomerCare care)
        {
            var result = _masterService.AddCare(care);
            return result;
        }

        [HttpPost("edit-customer-care")]
        public ServiceResponse<bool> EditCare(MasterCustomerCare care)
        {
            var result = _masterService.EditCare(care);
            return result;
        }

        [HttpPost("disable-customer-care")]
        public ServiceResponse<bool> DisableCare(MasterCustomerCare care)
        {
            var result = _masterService.DisableCare(care);
            return result;
        }

        [HttpGet("get-item-designers")]
        public ServiceResponse<IList<MasterItemDesigner>> GetAllItemDesigners()
        {
            var result = _masterService.GetAllItemDesigners();
            return result;
        }

        [HttpPost("add-item-designer")]
        public ServiceResponse<bool> AddItemDesigner(MasterItemDesigner designer)
        {
            var result = _masterService.AddItemDesigner(designer);
            return result;
        }

        [HttpPost("edit-item-designer")]
        public ServiceResponse<bool> EditItemDesigner(MasterItemDesigner designer)
        {
            var result = _masterService.EditItemDesigner(designer);
            return result;
        }

        [HttpPost("disable-item-designer")]
        public ServiceResponse<bool> DisableItemDesigner(MasterItemDesigner designer)
        {
            var result = _masterService.DisableItemDesigner(designer);
            return result;
        }

        [HttpGet("get-stones")]
        public ServiceResponse<IList<MasterStone>> GetAllStones()
        {
            var result = _masterService.GetAllStones();
            return result;
        }

        [HttpPost("add-stone")]
        public ServiceResponse<bool> AddStone(MasterStone stone)
        {
            var result = _masterService.AddStone(stone);
            return result;
        }

        [HttpPost("edit-stone")]
        public ServiceResponse<bool> EditStone(MasterStone stone)
        {
            var result = _masterService.EditStone(stone);
            return result;
        }

        [HttpPost("disable-stone")]
        public ServiceResponse<bool> DisableStone(MasterStone stone)
        {
            var result = _masterService.DisableStone(stone);
            return result;
        }

        [HttpGet("get-design-stones")]
        public ServiceResponse<IList<MasterDesignStone>> GetAllDesignStones()
        {
            var result = _masterService.GetAllDesignStones();
            return result;
        }

        [HttpPost("add-design-stone")]
        public ServiceResponse<bool> AddDesignStone(MasterDesignStone stone)
        {
            var result = _masterService.AddDesignStone(stone);
            return result;
        }

        [HttpPost("edit-design-stone")]
        public ServiceResponse<bool> EditDesignStone(MasterDesignStone stone)
        {
            var result = _masterService.EditDesignStone(stone);
            return result;
        }

        [HttpPost("disable-design-stone")]
        public ServiceResponse<bool> DisableDesignStone(MasterDesignStone stone)
        {
            var result = _masterService.DisableDesignStone(stone);
            return result;
        }

        [HttpGet("get-diamond-quality")]
        [System.Web.Http.HttpGet]
        public ServiceResponse<IList<MasterDiamondQuality>> GetAllDiamondqualities()
        {
            var result = _masterService.GetAllDiamondqualities();
            return result;
        }

        [HttpPost("add-diamond-quality")]
        public ServiceResponse<bool> AddDiamondQuality(MasterDiamondQuality quality)
        {
            var result = _masterService.AddDiamondQuality(quality);
            return result;
        }

        [HttpPost("edit-diamond-quality")]
        public ServiceResponse<bool> EditDiamondQuality(MasterDiamondQuality quality)
        {
            var result = _masterService.EditDiamondQuality(quality);
            return result;
        }

        [HttpPost("disable-diamond-quality")]
        public ServiceResponse<bool> DisableDiamondQuality(MasterDiamondQuality quality)
        {
            var result = _masterService.DisableDiamondQuality(quality);
            return result;
        }

        [HttpGet("get-distributor-types")]
        public ServiceResponse<IList<MasterDistributorType>> GetAllDistributorTypes()
        {
            var result = _masterService.GetAllDistributorTypes();
            return result;
        }

        [HttpPost("add-distributor-type")]
        public ServiceResponse<bool> AddDistributortype(MasterDistributorType distributorType)
        {
            var result = _masterService.AddDistributortype(distributorType);
            return result;
        }

        [HttpPost("edit-distributor-type")]
        public ServiceResponse<bool> EditDistributortype(MasterDistributorType distributorType)
        {
            var result = _masterService.EditDistributortype(distributorType);
            return result;
        }

        [HttpPost("disable-distributor-type")]
        public ServiceResponse<bool> DisableDistributortype(MasterDistributorType distributorType)
        {
            var result = _masterService.DisableDistributortype(distributorType);
            return result;
        }

        [HttpGet("get-email")]
        public ServiceResponse<IList<MasterEmail>> GetAllEmail()
        {
            var result = _masterService.GetAllEmail();
            return result;
        }

        [HttpPost("add-email")]
        public ServiceResponse<bool> AddEmail(MasterEmail email)
        {
            var result = _masterService.AddEmail(email);
            return result;
        }

        [HttpPost("edit-email")]
        public ServiceResponse<bool> EditEmail(MasterEmail email)
        {
            var result = _masterService.EditEmail(email);
            return result;
        }

        [HttpPost("disable-email")]
        public ServiceResponse<bool> DisableEmail(MasterEmail email)
        {
            var result = _masterService.DisableEmail(email);
            return result;
        }

        [HttpGet("get-email-list")]
        public ServiceResponse<IList<MasterEmailIdList>> GetAllEmailList()
        {
            var result = _masterService.GetAllEmailList();
            return result;
        }

        [HttpPost("add-email-list")]
        public ServiceResponse<bool> AddEmailIDToList(MasterEmailIdList email)
        {
            var result = _masterService.AddEmailIDToList(email);
            return result;
        }

        [HttpPost("edit-email-list")]
        public ServiceResponse<bool> EditEmailList(MasterEmailIdList email)
        {
            var result = _masterService.EditEmailList(email);
            return result;
        }

        [HttpPost("disable-email-list")]
        public ServiceResponse<bool> DisableEmaillist(MasterEmailIdList email)
        {
            var result = _masterService.DisableEmaillist(email);
            return result;
        }

        [HttpGet("get-email-subjects")]
        public ServiceResponse<IList<MasterEmailSubject>> GetAllEmailSubjects()
        {
            var result = _masterService.GetAllEmailSubjects();
            return result;
        }

        [HttpPost("add-email-subject")]
        public ServiceResponse<bool> AddEmailSubject(MasterEmailSubject subject)
        {
            var result = _masterService.AddEmailSubject(subject);
            return result;
        }

        [HttpPost("edit-email-subject")]
        public ServiceResponse<bool> EditEmailSubject(MasterEmailSubject subject)
        {
            var result = _masterService.EditEmailSubject(subject);
            return result;
        }

        [HttpPost("disable-email-subject")]
        public ServiceResponse<bool> DisableEmailsubject(MasterEmailSubject subject)
        {
            var result = _masterService.DisableEmailsubject(subject);
            return result;
        }

        [HttpGet("get-uom")]
        public ServiceResponse<IList<MasterUom>> GetAllUom()
        {
            var result = _masterService.GetAllUom();
            return result;
        }

        [HttpPost("add-uom")]
        public ServiceResponse<bool> AddUOM(MasterUom uom)
        {
            var result = _masterService.AddUOM(uom);
            return result;
        }

        [HttpPost("edit-uom")]
        public ServiceResponse<bool> EditUOM(MasterUom uom)
        {
            var result = _masterService.EditUOM(uom);
            return result;
        }

        [HttpPost("disable-uom")]
        public ServiceResponse<bool> DisableUom(MasterUom uom)
        {
            var result = _masterService.DisableUom(uom);
            return result;
        }

        [HttpGet("get-metals")]
        public ServiceResponse<IList<MasterMetal>> GetAllMetals()
        {
            var result = _masterService.GetAllMetals();
            return result;
        }

        [HttpPost("add-metal")]
        public ServiceResponse<bool> AddMetal(MasterMetal metal)
        {
            var result = _masterService.AddMetal(metal);
            return result;
        }

        [HttpPost("edit-metal")]
        public ServiceResponse<bool> EditMetal(MasterMetal metal)
        {
            var result = _masterService.EditMetal(metal);
            return result;
        }

        [HttpPost("disable-metal")]
        public ServiceResponse<bool> DisableMetal(MasterMetal metal)
        {
            var result = _masterService.DisableMetal(metal);
            return result;
        }

        [HttpGet("get-stone-colors")]
        public ServiceResponse<IList<MasterStoneColor>> GetAllStoneColors()
        {
            var result = _masterService.GetAllStoneColors();
            return result;
        }

        [HttpPost("add-stone-color")]
        public ServiceResponse<bool> AddStoneColor(MasterStoneColor scolor)
        {
            var result = _masterService.AddStoneColor(scolor);
            return result;
        }

        [HttpPost("edit-stone-color")]
        public ServiceResponse<bool> EditStoneColor(MasterStoneColor scolor)
        {
            var result = _masterService.EditStoneColor(scolor);
            return result;
        }

        [HttpPost("disable-stone-color")]
        public ServiceResponse<bool> DisableStoneColor(MasterStoneColor scolor)
        {
            var result = _masterService.DisableStoneColor(scolor);
            return result;
        }

        [HttpPost("get-stone-shapes")]
        public ServiceResponse<IList<MasterShapes>> GetAllStoneShapes()
        {
            var result = _masterService.GetAllStoneShapes();
            return result;
        }

        [HttpPost("add-stone-shape")]
        public ServiceResponse<bool> AddStoneShape(MasterShapes shape)
        {
            var result = _masterService.AddStoneShape(shape);
            return result;
        }

        [HttpPost("edit-stone-shape")]
        public ServiceResponse<bool> EditStoneShape(MasterShapes shape)
        {
            var result = _masterService.EditStoneShape(shape);
            return result;
        }

        [HttpPost("disable-stone-shape")]
        public ServiceResponse<bool> DisableStoneShape(MasterShapes shape)
        {
            var result = _masterService.DisableStoneShape(shape);
            return result;
        }

        [HttpGet("get-pptags")]
        public ServiceResponse<IList<MasterPPTag>> GetAllPPTags()
        {
            var result = _masterService.GetAllPPTags();
            return result;
        }

        [HttpPost("add-pptag")]
        public ServiceResponse<bool> AddPPTag(MasterPPTag pptag)
        {
            var result = _masterService.AddPPTag(pptag);
            return result;
        }

        [HttpPost("edit-pptag")]
        public ServiceResponse<bool> EditPPTag(MasterPPTag pptag)
        {
            var result = _masterService.EditPPTag(pptag);
            return result;
        }

        [HttpPost("disable-pptag")]
        public ServiceResponse<bool> DisablePPTag(MasterPPTag pptag)
        {
            var result = _masterService.DisablePPTag(pptag);
            return result;
        }

        [HttpGet("get-genders")]
        public ServiceResponse<IList<MasterGender>> GetAllGenders()
        {
            var result = _masterService.GetAllGenders();
            return result;
        }

        [HttpPost("add-gender")]
        public ServiceResponse<bool> AddGender(MasterGender gender)
        {
            var result = _masterService.AddGender(gender);
            return result;
        }

        [HttpPost("edit-gender")]
        public ServiceResponse<bool> EditGender(MasterGender gender)
        {
            var result = _masterService.EditGender(gender);
            return result;
        }

        [HttpPost("disable-gender")]
        public ServiceResponse<bool> DisableGender(MasterGender gender)
        {
            var result = _masterService.DisableGender(gender);
            return result;
        }

        [HttpGet("get-star-colors")]
        public ServiceResponse<IList<MasterStartColor>> GetAllItemStars()
        {
            var result = _masterService.GetAllItemStars();
            return result;
        }

        [HttpPost("add-star-color")]
        public ServiceResponse<bool> AddStarColor(MasterStartColor scolor)
        {
            var result = _masterService.AddStarColor(scolor);
            return result;
        }

        [HttpPost("edit-star-color")]
        public ServiceResponse<bool> EditStarColor(MasterStartColor scolor)
        {
            var result = _masterService.EditStarColor(scolor);
            return result;
        }

        [HttpPost("disable-star-color")]
        public ServiceResponse<bool> DisableStarColor(MasterStartColor scolor)
        {
            var result = _masterService.DisableStarColor(scolor);
            return result;
        }

        [HttpGet("get-stone-quality")]
        public ServiceResponse<IList<MasterStoneQuality>> GetStoneQuality()
        {
            var result = _masterService.GetStoneQuality();
            return result;
        }

        [HttpPost("add-stone-quality")]
        public ServiceResponse<bool> AddStoneQuality(MasterStoneQuality squality)
        {
            var result = _masterService.AddStoneQuality(squality);
            return result;
        }

        [HttpPost("edit-stone-quality")]
        public ServiceResponse<bool> EditStoneQuality(MasterStoneQuality squality)
        {
            var result = _masterService.EditStoneQuality(squality);
            return result;
        }

        [HttpPost("disable-stone-quality")]
        public ServiceResponse<bool> DisableStoneQuality(MasterStoneQuality squality)
        {
            var result = _masterService.DisableStoneQuality(squality);
            return result;
        }

        [HttpGet("get-item-days")]
        public ServiceResponse<IList<MasterDays>> GetItemDays()
        {
            var result = _masterService.GetItemDays();
            return result;
        }

        [HttpPost("add-item-day")]
        public ServiceResponse<bool> AddItemDays(MasterDays days)
        {
            var result = _masterService.AddItemDays(days);
            return result;
        }

        [HttpPost("edit-item-day")]
        public ServiceResponse<bool> EditItemDay(MasterDays days)
        {
            var result = _masterService.EditItemDay(days);
            return result;
        }

        [HttpPost("disable-item-day")]
        public ServiceResponse<bool> DisableItemDay(MasterDays days)
        {
            var result = _masterService.DisableItemDay(days);
            return result;
        }

        [HttpGet("get-subcategories")]
        public ServiceResponse<IList<MasterSubCategory>> GetSubCategories()
        {
            var result = _masterService.GetSubCategories();
            return result;
        }

        [HttpPost("add-subcategory")]
        public ServiceResponse<bool> AddSubCategory(MasterSubCategory scat)
        {
            var result = _masterService.AddSubCategory(scat);
            return result;
        }

        [HttpPost("edit-subcategory")]
        public ServiceResponse<bool> EditSubCategory(MasterSubCategory scat)
        {
            var result = _masterService.EditSubCategory(scat);
            return result;
        }

        [HttpPost("disable-subcategory")]
        public ServiceResponse<bool> DisableSubCategory(MasterSubCategory scat)
        {
            var result = _masterService.DisableSubCategory(scat);
            return result;
        }

        [HttpGet("get-titles")]
        public ServiceResponse<IList<MasterNameTitle>> GetTitles()
        {
            var result = _masterService.GetTitles();
            return result;
        }

        [HttpPost("add-title")]
        public ServiceResponse<bool> AddTitle(MasterNameTitle title)
        {
            var result = _masterService.AddTitle(title);
            return result;
        }

        [HttpPost("edit-title")]
        public ServiceResponse<bool> EditTitle(MasterNameTitle title)
        {
            var result = _masterService.EditTitle(title);
            return result;
        }

        [HttpPost("disable-title")]
        public ServiceResponse<bool> DisableTitle(MasterNameTitle title)
        {
            var result = _masterService.DisableTitle(title);
            return result;
        }

        [HttpGet("get-parts")]
        public ServiceResponse<IList<MasterPart>> GetParts()
        {
            var result = _masterService.GetParts();
            return result;
        }

        [HttpPost("add-part")]
        public ServiceResponse<bool> AddPart(MasterPart part)
        {
            var result = _masterService.AddPart(part);
            return result;
        }

        [HttpPost("edit-part")]
        public ServiceResponse<bool> EditPart(MasterPart part)
        {
            var result = _masterService.EditPart(part);
            return result;
        }

        [HttpPost("disable-part")]
        public ServiceResponse<bool> DisablePart(MasterPart part)
        {
            var result = _masterService.DisablePart(part);
            return result;
        }

        [HttpGet("get-languages")]
        public ServiceResponse<IList<MasterLanguages>> GetLanguages()
        {
            var result = _masterService.GetLanguages();
            return result;
        }

        [HttpPost("add-language")]
        public ServiceResponse<bool> AddLanguage(MasterLanguages language)
        {
            var result = _masterService.AddLanguage(language);
            return result;
        }

        [HttpPost("edit-language")]
        public ServiceResponse<bool> EditLanguage(MasterLanguages language)
        {
            var result = _masterService.EditLanguage(language);
            return result;
        }

        [HttpPost("disable-language")]
        public ServiceResponse<bool> DisableLanguage(MasterLanguages language)
        {
            var result = _masterService.DisableLanguage(language);
            return result;
        }

        [HttpGet("get-religions")]
        public ServiceResponse<IList<MasterReligion>> GetReligions()
        {
            var result = _masterService.GetReligions();
            return result;
        }

        [HttpPost("add-religion")]
        public ServiceResponse<bool> AddReligion(MasterReligion religion)
        {
            var result = _masterService.AddReligion(religion);
            return result;
        }

        [HttpPost("edit-religion")]
        public ServiceResponse<bool> EditReligion(MasterReligion religion)
        {
            var result = _masterService.EditReligion(religion);
            return result;
        }

        [HttpPost("disable-religion")]
        public ServiceResponse<bool> DisableReligion(MasterReligion religion)
        {
            var result = _masterService.DisableReligion(religion);
            return result;
        }

        [HttpGet("get-ptypes")]
        public ServiceResponse<IList<MasterProductType>> GetProductTypes()
        {
            var result = _masterService.GetProductTypes();
            return result;
        }

        [HttpPost("add-ptype")]
        public ServiceResponse<bool> AddProductType(MasterProductType ptype)
        {
            var result = _masterService.AddProductType(ptype);
            return result;
        }

        [HttpPost("edit-ptype")]
        public ServiceResponse<bool> EditProductType(MasterProductType ptype)
        {
            var result = _masterService.EditProductType(ptype);
            return result;
        }

        [HttpPost("disable-ptype")]
        public ServiceResponse<bool> DisableProductType(MasterProductType ptype)
        {
            var result = _masterService.DisableProductType(ptype);
            return result;
        }

        [HttpGet("get-common")]
        public ServiceResponse<IList<MasterCommon>> GetCommonMasters()
        {
            var result = _masterService.GetCommonMasters();
            return result;
        }

        [HttpPost("add-common")]
        public ServiceResponse<bool> AddCommon(MasterCommon common)
        {
            var result = _masterService.AddCommon(common);
            return result;
        }

        [HttpPost("edit-common")]
        public ServiceResponse<bool> EditCommon(MasterCommon common)
        {
            var result = _masterService.EditCommon(common);
            return result;
        }

        [HttpPost("disable-common")]
        public ServiceResponse<bool> DisableCommon(MasterCommon common)
        {
            var result = _masterService.DisableCommon(common);
            return result;
        }

        [HttpGet("get-notifications")]
        public ServiceResponse<IList<T_NOTIFICATION_MST>> GetNotifications()
        {
            var result = _masterService.GetNotifications();
            return result;
        }

        [HttpPost("add-notification")]
        public ServiceResponse<bool> AddNotification(T_NOTIFICATION_MST notification)
        {
            var result = _masterService.AddNotification(notification);
            return result;
        }

        [HttpPost("edit-notification")]
        public ServiceResponse<bool> EditNotification(T_NOTIFICATION_MST notification)
        {
            var result = _masterService.EditNotification(notification);
            return result;
        }

        [HttpPost("disable-notification")]
        public ServiceResponse<bool> DisableNotification(T_NOTIFICATION_MST notification)
        {
            var result = _masterService.DisableNotification(notification);
            return result;
        }

        [HttpGet("get-splash-screens")]
        public ServiceResponse<IList<T_APP_SPLASH_MST>> GetSplashScreens()
        {
            var result = _masterService.GetSplashScreens();
            return result;
        }

        [HttpPost("add-splash-screen")]
        public ServiceResponse<bool> AddSplashScreen(T_APP_SPLASH_MST splash)
        {
            var result = _masterService.AddSplashScreen(splash);
            return result;
        }

        [HttpPost("edit-splash-screen")]
        public ServiceResponse<bool> EditSplashScreen(T_APP_SPLASH_MST splash)
        {
            var result = _masterService.EditSplashScreen(splash);
            return result;
        }

        [HttpPost("disable-splash-screen")]
        public ServiceResponse<bool> DisableSplashScreen(T_APP_SPLASH_MST splash)
        {
            var result = _masterService.DisableSplashScreen(splash);
            return result;
        }

        [HttpGet("get-solitaire-parameters")]
        public ServiceResponse<IList<T_DIA_COMMON_MST>> GetSolitaireParameters()
        {
            var result = _masterService.GetSolitaireParameters();
            return result;
        }

        [HttpPost("add-solitaire-parameter")]
        public ServiceResponse<bool> AddSolitairepParameter(T_DIA_COMMON_MST SOLITAIREPARAM)
        {
            var result = _masterService.AddSolitairepParameter(SOLITAIREPARAM);
            return result;
        }

        [HttpPost("edit-solitaire-parameter")]
        public ServiceResponse<bool> Editsolitaireparameter(T_DIA_COMMON_MST SOLITAIREPARAM)
        {
            var result = _masterService.Editsolitaireparameter(SOLITAIREPARAM);
            return result;
        }

        [HttpPost("disable-solitaire-parameter")]
        public ServiceResponse<bool> Disablesolitaireparameter(T_DIA_COMMON_MST solitaireparameter)
        {
            var result = _masterService.Disablesolitaireparameter(solitaireparameter);
            return result;
        }

        [HttpPost("add-items-mapping")]
        public ServiceResponse<bool> AddItemsMapping(ItemMapping ItemsMap)
        {
            var result = _masterService.AddItemsMapping(ItemsMap);
            return result;
        }

        [HttpPost("add-collection-user-mapping")]
        public ServiceResponse<bool> AddCollectionUserMapping(CollectionUserMapping CollectionUserMap)
        {
            var result = _masterService.AddCollectionUserMapping(CollectionUserMap);
            return result;
        }

        [HttpGet("get-items-mapping-users")]
        public ServiceResponse<IList<Users>> GetItemsMappingUsers(string ZoneID, string StateID, string TerritoryID, string CityID, string AreaID, string UsertypeID)
        {
            var result = _masterService.GetItemsMappingUsers( "0", "0", "0", "0", "0", "0");
            return result;
        }

        [HttpGet("get-outlet-category")]
        public ServiceResponse<IList<MasterOutletCategory>> GetOutLetCategory()
        {
            var result = _masterService.GetOutLetCategory();
            return result;
        }

        [HttpGet("get-all-territory")]
        public ServiceResponse<IList<MasterTerritory>> GetAllTerritory(Int32 MstId)
        {
            var result = _masterService.GetAllTerritory(MstId);
            return result;
        }

        [HttpGet("get-all-users")]
        public ServiceResponse<IList<User>> GetAllUsers()
        {
            var result = _masterService.GetAllUsers();
            return result;
        }

        [HttpGet("get-image-view")]
        public ServiceResponse<IList<ImageView>> GetAllImageView()
        {
            var result = _masterService.GetAllImageView();
            return result;
        }

        [HttpPost("add-territory")]
        public ServiceResponse<bool> AddTerritory(MasterTerritory territory)
        {
            var result = _masterService.AddTerritory(territory);
            return result;
        }

        [HttpPost("edit-territory")]
        public ServiceResponse<bool> EditTerritory(MasterTerritory territory)
        {
            var result = _masterService.EditTerritory(territory);
            return result;
        }

        [HttpPost("disable-territory")]
        public ServiceResponse<bool> DisableTerritory(MasterTerritory territory)
        {
            var result = _masterService.DisableTerritory(territory);
            return result;
        }

        [HttpGet("get-menu")]
        public ServiceResponse<IList<MenuMaster>> GetMenu()
        {
            var result = _masterService.GetMenu();
            return result;
        }

        [HttpGet("get-menu-master-old")]
        public ServiceResponse<IList<MenuMaster>> GetMenuMasterOld()
        {
            var result = _masterService.GetMenuMasterOld();
            return result;
        }

        [HttpGet("get-menu-master")]
        public ServiceResponse<IList<MenuMaster>> GetMenuMaster()
        {
            var result = _masterService.GetMenuMaster();
            return result;
        }

        //[HttpGet("get-menu-master-data")]
        //public ServiceResponse<IList<MenuMaster>> GetMenuMasterData(int dataid)
        //{
        //    var result = _masterService.GetMenuMasterData(dataid);
        //    return result;
        //}

        [HttpGet("get-menu-master-data")]
        public ServiceResponse<IList<MenuItem>> GetMenuMasterData(int dataid)
        {
            var result = _masterService.GetMenuMasterData(dataid);
            return result;
        }

        [HttpPost("add-user-menu-permission")]
        public ServiceResponse<bool> AddUserMenuPermission(UserMenuPermission UserMenuPermission)
        {
            var result = _masterService.AddUserMenuPermission(UserMenuPermission);
            return result;
        }

        [HttpGet("get-user-menu-permission")]
        public ServiceResponse<IList<UserMenuPermission>> GetUserMenuPermission(int dataid)
        {
            var result = _masterService.GetUserMenuPermission(0);
            return result;
        }

        [HttpGet("get-user-menu-permission-crud")]
        public ServiceResponse<IList<UserMenuPermissionCRUD>> GetUserMenuPermissionCRUD(int menupermissionid)
        {
            var result = _masterService.GetUserMenuPermissionCRUD(0);
            return result;
        }

        [HttpPost("add-user-menu-permission-crud")]
        public ServiceResponse<bool> AddUserMenuPermissionCRUD(UserMenuPermissionCRUD UserMenuPermissionCRUD)
        {
            var result = _masterService.AddUserMenuPermissionCRUD(UserMenuPermissionCRUD);
            return result;
        }

        [HttpGet("get-user-menu-permission-crud-by-dataid")]
        [System.Web.Http.HttpGet]
        public ServiceResponse<IList<UserMenuPermissionCRUD>> GetUserMenuPermissionCRUDByDataId(int dataid, int menumasterid)
        {
            var result = _masterService.GetUserMenuPermissionCRUDByDataId(0, 0);
            return result;
        }

        [HttpGet("get-team-users")]
        [System.Web.Http.HttpGet]
        public ServiceResponse<IList<MasterTeams>> GetTeamUser(int Flag)
        {
            var result = _masterService.GetTeamUser(Flag);
            return result;
        }
    }
}
