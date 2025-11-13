using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpPost("kisna-item-list")]
        public Task<ResponseDetails> GetKisnaItemList(KisnaItemListingParams kisnaitemlistparams)
        {
            var result = _commonService.GetKisnaItemList(kisnaitemlistparams);
            return result;
        }

        [HttpPost("plain-gold-item-list")]
        public Task<ResponseDetails> GetPlainGoldItemList(PlainGoldItemListingParams plaingolditemlistparams)
        {
            var result = _commonService.GetPlainGoldItemList(plaingolditemlistparams);
            return result;
        }

        [HttpPost("gleam-solitaire-item-list")]
        public Task<ResponseDetails> GetGleamSoltaireItemList(GleamSolitaireItemListingParams gleamsolitaireitemlistparams)
        {
            var result = _commonService.GetGleamSoltaireItemList(gleamsolitaireitemlistparams);
            return result;
        }

        [HttpPost("Color-stone-item-list")]
        public Task<ResponseDetails> GetColorStoneItemList(ColorStoneItemListingParams colorstoneitemlistparams)
        {
            var result = _commonService.GetColorStoneItemList(colorstoneitemlistparams);
            return result;
        }

        [HttpPost("platinum-item-list")]
        public Task<ResponseDetails> GetPlatinumItemList(PlatinumItemListingParams platinumitemlistparams)
        {
            var result = _commonService.GetPlatinumItemList(platinumitemlistparams);
            return result;
        }

        [HttpPost("couple-band-item-list")]
        public Task<ResponseDetails> GetCoupleBandItemList(CoupleBandItemListingParams couplebanditemlistparams)
        {
            var result = _commonService.GetCoupleBandItemList(couplebanditemlistparams);
            return result;
        }

        [HttpPost("rare-solitaire-item-list")]
        public Task<ResponseDetails> GetRareSolitareItemList([FromBody] RareSolitareItemListingParams raresolitareitemlistparams)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = _commonService.GetRareSolitareItemList(raresolitareitemlistparams, commonHeader);
            return result;
        }

        [HttpPost("family-product")]
        public Task<ResponseDetails> GetFamilyProductList(FamilyProductListingParams familyproductlistparams)
        {
            var result = _commonService.GetFamilyProductList(familyproductlistparams);
            return result;
        }

        [HttpPost("illumine-item-list")]
        public Task<ResponseDetails> IllumineItemList(IllumineItemListingParams illumineitemlistparams)
        {
            var result = _commonService.IllumineItemList(illumineitemlistparams);
            return result;
        }

        [HttpPost("all-category-list")]
        public Task<ResponseDetails> AllCategoryList(AllCategoryListingParams allcategorylistparams)
        {
            var result = _commonService.AllCategoryList(allcategorylistparams);
            return result;
        }

        [HttpPost("all-category-list-vi")]
        public Task<ResponseDetails> AllCategoryListVI(AllCategoryListingParams allcategorylistparams)
        {
            var result = _commonService.AllCategoryListVI(allcategorylistparams);
            return result;
        }

        [HttpPost("new-kisna-premium-item-list")]
        public Task<ResponseDetails> GetNewKisnaPremiumItemList(NewKisnaPremiumItemListingParams newkisnapremiumitemlistparams)
        {
            var result = _commonService.GetNewKisnaPremiumItemList(newkisnapremiumitemlistparams);
            return result;
        }

        [HttpPost("esme-collection-item-list")]
        public Task<ResponseDetails> GetEsmeCollectionItemList(EsmeCollectionItemListingParams esmecollectionitemlistparams)
        {
            var result = _commonService.GetEsmeCollectionItemList(esmecollectionitemlistparams);
            return result;
        }

        [HttpPost("akshaya-gold-item-list")]
        public Task<ResponseDetails> GetAkshayaGoldItemList(AkshayaGoldItemListingParams akshayagolditemlistparams)
        {
            var result = _commonService.GetAkshayaGoldItemList(akshayagolditemlistparams);
            return result;
        }

        [HttpPost("new-development-item-list")]
        public Task<ResponseDetails> GetNewDevelopmentItemList(NewDevelopmentItemListingParams newdevelopmentitemlistparams)
        {
            var result = _commonService.GetNewDevelopmentItemList(newdevelopmentitemlistparams);
            return result;
        }

        [HttpPost("tinny-wonders-item-list")]
        public Task<ResponseDetails> GetTinnyWondersItemList(TinnyWondersItemListingParams tinnywondersitemlistparams)
        {
            var result = _commonService.GetTinnyWondersItemList(tinnywondersitemlistparams);
            return result;
        }

        [HttpPost("common-item-filter")]
        public Task<ResponseDetails> CommonItemFilterList(CommonItemFilterParams commonitemfilterparams)
        {
            var result = _commonService.CommonItemFilterList(commonitemfilterparams);
            return result;
        }

        [HttpPost("solitaire-subcategory-new")]
        public Task<ResponseDetails> SoliterSubCategoryNewList(SoliterSubCategoryNewParams solitersubcategoryparams)
        {
            var result = _commonService.SoliterSubCategoryNewList(solitersubcategoryparams);
            return result;
        }

        [HttpPost("category-button-list")]
        public Task<ResponseDetails> CategoryButtonList([FromBody] CategoryButtonListingParams categorybuttonlistparams)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = _commonService.CategoryButtonList(categorybuttonlistparams, commonHeader);
            return result;
        }

        [HttpPost("home-button")]
        public Task<ResponseDetails> HomeButtonList(HomeButtonListingParams homebuttonlistparams)
        {
            var result = _commonService.HomeButtonList(homebuttonlistparams);
            return result;
        }

        [HttpPost("solitaire-category-list")]
        public Task<ResponseDetails> SolitaireCategoryList(SoliCategoryListingParams solicategorylistparams)
        {
            var result = _commonService.SolitaireCategoryList(solicategorylistparams);
            return result;
        }

        [HttpPost("items-sortby")]
        public Task<ResponseDetails> ItemsSortByList([FromBody] CategoryButtonListingParams categorybuttonlistparams)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = _commonService.ItemsSortByList(categorybuttonlistparams, commonHeader);
            return result;
        }

        [HttpPost("plain-gold-sortby-list")]
        public Task<ResponseDetails> PlaingoldSortByList()
        {
            var result = _commonService.PlaingoldSortByList();
            return result;
        }

        [HttpPost("extra-gold-ratewise-rate")]
        public Task<ResponseDetails> ExtraGoldratewiseRate(ExtraGoldratewiseRateParams extraGoldratewiseRateParams)
        {
            var result = _commonService.ExtraGoldratewiseRate(extraGoldratewiseRateParams);
            return result;
        }

        [HttpPost("search-all-items-list")]
        public Task<ResponseDetails> SearchAllItemsList(SearchAllItemsListParams searchallitemlistParams)
        {
            var result = _commonService.SearchAllItemsList(searchallitemlistParams);
            return result;
        }

        [HttpPost("plain-gold-item-filter")]
        public Task<ResponseDetails> PlainGoldItemFilter(PlainGoldItemFilterParams request)
        {
            var result = _commonService.PlainGoldItemFilter(request);
            return result;
        }

        [HttpPost("items-detail-show")]
        public Task<ItemDetails_Static> ViewItemDetails(PayloadsItemDetails Details)
        {
            var result = _commonService.ViewItemDetails(Details);
            return result;
        }

        [HttpPost("solitaire-detail")]
        public Task<SoliterView_Static> ViewSoliterDetails(PayloadsItemDetails Details)
        {
            var result = _commonService.ViewSoliterDetails(Details);
            return result;
        }

        [HttpPost("all-category-list-new")]
        public async Task<ResponseDetails> AllCategoryListNew(AllCategoryListingParamsNew allcategorylistparams)
        {
            var result = await _commonService.AllCategoryListNew(allcategorylistparams);
            return result;
        }

        [HttpPost("gold-price-rate-wise")]
        public async Task<ResponseDetails> GoldPriceRateWise(GoldPriceRateWise rate)
        {
            var result = await _commonService.GoldPriceRateWise(rate);
            return result;
        }

        [HttpPost("price-wise-list")]
        public async Task<ResponseDetails> PriceWiseList(AllCategoryListingParamsNew price)
        {
            var result = await _commonService.PriceWiseList(price);
            return result;
        }

        [HttpPost("category-button-list-new")]
        public async Task<ResponseDetails> CategoryButtonListNew([FromBody]CategoryButtonListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.CategoryButtonListNew(param, commonHeader);
            return result;
        }

        [HttpPost("splash-screen-list")]
        public async Task<ResponseDetails> SplashScreenList([FromBody]SplashScreenListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.SplashScreenList(param, commonHeader);
            return result;
        }

        [HttpPost("price-wise-category-list")]
         public async Task<ResponseDetails> PriceWiseCatgoryList([FromBody] PriceWiseCategoryListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.PriceWiseCatgoryList(param, commonHeader);
            return result;
        }

        [HttpPost("ho-stock-category-list")]
        public async Task<ResponseDetails> HOStockCategories([FromBody]HOStockCategoriesRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.HOStockCategories(param, commonHeader);
            return result;
        }

        [HttpPost("solitaire-sort-by")]
        public async Task<ResponseDetails> SolitaireSortBy()
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.SolitaireSortBy(commonHeader);
            return result;
        }

        [HttpPost("Ho-stock-sort-by")]
        public async Task<ResponseDetails> HOStockSortBy([FromBody]HOStockSortByRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.HOStockSortBy(param, commonHeader);
            return result;
        }

        [HttpPost("Ho-stock-item-list")]
        public async Task<ResponseDetails> HOStockItemList([FromBody]HOStockItemListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.HOStockItemList(param, commonHeader);
            return result;
        }

        [HttpPost("custom-collection-sort-by")]
        public async Task<ResponseDetails> CustomCollectionSortBy([FromBody]CustomCollectionSortByRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.CustomCollectionSortBy(param, commonHeader);
            return result;
        }

        [HttpPost("price-wise-item-sort-by")]
        public async Task<ResponseDetails> PriceWiseItemSortBy()
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.PriceWiseItemSortBy(commonHeader);
            return result;
        }

        [HttpPost("all-sub-category-list")]
        public async Task<ResponseDetails> GetAllSubCategoryList([FromBody] AllSubCategoryListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.GetAllSubCategoryList(param, commonHeader);
            return result;
        }

        [HttpPost("plain-gold-item-list-fransis")]
        public async Task<ResponseDetails> PlainGoldItemListFranSIS([FromBody] PlainGoldItemListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.PlainGoldItemListFranSIS(param, commonHeader);
            return result;
        }

        [HttpPost("color-stone-item-list-fransis")]
        public async Task<ResponseDetails> ColorStoneItemListFranSIS([FromBody] ColorStoneItemListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.ColorStoneItemListFranSIS(param, commonHeader);
            return result;
        }

        [HttpPost("platinum-item-list-fransis")]
        public async Task<ResponseDetails> PlatinumItemListFranSIS([FromBody] PlatinumItemListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.PlatinumItemListFranSIS(param, commonHeader);
            return result;
        }

        [HttpPost("couple-band-item-list-fransis")]
        public async Task<ResponseDetails> CoupleBandItemListFranSIS([FromBody] CoupleBandItemListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.CoupleBandItemListFranSIS(param, commonHeader);
            return result;
        }

        [HttpPost("illumine-item-list-fransis")]
        public async Task<ResponseDetails> IllumineItemListFranSIS([FromBody] IllumineItemListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.IllumineItemListFranSIS(param, commonHeader);
            return result;
        }

        [HttpPost("glem-solitaire-item-list-sis")]
        public async Task<ResponseDetails> GlemSolitaireItemListSIS([FromBody] GlemSolitaireItemListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.GlemSolitaireItemListSIS(param, commonHeader);
            return result;
        }

        [HttpPost("rare-solitaire-item-list-fran")]
        public async Task<ResponseDetails> RareSolitaireItemListFran(RareSolitaireItemListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.RareSolitaireItemListFran(param, commonHeader);
            return result;
        }

        [HttpPost("kisna-item-list-fran")]
        public async Task<ResponseDetails> KisnaItemListFran(KisnaItemListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.KisnaItemListFran(param, commonHeader);
            return result;
        }


        [HttpPost("common-item-filter-list-fransis")]
        public async Task<ResponseDetails> CommonItemFilterFranSIS([FromBody] CommonItemFilterRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.CommonItemFilterFranSIS(param, commonHeader);
            return result;
        }

        [HttpPost("banner-design")]
        public async Task<ResponseDetails> BannerWishList([FromBody] BannerWishListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.BannerWishList(param, commonHeader);
            return result;
        }

        [HttpPost("items-detail-show-fran")]
        public async Task<ResponseDetails> ItemDetailShowFran([FromBody] ItemDetailsRquest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _commonService.ItemDetailShowFran(param, commonHeader);
            return result;
        }

        [HttpPost("chat-custoemr-care")]
        public async Task<ResponseDetails> ChatCustomerCare(ChatCustomerCareRequest param)
        {
            var result = await _commonService.ChatCustomerCare(param);
            return result;
        }

        [HttpPost("franchise-wise-stock")]
        public async Task<ResponseDetails> FranchiseWiseStock(FranchiseWiseStockRequest param)
        {
            var result = await _commonService.FranchiseWiseStock(param);
            return result;
        }

        [HttpPost("capping-sort-by")]
        public async Task<ResponseDetails> CappingSortBy(CappingSortByRequest param)
        {
            var result = await _commonService.CappingSortBy(param);
            return result;
        }

        [HttpPost("capping-filter")]
        public async Task<ResponseDetails> CappingFilter(CappingFilterRequest request)
        {
            var result = await _commonService.CappingFilter(request);
            return result;
        }

        [HttpPost("capping-item-list")]
        public async Task<ResponseDetails> CappingItemList(CappingItemListRequest param)
        {
            var result = await _commonService.CappingItemList(param);
            return result;
        }

        [HttpPost("consumer-form-store")]
        public async Task<ResponseDetails> ConsumerFormStore(ConsumerFormStoreRequest param)
        {
            var result = await _commonService.ConsumerFormStore(param);
            return result;
        }

        [HttpPost("stock-wise-item-data")]
        public async Task<ResponseDetails> StockWiseItemData(StockWiseItemDataRequest param)
        {
            var result = await _commonService.StockWiseItemData(param);
            return result;
        }

        [HttpPost("popular-items")]
        public async Task<ResponseDetails> PopularItems(PopularItemsRequest param)
        {
            var result = await _commonService.PopularItems(param);
            return result;
        }

        [HttpPost("popular-items-filter")]
        public async Task<ResponseDetails> PopularItemsFilter(PopularItemsFilterRequest request)
        {
            var result = await _commonService.PopularItemsFilter(request);
            return result;
        }

        [HttpPost("piece-verify")]
        public async Task<ResponseDetails> PieceVerify(PieceVerifyRequest param)
        {
            var result = await _commonService.PieceVerify(param);
            return result;
        }

        [HttpPost("piece-verify-excel")]
        public async Task<ResponseDetails> PieceVerifyExcel(PieceVerifyExcelRequest param)
        {
            var result = await _commonService.PieceVerifyExcel(param);
            return result;
        }

        [HttpPost("top-recommanded-items")]
        public async Task<ResponseDetails> TopRecommandedItems(TopRecommandedItemsRequest param)
        {
            var result = await _commonService.TopRecommandedItems(param);
            return result;
        }
    }
}
