using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface ICommonService
    {
        public Task<ResponseDetails> GetKisnaItemList(KisnaItemListingParams kisnaitemlistparams);
        public Task<ResponseDetails> GetPlainGoldItemList(PlainGoldItemListingParams plaingolditemlistparams);
        public Task<ResponseDetails> GetGleamSoltaireItemList(GleamSolitaireItemListingParams gleamsolitaireitemlistparams);
        public Task<ResponseDetails> GetColorStoneItemList(ColorStoneItemListingParams colorstoneitemlistparams);
        public Task<ResponseDetails> GetPlatinumItemList(PlatinumItemListingParams platinumitemlistparams);
        public Task<ResponseDetails> GetCoupleBandItemList(CoupleBandItemListingParams couplebanditemlistparams);
        public Task<ResponseDetails> GetRareSolitareItemList(RareSolitareItemListingParams raresolitareitemlistparams, CommonHeader header);
        public Task<ResponseDetails> GetFamilyProductList(FamilyProductListingParams familyproductlistparams);
        public Task<ResponseDetails> IllumineItemList(IllumineItemListingParams illumineitemlistparams);
        public Task<ResponseDetails> AllCategoryList(AllCategoryListingParams allcategorylistparams);
        public Task<ResponseDetails> AllCategoryListVI(AllCategoryListingParams allcategorylistparams);
        public Task<ResponseDetails> GetNewKisnaPremiumItemList(NewKisnaPremiumItemListingParams newkisnapremiumitemlistparams);
        public Task<ResponseDetails> GetEsmeCollectionItemList(EsmeCollectionItemListingParams esmecollectionitemlistparams);
        public Task<ResponseDetails> GetAkshayaGoldItemList(AkshayaGoldItemListingParams akshayagolditemlistparams);
        public Task<ResponseDetails> GetNewDevelopmentItemList(NewDevelopmentItemListingParams newdevelopmentitemlistparams);
        public Task<ResponseDetails> GetTinnyWondersItemList(TinnyWondersItemListingParams tinnywondersitemlistparams);
        public Task<ResponseDetails> CommonItemFilterList(CommonItemFilterParams commonitemfilterparams);
        public Task<ResponseDetails> SoliterSubCategoryNewList(SoliterSubCategoryNewParams solitersubcategoryparams);
        public Task<ResponseDetails> CategoryButtonList(CategoryButtonListingParams categorybuttonlistparams, CommonHeader header);
        public Task<ResponseDetails> HomeButtonList(HomeButtonListingParams homebuttonlistparams);
        public Task<ResponseDetails> SolitaireCategoryList(SoliCategoryListingParams solicategorylistparams);
        public Task<ResponseDetails> ItemsSortByList(CategoryButtonListingParams categorybuttonlistparams, CommonHeader header);
        public Task<ResponseDetails> PlaingoldSortByList();
        public Task<ResponseDetails> ExtraGoldratewiseRate(ExtraGoldratewiseRateParams extraGoldratewiseRateParams);
        public Task<ResponseDetails> SearchAllItemsList(SearchAllItemsListParams searchallitemlistParams);
        public Task<ResponseDetails> PlainGoldItemFilter(PlainGoldItemFilterParams request);
        public Task<ItemDetails_Static> ViewItemDetails(PayloadsItemDetails Details);
        public Task<SoliterView_Static> ViewSoliterDetails(PayloadsItemDetails Details);
        public Task<ResponseDetails> AllCategoryListNew(AllCategoryListingParamsNew allcategorylistparams);
        public Task<ResponseDetails> GoldPriceRateWise(GoldPriceRateWise rate);
        public Task<ResponseDetails> PriceWiseList(AllCategoryListingParamsNew price);
        public Task<ResponseDetails> CategoryButtonListNew(CategoryButtonListRequest param, CommonHeader header);
        public Task<ResponseDetails> SplashScreenList(SplashScreenListRequest param, CommonHeader header);
        public Task<ResponseDetails> PriceWiseCatgoryList(PriceWiseCategoryListRequest param, CommonHeader header);
        public Task<ResponseDetails> HOStockCategories(HOStockCategoriesRequest param, CommonHeader header);
        public Task<ResponseDetails> SolitaireSortBy(CommonHeader header);
        public Task<ResponseDetails> HOStockSortBy(HOStockSortByRequest param, CommonHeader header);
        public Task<ResponseDetails> HOStockItemList(HOStockItemListRequest param, CommonHeader header);
        public Task<ResponseDetails> CustomCollectionSortBy(CustomCollectionSortByRequest param, CommonHeader header);
        public Task<ResponseDetails> PriceWiseItemSortBy(CommonHeader header);
        public Task<ResponseDetails> GetAllSubCategoryList(AllSubCategoryListRequest param, CommonHeader header);
        public Task<ResponseDetails> PlainGoldItemListFranSIS(PlainGoldItemListRequest param,CommonHeader header);
        public Task<ResponseDetails> ColorStoneItemListFranSIS(ColorStoneItemListRequest param, CommonHeader header);
        public Task<ResponseDetails> PlatinumItemListFranSIS(PlatinumItemListRequest param, CommonHeader header);
        public Task<ResponseDetails> CoupleBandItemListFranSIS(CoupleBandItemListRequest param, CommonHeader header);
        public Task<ResponseDetails> IllumineItemListFranSIS(IllumineItemListRequest param, CommonHeader header);
        public Task<ResponseDetails> GlemSolitaireItemListSIS(GlemSolitaireItemListRequest param, CommonHeader header);
        public Task<ResponseDetails> RareSolitaireItemListFran(RareSolitaireItemListRequest param, CommonHeader header);
        public Task<ResponseDetails> KisnaItemListFran(KisnaItemListRequest param, CommonHeader header);
        public Task<ResponseDetails> CommonItemFilterFranSIS(CommonItemFilterRequest param, CommonHeader header);
        public Task<ResponseDetails> BannerWishList(BannerWishListRequest param, CommonHeader header);
        public Task<ResponseDetails> ItemDetailShowFran(ItemDetailsRquest param, CommonHeader header);
        public Task<ResponseDetails> ChatCustomerCare(ChatCustomerCareRequest param);
        public Task<ResponseDetails> FranchiseWiseStock(FranchiseWiseStockRequest param);
        public Task<ResponseDetails> CappingSortBy(CappingSortByRequest param);
        public Task<ResponseDetails> CappingItemList(CappingItemListRequest param);
        public Task<ResponseDetails> ConsumerFormStore(ConsumerFormStoreRequest param);
        public Task<ResponseDetails> StockWiseItemData(StockWiseItemDataRequest param);
        public Task<ResponseDetails> PopularItems(PopularItemsRequest param);
        public Task<ResponseDetails> TopRecommandedItems(TopRecommandedItemsRequest param);
    }
}
