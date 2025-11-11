using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Exchange.WebServices.Data;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.Json;
using Xunit.Abstractions;


namespace NewAvatarWebApis.Infrastructure.Services
{
    public class CommonService : ICommonService
    {
        public string _connection = DBCommands.CONNECTION_STRING;
        public async Task<ResponseDetails> GetKisnaItemList(KisnaItemListingParams kisnaitemlistparams)
        {
            int current_page = kisnaitemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<KisnaItemListing> kisnaItemList = new List<KisnaItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.KISNA_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        string reqamount = kisnaitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        string reqmetalwt = kisnaitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        string reqdiawt = kisnaitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        string SortIds = string.IsNullOrWhiteSpace(kisnaitemlistparams.sort_id) ? "" : kisnaitemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(kisnaitemlistparams.variant) ? "Y" : kisnaitemlistparams.variant;
                        string mode = string.IsNullOrWhiteSpace(kisnaitemlistparams.mode) ? "off" : kisnaitemlistparams.mode;

                        int Page = kisnaitemlistparams.page > 0 ? kisnaitemlistparams.page - 1 : 0;
                        int Limit = kisnaitemlistparams.default_limit_app_page > 0 ? kisnaitemlistparams.default_limit_app_page : 20; // Default limit
                        int DataId = kisnaitemlistparams.data_id > 0 ? kisnaitemlistparams.data_id : 0;
                        int DataLoginTypeID = kisnaitemlistparams.data_login_type > 0 ? kisnaitemlistparams.data_login_type : 0;
                        string ItemName = string.IsNullOrWhiteSpace(kisnaitemlistparams.item_name) ? "" : kisnaitemlistparams.item_name;

                        int CategoryID = kisnaitemlistparams.category_id > 0 ? kisnaitemlistparams.category_id : 0;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(kisnaitemlistparams.sub_category_id) ? "" : kisnaitemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(kisnaitemlistparams.dsg_size) ? "" : kisnaitemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(kisnaitemlistparams.dsg_kt) ? "" : kisnaitemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(kisnaitemlistparams.dsg_color) ? "" : kisnaitemlistparams.dsg_color;
                        int ItemID = kisnaitemlistparams.Item_ID > 0 ? kisnaitemlistparams.Item_ID : 0;

                        string Stock_Av = string.IsNullOrWhiteSpace(kisnaitemlistparams.Stock_Av) ? "" : kisnaitemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(kisnaitemlistparams.Family_Av) ? "" : kisnaitemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(kisnaitemlistparams.Regular_Av) ? "" : kisnaitemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(kisnaitemlistparams.wearit) ? "" : kisnaitemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(kisnaitemlistparams.tryon) ? "" : kisnaitemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(kisnaitemlistparams.gender_id) ? "" : kisnaitemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(kisnaitemlistparams.item_tag) ? "" : kisnaitemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(kisnaitemlistparams.brand) ? "" : kisnaitemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(kisnaitemlistparams.delivery_days) ? "" : kisnaitemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(kisnaitemlistparams.ItemSubCtgIDs) ? "" : kisnaitemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(kisnaitemlistparams.ItemSubSubCtgIDs) ? "" : kisnaitemlistparams.ItemSubSubCtgIDs;

                        string SalesLocation = string.IsNullOrWhiteSpace(kisnaitemlistparams.sales_location) ? "" : kisnaitemlistparams.sales_location;
                        int DesignTimeLine = kisnaitemlistparams.design_timeline > 0 ? kisnaitemlistparams.design_timeline : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] as int? ?? 0;
                                    int category_id = dataReader["category_id"] as int? ?? 0;
                                    string item_description = dataReader["item_description"] as string ?? string.Empty;
                                    string item_code = dataReader["item_code"] as string ?? string.Empty;
                                    string item_name = dataReader["item_name"] as string ?? string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] as int? ?? 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] as string ?? string.Empty;
                                    string item_kt = dataReader["item_kt"] as string ?? string.Empty;
                                    string ItemIsSRP = dataReader["ItemIsSRP"] as string ?? string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] as int? ?? 0;
                                    string priceflag = dataReader["priceflag"] as string ?? string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] as decimal? ?? 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] as int? ?? 0;
                                    string image_path = dataReader["image_path"] as string ?? string.Empty;
                                    string item_color = dataReader["item_color"] as string ?? string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] as int? ?? 0;
                                    string item_soliter = dataReader["item_soliter"] as string ?? string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] as string ?? string.Empty;
                                    decimal item_price = dataReader["item_price"] as decimal? ?? 0;
                                    decimal dist_price = dataReader["dist_price"] as decimal? ?? 0;
                                    string item_text = dataReader["item_text"] as string ?? string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] as string ?? string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] as string ?? string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] as string ?? string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] as string ?? string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] as string ?? string.Empty;
                                    string wearit_text = dataReader["wearit_text"] as string ?? string.Empty;
                                    decimal cart_price = dataReader["cart_price"] as decimal? ?? 0;
                                    int variantCount = dataReader["variantCount"] as int? ?? 0;
                                    int item_color_id = dataReader["item_color_id"] as int? ?? 0;
                                    int cart_id = dataReader["cart_id"] as int? ?? 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] as int? ?? 0;
                                    int wish_count = dataReader["wish_count"] as int? ?? 0;
                                    int wearit_count = dataReader["wearit_count"] as int? ?? 0;

                                    last_page = dataReader["last_page"] as int? ?? 0;
                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] as string ?? string.Empty;

                                    // COLOR SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // IMAGE COLORS
                                    List<Item_images_colorListing> itemImageColorList = new List<Item_images_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    // ItemLivePriceCalculation
                                    IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();

                                    // DP_RP_Calculation
                                    IList<CartItemDPRPCALCListing> cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;

                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] as string ?? string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] as string ?? string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] as string ?? string.Empty;
                                        default_color_name = dataReader["default_color_name"] as string ?? string.Empty;
                                        default_color_code = dataReader["item_color_id"] as int? ?? 0;

                                        //LivePrice
                                        if (ItemIsSRP == "Y")
                                        {
                                            CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                            cartitempricedetaillistparams.DataID = DataId;
                                            cartitempricedetaillistparams.ItemID = item_id;
                                            cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                            cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                            if (cartItemPriceDetailList_gold.Count > 0)
                                            {
                                                dist_price = cartItemPriceDetailList_gold[0].dp_final_price;
                                            }

                                            CartItemDPRPCALCListingParams cartitemDPRPCALClistparams = new CartItemDPRPCALCListingParams();
                                            cartitemDPRPCALClistparams.DataID = DataId;
                                            cartitemDPRPCALClistparams.MRP = item_mrp;
                                            cartItemDPRPCALCList = objHelpers.Get_DP_RP_Calculation(cartitemDPRPCALClistparams);

                                            if (cartItemDPRPCALCList.Count > 0)
                                            {
                                                item_price = cartItemDPRPCALCList[0].R_Price;
                                            }
                                        }
                                    }

                                    item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock(item_code, MstType);
                                    }

                                    kisnaItemList.Add(new KisnaItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        item_kt = item_kt,
                                        ItemIsSRP = ItemIsSRP,
                                        sub_category_id = sub_category_id.ToString(),
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        plaingold_status = plaingold_status,
                                        item_price = item_price.ToString(),
                                        dist_price = dist_price.ToString(),
                                        item_text = item_text,
                                        item_brand_text = item_brand_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                    });
                                }
                            }
                        }
                    }
                }
                if (kisnaItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = kisnaItemList.ToString();
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<KisnaItemListing>(); // Empty list
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                // Handle specific SQL exceptions
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<KisnaItemListing>();  // Return empty list on error
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetPlainGoldItemList(PlainGoldItemListingParams plaingolditemlistparams)
        {
            int current_page = plaingolditemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<PlainGoldItemListing> plaingoldItemList = new List<PlainGoldItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PLAINGOLD_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, WestageMin, WestageMax, WeightMin, WeightMax;

                        // Price Range
                        string reqamount = plaingolditemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Westage Range
                        string reqdsg_westage = plaingolditemlistparams.dsg_westage as string ?? string.Empty;
                        ParseMinMax(reqdsg_westage, out WestageMin, out WestageMax);

                        // Weight Range
                        string reqdsg_weight = plaingolditemlistparams.dsg_weight as string ?? string.Empty;
                        ParseMinMax(reqdsg_weight, out WeightMin, out WeightMax);

                        int Page = plaingolditemlistparams.page > 0 ? plaingolditemlistparams.page - 1 : 0;
                        int Limit = plaingolditemlistparams.default_limit_app_page > 0 ? plaingolditemlistparams.default_limit_app_page : 20;

                        int DataId = plaingolditemlistparams.data_id > 0 ? plaingolditemlistparams.data_id : 0;
                        int DataLoginTypeID = plaingolditemlistparams.data_login_type > 0 ? plaingolditemlistparams.data_login_type : 0;
                        string mode = string.IsNullOrWhiteSpace(plaingolditemlistparams.mode) ? "off" : plaingolditemlistparams.mode;

                        int CategoryID = plaingolditemlistparams.category_id > 0 ? plaingolditemlistparams.category_id : 0;

                        // Default values if no sorting or mode
                        string SortIds = string.IsNullOrWhiteSpace(plaingolditemlistparams.sort_id) ? "" : plaingolditemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(plaingolditemlistparams.variant) ? "Y" : plaingolditemlistparams.variant;

                        string ItemName = string.IsNullOrWhiteSpace(plaingolditemlistparams.item_name) ? "" : plaingolditemlistparams.item_name;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(plaingolditemlistparams.sub_category_id) ? "" : plaingolditemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(plaingolditemlistparams.dsg_size) ? "" : plaingolditemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(plaingolditemlistparams.dsg_kt) ? "" : plaingolditemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(plaingolditemlistparams.dsg_color) ? "" : plaingolditemlistparams.dsg_color;

                        int ItemID = plaingolditemlistparams.Item_ID > 0 ? plaingolditemlistparams.Item_ID : 0;

                        string Stock_Av = string.IsNullOrWhiteSpace(plaingolditemlistparams.Stock_Av) ? "" : plaingolditemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(plaingolditemlistparams.Family_Av) ? "" : plaingolditemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(plaingolditemlistparams.Regular_Av) ? "" : plaingolditemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(plaingolditemlistparams.wearit) ? "" : plaingolditemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(plaingolditemlistparams.tryon) ? "" : plaingolditemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(plaingolditemlistparams.gender_id) ? "" : plaingolditemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(plaingolditemlistparams.item_tag) ? "" : plaingolditemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(plaingolditemlistparams.brand) ? "" : plaingolditemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(plaingolditemlistparams.delivery_days) ? "" : plaingolditemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(plaingolditemlistparams.ItemSubCtgIDs) ? "" : plaingolditemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(plaingolditemlistparams.ItemSubSubCtgIDs) ? "" : plaingolditemlistparams.ItemSubSubCtgIDs;

                        string SalesLocation = string.IsNullOrWhiteSpace(plaingolditemlistparams.sales_location) ? "" : plaingolditemlistparams.sales_location;
                        int DesignTimeLine = plaingolditemlistparams.design_timeline > 0 ? plaingolditemlistparams.design_timeline : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@WeightMin", WeightMin);
                        cmd.Parameters.AddWithValue("@WeightMax", WeightMax);
                        cmd.Parameters.AddWithValue("@WestageMin", WestageMin);
                        cmd.Parameters.AddWithValue("@WestageMax", WestageMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] as int? ?? 0;
                                    int category_id = dataReader["category_id"] as int? ?? 0;
                                    string item_description = dataReader["item_description"] as string ?? string.Empty;
                                    string item_code = dataReader["item_code"] as string ?? string.Empty;
                                    string item_name = dataReader["item_name"] as string ?? string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] as int? ?? 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] as string ?? string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] as string ?? string.Empty;
                                    string item_kt = dataReader["item_kt"] as string ?? string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] as int? ?? 0;
                                    string priceflag = dataReader["priceflag"] as string ?? string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] as decimal? ?? 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] as int? ?? 0;
                                    string image_path = dataReader["image_path"] as string ?? string.Empty;
                                    string item_color = dataReader["item_color"] as string ?? string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] as int? ?? 0;
                                    string item_soliter = dataReader["item_soliter"] as string ?? string.Empty;
                                    string item_text = dataReader["item_text"] as string ?? string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] as string ?? string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] as string ?? string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] as string ?? string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] as string ?? string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] as string ?? string.Empty;
                                    string wearit_text = dataReader["wearit_text"] as string ?? string.Empty;
                                    decimal cart_price = dataReader["cart_price"] as decimal? ?? 0;
                                    int variantCount = dataReader["variantCount"] as int? ?? 0;
                                    int item_color_id = dataReader["item_color_id"] as int? ?? 0;
                                    int cart_id = dataReader["cart_id"] as int? ?? 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] as int? ?? 0;
                                    int wish_count = dataReader["wish_count"] as int? ?? 0;
                                    int wearit_count = dataReader["wearit_count"] as int? ?? 0;

                                    last_page = dataReader["last_page"] as int? ?? 0;
                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] as string ?? string.Empty;

                                    // COLOR SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;
                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] as string ?? string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] as string ?? string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] as string ?? string.Empty;
                                        default_color_name = dataReader["default_color_name"] as string ?? string.Empty;
                                        default_color_code = dataReader["item_color_id"] as int? ?? 0;
                                    }

                                    item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock(item_code, MstType);
                                    }

                                    plaingoldItemList.Add(new PlainGoldItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        plaingold_status = plaingold_status,
                                        item_kt = item_kt,
                                        sub_category_id = sub_category_id.ToString(),
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        item_text = item_text,
                                        item_brand_text = item_brand_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                    });
                                }
                            }
                        }
                    }
                }
                if (plaingoldItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = plaingoldItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<PlainGoldItemListing>(); // Empty list
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<PlainGoldItemListing>();  // Return empty list on error
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetGleamSoltaireItemList(GleamSolitaireItemListingParams gleamsolitaireitemlistparams)
        {
            int current_page = gleamsolitaireitemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<GleamSolitaireItemListing> gleamsolitaireItemList = new List<GleamSolitaireItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GLEAMSOLITAIRE_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        string reqamount = gleamsolitaireitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        string reqmetalwt = gleamsolitaireitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        string reqdiawt = gleamsolitaireitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        string mode = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.mode) ? "off" : gleamsolitaireitemlistparams.mode;

                        int Page = gleamsolitaireitemlistparams.page > 0 ? gleamsolitaireitemlistparams.page - 1 : 0;
                        int Limit = gleamsolitaireitemlistparams.default_limit_app_page > 0 ? gleamsolitaireitemlistparams.default_limit_app_page : 20;
                        int DataId = gleamsolitaireitemlistparams.data_id > 0 ? gleamsolitaireitemlistparams.data_id : 0;
                        int DataLoginTypeID = gleamsolitaireitemlistparams.data_login_type > 0 ? gleamsolitaireitemlistparams.data_login_type : 0;
                        int CategoryID = gleamsolitaireitemlistparams.category_id > 0 ? gleamsolitaireitemlistparams.category_id : 0;

                        string SortIds = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.sort_id) ? "" : gleamsolitaireitemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.variant) ? "Y" : gleamsolitaireitemlistparams.variant;
                        string ItemName = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.item_name) ? "" : gleamsolitaireitemlistparams.item_name;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.sub_category_id) ? "" : gleamsolitaireitemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.dsg_size) ? "" : gleamsolitaireitemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.dsg_kt) ? "" : gleamsolitaireitemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.dsg_color) ? "" : gleamsolitaireitemlistparams.dsg_color;

                        int ItemID = gleamsolitaireitemlistparams.Item_ID > 0 ? gleamsolitaireitemlistparams.Item_ID : 0;

                        string Stock_Av = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.Stock_Av) ? "" : gleamsolitaireitemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.Family_Av) ? "" : gleamsolitaireitemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.Regular_Av) ? "" : gleamsolitaireitemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.wearit) ? "" : gleamsolitaireitemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.tryon) ? "" : gleamsolitaireitemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.gender_id) ? "" : gleamsolitaireitemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.item_tag) ? "" : gleamsolitaireitemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.brand) ? "" : gleamsolitaireitemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.delivery_days) ? "" : gleamsolitaireitemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.ItemSubCtgIDs) ? "" : gleamsolitaireitemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.ItemSubSubCtgIDs) ? "" : gleamsolitaireitemlistparams.ItemSubSubCtgIDs;

                        string SalesLocation = string.IsNullOrWhiteSpace(gleamsolitaireitemlistparams.sales_location) ? "" : gleamsolitaireitemlistparams.sales_location;
                        int DesignTimeLine = gleamsolitaireitemlistparams.design_timeline > 0 ? gleamsolitaireitemlistparams.design_timeline : 0;


                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] as int? ?? 0;
                                    int category_id = dataReader["category_id"] as int? ?? 0;
                                    string item_description = dataReader["item_description"] as string ?? string.Empty;
                                    string item_code = dataReader["item_code"] as string ?? string.Empty;
                                    string item_name = dataReader["item_name"] as string ?? string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] as int? ?? 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] as string ?? string.Empty;
                                    string item_kt = dataReader["item_kt"] as string ?? string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] as int? ?? 0;
                                    string ItemIsSRP = dataReader["ItemIsSRP"] as string ?? string.Empty;
                                    string priceflag = dataReader["priceflag"] as string ?? string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] as decimal? ?? 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] as int? ?? 0;
                                    string image_path = dataReader["image_path"] as string ?? string.Empty;

                                    //mode=on
                                    string item_color = dataReader["item_color"] as string ?? string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] as int? ?? 0;
                                    string item_soliter = dataReader["item_soliter"] as string ?? string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] as string ?? string.Empty;
                                    decimal item_price = dataReader["item_price"] as decimal? ?? 0;
                                    decimal dist_price = dataReader["dist_price"] as decimal? ?? 0;
                                    string item_text = dataReader["item_text"] as string ?? string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] as string ?? string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] as string ?? string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] as string ?? string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] as string ?? string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] as string ?? string.Empty;
                                    string wearit_text = dataReader["wearit_text"] as string ?? string.Empty;
                                    decimal cart_price = dataReader["cart_price"] as decimal? ?? 0;
                                    int variantCount = dataReader["variantCount"] as int? ?? 0;
                                    int item_color_id = dataReader["item_color_id"] as int? ?? 0;
                                    int cart_id = dataReader["cart_id"] as int? ?? 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] as int? ?? 0;
                                    int wish_count = dataReader["wish_count"] as int? ?? 0;
                                    int wearit_count = dataReader["wearit_count"] as int? ?? 0;

                                    last_page = dataReader["last_page"] as int? ?? 0;
                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] as string ?? string.Empty;

                                    // img_wearit_titleimg_wish_title SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // IMAGE COLORS
                                    List<Item_images_colorListing> itemImageColorList = new List<Item_images_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    // ItemLivePriceCalculation
                                    IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();

                                    // DP_RP_Calculation
                                    IList<CartItemDPRPCALCListing> cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;

                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] as string ?? string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] as string ?? string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] as string ?? string.Empty;
                                        default_color_name = dataReader["default_color_name"] as string ?? string.Empty;
                                        default_color_code = dataReader["item_color_id"] as int? ?? 0;

                                        //LivePrice
                                        if (ItemIsSRP == "Y")
                                        {
                                            CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                            cartitempricedetaillistparams.DataID = DataId;
                                            cartitempricedetaillistparams.ItemID = item_id;
                                            cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                            cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                            if (cartItemPriceDetailList_gold.Count > 0)
                                            {
                                                dist_price = cartItemPriceDetailList_gold[0].dp_final_price;
                                            }

                                            CartItemDPRPCALCListingParams cartitemDPRPCALClistparams = new CartItemDPRPCALCListingParams();
                                            cartitemDPRPCALClistparams.DataID = DataId;
                                            cartitemDPRPCALClistparams.MRP = item_mrp;
                                            cartItemDPRPCALCList = objHelpers.Get_DP_RP_Calculation(cartitemDPRPCALClistparams);

                                            if (cartItemDPRPCALCList.Count > 0)
                                            {
                                                item_price = cartItemDPRPCALCList[0].R_Price;
                                            }
                                        }
                                    }

                                    item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock(item_code, MstType);
                                    }

                                    gleamsolitaireItemList.Add(new GleamSolitaireItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        item_kt = item_kt,
                                        sub_category_id = sub_category_id.ToString(),
                                        ItemIsSRP = ItemIsSRP,
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        plaingold_status = plaingold_status,
                                        item_price = item_price.ToString(),
                                        dist_price = dist_price.ToString(),
                                        item_text = item_text,
                                        item_brand_text = item_brand_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                    });
                                }
                            }
                        }
                    }
                }
                if (gleamsolitaireItemList.Any())
                {
                    //return Ok(gleamsolitaireItemList);
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = gleamsolitaireItemList;
                }
                else
                {
                    //return NotFound();
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<GleamSolitaireItemListing>(); // Empty list
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                //return BadRequest(sqlEx.Message);
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<GleamSolitaireItemListing>();  // Return empty list on error
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetColorStoneItemList(ColorStoneItemListingParams colorstoneitemlistparams)
        {
            int current_page = colorstoneitemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<ColorStoneItemListing> colorstoneItemList = new List<ColorStoneItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COLORSTONE_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        string reqamount = colorstoneitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        string reqmetalwt = colorstoneitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        string reqdiawt = colorstoneitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        string mode = string.IsNullOrWhiteSpace(colorstoneitemlistparams.mode) ? "off" : colorstoneitemlistparams.mode;

                        int Page = colorstoneitemlistparams.page > 0 ? colorstoneitemlistparams.page - 1 : 0;
                        int Limit = colorstoneitemlistparams.default_limit_app_page > 0 ? colorstoneitemlistparams.default_limit_app_page : 20;
                        int DataId = colorstoneitemlistparams.data_id > 0 ? colorstoneitemlistparams.data_id : 0;
                        int DataLoginTypeID = colorstoneitemlistparams.data_login_type > 0 ? colorstoneitemlistparams.data_login_type : 0;
                        int CategoryID = colorstoneitemlistparams.category_id > 0 ? colorstoneitemlistparams.category_id : 0;

                        string SortIds = string.IsNullOrWhiteSpace(colorstoneitemlistparams.sort_id) ? "" : colorstoneitemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(colorstoneitemlistparams.variant) ? "Y" : colorstoneitemlistparams.variant;
                        string ItemName = string.IsNullOrWhiteSpace(colorstoneitemlistparams.item_name) ? "" : colorstoneitemlistparams.item_name;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(colorstoneitemlistparams.sub_category_id) ? "" : colorstoneitemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(colorstoneitemlistparams.dsg_size) ? "" : colorstoneitemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(colorstoneitemlistparams.dsg_kt) ? "" : colorstoneitemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(colorstoneitemlistparams.dsg_color) ? "" : colorstoneitemlistparams.dsg_color;

                        int ItemID = colorstoneitemlistparams.Item_ID > 0 ? colorstoneitemlistparams.Item_ID : 0;

                        string Stock_Av = string.IsNullOrWhiteSpace(colorstoneitemlistparams.Stock_Av) ? "" : colorstoneitemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(colorstoneitemlistparams.Family_Av) ? "" : colorstoneitemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(colorstoneitemlistparams.Regular_Av) ? "" : colorstoneitemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(colorstoneitemlistparams.wearit) ? "" : colorstoneitemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(colorstoneitemlistparams.tryon) ? "" : colorstoneitemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(colorstoneitemlistparams.gender_id) ? "" : colorstoneitemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(colorstoneitemlistparams.item_tag) ? "" : colorstoneitemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(colorstoneitemlistparams.brand) ? "" : colorstoneitemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(colorstoneitemlistparams.delivery_days) ? "" : colorstoneitemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(colorstoneitemlistparams.ItemSubCtgIDs) ? "" : colorstoneitemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(colorstoneitemlistparams.ItemSubSubCtgIDs) ? "" : colorstoneitemlistparams.ItemSubSubCtgIDs;

                        string SalesLocation = string.IsNullOrWhiteSpace(colorstoneitemlistparams.sales_location) ? "" : colorstoneitemlistparams.sales_location;
                        int DesignTimeLine = colorstoneitemlistparams.design_timeline > 0 ? colorstoneitemlistparams.design_timeline : 0;


                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] as int? ?? 0;
                                    int category_id = dataReader["category_id"] as int? ?? 0;
                                    string item_description = dataReader["item_description"] as string ?? string.Empty;
                                    string item_code = dataReader["item_code"] as string ?? string.Empty;
                                    string item_name = dataReader["item_name"] as string ?? string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] as int? ?? 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] as string ?? string.Empty;
                                    string item_kt = dataReader["item_kt"] as string ?? string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] as int? ?? 0;
                                    string priceflag = dataReader["priceflag"] as string ?? string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] as decimal? ?? 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] as int? ?? 0;
                                    string image_path = dataReader["image_path"] as string ?? string.Empty;

                                    //mode=on
                                    string item_color = dataReader["item_color"] as string ?? string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] as int? ?? 0;
                                    string item_soliter = dataReader["item_soliter"] as string ?? string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] as string ?? string.Empty;
                                    string item_text = dataReader["item_text"] as string ?? string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] as string ?? string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] as string ?? string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] as string ?? string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] as string ?? string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] as string ?? string.Empty;
                                    string wearit_text = dataReader["wearit_text"] as string ?? string.Empty;
                                    decimal cart_price = dataReader["cart_price"] as decimal? ?? 0;
                                    int variantCount = dataReader["variantCount"] as int? ?? 0;
                                    int item_color_id = dataReader["item_color_id"] as int? ?? 0;
                                    int cart_id = dataReader["cart_id"] as int? ?? 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] as int? ?? 0;
                                    int wish_count = dataReader["wish_count"] as int? ?? 0;
                                    int wearit_count = dataReader["wearit_count"] as int? ?? 0;

                                    last_page = dataReader["last_page"] as int? ?? 0;
                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] as string ?? string.Empty;

                                    // SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // IMAGE COLORS
                                    List<Item_images_colorListing> itemImageColorList = new List<Item_images_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;

                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] as string ?? string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] as string ?? string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] as string ?? string.Empty;
                                        default_color_name = dataReader["default_color_name"] as string ?? string.Empty;
                                        default_color_code = dataReader["item_color_id"] as int? ?? 0;
                                    }

                                    item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock(item_code, MstType);
                                    }

                                    colorstoneItemList.Add(new ColorStoneItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        item_kt = item_kt,
                                        sub_category_id = sub_category_id.ToString(),
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        plaingold_status = plaingold_status,
                                        item_text = item_text,
                                        item_brand_text = item_brand_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                    });
                                }
                            }
                        }
                    }
                }
                if (colorstoneItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = colorstoneItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<ColorStoneItemListing>(); // Empty list
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<ColorStoneItemListing>();  // Return empty list on error
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetPlatinumItemList(PlatinumItemListingParams platinumitemlistparams)
        {
            int current_page = platinumitemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<PlatinumItemListing> platinumItemList = new List<PlatinumItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PLATINUM_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        string reqamount = platinumitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        string reqmetalwt = platinumitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        string reqdiawt = platinumitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        string mode = string.IsNullOrWhiteSpace(platinumitemlistparams.mode) ? "off" : platinumitemlistparams.mode;

                        int Page = platinumitemlistparams.page > 0 ? platinumitemlistparams.page - 1 : 0;
                        int Limit = platinumitemlistparams.default_limit_app_page > 0 ? platinumitemlistparams.default_limit_app_page : 20;
                        int DataId = platinumitemlistparams.data_id > 0 ? platinumitemlistparams.data_id : 0;
                        int DataLoginTypeID = platinumitemlistparams.data_login_type > 0 ? platinumitemlistparams.data_login_type : 0;
                        int CategoryID = platinumitemlistparams.category_id > 0 ? platinumitemlistparams.category_id : 0;

                        string SortIds = string.IsNullOrWhiteSpace(platinumitemlistparams.sort_id) ? "" : platinumitemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(platinumitemlistparams.variant) ? "Y" : platinumitemlistparams.variant;
                        string ItemName = string.IsNullOrWhiteSpace(platinumitemlistparams.item_name) ? "" : platinumitemlistparams.item_name;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(platinumitemlistparams.sub_category_id) ? "" : platinumitemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(platinumitemlistparams.dsg_size) ? "" : platinumitemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(platinumitemlistparams.dsg_kt) ? "" : platinumitemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(platinumitemlistparams.dsg_color) ? "" : platinumitemlistparams.dsg_color;

                        int ItemID = platinumitemlistparams.Item_ID > 0 ? platinumitemlistparams.Item_ID : 0;

                        string Stock_Av = string.IsNullOrWhiteSpace(platinumitemlistparams.Stock_Av) ? "" : platinumitemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(platinumitemlistparams.Family_Av) ? "" : platinumitemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(platinumitemlistparams.Regular_Av) ? "" : platinumitemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(platinumitemlistparams.wearit) ? "" : platinumitemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(platinumitemlistparams.tryon) ? "" : platinumitemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(platinumitemlistparams.gender_id) ? "" : platinumitemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(platinumitemlistparams.item_tag) ? "" : platinumitemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(platinumitemlistparams.brand) ? "" : platinumitemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(platinumitemlistparams.delivery_days) ? "" : platinumitemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(platinumitemlistparams.ItemSubCtgIDs) ? "" : platinumitemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(platinumitemlistparams.ItemSubSubCtgIDs) ? "" : platinumitemlistparams.ItemSubSubCtgIDs;

                        string SalesLocation = string.IsNullOrWhiteSpace(platinumitemlistparams.sales_location) ? "" : platinumitemlistparams.sales_location;
                        int DesignTimeLine = platinumitemlistparams.design_timeline > 0 ? platinumitemlistparams.design_timeline : 0;


                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] as int? ?? 0;
                                    int category_id = dataReader["category_id"] as int? ?? 0;
                                    string item_description = dataReader["item_description"] as string ?? string.Empty;
                                    string item_code = dataReader["item_code"] as string ?? string.Empty;
                                    string item_name = dataReader["item_name"] as string ?? string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] as int? ?? 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] as string ?? string.Empty;
                                    string item_kt = dataReader["item_kt"] as string ?? string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] as int? ?? 0;
                                    string priceflag = dataReader["priceflag"] as string ?? string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] as decimal? ?? 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] as int? ?? 0;
                                    string image_path = dataReader["image_path"] as string ?? string.Empty;

                                    //mode=on
                                    string item_color = dataReader["item_color"] as string ?? string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] as int? ?? 0;
                                    string item_soliter = dataReader["item_soliter"] as string ?? string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] as string ?? string.Empty;
                                    string item_text = dataReader["item_text"] as string ?? string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] as string ?? string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] as string ?? string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] as string ?? string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] as string ?? string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] as string ?? string.Empty;
                                    string wearit_text = dataReader["wearit_text"] as string ?? string.Empty;
                                    decimal cart_price = dataReader["cart_price"] as decimal? ?? 0;
                                    int variantCount = dataReader["variantCount"] as int? ?? 0;
                                    int item_color_id = dataReader["item_color_id"] as int? ?? 0;
                                    int cart_id = dataReader["cart_id"] as int? ?? 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] as int? ?? 0;
                                    int wish_count = dataReader["wish_count"] as int? ?? 0;
                                    int wearit_count = dataReader["wearit_count"] as int? ?? 0;

                                    last_page = dataReader["last_page"] as int? ?? 0;
                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] as string ?? string.Empty;

                                    // img_wearit_titleimg_wish_title SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // IMAGE COLORS
                                    List<Item_images_colorListing> itemImageColorList = new List<Item_images_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;

                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] as string ?? string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] as string ?? string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] as string ?? string.Empty;
                                        default_color_name = dataReader["default_color_name"] as string ?? string.Empty;
                                        default_color_code = dataReader["item_color_id"] as int? ?? 0;
                                    }

                                    item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock(item_code, MstType);
                                    }

                                    platinumItemList.Add(new PlatinumItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        item_kt = item_kt,
                                        sub_category_id = sub_category_id.ToString(),
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        plaingold_status = plaingold_status,
                                        item_text = item_text,
                                        item_brand_text = item_brand_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                    });
                                }
                            }
                        }
                    }
                }
                if (platinumItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = platinumItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<PlatinumItemListing>(); // Empty list
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<PlatinumItemListing>();  // Return empty list on error
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetCoupleBandItemList(CoupleBandItemListingParams couplebanditemlistparams)
        {
            int current_page = couplebanditemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<CoupleBandItemListing> couplebandItemList = new List<CoupleBandItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COUPLEBAND_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        string reqamount = couplebanditemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        string reqmetalwt = couplebanditemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        string reqdiawt = couplebanditemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        string mode = string.IsNullOrWhiteSpace(couplebanditemlistparams.mode) ? "off" : couplebanditemlistparams.mode;

                        int Page = couplebanditemlistparams.page > 0 ? couplebanditemlistparams.page - 1 : 0;
                        int Limit = couplebanditemlistparams.default_limit_app_page > 0 ? couplebanditemlistparams.default_limit_app_page : 20;
                        int DataId = couplebanditemlistparams.data_id > 0 ? couplebanditemlistparams.data_id : 0;
                        int DataLoginTypeID = couplebanditemlistparams.data_login_type > 0 ? couplebanditemlistparams.data_login_type : 0;
                        int CategoryID = couplebanditemlistparams.category_id > 0 ? couplebanditemlistparams.category_id : 0;

                        string SortIds = string.IsNullOrWhiteSpace(couplebanditemlistparams.sort_id) ? "" : couplebanditemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(couplebanditemlistparams.variant) ? "Y" : couplebanditemlistparams.variant;
                        string ItemName = string.IsNullOrWhiteSpace(couplebanditemlistparams.item_name) ? "" : couplebanditemlistparams.item_name;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(couplebanditemlistparams.sub_category_id) ? "" : couplebanditemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(couplebanditemlistparams.dsg_size) ? "" : couplebanditemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(couplebanditemlistparams.dsg_kt) ? "" : couplebanditemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(couplebanditemlistparams.dsg_color) ? "" : couplebanditemlistparams.dsg_color;

                        int ItemID = couplebanditemlistparams.Item_ID > 0 ? couplebanditemlistparams.Item_ID : 0;

                        string Stock_Av = string.IsNullOrWhiteSpace(couplebanditemlistparams.Stock_Av) ? "" : couplebanditemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(couplebanditemlistparams.Family_Av) ? "" : couplebanditemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(couplebanditemlistparams.Regular_Av) ? "" : couplebanditemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(couplebanditemlistparams.wearit) ? "" : couplebanditemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(couplebanditemlistparams.tryon) ? "" : couplebanditemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(couplebanditemlistparams.gender_id) ? "" : couplebanditemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(couplebanditemlistparams.item_tag) ? "" : couplebanditemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(couplebanditemlistparams.brand) ? "" : couplebanditemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(couplebanditemlistparams.delivery_days) ? "" : couplebanditemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(couplebanditemlistparams.ItemSubCtgIDs) ? "" : couplebanditemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(couplebanditemlistparams.ItemSubSubCtgIDs) ? "" : couplebanditemlistparams.ItemSubSubCtgIDs;

                        string SalesLocation = string.IsNullOrWhiteSpace(couplebanditemlistparams.sales_location) ? "" : couplebanditemlistparams.sales_location;
                        int DesignTimeLine = couplebanditemlistparams.design_timeline > 0 ? couplebanditemlistparams.design_timeline : 0;


                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] as int? ?? 0;
                                    int category_id = dataReader["category_id"] as int? ?? 0;
                                    string item_description = dataReader["item_description"] as string ?? string.Empty;
                                    string item_code = dataReader["item_code"] as string ?? string.Empty;
                                    string item_name = dataReader["item_name"] as string ?? string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] as int? ?? 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] as string ?? string.Empty;
                                    string item_kt = dataReader["item_kt"] as string ?? string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] as int? ?? 0;
                                    string priceflag = dataReader["priceflag"] as string ?? string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] as decimal? ?? 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] as int? ?? 0;
                                    string image_path = dataReader["image_path"] as string ?? string.Empty;

                                    //mode=on
                                    string item_color = dataReader["item_color"] as string ?? string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] as int? ?? 0;
                                    string item_soliter = dataReader["item_soliter"] as string ?? string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] as string ?? string.Empty;
                                    string item_text = dataReader["item_text"] as string ?? string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] as string ?? string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] as string ?? string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] as string ?? string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] as string ?? string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] as string ?? string.Empty;
                                    string wearit_text = dataReader["wearit_text"] as string ?? string.Empty;
                                    decimal cart_price = dataReader["cart_price"] as decimal? ?? 0;
                                    int variantCount = dataReader["variantCount"] as int? ?? 0;
                                    int item_color_id = dataReader["item_color_id"] as int? ?? 0;
                                    int cart_id = dataReader["cart_id"] as int? ?? 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] as int? ?? 0;
                                    int wish_count = dataReader["wish_count"] as int? ?? 0;
                                    int wearit_count = dataReader["wearit_count"] as int? ?? 0;

                                    last_page = dataReader["last_page"] as int? ?? 0;
                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] as string ?? string.Empty;

                                    // img_wearit_titleimg_wish_title SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // IMAGE COLORS
                                    List<Item_images_colorListing> itemImageColorList = new List<Item_images_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;

                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] as string ?? string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] as string ?? string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] as string ?? string.Empty;
                                        default_color_name = dataReader["default_color_name"] as string ?? string.Empty;
                                        default_color_code = dataReader["item_color_id"] as int? ?? 0;
                                    }

                                    item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock(item_code, MstType);
                                    }

                                    couplebandItemList.Add(new CoupleBandItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        item_kt = item_kt,
                                        sub_category_id = sub_category_id.ToString(),
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        plaingold_status = plaingold_status,
                                        item_text = item_text,
                                        item_brand_text = item_brand_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                    });
                                }
                            }
                        }
                    }
                }
                if (couplebandItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = couplebandItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<CoupleBandItemListing>(); // Empty list
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<CoupleBandItemListing>();  // Return empty list on error
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetRareSolitareItemList(RareSolitareItemListingParams raresolitareitemlistparams, CommonHeader header)
        {
            string current_page = raresolitareitemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<RareSolitareItemListing> rareSolitareItemList = new List<RareSolitareItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.RARESOLITAIRE_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        string reqamount = raresolitareitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        string reqmetalwt = raresolitareitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        string reqdiawt = raresolitareitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        string mode = string.IsNullOrWhiteSpace(raresolitareitemlistparams.mode) ? "off" : raresolitareitemlistparams.mode;

                        // Default values if no sorting or mode
                        string DataLoginType = string.IsNullOrWhiteSpace(raresolitareitemlistparams.data_login_type) ? "" : raresolitareitemlistparams.data_login_type;
                        string MasterCommonId = string.IsNullOrWhiteSpace(raresolitareitemlistparams.master_common_id) ? "" : raresolitareitemlistparams.master_common_id;
                        string SortIds = string.IsNullOrWhiteSpace(raresolitareitemlistparams.sort_id) ? "" : raresolitareitemlistparams.sort_id;
                        string SubCategoryIDs = string.IsNullOrWhiteSpace(raresolitareitemlistparams.sub_category_id) ? "" : raresolitareitemlistparams.sub_category_id;
                        string DsgColors = string.IsNullOrWhiteSpace(raresolitareitemlistparams.dsg_color) ? "" : raresolitareitemlistparams.dsg_color;
                        string Genders = string.IsNullOrWhiteSpace(raresolitareitemlistparams.gender_id) ? "" : raresolitareitemlistparams.gender_id;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(raresolitareitemlistparams.brand) ? "" : raresolitareitemlistparams.brand;

                        string button_cd = string.IsNullOrWhiteSpace(raresolitareitemlistparams.button_cd) ? "" : raresolitareitemlistparams.button_cd;

                        string Page = string.IsNullOrWhiteSpace(raresolitareitemlistparams.page) ? "" : raresolitareitemlistparams.page;
                        string Limit = string.IsNullOrWhiteSpace(raresolitareitemlistparams.default_limit_app_page) ? "" : raresolitareitemlistparams.default_limit_app_page; // Default limit
                        string DataId = string.IsNullOrWhiteSpace(raresolitareitemlistparams.data_id) ? "" : raresolitareitemlistparams.data_id;
                        string CategoryID = string.IsNullOrWhiteSpace(raresolitareitemlistparams.category_id) ? "" : raresolitareitemlistparams.category_id;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginType", DataLoginType);
                        cmd.Parameters.AddWithValue("@MasterCommonId", MasterCommonId);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@button_cd", button_cd);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] as int? ?? 0;
                                    decimal item_mrp = dataReader["item_mrp"] as decimal? ?? 0;
                                    string item_name = dataReader["item_name"] as string ?? string.Empty;
                                    string item_description = dataReader["item_description"] as string ?? string.Empty;
                                    int category_id = dataReader["category_id"] as int? ?? 0;
                                    string item_kt = dataReader["item_kt"] as string ?? string.Empty;
                                    string IsNewCollection = dataReader["IsNewCollection"] as string ?? string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] as string ?? string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] as string ?? string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] as string ?? string.Empty;
                                    int wish_count = dataReader["wish_count"] as int? ?? 0;
                                    string image_path = dataReader["image_path"] as string ?? string.Empty;
                                    int mostOrder = dataReader["mostOrder"] as int? ?? 0;

                                    last_page = dataReader["last_page"] as int? ?? 0;
                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    rareSolitareItemList.Add(new RareSolitareItemListing
                                    {
                                        itemId = item_id.ToString(),
                                        itemMrp = item_mrp.ToString(),
                                        itemName = item_name,
                                        itemDescription = item_description,
                                        categoryId = category_id.ToString(),
                                        itemKt = item_kt,
                                        isNewCollection = IsNewCollection,
                                        rupySymbol = rupy_symbol,
                                        imgWatchTitle = img_watch_title,
                                        imgWishTitle = img_wish_title,
                                        wishCount = wish_count.ToString(),
                                        imagePath = image_path,
                                        mostOrder = mostOrder.ToString(),
                                        productTags = itemTagList,
                                    });
                                }
                            }
                        }
                    }
                }
                if (rareSolitareItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = rareSolitareItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<RareSolitareItemListing>(); // Empty list
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                // Handle specific SQL exceptions
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<RareSolitareItemListing>();  // Return empty list on error
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetFamilyProductList(FamilyProductListingParams familyproductlistparams)
        {
            var responseDetails = new ResponseDetails();
            int total_items = 0;

            IList<FamilyProductListing> familyProductList = new List<FamilyProductListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FAMILYPRODUCT;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", familyproductlistparams.data_id);
                        cmd.Parameters.AddWithValue("@ItemId", familyproductlistparams.item_id);
                        cmd.Parameters.AddWithValue("@CategoryId", familyproductlistparams.category_id);
                        cmd.Parameters.AddWithValue("@MasterCommonId", familyproductlistparams.master_common_id);

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int CategoryId = familyproductlistparams.category_id as int? ?? 0;
                                    int master_common_id = familyproductlistparams.master_common_id as int? ?? 112;

                                    int item_id = dataReader["item_id"] as int? ?? 0;
                                    string item_gold = dataReader["item_gold"] as string ?? string.Empty;
                                    decimal ItemDisLabourPer = dataReader["ItemDisLabourPer"] as decimal? ?? 0;
                                    decimal labour_per = dataReader["labour_per"] as decimal? ?? 0;
                                    int product_itemid = dataReader["product_itemid"] as int? ?? 0;
                                    string ItemAproxDay = dataReader["ItemAproxDay"] as string ?? string.Empty;
                                    string item_code = dataReader["item_code"] as string ?? string.Empty;
                                    int category_id = dataReader["category_id"] as int? ?? 0;
                                    string item_name = dataReader["item_name"] as string ?? string.Empty;
                                    string item_sku = dataReader["item_sku"] as string ?? string.Empty;
                                    string item_description = dataReader["item_description"] as string ?? string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] as decimal? ?? 0;
                                    string ItemFranchiseSts = dataReader["ItemFranchiseSts"] as string ?? string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] as int? ?? 0;
                                    decimal item_price = dataReader["item_price"] as decimal? ?? 0;
                                    decimal dist_price = dataReader["dist_price"] as decimal? ?? 0;
                                    string image_path = dataReader["image_path"] as string ?? string.Empty;
                                    string dsg_sfx = dataReader["dsg_sfx"] as string ?? string.Empty;
                                    string dsg_size = dataReader["dsg_size"] as string ?? string.Empty;
                                    string dsg_kt = dataReader["dsg_kt"] as string ?? string.Empty;
                                    string dsg_color = dataReader["dsg_color"] as string ?? string.Empty;
                                    string item_soliter = dataReader["item_soliter"] as string ?? string.Empty;
                                    int star = dataReader["star"] as int? ?? 0;
                                    string cart_img = dataReader["cart_img"] as string ?? string.Empty;
                                    string img_cart_title = dataReader["img_cart_title"] as string ?? string.Empty;
                                    string watch_img = dataReader["watch_img"] as string ?? string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] as string ?? string.Empty;
                                    int wearit_count = dataReader["wearit_count"] as int? ?? 0;
                                    string wearit_status = dataReader["wearit_status"] as string ?? string.Empty;
                                    string wearit_img = dataReader["wearit_img"] as string ?? string.Empty;
                                    string wearit_none_img = dataReader["wearit_none_img"] as string ?? string.Empty;
                                    string wearit_color = dataReader["wearit_color"] as string ?? string.Empty;
                                    string wearit_text = dataReader["wearit_text"] as string ?? string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] as string ?? string.Empty;
                                    int wish_count = dataReader["wish_count"] as int? ?? 0;
                                    string wish_default_img = dataReader["wish_default_img"] as string ?? string.Empty;
                                    string wish_fill_img = dataReader["wish_fill_img"] as string ?? string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] as string ?? string.Empty;
                                    string item_review = dataReader["item_review"] as string ?? string.Empty;
                                    string item_size = dataReader["item_size"] as string ?? string.Empty;
                                    string item_kt = dataReader["item_kt"] as string ?? string.Empty;
                                    string item_color = dataReader["item_color"] as string ?? string.Empty;
                                    string item_metal = dataReader["item_metal"] as string ?? string.Empty;
                                    decimal item_wt = dataReader["item_wt"] as decimal? ?? 0;
                                    string item_stone = dataReader["item_stone"] as string ?? string.Empty;
                                    double item_stone_wt = dataReader["item_stone_wt"] as double? ?? 0;
                                    int item_stone_qty = dataReader["item_stone_qty"] as int? ?? 0;
                                    string star_color = dataReader["star_color"] as string ?? string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] as int? ?? 0;
                                    string price_text = dataReader["price_text"] as string ?? string.Empty;
                                    decimal cart_price = dataReader["cart_price"] as decimal? ?? 0;
                                    int item_color_id = dataReader["item_color_id"] as int? ?? 0;
                                    string item_details = dataReader["item_details"] as string ?? string.Empty;
                                    string item_diamond_details = dataReader["item_diamond_details"] as string ?? string.Empty;
                                    string item_text = dataReader["item_text"] as string ?? string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] as string ?? string.Empty;
                                    string more_item_details = dataReader["more_item_details"] as string ?? string.Empty;
                                    string item_stock = dataReader["item_stock"] as string ?? string.Empty;
                                    int cart_item_qty = dataReader["cart_item_qty"] as int? ?? 0;
                                    string rupy_symbol = dataReader["rupy_symbol"] as string ?? string.Empty;
                                    int variantCount = dataReader["variantCount"] as int? ?? 0;
                                    int cart_id = dataReader["cart_id"] as int? ?? 0;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] as int? ?? 0;
                                    string item_gender = dataReader["item_gender"] as string ?? string.Empty;
                                    int ItemTypeCommonID = dataReader["ItemTypeCommonID"] as int? ?? 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] as string ?? string.Empty;
                                    int ItemAproxDayCommonID = dataReader["ItemAproxDayCommonID"] as int? ?? 0;
                                    string priceflag = dataReader["priceflag"] as string ?? string.Empty;
                                    string ItemPlainGold = dataReader["ItemPlainGold"] as string ?? string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] as string ?? string.Empty;
                                    int ItemBrandCommonID = dataReader["ItemBrandCommonID"] as int? ?? 0;
                                    string ItemIsSRP = dataReader["ItemIsSRP"] as string ?? string.Empty;
                                    decimal mrp_withtax = dataReader["mrp_withtax"] as decimal? ?? 0;
                                    decimal diamond_price = dataReader["mrp_withtax"] as decimal? ?? 0;
                                    decimal gold_price = dataReader["gold_price"] as decimal? ?? 0;
                                    decimal platinum_price = dataReader["platinum_price"] as decimal? ?? 0;
                                    decimal labour_price = dataReader["labour_price"] as decimal? ?? 0;
                                    decimal metal_price = dataReader["metal_price"] as decimal? ?? 0;
                                    decimal other_price = dataReader["other_price"] as decimal? ?? 0;
                                    decimal stone_price = dataReader["stone_price"] as decimal? ?? 0;
                                    string ItemSizeAvailable = dataReader["ItemSizeAvailable"] as string ?? string.Empty;
                                    decimal ItmStockQty = dataReader["ItmStockQty"] as decimal? ?? 0;
                                    decimal InColorStock = dataReader["InColorStock"] as decimal? ?? 0;
                                    string DataGoldFlag = dataReader["DataGoldFlag"] as string ?? string.Empty;
                                    string MstType = dataReader["MstType"] as string ?? string.Empty;
                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    int selectedColor = dataReader["selectedColor"] as int? ?? 0;
                                    string selectedSize = dataReader["selectedSize"] as string ?? string.Empty;
                                    int selectedColor1 = dataReader["selectedColor1"] as int? ?? 0;
                                    int selectedSize1 = dataReader["selectedSize1"] as int? ?? 0;
                                    string field_name = dataReader["field_name"] as string ?? string.Empty;
                                    string color_name = dataReader["color_name"] as string ?? string.Empty;
                                    string default_color_name = dataReader["default_color_name"] as string ?? string.Empty;
                                    int default_color_code = dataReader["default_color_code"] as int? ?? 0;
                                    int default_size_name = dataReader["default_size_name"] as int? ?? 0;

                                    // LARAVEL API VARIABLES
                                    int itemid = item_id;
                                    int brand_id = ItemBrandCommonID;
                                    string colorname = dsg_color;
                                    int metalid = ItemMetalCommonID;
                                    decimal item_stock_qty = Math.Round(ItmStockQty, 0);
                                    decimal item_stock_colorsize_qty = Math.Round(InColorStock, 0);
                                    int CartID = cart_id;
                                    string designkt = dsg_kt;

                                    //productTags
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();

                                    itemtaglistparams.item_id = itemid;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);
                                    //productTags

                                    price_text = "MRP.₹" + Math.Round(mrp_withtax, 0) + "/-";
                                    //item_mrp = Math.Round(mrp_withtax, 0);
                                    dist_price = Math.Round(dist_price, 0);
                                    decimal FinalDMrp = Math.Round(dist_price, 0);
                                    string weight = "";
                                    string approx_day = ItemAproxDay;
                                    string approxday_detail = approx_day.Trim().Length > 0 ? " Approx Delivery: " + approx_day + "." : "";

                                    string totalLabourPer = "";
                                    if (DataGoldFlag == "Y")
                                    {
                                        if (item_gold == "Y")
                                        {
                                            weight = "Gold Weight: " + Math.Round(item_wt, 2);
                                            weight += ", Gold Price : " + gold_price;
                                            weight += ", KT : " + designkt;
                                            weight += ", " + approxday_detail;

                                            totalLabourPer = "L(" + Math.Round(labour_per, 1);
                                            totalLabourPer += "% + ";
                                            totalLabourPer += Math.Round(ItemDisLabourPer, 1) + "%)";
                                        }
                                        else
                                        {
                                            weight = "Metal Weight: " + Math.Round(item_wt, 2);
                                            weight += ", Diamond Weight: " + Math.Round(item_stone_wt, 2);
                                            weight += ", " + approxday_detail;
                                        }
                                    }

                                    decimal item_mrp_new = 0;
                                    decimal fran_diamond_price = Math.Round(diamond_price, 0);
                                    decimal fran_gold_price = Math.Round(gold_price, 0);
                                    decimal fran_platinum_price = Math.Round(platinum_price, 0);
                                    decimal fran_labour_price = Math.Round(labour_price, 0);
                                    decimal fran_metal_price = Math.Round(metal_price, 0);
                                    decimal fran_other_price = Math.Round(other_price, 0);
                                    decimal fran_stone_price = Math.Round(stone_price, 0);

                                    if (MstType == "F")
                                    {
                                        if (ItemIsSRP == "Y" && ItemPlainGold == "Y")
                                        {
                                            item_mrp_new = Math.Round(mrp_withtax, 0);
                                        }
                                        else if (ItemPlainGold == "N")
                                        {
                                            item_mrp_new = Math.Round(mrp_withtax, 0);
                                        }
                                        else
                                        {
                                            item_mrp_new = Math.Round(item_mrp, 0);
                                        }
                                    }
                                    else
                                    {
                                        if (ItemIsSRP == "Y" && ItemPlainGold == "Y")
                                        {
                                            item_mrp_new = Math.Round(mrp_withtax, 0);
                                        }
                                        else if (ItemPlainGold == "N" && ItemIsSRP == "Y")
                                        {
                                            item_mrp_new = Math.Round(mrp_withtax, 0);
                                        }
                                        else
                                        {
                                            item_mrp_new = Math.Round(item_mrp, 0);
                                        }
                                    }

                                    item_mrp = item_mrp_new;
                                    cart_price = item_mrp_new;
                                    price_text = "MRP.₹" + Math.Round(item_mrp_new, 0) + "/-";

                                    if (CategoryId > 0)
                                    {
                                    }
                                    else
                                    {
                                        CategoryId = category_id;
                                    }

                                    // PRODUCT SIZELIST
                                    CartProduct_sizeListingParams cartproductsizelistparams = new CartProduct_sizeListingParams();
                                    List<CartProduct_sizeListing> cartProductSizeList = new List<CartProduct_sizeListing>();

                                    if (ItemSizeAvailable == "N")
                                    {
                                        selectedSize = "";
                                        default_size_name = 0;
                                    }
                                    else
                                    {
                                        cartproductsizelistparams.ItemID = itemid;
                                        cartproductsizelistparams.CategoryID = CategoryId;
                                        cartproductsizelistparams.BranID = ItemBrandCommonID;
                                        cartproductsizelistparams.ItemNosePinScrewSts = ItemNosePinScrewSts;
                                        cartproductsizelistparams.ItemGenderCommonID = ItemGenderCommonID;
                                        cartproductsizelistparams.field_name = field_name;
                                        cartproductsizelistparams.selectedSize = selectedSize;
                                        cartproductsizelistparams.default_size_name = default_size_name;

                                        cartProductSizeList = objHelpers.GetCartProductSizeList(cartproductsizelistparams);

                                        if (cartProductSizeList.Count > 0)
                                        {
                                            field_name = cartProductSizeList[0].field_name;
                                            selectedSize = cartProductSizeList[0].selectedSize;
                                            default_size_name = cartProductSizeList[0].default_size_name;
                                        }
                                    }
                                    // PRODUCT SIZELIST

                                    // COLOR LIST
                                    CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    cartitemcolorlistparams.item_id = itemid;
                                    cartitemcolorlistparams.default_color_code = colorname;
                                    cartitemcolorlistparams.metalid = metalid;

                                    cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);
                                    // COLOR LIST

                                    // COLOR SIZELIST
                                    CartItem_itemsColorSizeListingParams cartitemcolorsizelistparams = new CartItem_itemsColorSizeListingParams();
                                    List<CartItem_itemsColorSizeListing> cartItemColorSizeList = new List<CartItem_itemsColorSizeListing>();

                                    cartitemcolorsizelistparams.itemid = itemid;
                                    cartitemcolorsizelistparams.cart_auto_id = -1;  //FOR FAMILY PRODUCT
                                    cartitemcolorsizelistparams.CartID = cart_id;

                                    if (cart_id > 0)
                                    {
                                        cartItemColorSizeList = objHelpers.GetCartItemsColorSizeList(cartitemcolorsizelistparams);
                                    }
                                    // COLOR SIZELIST

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();
                                    itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();
                                    // ORDER INSTRUCTION LIST

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();
                                    itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();
                                    // ORDER CUSTOM INSTRUCTION LIST

                                    // IMAGE COLORS
                                    CartItem_item_images_colorListingParams cartitemimagecolorlistparams = new CartItem_item_images_colorListingParams();
                                    List<CartItem_item_images_colorListing> cartItemImageColorList = new List<CartItem_item_images_colorListing>();

                                    cartitemimagecolorlistparams.itemid = itemid;
                                    cartitemimagecolorlistparams.master_common_id = master_common_id;
                                    cartItemImageColorList = objHelpers.GetCartItemImageColorList(cartitemimagecolorlistparams);
                                    // IMAGE COLORS

                                    familyProductList.Add(new FamilyProductListing
                                    {
                                        item_id = item_id.ToString(),
                                        item_gold = item_gold.ToString(),
                                        ItemDisLabourPer = ItemDisLabourPer.ToString(),
                                        labour_per = labour_per.ToString(),
                                        product_itemid = product_itemid.ToString(),
                                        ItemAproxDay = ItemAproxDay,
                                        item_code = item_code,
                                        item_name = item_name,
                                        item_sku = item_sku,
                                        item_description = item_description,
                                        item_mrp = item_mrp.ToString(),
                                        ItemFranchiseSts = ItemFranchiseSts,
                                        sub_category_id = sub_category_id.ToString(),
                                        item_price = item_price.ToString(),
                                        dist_price = dist_price.ToString(),
                                        FinalDMrp = FinalDMrp.ToString(),
                                        image_path = image_path,
                                        dsg_sfx = dsg_sfx,
                                        dsg_size = dsg_size,
                                        dsg_kt = dsg_kt,
                                        dsg_color = dsg_color,
                                        item_soliter = item_soliter,
                                        star = star.ToString(),
                                        cart_img = cart_img,
                                        img_cart_title = img_cart_title,
                                        watch_img = watch_img,
                                        img_watch_title = img_watch_title,
                                        wearit_count = wearit_count.ToString(),
                                        wearit_status = wearit_status,
                                        wearit_img = wearit_img,
                                        wearit_none_img = wearit_none_img,
                                        wearit_color = wearit_color,
                                        wearit_text = wearit_text,
                                        img_wearit_title = img_wearit_title,
                                        wish_count = wish_count.ToString(),
                                        wish_default_img = wish_default_img,
                                        wish_fill_img = wish_fill_img,
                                        img_wish_title = img_wish_title,
                                        item_review = item_review,
                                        item_size = item_size,
                                        item_kt = item_kt,
                                        item_color = item_color,
                                        item_metal = item_metal,
                                        item_wt = item_wt.ToString(),
                                        item_stone = item_stone,
                                        item_stone_wt = item_stone_wt.ToString(),
                                        item_stone_qty = item_stone_qty.ToString(),
                                        star_color = star_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        price_text = price_text,
                                        cart_price = cart_price.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        item_details = item_details,
                                        item_diamond_details = item_diamond_details,
                                        item_text = item_text,
                                        more_item_details = more_item_details,
                                        item_stock = item_stock,
                                        cart_item_qty = cart_item_qty.ToString(),
                                        rupy_symbol = rupy_symbol,
                                        variantCount = variantCount.ToString(),
                                        cart_id = cart_id.ToString(),
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        item_gender = item_gender,
                                        ItemTypeCommonID = ItemTypeCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        ItemAproxDayCommonID = ItemAproxDayCommonID.ToString(),
                                        priceflag = priceflag,
                                        ItemPlainGold = ItemPlainGold,
                                        plaingold_status = plaingold_status,
                                        ItemBrandCommonID = ItemBrandCommonID.ToString(),
                                        ItemIsSRP = ItemIsSRP,
                                        mrp_withtax = mrp_withtax.ToString(),
                                        diamond_price = diamond_price.ToString(),
                                        gold_price = gold_price.ToString(),
                                        platinum_price = platinum_price.ToString(),
                                        labour_price = labour_price.ToString(),
                                        metal_price = metal_price.ToString(),
                                        other_price = other_price.ToString(),
                                        stone_price = stone_price.ToString(),
                                        ItemSizeAvailable = ItemSizeAvailable,
                                        item_stock_qty = item_stock_qty.ToString(),
                                        item_stock_colorsize_qty = item_stock_colorsize_qty.ToString(),
                                        selectedColor = selectedColor.ToString(),
                                        selectedSize = selectedSize.ToString(),
                                        selectedColor1 = selectedColor1.ToString(),
                                        selectedSize1 = selectedSize1.ToString(),
                                        field_name = field_name,
                                        color_name = color_name,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        default_size_name = default_size_name.ToString(),
                                        fran_diamond_price = fran_diamond_price.ToString(),
                                        fran_gold_price = fran_gold_price.ToString(),
                                        fran_platinum_price = fran_platinum_price.ToString(),
                                        fran_labour_price = fran_labour_price.ToString(),
                                        fran_metal_price = fran_metal_price.ToString(),
                                        fran_other_price = fran_other_price.ToString(),
                                        fran_stone_price = fran_stone_price.ToString(),
                                        weight = weight,
                                        totalLabourPer = totalLabourPer,

                                        productTags = itemTagList,
                                        sizeList = cartProductSizeList,
                                        colorList = cartItemColorList,
                                        itemsColorSizeList = cartItemColorSizeList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        item_images_color = cartItemImageColorList,
                                    });

                                }
                            }
                        }
                    }
                }

                if (familyProductList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Family product successfully";
                    responseDetails.status = "200";
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.current_page = "1";
                    responseDetails.last_page = "1";
                    responseDetails.data = familyProductList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.total_items = "0";
                    responseDetails.current_page = "0";
                    responseDetails.last_page = "0";
                    responseDetails.data = new List<FamilyProductListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.total_items = "0";
                responseDetails.current_page = "0";
                responseDetails.last_page = "0";
                responseDetails.data = new List<FamilyProductListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> IllumineItemList(IllumineItemListingParams illumineitemlistparams)
        {
            int current_page = illumineitemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<IllumineItemListing> illumineItemList = new List<IllumineItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ILLUMINE_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        string reqamount = illumineitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        string reqmetalwt = illumineitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        string reqdiawt = illumineitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        int DataId = illumineitemlistparams.data_id > 0 ? illumineitemlistparams.data_id : 0;
                        int CategoryID = illumineitemlistparams.category_id > 0 ? illumineitemlistparams.category_id : 0;

                        string SortIds = string.IsNullOrWhiteSpace(illumineitemlistparams.sort_id) ? "" : illumineitemlistparams.sort_id;
                        string DsgColors = string.IsNullOrWhiteSpace(illumineitemlistparams.dsg_color) ? "" : illumineitemlistparams.dsg_color;
                        string Genders = string.IsNullOrWhiteSpace(illumineitemlistparams.gender_id) ? "" : illumineitemlistparams.gender_id;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(illumineitemlistparams.brand) ? "" : illumineitemlistparams.brand;
                        string SubCategoryIDs = string.IsNullOrWhiteSpace(illumineitemlistparams.sub_category_id) ? "" : illumineitemlistparams.sub_category_id;
                        string ItemSubCategoryIDs = string.IsNullOrWhiteSpace(illumineitemlistparams.item_sub_category_id)
                            ? ""
                            : string.Join(",", illumineitemlistparams.item_sub_category_id
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(id => id.Trim())
                                .Where(id => !string.IsNullOrWhiteSpace(id)));

                        int Page = illumineitemlistparams.page > 0 ? illumineitemlistparams.page - 1 : 1;
                        int Limit = illumineitemlistparams.default_limit_app_page > 0 ? illumineitemlistparams.default_limit_app_page : 20; // Default limit

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@ItemSubCategoryIds", ItemSubCategoryIDs);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_id"]) : 0;
                                    decimal item_mrp = dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0;
                                    string item_name = dataReader["item_name"] != DBNull.Value ? Convert.ToString(dataReader["item_name"]) : string.Empty;
                                    string item_description = dataReader["item_description"] != DBNull.Value ? Convert.ToString(dataReader["item_description"]) : string.Empty;
                                    int category_id = dataReader["category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["category_id"]) : 0;
                                    string item_kt = dataReader["item_kt"] != DBNull.Value ? Convert.ToString(dataReader["item_kt"]) : string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] != DBNull.Value ? Convert.ToString(dataReader["rupy_symbol"]) : string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] != DBNull.Value ? Convert.ToString(dataReader["img_watch_title"]) : string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wish_title"]) : string.Empty;
                                    int wish_count = dataReader["wish_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wish_count"]) : 0;
                                    string image_path = dataReader["image_path"] != DBNull.Value ? Convert.ToString(dataReader["image_path"]) : string.Empty;
                                    int mostOrder = dataReader["mostOrder"] != DBNull.Value ? Convert.ToInt32(dataReader["mostOrder"]) : 0;

                                    last_page = dataReader["last_page"] != DBNull.Value ? Convert.ToInt32(dataReader["last_page"]) : 0;
                                    total_items = dataReader["total_items"] != DBNull.Value ? Convert.ToInt32(dataReader["total_items"]) : 0;

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    illumineItemList.Add(new IllumineItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        item_mrp = item_mrp.ToString(),
                                        item_name = item_name,
                                        item_description = item_description,
                                        category_id = category_id.ToString(),
                                        item_kt = item_kt,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wish_title = img_wish_title,
                                        wish_count = wish_count.ToString(),
                                        image_path = image_path,
                                        mostOrder = mostOrder.ToString(),
                                        productTags = itemTagList,
                                    });
                                }
                            }
                        }
                    }
                }
                if (illumineItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = illumineItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<IllumineItemListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<IllumineItemListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> AllCategoryList(AllCategoryListingParams allcategorylistparams)
        {
            var responseDetails = new ResponseDetails();

            IList<AllCategoryListing> allCategoryList = new List<AllCategoryListing>();
            IList<CategoryButtonListing> categoryButtonList = new List<CategoryButtonListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ALLCATEGORYLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        int data_login_type = allcategorylistparams.data_login_type > 0 ? allcategorylistparams.data_login_type : 0;
                        string type = string.IsNullOrWhiteSpace(allcategorylistparams.type) ? "" : allcategorylistparams.type;
                        string device_type = string.IsNullOrWhiteSpace(allcategorylistparams.device_type) ? "" : allcategorylistparams.device_type;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@device_type", device_type);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            int category_id = rowdetails["category_id"] != DBNull.Value ? Convert.ToInt32(rowdetails["category_id"]) : 0;
                                            string category_name = rowdetails["category_name"] != DBNull.Value ? Convert.ToString(rowdetails["category_name"]) : string.Empty;
                                            int master_common_id = rowdetails["master_common_id"] != DBNull.Value ? Convert.ToInt32(rowdetails["master_common_id"]) : 0;
                                            string image_path = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            int total_count = rowdetails["count"] != DBNull.Value ? Convert.ToInt32(rowdetails["count"]) : 0;

                                            allCategoryList.Add(new AllCategoryListing
                                            {
                                                category_id = category_id.ToString(),
                                                category_name = category_name,
                                                master_common_id = master_common_id.ToString(),
                                                image_path = image_path,
                                                count = total_count.ToString(),
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }

                                if (device_type != "WEB")
                                {
                                    if (ds.Tables.Count > 1)
                                    {
                                        if (ds.Tables[1].Rows.Count > 0)
                                        {
                                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                            {
                                                try
                                                {
                                                    var btndetails = ds.Tables[1].Rows[j];

                                                    string button_name = btndetails["button_name"] != DBNull.Value ? Convert.ToString(btndetails["button_name"]) : string.Empty;
                                                    string btn_cd = btndetails["btn_cd"] != DBNull.Value ? Convert.ToString(btndetails["btn_cd"]) : string.Empty;

                                                    categoryButtonList.Add(new CategoryButtonListing
                                                    {
                                                        button_name = button_name,
                                                        btn_cd = btn_cd,
                                                    });
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                if (allCategoryList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = allCategoryList;
                    if (categoryButtonList.Any())
                    {
                        responseDetails.button = categoryButtonList;
                    }
                    else
                    {
                        responseDetails.button = new List<CategoryButtonListing>();
                    }
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<AllCategoryListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<AllCategoryListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> AllCategoryListVI(AllCategoryListingParams allcategorylistparams)
        {
            var responseDetails = new ResponseDetails();

            IList<AllCategoryListing> allCategoryList = new List<AllCategoryListing>();
            IList<CategoryButtonListing> categoryButtonList = new List<CategoryButtonListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ALLCATEGORYLISTVI;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        int categoryid = allcategorylistparams.categoryid > 0 ? allcategorylistparams.categoryid : 0;
                        string device_type = string.IsNullOrWhiteSpace(allcategorylistparams.device_type) ? "" : allcategorylistparams.device_type;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@category_id", categoryid);
                        cmd.Parameters.AddWithValue("@device_type", device_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            int category_id = rowdetails["category_id"] != DBNull.Value ? Convert.ToInt32(rowdetails["category_id"]) : 0;
                                            string category_name = rowdetails["category_name"] != DBNull.Value ? Convert.ToString(rowdetails["category_name"]) : string.Empty;
                                            int master_common_id = rowdetails["master_common_id"] != DBNull.Value ? Convert.ToInt32(rowdetails["master_common_id"]) : 0;
                                            string image_path = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            int total_count = rowdetails["count"] != DBNull.Value ? Convert.ToInt32(rowdetails["count"]) : 0;

                                            allCategoryList.Add(new AllCategoryListing
                                            {
                                                category_id = category_id.ToString(),
                                                category_name = category_name,
                                                master_common_id = master_common_id.ToString(),
                                                image_path = image_path,
                                                count = total_count.ToString(),
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }

                                if (ds.Tables.Count > 1)
                                {
                                    for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                    {
                                        try
                                        {
                                            var btndetails = ds.Tables[1].Rows[j];

                                            string button_name = btndetails["button_name"] != DBNull.Value ? Convert.ToString(btndetails["button_name"]) : string.Empty;
                                            string btn_cd = btndetails["btn_cd"] != DBNull.Value ? Convert.ToString(btndetails["btn_cd"]) : string.Empty;

                                            categoryButtonList.Add(new CategoryButtonListing
                                            {
                                                button_name = button_name,
                                                btn_cd = btn_cd,
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                if (allCategoryList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = allCategoryList;
                    if (categoryButtonList.Any())
                    {
                        responseDetails.button = categoryButtonList;
                    }
                    else
                    {
                        responseDetails.button = new List<CategoryButtonListing>();
                    }
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<AllCategoryListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<AllCategoryListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetNewKisnaPremiumItemList(NewKisnaPremiumItemListingParams newkisnapremiumitemlistparams)
        {
            int current_page = newkisnapremiumitemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<NewKisnaPremiumItemListing> newkisnapremiumItemList = new List<NewKisnaPremiumItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.NEWKISNAPREMIUM_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        // Helper function to parse comma-separated strings into Min/Max decimal values
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        string reqamount = newkisnapremiumitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        string reqmetalwt = newkisnapremiumitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        string reqdiawt = newkisnapremiumitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        // Default values if no sorting or mode
                        string SortIds = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.sort_id) ? "" : newkisnapremiumitemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.variant) ? "Y" : newkisnapremiumitemlistparams.variant;
                        string mode = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.mode) ? "off" : newkisnapremiumitemlistparams.mode;

                        int Page = newkisnapremiumitemlistparams.page > 0 ? newkisnapremiumitemlistparams.page - 1 : 0;
                        int Limit = newkisnapremiumitemlistparams.default_limit_app_page > 0 ? newkisnapremiumitemlistparams.default_limit_app_page : 20;
                        int DataId = newkisnapremiumitemlistparams.data_id > 0 ? newkisnapremiumitemlistparams.data_id : 0;
                        int DataLoginTypeID = newkisnapremiumitemlistparams.data_login_type > 0 ? newkisnapremiumitemlistparams.data_login_type : 0;
                        string ItemName = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.item_name) ? "" : newkisnapremiumitemlistparams.item_name;

                        int CategoryID = newkisnapremiumitemlistparams.category_id > 0 ? newkisnapremiumitemlistparams.category_id : 0;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.sub_category_id) ? "" : newkisnapremiumitemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.dsg_size) ? "" : newkisnapremiumitemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.dsg_kt) ? "" : newkisnapremiumitemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.dsg_color) ? "" : newkisnapremiumitemlistparams.dsg_color;
                        int ItemID = newkisnapremiumitemlistparams.Item_ID > 0 ? newkisnapremiumitemlistparams.Item_ID : 0;

                        string isStockFilterParams = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.Stock_Av) ? "N" : newkisnapremiumitemlistparams.Stock_Av;
                        string Stock_Av = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.Stock_Av) ? "" : newkisnapremiumitemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.Family_Av) ? "" : newkisnapremiumitemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.Regular_Av) ? "" : newkisnapremiumitemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.wearit) ? "" : newkisnapremiumitemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.tryon) ? "" : newkisnapremiumitemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.gender_id) ? "" : newkisnapremiumitemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.item_tag) ? "" : newkisnapremiumitemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.brand) ? "" : newkisnapremiumitemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.delivery_days) ? "" : newkisnapremiumitemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.ItemSubCtgIDs) ? "" : newkisnapremiumitemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.ItemSubSubCtgIDs) ? "" : newkisnapremiumitemlistparams.ItemSubSubCtgIDs;
                        string ItemSubCategoryIDs = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.item_sub_category_id) ? "" : newkisnapremiumitemlistparams.item_sub_category_id;

                        string SalesLocation = string.IsNullOrWhiteSpace(newkisnapremiumitemlistparams.sales_location) ? "" : newkisnapremiumitemlistparams.sales_location;
                        int DesignTimeLine = newkisnapremiumitemlistparams.design_timeline > 0 ? newkisnapremiumitemlistparams.design_timeline : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@isStockFilter", isStockFilterParams);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@ItemSubCategoryIDs", ItemSubCategoryIDs);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_id"]) : 0;
                                    int category_id = dataReader["category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["category_id"]) : 0;
                                    string item_description = dataReader["item_description"] != DBNull.Value ? Convert.ToString(dataReader["item_description"]) : string.Empty;
                                    string item_code = dataReader["item_code"] != DBNull.Value ? Convert.ToString(dataReader["item_code"]) : string.Empty;
                                    string item_name = dataReader["item_name"] != DBNull.Value ? Convert.ToString(dataReader["item_name"]) : string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemGenderCommonID"]) : 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(dataReader["ItemNosePinScrewSts"]) : string.Empty;
                                    string ApproxDeliveryDate = dataReader["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(dataReader["ApproxDeliveryDate"]) : string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] != DBNull.Value ? Convert.ToString(dataReader["item_brand_text"]) : string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["sub_category_id"]) : 0;
                                    string priceflag = dataReader["priceflag"] != DBNull.Value ? Convert.ToString(dataReader["priceflag"]) : string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] != DBNull.Value ? Convert.ToInt32(dataReader["max_qty_sold"]) : 0;
                                    string image_path = dataReader["image_path"] != DBNull.Value ? Convert.ToString(dataReader["image_path"]) : string.Empty;
                                    int mostOrder = dataReader["mostOrder"] != DBNull.Value ? Convert.ToInt32(dataReader["mostOrder"]) : 0;
                                    string isStockFilter = dataReader["isStockFilter"] != DBNull.Value ? Convert.ToString(dataReader["isStockFilter"]) : string.Empty;
                                    string item_color = dataReader["item_color"] != DBNull.Value ? Convert.ToString(dataReader["item_color"]) : string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemMetalCommonID"]) : 0;
                                    string item_soliter = dataReader["item_soliter"] != DBNull.Value ? Convert.ToString(dataReader["item_soliter"]) : string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] != DBNull.Value ? Convert.ToString(dataReader["plaingold_status"]) : string.Empty;
                                    int ItemAproxDayCommonID = dataReader["ItemAproxDayCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemAproxDayCommonID"]) : 0;
                                    string item_text = dataReader["item_text"] != DBNull.Value ? Convert.ToString(dataReader["item_text"]) : string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] != DBNull.Value ? Convert.ToString(dataReader["rupy_symbol"]) : string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] != DBNull.Value ? Convert.ToString(dataReader["img_watch_title"]) : string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wearit_title"]) : string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wish_title"]) : string.Empty;
                                    string wearit_text = dataReader["wearit_text"] != DBNull.Value ? Convert.ToString(dataReader["wearit_text"]) : string.Empty;
                                    decimal cart_price = dataReader["cart_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["cart_price"]) : 0;
                                    int variantCount = dataReader["variantCount"] != DBNull.Value ? Convert.ToInt32(dataReader["variantCount"]) : 0;
                                    int item_color_id = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;
                                    int cart_id = dataReader["cart_id"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_id"]) : 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_item_qty"]) : 0;
                                    int wish_count = dataReader["wish_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wish_count"]) : 0;
                                    int wearit_count = dataReader["wearit_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wearit_count"]) : 0;

                                    last_page = dataReader["last_page"] != DBNull.Value ? Convert.ToInt32(dataReader["last_page"]) : 0;
                                    total_items = dataReader["total_items"] != DBNull.Value ? Convert.ToInt32(dataReader["total_items"]) : 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] != DBNull.Value ? Convert.ToString(dataReader["MstType"]) : string.Empty;

                                    // COLOR SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // IMAGE COLORS
                                    List<Item_images_colorListing> itemImageColorList = new List<Item_images_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    // ItemLivePriceCalculation
                                    IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();

                                    // DP_RP_Calculation
                                    IList<CartItemDPRPCALCListing> cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingKisnaPremiumList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;

                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            // $data_collection1
                                            int CATEGORY_EARRING_COMMON_ID = 21;
                                            if (category_id == CATEGORY_EARRING_COMMON_ID)
                                            {
                                                IList<ItemSelectedSizeList> itemSelectedSize = new List<ItemSelectedSizeList>();
                                                itemSelectedSize = objHelpers.GetSelectedSizeByCollectionList(item_id, MstType);
                                                if (itemSelectedSize.Any())
                                                {
                                                    int tmp_selectedSize = itemSelectedSize[0].selectedSize;
                                                    int tmp_flag_exclude_sizes = itemSelectedSize[0].flag_exclude_sizes;
                                                    string tmp_exclude_sizes = itemSelectedSize[0].exclude_sizes;

                                                    if (tmp_flag_exclude_sizes == 1)
                                                    {
                                                        ExcludeSizes = tmp_exclude_sizes;
                                                    }
                                                    selectedSize = tmp_selectedSize;
                                                }
                                            }
                                            // $data_collection1

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] != DBNull.Value ? Convert.ToString(dataReader["color_name"]) : string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] != DBNull.Value ? Convert.ToString(dataReader["selectedColor1"]) : string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] != DBNull.Value ? Convert.ToString(dataReader["selectedSize1"]) : string.Empty;
                                        default_color_name = dataReader["default_color_name"] != DBNull.Value ? Convert.ToString(dataReader["default_color_name"]) : string.Empty;
                                        default_color_code = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;
                                    }

                                    // item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);
                                    item_mrp = Math.Round(dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock_KisnaPremium(item_code, MstType);
                                    }

                                    newkisnapremiumItemList.Add(new NewKisnaPremiumItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        ApproxDeliveryDate = ApproxDeliveryDate,
                                        item_brand_text = item_brand_text,
                                        sub_category_id = sub_category_id.ToString(),
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        mostOrder = mostOrder.ToString(),
                                        isStockFilter = isStockFilter,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        plaingold_status = plaingold_status,
                                        ItemAproxDayCommonID = ItemAproxDayCommonID.ToString(),
                                        item_text = item_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                        //item_images_color = itemImageColorList,
                                    });
                                }
                            }
                        }
                    }
                }
                if (newkisnapremiumItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = newkisnapremiumItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<NewKisnaPremiumItemListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<NewKisnaPremiumItemListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetEsmeCollectionItemList(EsmeCollectionItemListingParams esmecollectionitemlistparams)
        {
            int current_page = esmecollectionitemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<EsmeCollectionItemListing> esmecollectionItemList = new List<EsmeCollectionItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ESMECOLLECTION_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        //ParseMinMax(esmecollectionitemlistparams.amount, out PriceMin, out PriceMax);
                        string reqamount = esmecollectionitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        //ParseMinMax(esmecollectionitemlistparams.metalwt, out MetalWtMin, out MetalWtMax);
                        string reqmetalwt = esmecollectionitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        //ParseMinMax(esmecollectionitemlistparams.diawt, out DiaWtMin, out DiaWtMax);
                        string reqdiawt = esmecollectionitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        // Default values if no sorting or mode
                        string SortIds = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.sort_id) ? "" : esmecollectionitemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.variant) ? "Y" : esmecollectionitemlistparams.variant;
                        string mode = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.mode) ? "off" : esmecollectionitemlistparams.mode;

                        int Page = esmecollectionitemlistparams.page > 0 ? esmecollectionitemlistparams.page - 1 : 0;
                        int Limit = esmecollectionitemlistparams.default_limit_app_page > 0 ? esmecollectionitemlistparams.default_limit_app_page : 20;
                        int DataId = esmecollectionitemlistparams.data_id > 0 ? esmecollectionitemlistparams.data_id : 0;
                        int DataLoginTypeID = esmecollectionitemlistparams.data_login_type > 0 ? esmecollectionitemlistparams.data_login_type : 0;
                        string ItemName = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.item_name) ? "" : esmecollectionitemlistparams.item_name;

                        int CategoryID = esmecollectionitemlistparams.category_id > 0 ? esmecollectionitemlistparams.category_id : 0;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.sub_category_id) ? "" : esmecollectionitemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.dsg_size) ? "" : esmecollectionitemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.dsg_kt) ? "" : esmecollectionitemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.dsg_color) ? "" : esmecollectionitemlistparams.dsg_color;
                        int ItemID = esmecollectionitemlistparams.Item_ID > 0 ? esmecollectionitemlistparams.Item_ID : 0;

                        string isStockFilterParams = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.Stock_Av) ? "N" : esmecollectionitemlistparams.Stock_Av;
                        string Stock_Av = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.Stock_Av) ? "" : esmecollectionitemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.Family_Av) ? "" : esmecollectionitemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.Regular_Av) ? "" : esmecollectionitemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.wearit) ? "" : esmecollectionitemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.tryon) ? "" : esmecollectionitemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.gender_id) ? "" : esmecollectionitemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.item_tag) ? "" : esmecollectionitemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.brand) ? "" : esmecollectionitemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.delivery_days) ? "" : esmecollectionitemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.ItemSubCtgIDs) ? "" : esmecollectionitemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.ItemSubSubCtgIDs) ? "" : esmecollectionitemlistparams.ItemSubSubCtgIDs;
                        string ItemSubCategoryIDs = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.item_sub_category_id) ? "" : esmecollectionitemlistparams.item_sub_category_id;

                        string SalesLocation = string.IsNullOrWhiteSpace(esmecollectionitemlistparams.sales_location) ? "" : esmecollectionitemlistparams.sales_location;
                        int DesignTimeLine = esmecollectionitemlistparams.design_timeline > 0 ? esmecollectionitemlistparams.design_timeline : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@isStockFilter", isStockFilterParams);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@ItemSubCategoryIDs", ItemSubCategoryIDs);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_id"]) : 0;
                                    int category_id = dataReader["category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["category_id"]) : 0;
                                    string item_description = dataReader["item_description"] != DBNull.Value ? Convert.ToString(dataReader["item_description"]) : string.Empty;
                                    string item_code = dataReader["item_code"] != DBNull.Value ? Convert.ToString(dataReader["item_code"]) : string.Empty;
                                    string item_name = dataReader["item_name"] != DBNull.Value ? Convert.ToString(dataReader["item_name"]) : string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemGenderCommonID"]) : 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(dataReader["ItemNosePinScrewSts"]) : string.Empty;
                                    string item_kt = dataReader["item_kt"] != DBNull.Value ? Convert.ToString(dataReader["item_kt"]) : string.Empty;
                                    string ApproxDeliveryDate = dataReader["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(dataReader["ApproxDeliveryDate"]) : string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] != DBNull.Value ? Convert.ToString(dataReader["item_brand_text"]) : string.Empty;
                                    string ItemIsSRP = dataReader["ItemIsSRP"] != DBNull.Value ? Convert.ToString(dataReader["ItemIsSRP"]) : string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["sub_category_id"]) : 0;
                                    string priceflag = dataReader["priceflag"] != DBNull.Value ? Convert.ToString(dataReader["priceflag"]) : string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] != DBNull.Value ? Convert.ToInt32(dataReader["max_qty_sold"]) : 0;
                                    string image_path = dataReader["image_path"] != DBNull.Value ? Convert.ToString(dataReader["image_path"]) : string.Empty;
                                    int mostOrder = dataReader["mostOrder"] != DBNull.Value ? Convert.ToInt32(dataReader["mostOrder"]) : 0;
                                    string isStockFilter = dataReader["isStockFilter"] != DBNull.Value ? Convert.ToString(dataReader["isStockFilter"]) : string.Empty;
                                    string item_color = dataReader["item_color"] != DBNull.Value ? Convert.ToString(dataReader["item_color"]) : string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemMetalCommonID"]) : 0;
                                    string item_soliter = dataReader["item_soliter"] != DBNull.Value ? Convert.ToString(dataReader["item_soliter"]) : string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] != DBNull.Value ? Convert.ToString(dataReader["plaingold_status"]) : string.Empty;
                                    int ItemAproxDayCommonID = dataReader["ItemAproxDayCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemAproxDayCommonID"]) : 0;
                                    decimal item_price = dataReader["item_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_price"]) : 0;
                                    decimal dist_price = dataReader["dist_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["dist_price"]) : 0;
                                    string item_text = dataReader["item_text"] != DBNull.Value ? Convert.ToString(dataReader["item_text"]) : string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] != DBNull.Value ? Convert.ToString(dataReader["rupy_symbol"]) : string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] != DBNull.Value ? Convert.ToString(dataReader["img_watch_title"]) : string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wearit_title"]) : string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wish_title"]) : string.Empty;
                                    string wearit_text = dataReader["wearit_text"] != DBNull.Value ? Convert.ToString(dataReader["wearit_text"]) : string.Empty;
                                    decimal cart_price = dataReader["cart_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["cart_price"]) : 0;
                                    int variantCount = dataReader["variantCount"] != DBNull.Value ? Convert.ToInt32(dataReader["variantCount"]) : 0;
                                    int item_color_id = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;
                                    int cart_id = dataReader["cart_id"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_id"]) : 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_item_qty"]) : 0;
                                    int wish_count = dataReader["wish_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wish_count"]) : 0;
                                    int wearit_count = dataReader["wearit_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wearit_count"]) : 0;

                                    last_page = dataReader["last_page"] != DBNull.Value ? Convert.ToInt32(dataReader["last_page"]) : 0;
                                    total_items = dataReader["total_items"] != DBNull.Value ? Convert.ToInt32(dataReader["total_items"]) : 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] != DBNull.Value ? Convert.ToString(dataReader["MstType"]) : string.Empty;

                                    // COLOR SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // IMAGE COLORS
                                    List<Item_images_colorListing> itemImageColorList = new List<Item_images_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    // ItemLivePriceCalculation
                                    IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();

                                    // DP_RP_Calculation
                                    IList<CartItemDPRPCALCListing> cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;

                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            // $data_collection1
                                            int CATEGORY_EARRING_COMMON_ID = 21;
                                            if (category_id == CATEGORY_EARRING_COMMON_ID)
                                            {
                                                IList<ItemSelectedSizeList> itemSelectedSize = new List<ItemSelectedSizeList>();
                                                itemSelectedSize = objHelpers.GetSelectedSizeByCollectionList(item_id, MstType);
                                                if (itemSelectedSize.Any())
                                                {
                                                    int tmp_selectedSize = itemSelectedSize[0].selectedSize;
                                                    int tmp_flag_exclude_sizes = itemSelectedSize[0].flag_exclude_sizes;
                                                    string tmp_exclude_sizes = itemSelectedSize[0].exclude_sizes;

                                                    if (tmp_flag_exclude_sizes == 1)
                                                    {
                                                        ExcludeSizes = tmp_exclude_sizes;
                                                    }
                                                    selectedSize = tmp_selectedSize;
                                                }
                                            }
                                            // $data_collection1

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] != DBNull.Value ? Convert.ToString(dataReader["color_name"]) : string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] != DBNull.Value ? Convert.ToString(dataReader["selectedColor1"]) : string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] != DBNull.Value ? Convert.ToString(dataReader["selectedSize1"]) : string.Empty;
                                        default_color_name = dataReader["default_color_name"] != DBNull.Value ? Convert.ToString(dataReader["default_color_name"]) : string.Empty;
                                        default_color_code = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;

                                        //LivePrice
                                        if (ItemIsSRP == "Y")
                                        {
                                            CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                            cartitempricedetaillistparams.DataID = DataId;
                                            cartitempricedetaillistparams.ItemID = item_id;
                                            cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                            cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                            if (cartItemPriceDetailList_gold.Count > 0)
                                            {
                                                dist_price = cartItemPriceDetailList_gold[0].dp_final_price;
                                            }

                                            CartItemDPRPCALCListingParams cartitemDPRPCALClistparams = new CartItemDPRPCALCListingParams();
                                            cartitemDPRPCALClistparams.DataID = DataId;
                                            cartitemDPRPCALClistparams.MRP = item_mrp;
                                            cartItemDPRPCALCList = objHelpers.Get_DP_RP_Calculation(cartitemDPRPCALClistparams);

                                            if (cartItemDPRPCALCList.Count > 0)
                                            {
                                                item_price = cartItemDPRPCALCList[0].R_Price;
                                            }
                                        }
                                    }

                                    // item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);
                                    item_mrp = Math.Round(dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock_KisnaPremium(item_code, MstType);
                                    }

                                    esmecollectionItemList.Add(new EsmeCollectionItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        item_kt = item_kt,
                                        ApproxDeliveryDate = ApproxDeliveryDate,
                                        item_brand_text = item_brand_text,
                                        ItemIsSRP = ItemIsSRP,
                                        sub_category_id = sub_category_id.ToString(),
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        mostOrder = mostOrder.ToString(),
                                        isStockFilter = isStockFilter,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        plaingold_status = plaingold_status,
                                        ItemAproxDayCommonID = ItemAproxDayCommonID.ToString(),
                                        item_price = item_price.ToString(),
                                        dist_price = dist_price.ToString(),
                                        item_text = item_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                        //item_images_color = itemImageColorList,
                                    });
                                }
                            }
                        }
                    }
                }
                if (esmecollectionItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = esmecollectionItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<EsmeCollectionItemListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<EsmeCollectionItemListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetAkshayaGoldItemList(AkshayaGoldItemListingParams akshayagolditemlistparams)
        {
            int current_page = akshayagolditemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<AkshayaGoldItemListing> akshayagoldItemList = new List<AkshayaGoldItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.AKSHAYAGOLD_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        //ParseMinMax(akshayagolditemlistparams.amount, out PriceMin, out PriceMax);
                        string reqamount = akshayagolditemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        //ParseMinMax(akshayagolditemlistparams.metalwt, out MetalWtMin, out MetalWtMax);
                        string reqmetalwt = akshayagolditemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        //ParseMinMax(akshayagolditemlistparams.diawt, out DiaWtMin, out DiaWtMax);
                        string reqdiawt = akshayagolditemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        // Default values if no sorting or mode
                        string SortIds = string.IsNullOrWhiteSpace(akshayagolditemlistparams.sort_id) ? "" : akshayagolditemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(akshayagolditemlistparams.variant) ? "Y" : akshayagolditemlistparams.variant;
                        string mode = string.IsNullOrWhiteSpace(akshayagolditemlistparams.mode) ? "off" : akshayagolditemlistparams.mode;

                        int Page = akshayagolditemlistparams.page > 0 ? akshayagolditemlistparams.page - 1 : 0;
                        int Limit = akshayagolditemlistparams.default_limit_app_page > 0 ? akshayagolditemlistparams.default_limit_app_page : 20;
                        int DataId = akshayagolditemlistparams.data_id > 0 ? akshayagolditemlistparams.data_id : 0;
                        int DataLoginTypeID = akshayagolditemlistparams.data_login_type > 0 ? akshayagolditemlistparams.data_login_type : 0;
                        string ItemName = string.IsNullOrWhiteSpace(akshayagolditemlistparams.item_name) ? "" : akshayagolditemlistparams.item_name;

                        int CategoryID = akshayagolditemlistparams.category_id > 0 ? akshayagolditemlistparams.category_id : 0;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(akshayagolditemlistparams.sub_category_id) ? "" : akshayagolditemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(akshayagolditemlistparams.dsg_size) ? "" : akshayagolditemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(akshayagolditemlistparams.dsg_kt) ? "" : akshayagolditemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(akshayagolditemlistparams.dsg_color) ? "" : akshayagolditemlistparams.dsg_color;
                        int ItemID = akshayagolditemlistparams.Item_ID > 0 ? akshayagolditemlistparams.Item_ID : 0;

                        string isStockFilterParams = string.IsNullOrWhiteSpace(akshayagolditemlistparams.Stock_Av) ? "N" : akshayagolditemlistparams.Stock_Av;
                        string Stock_Av = string.IsNullOrWhiteSpace(akshayagolditemlistparams.Stock_Av) ? "" : akshayagolditemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(akshayagolditemlistparams.Family_Av) ? "" : akshayagolditemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(akshayagolditemlistparams.Regular_Av) ? "" : akshayagolditemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(akshayagolditemlistparams.wearit) ? "" : akshayagolditemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(akshayagolditemlistparams.tryon) ? "" : akshayagolditemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(akshayagolditemlistparams.gender_id) ? "" : akshayagolditemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(akshayagolditemlistparams.item_tag) ? "" : akshayagolditemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(akshayagolditemlistparams.brand) ? "" : akshayagolditemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(akshayagolditemlistparams.delivery_days) ? "" : akshayagolditemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(akshayagolditemlistparams.ItemSubCtgIDs) ? "" : akshayagolditemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(akshayagolditemlistparams.ItemSubSubCtgIDs) ? "" : akshayagolditemlistparams.ItemSubSubCtgIDs;
                        string ItemSubCategoryIDs = string.IsNullOrWhiteSpace(akshayagolditemlistparams.item_sub_category_id) ? "" : akshayagolditemlistparams.item_sub_category_id;

                        string SalesLocation = string.IsNullOrWhiteSpace(akshayagolditemlistparams.sales_location) ? "" : akshayagolditemlistparams.sales_location;
                        int DesignTimeLine = akshayagolditemlistparams.design_timeline > 0 ? akshayagolditemlistparams.design_timeline : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@isStockFilter", isStockFilterParams);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@ItemSubCategoryIDs", ItemSubCategoryIDs);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_id"]) : 0;
                                    int category_id = dataReader["category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["category_id"]) : 0;
                                    string item_description = dataReader["item_description"] != DBNull.Value ? Convert.ToString(dataReader["item_description"]) : string.Empty;
                                    string item_code = dataReader["item_code"] != DBNull.Value ? Convert.ToString(dataReader["item_code"]) : string.Empty;
                                    string item_name = dataReader["item_name"] != DBNull.Value ? Convert.ToString(dataReader["item_name"]) : string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemGenderCommonID"]) : 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(dataReader["ItemNosePinScrewSts"]) : string.Empty;
                                    string item_kt = dataReader["item_kt"] != DBNull.Value ? Convert.ToString(dataReader["item_kt"]) : string.Empty;
                                    string ApproxDeliveryDate = dataReader["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(dataReader["ApproxDeliveryDate"]) : string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] != DBNull.Value ? Convert.ToString(dataReader["item_brand_text"]) : string.Empty;
                                    string ItemIsSRP = dataReader["ItemIsSRP"] != DBNull.Value ? Convert.ToString(dataReader["ItemIsSRP"]) : string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["sub_category_id"]) : 0;
                                    string priceflag = dataReader["priceflag"] != DBNull.Value ? Convert.ToString(dataReader["priceflag"]) : string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] != DBNull.Value ? Convert.ToInt32(dataReader["max_qty_sold"]) : 0;
                                    string image_path = dataReader["image_path"] != DBNull.Value ? Convert.ToString(dataReader["image_path"]) : string.Empty;
                                    int mostOrder = dataReader["mostOrder"] != DBNull.Value ? Convert.ToInt32(dataReader["mostOrder"]) : 0;
                                    string isStockFilter = dataReader["isStockFilter"] != DBNull.Value ? Convert.ToString(dataReader["isStockFilter"]) : string.Empty;
                                    string item_color = dataReader["item_color"] != DBNull.Value ? Convert.ToString(dataReader["item_color"]) : string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemMetalCommonID"]) : 0;
                                    string item_soliter = dataReader["item_soliter"] != DBNull.Value ? Convert.ToString(dataReader["item_soliter"]) : string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] != DBNull.Value ? Convert.ToString(dataReader["plaingold_status"]) : string.Empty;
                                    int ItemAproxDayCommonID = dataReader["ItemAproxDayCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemAproxDayCommonID"]) : 0;
                                    decimal item_price = dataReader["item_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_price"]) : 0;
                                    decimal dist_price = dataReader["dist_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["dist_price"]) : 0;
                                    string item_text = dataReader["item_text"] != DBNull.Value ? Convert.ToString(dataReader["item_text"]) : string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] != DBNull.Value ? Convert.ToString(dataReader["rupy_symbol"]) : string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] != DBNull.Value ? Convert.ToString(dataReader["img_watch_title"]) : string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wearit_title"]) : string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wish_title"]) : string.Empty;
                                    string wearit_text = dataReader["wearit_text"] != DBNull.Value ? Convert.ToString(dataReader["wearit_text"]) : string.Empty;
                                    decimal cart_price = dataReader["cart_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["cart_price"]) : 0;
                                    int variantCount = dataReader["variantCount"] != DBNull.Value ? Convert.ToInt32(dataReader["variantCount"]) : 0;
                                    int item_color_id = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;
                                    int cart_id = dataReader["cart_id"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_id"]) : 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_item_qty"]) : 0;
                                    int wish_count = dataReader["wish_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wish_count"]) : 0;
                                    int wearit_count = dataReader["wearit_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wearit_count"]) : 0;

                                    last_page = dataReader["last_page"] != DBNull.Value ? Convert.ToInt32(dataReader["last_page"]) : 0;
                                    total_items = dataReader["total_items"] != DBNull.Value ? Convert.ToInt32(dataReader["total_items"]) : 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] != DBNull.Value ? Convert.ToString(dataReader["MstType"]) : string.Empty;

                                    // COLOR SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // IMAGE COLORS
                                    List<Item_images_colorListing> itemImageColorList = new List<Item_images_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    // ItemLivePriceCalculation
                                    IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();

                                    // DP_RP_Calculation
                                    IList<CartItemDPRPCALCListing> cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;

                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            // $data_collection1
                                            int CATEGORY_EARRING_COMMON_ID = 21;
                                            if (category_id == CATEGORY_EARRING_COMMON_ID)
                                            {
                                                IList<ItemSelectedSizeList> itemSelectedSize = new List<ItemSelectedSizeList>();
                                                itemSelectedSize = objHelpers.GetSelectedSizeByCollectionList(item_id, MstType);
                                                if (itemSelectedSize.Any())
                                                {
                                                    int tmp_selectedSize = itemSelectedSize[0].selectedSize;
                                                    int tmp_flag_exclude_sizes = itemSelectedSize[0].flag_exclude_sizes;
                                                    string tmp_exclude_sizes = itemSelectedSize[0].exclude_sizes;

                                                    if (tmp_flag_exclude_sizes == 1)
                                                    {
                                                        ExcludeSizes = tmp_exclude_sizes;
                                                    }
                                                    selectedSize = tmp_selectedSize;
                                                }
                                            }
                                            // $data_collection1

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] != DBNull.Value ? Convert.ToString(dataReader["color_name"]) : string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] != DBNull.Value ? Convert.ToString(dataReader["selectedColor1"]) : string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] != DBNull.Value ? Convert.ToString(dataReader["selectedSize1"]) : string.Empty;
                                        default_color_name = dataReader["default_color_name"] != DBNull.Value ? Convert.ToString(dataReader["default_color_name"]) : string.Empty;
                                        default_color_code = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;

                                        //LivePrice
                                        if (ItemIsSRP == "Y")
                                        {
                                            CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                            cartitempricedetaillistparams.DataID = DataId;
                                            cartitempricedetaillistparams.ItemID = item_id;
                                            cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                            cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                            if (cartItemPriceDetailList_gold.Count > 0)
                                            {
                                                dist_price = cartItemPriceDetailList_gold[0].dp_final_price;
                                            }

                                            CartItemDPRPCALCListingParams cartitemDPRPCALClistparams = new CartItemDPRPCALCListingParams();
                                            cartitemDPRPCALClistparams.DataID = DataId;
                                            cartitemDPRPCALClistparams.MRP = item_mrp;
                                            cartItemDPRPCALCList = objHelpers.Get_DP_RP_Calculation(cartitemDPRPCALClistparams);

                                            if (cartItemDPRPCALCList.Count > 0)
                                            {
                                                item_price = cartItemDPRPCALCList[0].R_Price;
                                            }
                                        }
                                    }

                                    // item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);
                                    item_mrp = Math.Round(dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock_KisnaPremium(item_code, MstType);
                                    }

                                    akshayagoldItemList.Add(new AkshayaGoldItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        item_kt = item_kt,
                                        ApproxDeliveryDate = ApproxDeliveryDate,
                                        item_brand_text = item_brand_text,
                                        ItemIsSRP = ItemIsSRP,
                                        sub_category_id = sub_category_id.ToString(),
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        mostOrder = mostOrder.ToString(),
                                        isStockFilter = isStockFilter,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        plaingold_status = plaingold_status,
                                        ItemAproxDayCommonID = ItemAproxDayCommonID.ToString(),
                                        item_price = item_price.ToString(),
                                        dist_price = dist_price.ToString(),
                                        item_text = item_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                        //item_images_color = itemImageColorList,
                                    });
                                }
                            }
                        }
                    }
                }
                if (akshayagoldItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = akshayagoldItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<AkshayaGoldItemListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<AkshayaGoldItemListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetNewDevelopmentItemList(NewDevelopmentItemListingParams newdevelopmentitemlistparams)
        {
            int current_page = newdevelopmentitemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<NewDevelopmentItemListing> newdevelopmentItemList = new List<NewDevelopmentItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.NEWDEVELOPMENT_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        //ParseMinMax(newdevelopmentitemlistparams.amount, out PriceMin, out PriceMax);
                        string reqamount = newdevelopmentitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        //ParseMinMax(newdevelopmentitemlistparams.metalwt, out MetalWtMin, out MetalWtMax);
                        string reqmetalwt = newdevelopmentitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        //ParseMinMax(newdevelopmentitemlistparams.diawt, out DiaWtMin, out DiaWtMax);
                        string reqdiawt = newdevelopmentitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        // Default values if no sorting or mode
                        string SortIds = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.sort_id) ? "" : newdevelopmentitemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.variant) ? "Y" : newdevelopmentitemlistparams.variant;
                        string mode = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.mode) ? "off" : newdevelopmentitemlistparams.mode;

                        int Page = newdevelopmentitemlistparams.page > 0 ? newdevelopmentitemlistparams.page - 1 : 0;
                        int Limit = newdevelopmentitemlistparams.default_limit_app_page > 0 ? newdevelopmentitemlistparams.default_limit_app_page : 20;
                        int DataId = newdevelopmentitemlistparams.data_id > 0 ? newdevelopmentitemlistparams.data_id : 0;
                        int DataLoginTypeID = newdevelopmentitemlistparams.data_login_type > 0 ? newdevelopmentitemlistparams.data_login_type : 0;
                        string ItemName = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.item_name) ? "" : newdevelopmentitemlistparams.item_name;

                        int CategoryID = newdevelopmentitemlistparams.category_id > 0 ? newdevelopmentitemlistparams.category_id : 0;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.sub_category_id) ? "" : newdevelopmentitemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.dsg_size) ? "" : newdevelopmentitemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.dsg_kt) ? "" : newdevelopmentitemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.dsg_color) ? "" : newdevelopmentitemlistparams.dsg_color;
                        int ItemID = newdevelopmentitemlistparams.Item_ID > 0 ? newdevelopmentitemlistparams.Item_ID : 0;

                        string isStockFilterParams = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.Stock_Av) ? "N" : newdevelopmentitemlistparams.Stock_Av;
                        string Stock_Av = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.Stock_Av) ? "" : newdevelopmentitemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.Family_Av) ? "" : newdevelopmentitemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.Regular_Av) ? "" : newdevelopmentitemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.wearit) ? "" : newdevelopmentitemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.tryon) ? "" : newdevelopmentitemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.gender_id) ? "" : newdevelopmentitemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.item_tag) ? "" : newdevelopmentitemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.brand) ? "" : newdevelopmentitemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.delivery_days) ? "" : newdevelopmentitemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.ItemSubCtgIDs) ? "" : newdevelopmentitemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.ItemSubSubCtgIDs) ? "" : newdevelopmentitemlistparams.ItemSubSubCtgIDs;
                        string ItemSubCategoryIDs = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.item_sub_category_id) ? "" : newdevelopmentitemlistparams.item_sub_category_id;

                        string SalesLocation = string.IsNullOrWhiteSpace(newdevelopmentitemlistparams.sales_location) ? "" : newdevelopmentitemlistparams.sales_location;
                        int DesignTimeLine = newdevelopmentitemlistparams.design_timeline > 0 ? newdevelopmentitemlistparams.design_timeline : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@isStockFilter", isStockFilterParams);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@ItemSubCategoryIDs", ItemSubCategoryIDs);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_id"]) : 0;
                                    int category_id = dataReader["category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["category_id"]) : 0;
                                    string item_description = dataReader["item_description"] != DBNull.Value ? Convert.ToString(dataReader["item_description"]) : string.Empty;
                                    string item_code = dataReader["item_code"] != DBNull.Value ? Convert.ToString(dataReader["item_code"]) : string.Empty;
                                    string item_name = dataReader["item_name"] != DBNull.Value ? Convert.ToString(dataReader["item_name"]) : string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemGenderCommonID"]) : 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(dataReader["ItemNosePinScrewSts"]) : string.Empty;
                                    string item_kt = dataReader["item_kt"] != DBNull.Value ? Convert.ToString(dataReader["item_kt"]) : string.Empty;
                                    string ApproxDeliveryDate = dataReader["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(dataReader["ApproxDeliveryDate"]) : string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] != DBNull.Value ? Convert.ToString(dataReader["item_brand_text"]) : string.Empty;
                                    string ItemIsSRP = dataReader["ItemIsSRP"] != DBNull.Value ? Convert.ToString(dataReader["ItemIsSRP"]) : string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["sub_category_id"]) : 0;
                                    string priceflag = dataReader["priceflag"] != DBNull.Value ? Convert.ToString(dataReader["priceflag"]) : string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] != DBNull.Value ? Convert.ToInt32(dataReader["max_qty_sold"]) : 0;
                                    string image_path = dataReader["image_path"] != DBNull.Value ? Convert.ToString(dataReader["image_path"]) : string.Empty;
                                    int mostOrder = dataReader["mostOrder"] != DBNull.Value ? Convert.ToInt32(dataReader["mostOrder"]) : 0;
                                    string isStockFilter = dataReader["isStockFilter"] != DBNull.Value ? Convert.ToString(dataReader["isStockFilter"]) : string.Empty;
                                    string item_color = dataReader["item_color"] != DBNull.Value ? Convert.ToString(dataReader["item_color"]) : string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemMetalCommonID"]) : 0;
                                    string item_soliter = dataReader["item_soliter"] != DBNull.Value ? Convert.ToString(dataReader["item_soliter"]) : string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] != DBNull.Value ? Convert.ToString(dataReader["plaingold_status"]) : string.Empty;
                                    int ItemAproxDayCommonID = dataReader["ItemAproxDayCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemAproxDayCommonID"]) : 0;
                                    decimal item_price = dataReader["item_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_price"]) : 0;
                                    decimal dist_price = dataReader["dist_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["dist_price"]) : 0;
                                    string item_text = dataReader["item_text"] != DBNull.Value ? Convert.ToString(dataReader["item_text"]) : string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] != DBNull.Value ? Convert.ToString(dataReader["rupy_symbol"]) : string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] != DBNull.Value ? Convert.ToString(dataReader["img_watch_title"]) : string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wearit_title"]) : string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wish_title"]) : string.Empty;
                                    string wearit_text = dataReader["wearit_text"] != DBNull.Value ? Convert.ToString(dataReader["wearit_text"]) : string.Empty;
                                    decimal cart_price = dataReader["cart_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["cart_price"]) : 0;
                                    int variantCount = dataReader["variantCount"] != DBNull.Value ? Convert.ToInt32(dataReader["variantCount"]) : 0;
                                    int item_color_id = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;
                                    int cart_id = dataReader["cart_id"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_id"]) : 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_item_qty"]) : 0;
                                    int wish_count = dataReader["wish_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wish_count"]) : 0;
                                    int wearit_count = dataReader["wearit_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wearit_count"]) : 0;

                                    last_page = dataReader["last_page"] != DBNull.Value ? Convert.ToInt32(dataReader["last_page"]) : 0;
                                    total_items = dataReader["total_items"] != DBNull.Value ? Convert.ToInt32(dataReader["total_items"]) : 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] != DBNull.Value ? Convert.ToString(dataReader["MstType"]) : string.Empty;

                                    // COLOR SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // IMAGE COLORS
                                    List<Item_images_colorListing> itemImageColorList = new List<Item_images_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    // ItemLivePriceCalculation
                                    IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();

                                    // DP_RP_Calculation
                                    IList<CartItemDPRPCALCListing> cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;

                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            // $data_collection1
                                            int CATEGORY_EARRING_COMMON_ID = 21;
                                            if (category_id == CATEGORY_EARRING_COMMON_ID)
                                            {
                                                IList<ItemSelectedSizeList> itemSelectedSize = new List<ItemSelectedSizeList>();
                                                itemSelectedSize = objHelpers.GetSelectedSizeByCollectionList(item_id, MstType);
                                                if (itemSelectedSize.Any())
                                                {
                                                    int tmp_selectedSize = itemSelectedSize[0].selectedSize;
                                                    int tmp_flag_exclude_sizes = itemSelectedSize[0].flag_exclude_sizes;
                                                    string tmp_exclude_sizes = itemSelectedSize[0].exclude_sizes;

                                                    if (tmp_flag_exclude_sizes == 1)
                                                    {
                                                        ExcludeSizes = tmp_exclude_sizes;
                                                    }
                                                    selectedSize = tmp_selectedSize;
                                                }
                                            }
                                            // $data_collection1

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] != DBNull.Value ? Convert.ToString(dataReader["color_name"]) : string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] != DBNull.Value ? Convert.ToString(dataReader["selectedColor1"]) : string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] != DBNull.Value ? Convert.ToString(dataReader["selectedSize1"]) : string.Empty;
                                        default_color_name = dataReader["default_color_name"] != DBNull.Value ? Convert.ToString(dataReader["default_color_name"]) : string.Empty;
                                        default_color_code = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;

                                        //LivePrice
                                        if (ItemIsSRP == "Y")
                                        {
                                            CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                            cartitempricedetaillistparams.DataID = DataId;
                                            cartitempricedetaillistparams.ItemID = item_id;
                                            cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                            cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                            if (cartItemPriceDetailList_gold.Count > 0)
                                            {
                                                dist_price = cartItemPriceDetailList_gold[0].dp_final_price;
                                            }

                                            CartItemDPRPCALCListingParams cartitemDPRPCALClistparams = new CartItemDPRPCALCListingParams();
                                            cartitemDPRPCALClistparams.DataID = DataId;
                                            cartitemDPRPCALClistparams.MRP = item_mrp;
                                            cartItemDPRPCALCList = objHelpers.Get_DP_RP_Calculation(cartitemDPRPCALClistparams);

                                            if (cartItemDPRPCALCList.Count > 0)
                                            {
                                                item_price = cartItemDPRPCALCList[0].R_Price;
                                            }
                                        }
                                    }

                                    // item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);
                                    item_mrp = Math.Round(dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock_KisnaPremium(item_code, MstType);
                                    }

                                    newdevelopmentItemList.Add(new NewDevelopmentItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        item_kt = item_kt,
                                        ApproxDeliveryDate = ApproxDeliveryDate,
                                        item_brand_text = item_brand_text,
                                        ItemIsSRP = ItemIsSRP,
                                        sub_category_id = sub_category_id.ToString(),
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        mostOrder = mostOrder.ToString(),
                                        isStockFilter = isStockFilter,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        plaingold_status = plaingold_status,
                                        ItemAproxDayCommonID = ItemAproxDayCommonID.ToString(),
                                        item_price = item_price.ToString(),
                                        dist_price = dist_price.ToString(),
                                        item_text = item_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                        //item_images_color = itemImageColorList,
                                    });
                                }
                            }
                        }
                    }
                }
                if (newdevelopmentItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = newdevelopmentItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<NewDevelopmentItemListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<NewDevelopmentItemListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetTinnyWondersItemList(TinnyWondersItemListingParams tinnywondersitemlistparams)
        {
            int current_page = tinnywondersitemlistparams.page;
            int last_page = 1;
            int total_items = 0;
            var responseDetails = new ResponseDetails();

            IList<TinnyWondersItemListing> tinnywondersItemList = new List<TinnyWondersItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.TINNYWONDERS_ITEMLIST;
                    dbConnection.Open();

                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        // Helper function to parse comma-separated strings into Min/Max decimal values
                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Count > 1)
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Count > 0 ? list[0] : 0;
                        }

                        // Parsing input parameters
                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        // Price Range
                        //ParseMinMax(tinnywondersitemlistparams.amount, out PriceMin, out PriceMax);
                        string reqamount = tinnywondersitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        //ParseMinMax(tinnywondersitemlistparams.metalwt, out MetalWtMin, out MetalWtMax);
                        string reqmetalwt = tinnywondersitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        //ParseMinMax(tinnywondersitemlistparams.diawt, out DiaWtMin, out DiaWtMax);
                        string reqdiawt = tinnywondersitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        // Default values if no sorting or mode
                        string SortIds = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.sort_id) ? "" : tinnywondersitemlistparams.sort_id;
                        string Variant = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.variant) ? "Y" : tinnywondersitemlistparams.variant;
                        string mode = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.mode) ? "off" : tinnywondersitemlistparams.mode;

                        int Page = tinnywondersitemlistparams.page > 0 ? tinnywondersitemlistparams.page - 1 : 0;
                        int Limit = tinnywondersitemlistparams.default_limit_app_page > 0 ? tinnywondersitemlistparams.default_limit_app_page : 20;
                        int DataId = tinnywondersitemlistparams.data_id > 0 ? tinnywondersitemlistparams.data_id : 0;
                        int DataLoginTypeID = tinnywondersitemlistparams.data_login_type > 0 ? tinnywondersitemlistparams.data_login_type : 0;
                        string ItemName = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.item_name) ? "" : tinnywondersitemlistparams.item_name;

                        int CategoryID = tinnywondersitemlistparams.category_id > 0 ? tinnywondersitemlistparams.category_id : 0;

                        string SubCategoryIDs = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.sub_category_id) ? "" : tinnywondersitemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.dsg_size) ? "" : tinnywondersitemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.dsg_kt) ? "" : tinnywondersitemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.dsg_color) ? "" : tinnywondersitemlistparams.dsg_color;
                        int ItemID = tinnywondersitemlistparams.Item_ID > 0 ? tinnywondersitemlistparams.Item_ID : 0;

                        string isStockFilterParams = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.Stock_Av) ? "N" : tinnywondersitemlistparams.Stock_Av;
                        string Stock_Av = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.Stock_Av) ? "" : tinnywondersitemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.Family_Av) ? "" : tinnywondersitemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.Regular_Av) ? "" : tinnywondersitemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.wearit) ? "" : tinnywondersitemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.tryon) ? "" : tinnywondersitemlistparams.tryon;

                        string Genders = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.gender_id) ? "" : tinnywondersitemlistparams.gender_id;
                        string TagWiseFilters = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.item_tag) ? "" : tinnywondersitemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.brand) ? "" : tinnywondersitemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.delivery_days) ? "" : tinnywondersitemlistparams.delivery_days;
                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.ItemSubCtgIDs) ? "" : tinnywondersitemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.ItemSubSubCtgIDs) ? "" : tinnywondersitemlistparams.ItemSubSubCtgIDs;
                        string ItemSubCategoryIDs = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.item_sub_category_id) ? "" : tinnywondersitemlistparams.item_sub_category_id;

                        string SalesLocation = string.IsNullOrWhiteSpace(tinnywondersitemlistparams.sales_location) ? "" : tinnywondersitemlistparams.sales_location;
                        int DesignTimeLine = tinnywondersitemlistparams.design_timeline > 0 ? tinnywondersitemlistparams.design_timeline : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@isStockFilter", isStockFilterParams);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", SalesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", DesignTimeLine);
                        cmd.Parameters.AddWithValue("@ItemSubCategoryIDs", ItemSubCategoryIDs);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int item_id = dataReader["item_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_id"]) : 0;
                                    int category_id = dataReader["category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["category_id"]) : 0;
                                    string item_description = dataReader["item_description"] != DBNull.Value ? Convert.ToString(dataReader["item_description"]) : string.Empty;
                                    string item_code = dataReader["item_code"] != DBNull.Value ? Convert.ToString(dataReader["item_code"]) : string.Empty;
                                    string item_name = dataReader["item_name"] != DBNull.Value ? Convert.ToString(dataReader["item_name"]) : string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemGenderCommonID"]) : 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(dataReader["ItemNosePinScrewSts"]) : string.Empty;
                                    string ApproxDeliveryDate = dataReader["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(dataReader["ApproxDeliveryDate"]) : string.Empty;
                                    string item_brand_text = dataReader["item_brand_text"] != DBNull.Value ? Convert.ToString(dataReader["item_brand_text"]) : string.Empty;
                                    int sub_category_id = dataReader["sub_category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["sub_category_id"]) : 0;
                                    string priceflag = dataReader["priceflag"] != DBNull.Value ? Convert.ToString(dataReader["priceflag"]) : string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0;
                                    int max_qty_sold = dataReader["max_qty_sold"] != DBNull.Value ? Convert.ToInt32(dataReader["max_qty_sold"]) : 0;
                                    string image_path = dataReader["image_path"] != DBNull.Value ? Convert.ToString(dataReader["image_path"]) : string.Empty;
                                    int mostOrder = dataReader["mostOrder"] != DBNull.Value ? Convert.ToInt32(dataReader["mostOrder"]) : 0;
                                    string isStockFilter = dataReader["isStockFilter"] != DBNull.Value ? Convert.ToString(dataReader["isStockFilter"]) : string.Empty;
                                    string item_color = dataReader["item_color"] != DBNull.Value ? Convert.ToString(dataReader["item_color"]) : string.Empty;
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemMetalCommonID"]) : 0;
                                    string item_soliter = dataReader["item_soliter"] != DBNull.Value ? Convert.ToString(dataReader["item_soliter"]) : string.Empty;
                                    string plaingold_status = dataReader["plaingold_status"] != DBNull.Value ? Convert.ToString(dataReader["plaingold_status"]) : string.Empty;
                                    int ItemAproxDayCommonID = dataReader["ItemAproxDayCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemAproxDayCommonID"]) : 0;
                                    string item_text = dataReader["item_text"] != DBNull.Value ? Convert.ToString(dataReader["item_text"]) : string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] != DBNull.Value ? Convert.ToString(dataReader["rupy_symbol"]) : string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] != DBNull.Value ? Convert.ToString(dataReader["img_watch_title"]) : string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wearit_title"]) : string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] != DBNull.Value ? Convert.ToString(dataReader["img_wish_title"]) : string.Empty;
                                    string wearit_text = dataReader["wearit_text"] != DBNull.Value ? Convert.ToString(dataReader["wearit_text"]) : string.Empty;
                                    decimal cart_price = dataReader["cart_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["cart_price"]) : 0;
                                    int variantCount = dataReader["variantCount"] != DBNull.Value ? Convert.ToInt32(dataReader["variantCount"]) : 0;
                                    int item_color_id = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;
                                    int cart_id = dataReader["cart_id"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_id"]) : 0;
                                    int cart_item_qty = dataReader["cart_item_qty"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_item_qty"]) : 0;
                                    int wish_count = dataReader["wish_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wish_count"]) : 0;
                                    int wearit_count = dataReader["wearit_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wearit_count"]) : 0;

                                    last_page = dataReader["last_page"] != DBNull.Value ? Convert.ToInt32(dataReader["last_page"]) : 0;
                                    total_items = dataReader["total_items"] != DBNull.Value ? Convert.ToInt32(dataReader["total_items"]) : 0;

                                    string color_name = "";
                                    string selectedColor1 = "";
                                    string selectedSize1 = "";
                                    string default_color_name = "";
                                    int default_color_code = 0;

                                    string MstType = dataReader["MstType"] != DBNull.Value ? Convert.ToString(dataReader["MstType"]) : string.Empty;

                                    // COLOR SIZELIST
                                    List<Item_ColorSizeListing> itemColorSizeList = new List<Item_ColorSizeListing>();

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();

                                    // COLOR LIST
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    // IMAGE COLORS
                                    List<Item_images_colorListing> itemImageColorList = new List<Item_images_colorListing>();

                                    // Category Mapping LIST
                                    IList<ItemCategoryMappingList> itemCategoryList = new List<ItemCategoryMappingList>();

                                    // Product Size LIST
                                    List<Item_sizeListing> itemSizeList = new List<Item_sizeListing>();

                                    // ItemLivePriceCalculation
                                    IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();

                                    // DP_RP_Calculation
                                    IList<CartItemDPRPCALCListing> cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

                                    string CategoryCommonID = "", field_name = "";
                                    int selectedSize = 0;

                                    if (mode == "on")
                                    {
                                        // COLOR SIZELIST (itemsColorSizeList)
                                        Item_ColorSizeListingParams itemcolorsizelistparams = new Item_ColorSizeListingParams();
                                        itemcolorsizelistparams.itemid = item_id;
                                        itemcolorsizelistparams.CartID = cart_id;
                                        itemColorSizeList = objHelpers.GetItemsColorSizeList(itemcolorsizelistparams);

                                        // ORDER INSTRUCTION LIST (itemOrderInstructionList)
                                        itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();

                                        // ORDER CUSTOM INSTRUCTION LIST (itemOrderCustomInstructionList)
                                        itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();

                                        // COLOR LIST (colorList)
                                        CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                        cartitemcolorlistparams.item_id = item_id;
                                        cartitemcolorlistparams.default_color_code = item_color;
                                        cartitemcolorlistparams.metalid = ItemMetalCommonID;
                                        cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);

                                        // categoryMappings
                                        itemCategoryList = objHelpers.GetItemCategoryMappingKisnaPremiumList(ItemGenderCommonID, ItemNosePinScrewSts);
                                        var filteredCategory = itemCategoryList.Where(p => p.category_id.ToString() == category_id.ToString()).ToList();
                                        if (filteredCategory.Count > 0)
                                        {
                                            CategoryCommonID = filteredCategory[0].size_common_id;
                                            field_name = filteredCategory[0].field_name;
                                            selectedSize = filteredCategory[0].default_size;

                                            string ExcludeSizes = filteredCategory[0].exclude_sizes as string ?? string.Empty;

                                            // $data_collection1
                                            int CATEGORY_EARRING_COMMON_ID = 21;
                                            if (category_id == CATEGORY_EARRING_COMMON_ID)
                                            {
                                                IList<ItemSelectedSizeList> itemSelectedSize = new List<ItemSelectedSizeList>();
                                                itemSelectedSize = objHelpers.GetSelectedSizeByCollectionList(item_id, MstType);
                                                if (itemSelectedSize.Any())
                                                {
                                                    int tmp_selectedSize = itemSelectedSize[0].selectedSize;
                                                    int tmp_flag_exclude_sizes = itemSelectedSize[0].flag_exclude_sizes;
                                                    string tmp_exclude_sizes = itemSelectedSize[0].exclude_sizes;

                                                    if (tmp_flag_exclude_sizes == 1)
                                                    {
                                                        ExcludeSizes = tmp_exclude_sizes;
                                                    }
                                                    selectedSize = tmp_selectedSize;
                                                }
                                            }
                                            // $data_collection1

                                            Items_sizeListingParams itemsSizelistparams = new Items_sizeListingParams();
                                            itemsSizelistparams.ItemID = item_id;
                                            itemsSizelistparams.CategoryCommonID = CategoryCommonID;
                                            itemsSizelistparams.ExcludeSizes = "";

                                            itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            if (itemSizeList.Count == 0)
                                            {
                                                itemsSizelistparams.ExcludeSizes = ExcludeSizes;
                                                itemSizeList = objHelpers.GetItemsSizeList(itemsSizelistparams);
                                            }
                                        }

                                        color_name = dataReader["color_name"] != DBNull.Value ? Convert.ToString(dataReader["color_name"]) : string.Empty;
                                        selectedColor1 = dataReader["selectedColor1"] != DBNull.Value ? Convert.ToString(dataReader["selectedColor1"]) : string.Empty;
                                        selectedSize1 = dataReader["selectedSize1"] != DBNull.Value ? Convert.ToString(dataReader["selectedSize1"]) : string.Empty;
                                        default_color_name = dataReader["default_color_name"] != DBNull.Value ? Convert.ToString(dataReader["default_color_name"]) : string.Empty;
                                        default_color_code = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;
                                    }

                                    // item_mrp = Math.Round(dataReader["item_mrp"] as decimal? ?? 0, 0);
                                    item_mrp = Math.Round(dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0, 0);

                                    // PRODUCT TAGS
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    itemtaglistparams.item_id = item_id;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);

                                    // isInFranchiseStore
                                    string isInFranchiseStore = "N";
                                    if (MstType == "F")
                                    {
                                        isInFranchiseStore = objHelpers.GetIsInFranchiseStock_KisnaPremium(item_code, MstType);
                                    }

                                    tinnywondersItemList.Add(new TinnyWondersItemListing
                                    {
                                        item_id = item_id.ToString(),
                                        category_id = category_id.ToString(),
                                        item_description = item_description,
                                        item_code = item_code,
                                        item_name = item_name,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        ApproxDeliveryDate = ApproxDeliveryDate,
                                        item_brand_text = item_brand_text,
                                        sub_category_id = sub_category_id.ToString(),
                                        priceflag = priceflag,
                                        item_mrp = item_mrp.ToString(),
                                        max_qty_sold = max_qty_sold.ToString(),
                                        image_path = image_path,
                                        mostOrder = mostOrder.ToString(),
                                        isStockFilter = isStockFilter,
                                        item_color = item_color,
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_soliter = item_soliter,
                                        plaingold_status = plaingold_status,
                                        ItemAproxDayCommonID = ItemAproxDayCommonID.ToString(),
                                        item_text = item_text,
                                        rupy_symbol = rupy_symbol,
                                        img_watch_title = img_watch_title,
                                        img_wearit_title = img_wearit_title,
                                        img_wish_title = img_wish_title,
                                        wearit_text = wearit_text,
                                        cart_price = cart_price.ToString(),
                                        variantCount = variantCount.ToString(),
                                        item_color_id = item_color_id.ToString(),
                                        cart_id = cart_id.ToString(),
                                        cart_item_qty = cart_item_qty.ToString(),
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),

                                        field_name = field_name,
                                        selectedSize = selectedSize,
                                        sizeList = itemSizeList,
                                        color_name = color_name,
                                        itemsColorSizeList = itemColorSizeList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        selectedColor1 = selectedColor1,
                                        selectedSize1 = selectedSize1,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        colorList = cartItemColorList,
                                        productTags = itemTagList,
                                        isInFranchiseStore = isInFranchiseStore,
                                        //item_images_color = itemImageColorList,
                                    });
                                }
                            }
                        }
                    }
                }
                if (tinnywondersItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = tinnywondersItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.current_page = current_page.ToString();
                    responseDetails.last_page = last_page.ToString();
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.data = new List<TinnyWondersItemListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.current_page = current_page.ToString();
                responseDetails.last_page = "1";
                responseDetails.total_items = "0";
                responseDetails.data = new List<TinnyWondersItemListing>();
                return responseDetails;
            }
        }
        public List<T> ExecuteStoredProcedure<T>(
            string storedProcedureName,
            CommonItemFilterParams filterParams,
            Func<SqlDataReader, T> mapRow)
        {
            var result = new List<T>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@data_id", filterParams.data_id > 0 ? filterParams.data_id : 0);
                        cmd.Parameters.AddWithValue("@CategoryID", filterParams.category_id > 0 ? filterParams.category_id : 0);
                        cmd.Parameters.AddWithValue("@MasterCommonId", filterParams.master_common_id > 0 ? filterParams.master_common_id : 0);
                        cmd.Parameters.AddWithValue("@button_code", string.IsNullOrWhiteSpace(filterParams.button_code) ? "" : filterParams.button_code);
                        cmd.Parameters.AddWithValue("@btn_cd", string.IsNullOrWhiteSpace(filterParams.btn_cd) ? "" : filterParams.btn_cd);

                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(mapRow(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing {storedProcedureName}: {ex.Message}");
            }

            return result;
        }

        public List<CommonItemFilterDsgKt> GetFilterData_DsgKt(CommonItemFilterParams commonitemfilterparams)
        {
            return ExecuteStoredProcedure(DBCommands.COMMONITEMFILTER_DSGKT, commonitemfilterparams, reader => new CommonItemFilterDsgKt
            {
                kt = reader["kt"] != DBNull.Value ? Convert.ToString(reader["kt"]) : string.Empty,
                Kt_Count = reader["Kt_Count"] != DBNull.Value ? Convert.ToInt32(reader["Kt_Count"]).ToString() : "0"
            });
        }

        private string FormatDecimalValue(object value, string defaultValue = "0.01")
        {
            if (value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()) || value.ToString() == "0.0")
                return defaultValue;

            if (decimal.TryParse(value.ToString(), out decimal parsedValue) && parsedValue > 0)
                return parsedValue.ToString("F2", CultureInfo.InvariantCulture);

            return defaultValue;
        }

        public List<CommonItemFilterDsgAmount> GetFilterData_DsgAmount(CommonItemFilterParams filterParams)
        {
            return ExecuteStoredProcedure(DBCommands.COMMONITEMFILTER_DSGAMOUNT, filterParams, reader => new CommonItemFilterDsgAmount
            {
                minprice = FormatDecimalValue(reader["minprice"]),
                maxprice = FormatDecimalValue(reader["maxprice"])
            });
        }

        public List<CommonItemFilterDsgMetalWt> GetFilterData_DsgMetalWt(CommonItemFilterParams filterParams)
        {
            return ExecuteStoredProcedure(DBCommands.COMMONITEMFILTER_DSGMETALWT, filterParams, reader => new CommonItemFilterDsgMetalWt
            {
                minmetalwt = FormatDecimalValue(reader["minmetalwt"], "1"),
                maxmetalwt = FormatDecimalValue(reader["maxmetalwt"], "1")
            });
        }

        public List<CommonItemFilterDsgDiamondWt> GetFilterData_DsgDiamondWt(CommonItemFilterParams filterParams)
        {
            return ExecuteStoredProcedure(DBCommands.COMMONITEMFILTER_DSGDIAMONDWT, filterParams, reader => new CommonItemFilterDsgDiamondWt
            {
                mindiawt = FormatDecimalValue(reader["mindiawt"]),
                maxdiawt = FormatDecimalValue(reader["maxdiawt"])
            });
        }

        public List<CommonItemFilterProductTag> GetFilterData_ProductTag(CommonItemFilterParams filterParams)
        {
            return ExecuteStoredProcedure(DBCommands.COMMONITEMFILTER_PRODUCTTAGS, filterParams, reader => new CommonItemFilterProductTag
            {
                tag_name = reader["tag_name"] != DBNull.Value ? Convert.ToString(reader["tag_name"]) : string.Empty,
                tag_count = reader["tag_count"] != DBNull.Value ? Convert.ToInt32(reader["tag_count"]).ToString() : "0"
            });
        }

        public List<CommonItemFilterBrand> GetFilterData_Brand(CommonItemFilterParams filterParams)
        {
            return ExecuteStoredProcedure(DBCommands.COMMONITEMFILTER_BRAND, filterParams, reader => new CommonItemFilterBrand
            {
                brand_id = reader["brand_id"] != DBNull.Value ? Convert.ToInt32(reader["brand_id"]).ToString() : "0",
                brand_name = reader["brand_name"] != DBNull.Value ? Convert.ToString(reader["brand_name"]) : string.Empty,
                brand_count = reader["brand_brand_countid"] != DBNull.Value ? Convert.ToInt32(reader["brand_count"]).ToString() : "0",
            });
        }

        public List<CommonItemFilterGender> GetFilterData_Gender(CommonItemFilterParams filterParams)
        {
            return ExecuteStoredProcedure(DBCommands.COMMONITEMFILTER_GENDER, filterParams, reader => new CommonItemFilterGender
            {
                gender_id = reader["gender_id"] != DBNull.Value ? Convert.ToInt32(reader["gender_id"]).ToString() : "0",
                gender_name = reader["gender_name"] != DBNull.Value ? Convert.ToString(reader["gender_name"]) : string.Empty,
                gender_count = reader["gender_count"] != DBNull.Value ? Convert.ToInt32(reader["gender_count"]).ToString() : "0",
            });
        }

        public List<CommonItemFilterApproxDelivery> GetFilterData_ApproxDelivery(CommonItemFilterParams filterParams)
        {
            return ExecuteStoredProcedure(DBCommands.COMMONITEMFILTER_APPROXDELIVERY, filterParams, reader => new CommonItemFilterApproxDelivery
            {
                ItemAproxDay = reader["ItemAproxDay"] != DBNull.Value ? Convert.ToString(reader["ItemAproxDay"]) : string.Empty,
                ItemAproxDay_count = reader["ItemAproxDay_count"] != DBNull.Value ? Convert.ToInt32(reader["ItemAproxDay_count"]).ToString() : "0",
            });
        }

        private List<T> MapDataTable<T>(DataTable table, Func<DataRow, T> mapFunc)
        {
            var result = new List<T>();
            if (table == null) return result;

            foreach (DataRow row in table.Rows)
            {
                try
                {
                    result.Add(mapFunc(row));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Mapping error: {ex.Message}");
                }
            }
            return result;
        }

        private CommonItemFilterStock MapStock(DataRow row) => new CommonItemFilterStock
        {
            stock_name = row["stock_name"]?.ToString(),
            stock_id = row["stock_id"]?.ToString()
        };

        private CommonItemFilterNameValue MapNameValue(DataRow row) => new CommonItemFilterNameValue
        {
            Name = row["Name"]?.ToString(),
            Value = row["Value"]?.ToString()
        };

        private CommonItemFilterFamilyProduct MapFamilyProduct(DataRow row) => new CommonItemFilterFamilyProduct
        {
            familyproduct_name = row["familyproduct_name"]?.ToString(),
            familyproduct_id = row["familyproduct_id"]?.ToString()
        };

        private CommonItemFilterExcludeDiscontinue MapExcludeDiscontinue(DataRow row) => new CommonItemFilterExcludeDiscontinue
        {
            excludediscontinue_name = row["excludediscontinue_name"]?.ToString(),
            excludediscontinue_id = row["excludediscontinue_id"]?.ToString()
        };

        private CommonItemFilterView MapViewFilter(DataRow row) => new CommonItemFilterView
        {
            view_name = row["view_name"]?.ToString(),
            view_id = row["view_id"]?.ToString()
        };

        public async Task<ResponseDetails> CommonItemFilterList(CommonItemFilterParams commonitemfilterparams)
        {
            var responseDetails = new ResponseDetails();
            var filterData = new CommonItemFilterData();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    using (var cmd = new SqlCommand(DBCommands.COMMONITEMFILTER_SUBCATEGORY, dbConnection))
                    {
                        dbConnection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", commonitemfilterparams.data_id);
                        cmd.Parameters.AddWithValue("@CategoryID", commonitemfilterparams.category_id);
                        cmd.Parameters.AddWithValue("@MasterCommonId", commonitemfilterparams.master_common_id);
                        cmd.Parameters.AddWithValue("@button_code", commonitemfilterparams.button_code ?? string.Empty);
                        cmd.Parameters.AddWithValue("@btn_cd", commonitemfilterparams.btn_cd ?? string.Empty);

                        var ds = new DataSet();
                        new SqlDataAdapter(cmd).Fill(ds);

                        // SubCategory
                        var subCategories = MapDataTable(ds.Tables[0], row => new CommonItemFilterSubCategory
                        {
                            category_id = row["category_id"]?.ToString(),
                            sub_category_id = row["sub_category_id"]?.ToString(),
                            sub_category_name = row["sub_category_name"]?.ToString(),
                            category_count = row["category_count"]?.ToString()
                        });
                        filterData.sub_category.data = subCategories;

                        if (subCategories.Any())
                        {
                            //dsg kt
                            filterData.dsg_kt.data = GetFilterData_DsgKt(commonitemfilterparams);

                            //price
                            var priceRange = GetFilterData_DsgAmount(commonitemfilterparams).FirstOrDefault();
                            if (priceRange != null)
                            {
                                filterData.dsg_amount.Add(new DsgAmountFilter { amount = priceRange.minprice });
                                filterData.dsg_amount.Add(new DsgAmountFilter { amount = priceRange.maxprice });
                            }

                            //metal wt
                            var metalWtRange = GetFilterData_DsgMetalWt(commonitemfilterparams).FirstOrDefault();
                            if (metalWtRange != null)
                            {
                                filterData.dsg_metalwt.Add(new DsgMetalWtFilter { metalwt = metalWtRange.minmetalwt });
                                filterData.dsg_metalwt.Add(new DsgMetalWtFilter { metalwt = metalWtRange.maxmetalwt });
                            }

                            //diamond wt
                            var diamondWtRange = GetFilterData_DsgDiamondWt(commonitemfilterparams).FirstOrDefault();
                            if (diamondWtRange != null)
                            {
                                filterData.dsg_diamondwt.Add(new DsgDiamondWtFilter { diamondwt = diamondWtRange.mindiawt });
                                filterData.dsg_diamondwt.Add(new DsgDiamondWtFilter { diamondwt = diamondWtRange.maxdiawt });
                            }

                            //product tag
                            filterData.productTags.data = GetFilterData_ProductTag(commonitemfilterparams);

                            //brand
                            filterData.brand.data = GetFilterData_Brand(commonitemfilterparams);

                            //gender
                            if (commonitemfilterparams.category_id == 21 || commonitemfilterparams.category_id == 22)
                                filterData.gender.data = GetFilterData_Gender(commonitemfilterparams);
                            else
                                filterData.gender = null;

                            //approx delivery
                            filterData.approx_develivery.data = GetFilterData_ApproxDelivery(commonitemfilterparams);

                            // Other filters
                            if (ds.Tables.Count > 1) filterData.stock_filter.data = MapDataTable(ds.Tables[1], MapStock);
                            if (ds.Tables.Count > 2) filterData.best_sellers_data.data = MapDataTable(ds.Tables[2], MapNameValue);
                            if (ds.Tables.Count > 3) filterData.designs_data.data = MapDataTable(ds.Tables[3], MapNameValue);
                            if (ds.Tables.Count > 4) filterData.familyproduct_filter.data = MapDataTable(ds.Tables[4], MapFamilyProduct);
                            if (ds.Tables.Count > 5) filterData.excludediscontinue_filter.data = MapDataTable(ds.Tables[5], MapExcludeDiscontinue);
                            if (ds.Tables.Count > 6) filterData.wearview_filter.data = MapDataTable(ds.Tables[6], MapViewFilter);
                            if (ds.Tables.Count > 7) filterData.tryonview_filter.data = MapDataTable(ds.Tables[7], MapViewFilter);
                        }

                        responseDetails = new ResponseDetails
                        {
                            success = subCategories.Any(),
                            status = "200",
                            message = subCategories.Any() ? "Successfully" : "No data found",
                            data = subCategories,
                            data1 = filterData
                        };

                        return responseDetails;
                    }
                }
            }
            catch (SqlException ex)
            {
                return new ResponseDetails
                {
                    success = false,
                    status = "400",
                    message = $"SQL error: {ex.Message}",
                    data = new List<CommonItemFilterSubCategory>()
                };
            }
        }

        public async Task<ResponseDetails> SoliterSubCategoryNewList(SoliterSubCategoryNewParams solitersubcategoryparams)
        {
            var responseDetails = new ResponseDetails();
            var filterData = new SoliterSubCategoryNewData();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    using (var cmd = new SqlCommand(DBCommands.SOLITER_SUBCATEGORYNEW, dbConnection))
                    {
                        //dbConnection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@CategoryID", solitersubcategoryparams.category_id);
                        cmd.Parameters.AddWithValue("@MasterCommonId", solitersubcategoryparams.master_common_id);
                        cmd.Parameters.AddWithValue("@btnCd", solitersubcategoryparams.btn_cd ?? string.Empty);

                        var ds = new DataSet();
                        new SqlDataAdapter(cmd).Fill(ds);

                        // SubCategory
                        var subCategories = MapDataTable(ds.Tables[0], row => new SoliterFilterSubCategory
                        {
                            category_id = (row["category_id"] != DBNull.Value ? Convert.ToInt32(row["category_id"]) : 0).ToString(),
                            sub_category_id = (row["sub_category_id"] != DBNull.Value ? Convert.ToInt32(row["sub_category_id"]) : 0).ToString(),
                            sub_category_name = row["sub_category_name"] != DBNull.Value ? row["sub_category_name"].ToString() : string.Empty,
                            category_count = (row["category_count"] != DBNull.Value ? Convert.ToInt32(row["category_count"]) : 0).ToString()
                        });
                        filterData.sub_category.data = subCategories;

                        if (subCategories.Any())
                        {
                            //dsg_color
                            filterData.dsg_color.data = MapDataTable(ds.Tables.Count > 1 ? ds.Tables[1] : null, row =>
                                                new SoliterFilterDsgColor { color = row["color"] != DBNull.Value ? row["color"].ToString() : string.Empty });

                            //product tag
                            filterData.productTags.data = MapDataTable(ds.Tables.Count > 2 ? ds.Tables[2] : null, row =>
                                                new SoliterFilterProductTag
                                                {
                                                    tag_name = row["tag_name"] != DBNull.Value ? row["tag_name"].ToString() : string.Empty,
                                                    tag_count = (row["tag_count"] != DBNull.Value ? Convert.ToInt32(row["tag_count"]) : 0).ToString()
                                                });

                            //brand
                            filterData.brand.data = MapDataTable(ds.Tables.Count > 3 ? ds.Tables[3] : null, row =>
                                                new SoliterFilterBrand
                                                {
                                                    brand_id = (row["brand_id"] != DBNull.Value ? Convert.ToInt32(row["brand_id"]) : 0).ToString(),
                                                    brand_name = row["brand_name"] != DBNull.Value ? row["brand_name"].ToString() : string.Empty,
                                                    brand_count = (row["brand_count"] != DBNull.Value ? Convert.ToInt32(row["brand_count"]) : 0).ToString()
                                                });

                            //item_sub_category
                            filterData.item_sub_category.data = MapDataTable(ds.Tables.Count > 4 ? ds.Tables[4] : null, row =>
                                                new SoliterFilterItemSubCategory
                                                {
                                                    sub_category_id = (row["sub_category_id"] != DBNull.Value ? Convert.ToInt32(row["sub_category_id"]) : 0).ToString(),
                                                    sub_category_name = row["sub_category_name"] != DBNull.Value ? row["sub_category_name"].ToString() : string.Empty,
                                                    sub_category_count = (row["sub_category_count"] != DBNull.Value ? Convert.ToInt32(row["sub_category_count"]) : 0).ToString()
                                                });

                            //item_sub_sub_category
                            filterData.item_sub_sub_category.data = MapDataTable(ds.Tables.Count > 5 ? ds.Tables[5] : null, row =>
                                                new SoliterFilterItemSubSubCategory
                                                {
                                                    sub_sub_category_id = (row["sub_sub_category_id"] != DBNull.Value ? Convert.ToInt32(row["sub_sub_category_id"]) : 0).ToString(),
                                                    sub_sub_category_name = row["sub_sub_category_name"] != DBNull.Value ? row["sub_sub_category_name"].ToString() : string.Empty,
                                                    sub_sub_category_count = (row["sub_sub_category_count"] != DBNull.Value ? Convert.ToInt32(row["sub_sub_category_count"]) : 0).ToString()
                                                });

                            //gender
                            filterData.gender.data = MapDataTable(ds.Tables.Count > 6 ? ds.Tables[6] : null, row =>
                                                new SoliterFilterGender
                                                {
                                                    gender_id = (row["gender_id"] != DBNull.Value ? Convert.ToInt32(row["gender_id"]) : 0).ToString(),
                                                    gender_name = row["gender_name"] != DBNull.Value ? row["gender_name"].ToString() : string.Empty,
                                                    gender_count = (row["gender_count"] != DBNull.Value ? Convert.ToInt32(row["gender_count"]) : 0).ToString()
                                                });


                        }

                        responseDetails = new ResponseDetails
                        {
                            success = subCategories.Any(),
                            status = "200",
                            message = subCategories.Any() ? "Successfully" : "No data found",
                            data = subCategories,
                            data1 = filterData
                        };

                        return responseDetails;
                    }
                }
            }
            catch (SqlException ex)
            {
                return new ResponseDetails
                {
                    success = false,
                    status = "400",
                    message = $"SQL error: {ex.Message}",
                    data = new List<CommonItemFilterSubCategory>()
                };
            }
        }

        public async Task<ResponseDetails> CategoryButtonList(CategoryButtonListingParams categorybuttonlistparams, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();

            IList<CategoryButtonListingNew> categoryButtonList = new List<CategoryButtonListingNew>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(DBCommands.CATEGORYBUTTONLIST, dbConnection))
                    {
                        int data_id = categorybuttonlistparams.data_id > 0 ? categorybuttonlistparams.data_id : 0;
                        int data_login_type = categorybuttonlistparams.data_login_type > 0 ? categorybuttonlistparams.data_login_type : 0;
                        int category_id = categorybuttonlistparams.category_id > 0 ? categorybuttonlistparams.category_id : 0;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataID", data_id);
                        cmd.Parameters.AddWithValue("@DataLoginType", data_login_type);
                        cmd.Parameters.AddWithValue("@CategoryID", category_id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categoryButtonList.Add(new CategoryButtonListingNew
                                {
                                    button_name = reader["button_name"] != DBNull.Value ? Convert.ToString(reader["button_name"]) : string.Empty,
                                    btn_cd = reader["button_cd"] != DBNull.Value ? Convert.ToString(reader["button_cd"]) : string.Empty,
                                    btn_type = reader["button_type"] != DBNull.Value ? Convert.ToString(reader["button_type"]) : string.Empty,
                                    btn_image = reader["button_image"] != DBNull.Value ? Convert.ToString(reader["button_image"]) : string.Empty,
                                });
                            }
                        }
                    }
                }

                if (categoryButtonList.Any())
                {
                    responseDetails.success = categoryButtonList.Any();
                    responseDetails.message = categoryButtonList.Any() ? "Data found" : "No data found";
                    responseDetails.status = "200";
                    if (categoryButtonList.Any())
                    {
                        responseDetails.Buttons = categoryButtonList;
                    }
                    else
                    {
                        responseDetails.Buttons = new List<CategoryButtonListingNew>();
                    }
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.Buttons = new List<CategoryButtonListingNew>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.Buttons = new List<CategoryButtonListingNew>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> HomeButtonList(HomeButtonListingParams homebuttonlistparams)
        {
            var responseDetails = new ResponseDetails();
            IList<HomeButtonListing> homeButtonList = new List<HomeButtonListing>();
            string imagepath = string.Empty;

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(DBCommands.HOMEBUTTON, dbConnection))
                    {
                        int data_id = homebuttonlistparams.data_id > 0 ? homebuttonlistparams.data_id : 0;
                        int data_login_type = homebuttonlistparams.data_login_type > 0 ? homebuttonlistparams.data_login_type : 0;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                imagepath = reader.GetSafeString("imagepath");
                                homeButtonList.Add(new HomeButtonListing
                                {
                                    button_name = reader.GetSafeString("button_name"),
                                    btn_cd = reader.GetSafeString("button_cd"),
                                    btn_type = reader.GetSafeString("button_type"),
                                    btn_image = reader.GetSafeString("button_image"),
                                });
                            }
                        }
                    }
                }

                responseDetails.success = homeButtonList.Any();
                responseDetails.message = homeButtonList.Any() ? "Successfully" : "No data found";
                responseDetails.status = "200";
                responseDetails.image = imagepath;
                responseDetails.button = homeButtonList;
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.button = new List<HomeButtonListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> SolitaireCategoryList(SoliCategoryListingParams solicategorylistparams)
        {
            var responseDetails = new ResponseDetails
            {
                success = false,
                message = "No data found",
                status = "200",
                data = new List<SoliCategoryListing>(),
                button = new List<CategoryButtonListing>()
            };

            var soliCategoryList = new List<SoliCategoryListing>();
            var categoryButtonList = new List<CategoryButtonListing>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    CommonHelpers objHelpers = new CommonHelpers();
                    using (SqlCommand cmd = new SqlCommand(DBCommands.SOLI_CATEGORY_LIST, dbConnection))
                    {
                        dbConnection.Open();
                        string display = string.IsNullOrWhiteSpace(solicategorylistparams.display) ? "" : solicategorylistparams.display;
                        string devicetype = string.IsNullOrWhiteSpace(solicategorylistparams.devicetype) ? "" : solicategorylistparams.devicetype;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@Display", display);
                        cmd.Parameters.AddWithValue("@DeviceType", devicetype);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    soliCategoryList.Add(new SoliCategoryListing
                                    {
                                        category_id = row.GetSafeInt("category_id").ToString(),
                                        category_name = row.GetSafeString("category_name"),
                                        master_common_id = row.GetSafeInt("master_common_id").ToString(),
                                        image_path = row.GetSafeString("image_path"),
                                        count = row.GetSafeInt("count").ToString()
                                    });
                                }

                                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                                {
                                    foreach (DataRow row in ds.Tables[1].Rows)
                                    {
                                        categoryButtonList.Add(new CategoryButtonListing
                                        {
                                            button_name = row.GetSafeString("button_name"),
                                            btn_cd = row.GetSafeString("btn_cd")
                                        });
                                    }
                                }

                            }
                        }
                    }
                }

                responseDetails.success = soliCategoryList.Any();
                responseDetails.message = soliCategoryList.Any() ? "Successfully" : "No data found";
                responseDetails.status = "200";
                responseDetails.data = soliCategoryList;
                responseDetails.button = categoryButtonList;

                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<SoliCategoryListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> ItemsSortByList(CategoryButtonListingParams categorybuttonlistparams, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<ItemsSortByListing> itemsSortByList = new List<ItemsSortByListing>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    using (SqlCommand cmd = new SqlCommand(DBCommands.ITEMSSORTBY, dbConnection))
                    {
                        dbConnection.Open();
                        int data_id = categorybuttonlistparams.data_id > 0 ? categorybuttonlistparams.data_id : 0;
                        int data_login_type = categorybuttonlistparams.data_login_type > 0 ? categorybuttonlistparams.data_login_type : 0;
                        int category_id = categorybuttonlistparams.category_id > 0 ? categorybuttonlistparams.category_id : 0;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@dataId", data_id);
                        cmd.Parameters.AddWithValue("@buttonCode", data_login_type);
                        cmd.Parameters.AddWithValue("@categoryId", category_id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                itemsSortByList.Add(new ItemsSortByListing
                                {
                                    sort_id = reader.GetSafeInt("sort_id").ToString(),
                                    sort_cd = reader.GetSafeString("sort_cd"),
                                    sort_name = reader.GetSafeString("sort_name"),
                                    sort_description = reader.GetSafeString("sort_description"),
                                    sort_mster_id = reader.GetSafeInt("sort_mster_id").ToString(),
                                });
                            }
                        }
                    }
                }

                responseDetails.success = itemsSortByList.Any();
                responseDetails.message = itemsSortByList.Any() ? "Successfully" : "No data found";
                responseDetails.status = "200";
                responseDetails.data = itemsSortByList;
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<ItemsSortByListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> PlaingoldSortByList()
        {
            var responseDetails = new ResponseDetails();
            IList<PlaingoldSortByListing> plaingoldSortByList = new List<PlaingoldSortByListing>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(DBCommands.PLAINGOLDSORTBYLIST, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                plaingoldSortByList.Add(new PlaingoldSortByListing
                                {
                                    sort_id = reader.GetSafeInt("sort_id").ToString(),
                                    sort_cd = reader.GetSafeString("sort_cd"),
                                    sort_name = reader.GetSafeString("sort_name"),
                                    sort_description = reader.GetSafeString("sort_description"),
                                    sort_mster_id = reader.GetSafeInt("sort_mster_id").ToString(),
                                });
                            }
                        }
                    }
                }

                responseDetails.success = plaingoldSortByList.Any();
                responseDetails.message = plaingoldSortByList.Any() ? "Successfully" : "No data found";
                responseDetails.status = "200";
                responseDetails.data = plaingoldSortByList;
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<PlaingoldSortByListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> ExtraGoldratewiseRate(ExtraGoldratewiseRateParams extraGoldratewiseRateParams)
        {
            var responseDetails = new ResponseDetails();
            IList<PlaingoldSortByListing> plaingoldSortByList = new List<PlaingoldSortByListing>();
            string ExtraPrice = "0";

            try
            {
                int flagcount = 0;
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    using (SqlCommand cmd = new SqlCommand(DBCommands.EXTRAGOLDRATEWISEPRICE, dbConnection))
                    {
                        dbConnection.Open();
                        decimal gold_weight = extraGoldratewiseRateParams.gold_weight > 0 ? extraGoldratewiseRateParams.gold_weight : 0;
                        string design_kt = string.IsNullOrWhiteSpace(extraGoldratewiseRateParams.design_kt) ? "" : extraGoldratewiseRateParams.design_kt;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@GoldWeight", gold_weight);
                        cmd.Parameters.AddWithValue("@DesignKT", design_kt);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                flagcount = 1;
                                ExtraPrice = reader.GetSafeDecimal("ExtraPrice").ToString();
                            }
                        }
                    }
                }

                responseDetails.success = flagcount > 0 ? true : false;
                responseDetails.message = flagcount > 0 ? "Successfully" : "No data found";
                responseDetails.status = "200";
                responseDetails.data = ExtraPrice;
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = ExtraPrice;
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> SearchAllItemsList(SearchAllItemsListParams searchallitemlistParams)
        {
            var response = new ResponseDetails();
            var searchAllItemsList = new List<SearchAllItemsListing>();
            int currentPage = searchallitemlistParams.page;
            int lastPage = 1, totalItems = 0;

            try
            {
                int DataID = searchallitemlistParams.data_id > 0 ? searchallitemlistParams.data_id : 0;
                string ItemName = string.IsNullOrWhiteSpace(searchallitemlistParams.item_name) ? "" : searchallitemlistParams.item_name;
                string Display = string.IsNullOrWhiteSpace(searchallitemlistParams.display) ? "" : searchallitemlistParams.display;
                int Page = searchallitemlistParams.page > 0 ? searchallitemlistParams.page - 1 : 0;

                using (var dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    using (var cmd = new SqlCommand(DBCommands.SEARCHALL_ITEMLIST, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@Display", Display);
                        cmd.Parameters.AddWithValue("@Page", Page);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tmpItemName = reader.GetSafeString("ItemName");
                                string tmpbrand = reader.GetSafeString("brand");
                                string tmpitem_name = reader.GetSafeString("item_name");
                                string tmppriceflag = reader.GetSafeString("priceflag");
                                string tmpItemIsSRP = reader.GetSafeString("ItemIsSRP");
                                string tmpDataItemInfo = reader.GetSafeString("DataItemInfo");
                                decimal tmpmrp_withtax = reader.GetSafeDecimal("mrp_withtax");

                                decimal tmpitem_mrp = 0;

                                if (tmpDataItemInfo == "Y" && tmpItemIsSRP == "Y")
                                {
                                    tmppriceflag = "Y";
                                }

                                if (tmppriceflag == "Y" && tmpItemIsSRP == "Y")
                                {
                                    string isInStockStr = string.Empty;
                                    string isInStock = reader.GetSafeString("isInStock");
                                    if (isInStock == "Y")
                                    {
                                        isInStockStr = "\nAvailable In Stock";
                                    }

                                    if (Display != "G" && Display != "AG")
                                    {
                                        tmpitem_mrp = Math.Round(tmpmrp_withtax, 0);
                                    }
                                    else
                                    {
                                        tmpitem_mrp = Math.Round(tmpitem_mrp, 0); ;
                                    }

                                    tmpitem_name = $"{tmpItemName} ({tmpbrand} - {tmpitem_mrp}) {isInStockStr}";
                                }
                                else
                                {
                                    if (Display == "Y")
                                    {
                                        tmpitem_name = $"{tmpItemName} ({tmpbrand})";
                                    }
                                }

                                searchAllItemsList.Add(new SearchAllItemsListing
                                {
                                    category_id = reader.GetSafeInt("category_id").ToString(),
                                    item_id = reader.GetSafeInt("item_id").ToString(),
                                    item_code = reader.GetSafeString("item_code"),
                                    ItemName = tmpItemName,
                                    brand = tmpbrand,
                                    item_name = tmpitem_name,
                                    item_decription = reader.GetSafeString("item_description"),
                                    item_type_common_id = reader.GetSafeInt("item_type_common_id").ToString(),
                                    ItemFranchiseSts = reader.GetSafeString("ItemFranchiseSts"),
                                    priceflag = tmppriceflag,
                                    ItemPlainGold = reader.GetSafeString("ItemPlainGold"),
                                    ItemIsSRP = tmpItemIsSRP,
                                    mrp_withtax = tmpmrp_withtax.ToString("0"),
                                });

                                lastPage = reader.GetSafeInt("last_page");
                                totalItems = reader.GetSafeInt("total_items");
                            }
                        }
                    }
                }

                response.current_page = currentPage.ToString();
                response.last_page = lastPage.ToString();
                response.total_items = totalItems.ToString();
                response.success = searchAllItemsList.Any();
                response.message = searchAllItemsList.Any() ? "Search items list successfully" : "No data found";
                response.status = "200";
                response.data = searchAllItemsList;
                return response;
            }
            catch (SqlException sqlEx)
            {
                response.success = false;
                response.message = $"SQL error: {sqlEx.Message}";
                response.status = "400";
                response.current_page = currentPage.ToString();
                response.last_page = "1";
                response.total_items = "0";
                response.data = searchAllItemsList;
                return response;
            }
        }

        public List<T> ExecuteStoredProcedure_Plaingold<T>(
                    string storedProcedureName,
                    PlaingoldItemFilterParams filterParams,
                    Func<SqlDataReader, T> mapRow)
        {
            var result = new List<T>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DataID", filterParams.data_id > 0 ? filterParams.data_id : 0);
                        cmd.Parameters.AddWithValue("@CategoryID", filterParams.category_id > 0 ? filterParams.category_id : 0);
                        cmd.Parameters.AddWithValue("@MasterCommonId", filterParams.master_common_id > 0 ? filterParams.master_common_id : 0);
                        cmd.Parameters.AddWithValue("@ButtonCode", string.IsNullOrWhiteSpace(filterParams.button_code) ? "" : filterParams.button_code);
                        cmd.Parameters.AddWithValue("@BtnCd", string.IsNullOrWhiteSpace(filterParams.btn_cd) ? "" : filterParams.btn_cd);

                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(mapRow(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing {storedProcedureName}: {ex.Message}");
            }

            return result;
        }

        private PlaingoldItemFilterDsgKt MapDsgKt_Plaingold(DataRow row) => new PlaingoldItemFilterDsgKt
        {
            kt = row.GetSafeString("kt"),
            Kt_Count = row.GetSafeInt("Kt_Count").ToString()
        };

        private PlaingoldItemFilterProductTag MapProductTag_Plaingold(DataRow row) => new PlaingoldItemFilterProductTag
        {
            tag_name = row.GetSafeString("tag_name"),
            tag_count = row.GetSafeInt("tag_count").ToString()
        };

        private PlaingoldItemFilterBrand MapBrand_Plaingold(DataRow row) => new PlaingoldItemFilterBrand
        {
            ItemBrandCommonID = row.GetSafeInt("ItemBrandCommonID").ToString(),
            brand_id = row.GetSafeInt("brand_id").ToString(),
            brand_name = row.GetSafeString("brand_name"),
            brand_count = row.GetSafeInt("brand_count").ToString()
        };

        private PlaingoldItemFilterGender MapGender_Plaingold(DataRow row) => new PlaingoldItemFilterGender
        {
            gender_id = row.GetSafeInt("gender_id").ToString(),
            gender_name = row.GetSafeString("gender_name"),
            gender_count = row.GetSafeInt("gender_count").ToString()
        };

        private PlaingoldItemFilterWestage MapWestage_Plaingold(DataRow row) => new PlaingoldItemFilterWestage
        {
            minprice = row.GetSafeDecimal("minprice"),
            maxprice = row.GetSafeDecimal("maxprice")
        };

        private PlaingoldItemFilterWeight MapWeight_Plaingold(DataRow row) => new PlaingoldItemFilterWeight
        {
            minWt = row.GetSafeDecimal("minWt"),
            maxWt = row.GetSafeDecimal("maxWt")
        };

        private PlaingoldItemFilterApproxDelivery MapApproxDelivery_Plaingold(DataRow row) => new PlaingoldItemFilterApproxDelivery
        {
            ItemAproxDay = row.GetSafeString("ItemAproxDay"),
            ItemAproxDay_count = row.GetSafeInt("ItemAproxDay_count").ToString()
        };

        private PlaingoldItemFilterStock MapStock_Plaingold(DataRow row) => new PlaingoldItemFilterStock
        {
            stock_name = row.GetSafeString("stock_name"),
            stock_id = row.GetSafeInt("stock_id").ToString()
        };

        private PlaingoldItemFilterNameValue MapNameValue_Plaingold(DataRow row) => new PlaingoldItemFilterNameValue
        {
            Name = row.GetSafeString("Name"),
            Value = row.GetSafeString("Value")
        };

        private PlaingoldItemFilterFamilyProduct MapFamilyProduct_Plaingold(DataRow row) => new PlaingoldItemFilterFamilyProduct
        {
            familyproduct_name = row.GetSafeString("familyproduct_name"),
            familyproduct_id = row.GetSafeString("familyproduct_id")
        };

        private PlaingoldItemFilterExcludeDiscontinue MapExcludeDiscontinue_Plaingold(DataRow row) => new PlaingoldItemFilterExcludeDiscontinue
        {
            excludediscontinue_name = row.GetSafeString("excludediscontinue_name"),
            excludediscontinue_id = row.GetSafeString("excludediscontinue_id")
        };

        private PlaingoldItemFilterView MapView_Plaingold(DataRow row) => new PlaingoldItemFilterView
        {
            view_name = row.GetSafeString("view_name"),
            view_id = row.GetSafeString("view_id")
        };

        private PlaingoldItemFilterImageAvail MapImageAvail_Plaingold(DataRow row) => new PlaingoldItemFilterImageAvail
        {
            imageavail_name = row.GetSafeString("imageavail_name"),
            imageavail_id = row.GetSafeString("imageavail_id")
        };

        private PlaingoldItemSubCategory MapItemSubCategory_Plaingold(DataRow row) => new PlaingoldItemSubCategory
        {
            sub_category_id = row.GetSafeInt("sub_category_id").ToString(),
            sub_category_name = row.GetSafeString("sub_category_name"),
            sub_category_count = row.GetSafeInt("sub_category_count").ToString()
        };

        public async Task<ResponseDetails> PlainGoldItemFilter(PlainGoldItemFilterParams request)
        {
            var responseDetails = new ResponseDetails();
            var filterData = new PlaingoldItemFilterData();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    using (var cmd = new SqlCommand(DBCommands.PLAINGOLDITEMFILTER_NEW, dbConnection))
                    {
                        dbConnection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@DataID", request.data_id);
                        cmd.Parameters.AddWithValue("@CategoryID", request.category_id);
                        cmd.Parameters.AddWithValue("@MasterCommonId", request.master_common_id);
                        cmd.Parameters.AddWithValue("@ButtonCode", request.button_code ?? string.Empty);
                        cmd.Parameters.AddWithValue("@Btn_cd", request.btn_cd ?? string.Empty);
                        var ds = new DataSet();
                        new SqlDataAdapter(cmd).Fill(ds);

                        // SubCategory
                        var subCategories = MapDataTable(ds.Tables[0], row => new PlaingoldItemFilterSubCategory
                        {
                            category_id = row["category_id"]?.ToString(),
                            sub_category_id = row["sub_category_id"]?.ToString(),
                            sub_category_name = row["sub_category_name"]?.ToString(),
                            category_count = row["category_count"]?.ToString()
                        });
                        filterData.sub_category.data = subCategories;

                        if (subCategories.Any())
                        {
                            //dsg kt
                            if (ds.Tables.Count > 1) filterData.dsg_kt.data = MapDataTable(ds.Tables[1], MapDsgKt_Plaingold);

                            //product tag
                            if (ds.Tables.Count > 2) filterData.productTags.data = MapDataTable(ds.Tables[2], MapProductTag_Plaingold);

                            //brand
                            if (ds.Tables.Count > 3) filterData.brand.data = MapDataTable(ds.Tables[3], MapBrand_Plaingold);

                            //gender
                            if (ds.Tables.Count > 4) filterData.gender.data = MapDataTable(ds.Tables[4], MapGender_Plaingold);

                            //approx delivery
                            if (ds.Tables.Count > 5) filterData.approx_develivery.data = MapDataTable(ds.Tables[5], MapApproxDelivery_Plaingold);

                            //westage
                            if (ds.Tables.Count > 6)
                            {
                                var westageRange = MapDataTable(ds.Tables[6], MapWestage_Plaingold).FirstOrDefault();
                                if (westageRange != null)
                                {
                                    filterData.dsg_westage.Add(new Plaingold_DsgWestageFilter { Westage = Math.Round(westageRange.minprice, 2).ToString() });
                                    filterData.dsg_westage.Add(new Plaingold_DsgWestageFilter { Westage = Math.Round(westageRange.maxprice, 2).ToString() });
                                }
                            }

                            //weight
                            if (ds.Tables.Count > 7)
                            {
                                var weightRange = MapDataTable(ds.Tables[7], MapWeight_Plaingold).FirstOrDefault();
                                if (weightRange != null)
                                {
                                    filterData.dsg_weight.Add(new Plaingold_DsgWeightFilter { Weight = Math.Round(weightRange.minWt, 2).ToString() });
                                    filterData.dsg_weight.Add(new Plaingold_DsgWeightFilter { Weight = Math.Round(weightRange.maxWt, 2).ToString() });
                                }
                            }

                            //stock filter
                            if (ds.Tables.Count > 8) filterData.stock_filter.data = MapDataTable(ds.Tables[8], MapStock_Plaingold);

                            // familyproduct filter
                            if (ds.Tables.Count > 9) filterData.familyproduct_filter.data = MapDataTable(ds.Tables[9], MapFamilyProduct_Plaingold);

                            // excludediscontinue filter
                            if (ds.Tables.Count > 10) filterData.excludediscontinue_filter.data = MapDataTable(ds.Tables[10], MapExcludeDiscontinue_Plaingold);

                            // wearview filter
                            if (ds.Tables.Count > 11) filterData.wearview_filter.data = MapDataTable(ds.Tables[11], MapView_Plaingold);

                            // tryonview filter
                            if (ds.Tables.Count > 12) filterData.tryonview_filter.data = MapDataTable(ds.Tables[12], MapView_Plaingold);

                            // imageavail filter
                            if (ds.Tables.Count > 13) filterData.imageavail_filter.data = MapDataTable(ds.Tables[13], MapImageAvail_Plaingold);

                            // item_sub_category filter
                            if (ds.Tables.Count > 14) filterData.item_sub_category.data = MapDataTable(ds.Tables[14], MapItemSubCategory_Plaingold);

                            // metal weight
                            filterData.dsg_metalwt = null;

                            // diamond weight
                            filterData.dsg_diamondwt = null;

                            // best_sellers_data filter
                            if (ds.Tables.Count > 15) filterData.best_sellers_data.data = MapDataTable(ds.Tables[15], MapNameValue_Plaingold);

                            // designs_data filter
                            if (ds.Tables.Count > 16) filterData.designs_data.data = MapDataTable(ds.Tables[16], MapNameValue_Plaingold);

                        }

                        responseDetails = new ResponseDetails
                        {
                            success = subCategories.Any(),
                            status = "200",
                            message = subCategories.Any() ? "Successfully" : "No data found",
                            data = null,
                            data1 = filterData
                        };

                        return responseDetails;
                    }
                }
            }
            catch (SqlException ex)
            {
                return new ResponseDetails
                {
                    success = false,
                    status = "400",
                    message = $"SQL error: {ex.Message}",
                    data = null,
                    data1 = new List<PlaingoldItemFilterData>()
                };
            }
        }

        public async Task<ItemDetails_Static> ViewItemDetails(PayloadsItemDetails Details)
        {
            try
            {
                ItemDetails_Static ItemdetailsStatic = new ItemDetails_Static();
                //   ItemDetails itemDetails = new ItemDetails();

                if (Details != null)
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {

                        string cmdQuery = DBCommands.VIEWITEMDETAILS;
                        dbConnection.Open();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {

                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CategoryId", Details.category_id);
                            cmd.Parameters.AddWithValue("@data_id", Details.data_id);
                            cmd.Parameters.AddWithValue("@data_login_type", Details.data_login_type);
                            cmd.Parameters.AddWithValue("@item_id", Details.item_id);
                            cmd.Parameters.AddWithValue("@master_common_id", Details.master_common_id);


                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = cmd;
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                IList<SizeList> sl = new List<SizeList>();
                                IList<ColorList> c = new List<ColorList>();
                                IList<ItemsColorSizeList> Icsl = new List<ItemsColorSizeList>();
                                IList<ItemOrderInstructionList> ol = new List<ItemOrderInstructionList>();
                                IList<ItemOrderCustomInstructionList> olc = new List<ItemOrderCustomInstructionList>();
                                IList<ProductTags> pd = new List<ProductTags>();
                                IList<Item_Image_Color> itemImageColor = new List<Item_Image_Color>();
                                IList<ApproxDays> ad = new List<ApproxDays>();
                                IList<DiamondData> dd = new List<DiamondData>();
                                //IList<ItemDetails> ItemDetails = new List<ItemDetails>();
                                ItemDetails itemDetails = new ItemDetails();
                                IList<Item_Images> ItemImages = new List<Item_Images>();
                                //  ItemDetails_Static ItemdetailsStatic = new ItemDetails_Static();

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0] != null)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            sl.Add(new SizeList
                                            {
                                                product_size_mst_id = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_id"]),
                                                product_size_mst_code = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_code"]),
                                                product_size_mst_name = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_name"]),
                                                product_size_mst_desc = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_desc"]),
                                            });
                                        }

                                    }

                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
                                {

                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    {
                                        c.Add(new ColorList
                                        {
                                            product_color_mst_id = Convert.ToString(ds.Tables[1].Rows[i]["product_color_mst_id"]),
                                            product_color_mst_code = Convert.ToString(ds.Tables[1].Rows[i]["product_color_mst_code"]),
                                            product_color_mst_name = Convert.ToString(ds.Tables[1].Rows[i]["product_color_mst_name"]),
                                            IsDefault = Convert.ToString(ds.Tables[1].Rows[i]["IsDefault"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 2)
                                {

                                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                    {
                                        Icsl.Add(new ItemsColorSizeList
                                        {
                                            cart_item_detail_id = Convert.ToString(ds.Tables[2].Rows[i]["cart_item_detail_id"]),
                                            ExtraGoldWeight = Convert.ToString(ds.Tables[2].Rows[i]["ExtraGoldWeight"]),
                                            ExtraGoldPrice = Convert.ToString(ds.Tables[2].Rows[i]["ExtraGoldPrice"]),
                                            cart_mst_id = Convert.ToString(ds.Tables[2].Rows[i]["cart_mst_id"]),
                                            cart_item_id = Convert.ToString(ds.Tables[2].Rows[i]["cart_item_id"]),
                                            cart_qty = Convert.ToString(ds.Tables[2].Rows[i]["cart_qty"]),
                                            cart_color_id = Convert.ToString(ds.Tables[2].Rows[i]["cart_color_id"]),
                                            cart_size_id = Convert.ToString(ds.Tables[2].Rows[i]["cart_size_id"]),
                                            cart_item_remarks = Convert.ToString(ds.Tables[2].Rows[i]["cart_item_remarks"]),
                                            cart_item_remarks_ids = Convert.ToString(ds.Tables[2].Rows[i]["cart_item_remarks_ids"]),
                                            cart_item_custom_remarks = Convert.ToString(ds.Tables[2].Rows[i]["cart_item_custom_remarks"]),
                                            cart_item_custom_remarks_ids = Convert.ToString(ds.Tables[2].Rows[i]["cart_item_custom_remarks_ids"]),
                                            cart_item_custom_remarks_status = Convert.ToString(ds.Tables[2].Rows[i]["cart_item_custom_remarks_status"]),

                                        });
                                    }
                                }


                                if (ds != null && ds.Tables != null && ds.Tables.Count > 3)
                                {

                                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                                    {
                                        ol.Add(new ItemOrderInstructionList
                                        {
                                            item_instruction_mst_id = Convert.ToString(ds.Tables[3].Rows[i]["item_instruction_mst_id"]),
                                            item_instruction_mst_code = Convert.ToString(ds.Tables[3].Rows[i]["item_instruction_mst_code"]),
                                            item_instruction_mst_name = Convert.ToString(ds.Tables[3].Rows[i]["item_instruction_mst_name"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 4)
                                {

                                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                                    {
                                        olc.Add(new ItemOrderCustomInstructionList
                                        {
                                            item_instruction_mst_id = Convert.ToString(ds.Tables[4].Rows[i]["item_instruction_mst_id"]),
                                            item_instruction_mst_code = Convert.ToString(ds.Tables[4].Rows[i]["item_instruction_mst_code"]),
                                            item_instruction_mst_name = Convert.ToString(ds.Tables[4].Rows[i]["item_instruction_mst_name"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 7)
                                {

                                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                                    {
                                        pd.Add(new ProductTags
                                        {
                                            tag_name = Convert.ToString(ds.Tables[7].Rows[i]["tag_name"]),
                                            tag_color = Convert.ToString(ds.Tables[7].Rows[i]["tag_color"]),
                                            StruItemCommonID = Convert.ToString(ds.Tables[7].Rows[i]["StruItemCommonID"]),
                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 8)
                                {

                                    for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                                    {
                                        itemImageColor.Add(new Item_Image_Color
                                        {
                                            color_id = Convert.ToString(ds.Tables[8].Rows[i]["color_id"]),
                                            color_image_details = ItemImages,

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 10)
                                {

                                    for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                                    {
                                        ItemImages.Add(new Item_Images
                                        {
                                            image_view_name = Convert.ToString(ds.Tables[10].Rows[i]["image_view_name"]),
                                            image_path = Convert.ToString(ds.Tables[10].Rows[i]["image_path"]),
                                            filetype = Convert.ToString(ds.Tables[10].Rows[i]["filetype"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 9)
                                {
                                    for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                                    {
                                        ad.Add(new ApproxDays
                                        {
                                            manufactureStartDate = Convert.ToString(ds.Tables[9].Rows[i]["manufactureStartDate"]),
                                            manufactureEndDate = Convert.ToString(ds.Tables[9].Rows[i]["manufactureEndDate"]),
                                            deliveryStartDate = Convert.ToString(ds.Tables[9].Rows[i]["deliveryStartDate"]),
                                            deliveryEndDate = Convert.ToString(ds.Tables[9].Rows[i]["deliveryEndDate"]),
                                            deliveryInDays = Convert.ToString(ds.Tables[9].Rows[i]["deliveryInDays"]),
                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 10)
                                {
                                    for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                                    {
                                        dd.Add(new DiamondData
                                        {
                                            diamond_price = Convert.ToString(ds.Tables[10].Rows[i]["diamond_price"]),
                                            diamond_wt = Convert.ToString(ds.Tables[10].Rows[i]["diamond_wt"]),
                                            diamond_qty = Convert.ToString(ds.Tables[10].Rows[i]["diamond_qty"]),
                                            diamond_shape = Convert.ToString(ds.Tables[10].Rows[i]["diamond_shape"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 6)
                                {
                                    itemDetails.item_id = Convert.ToString(ds.Tables[6].Rows[0]["item_id"]);
                                    itemDetails.item_soliter = Convert.ToString(ds.Tables[6].Rows[0]["item_soliter"]);
                                    itemDetails.ItemCtgCommonID = Convert.ToString(ds.Tables[6].Rows[0]["ItemCtgCommonID"]);
                                    itemDetails.ItemAproxDay = Convert.ToString(ds.Tables[6].Rows[0]["ItemAproxDay"]);
                                    itemDetails.ItemDAproxDay = Convert.ToString(ds.Tables[6].Rows[0]["ItemDAproxDay"]);
                                    itemDetails.plaingold_status = Convert.ToString(ds.Tables[6].Rows[0]["plaingold_status"]);
                                    itemDetails.item_name = Convert.ToString(ds.Tables[6].Rows[0]["item_name"]);
                                    itemDetails.item_description = Convert.ToString(ds.Tables[6].Rows[0]["item_description"]);
                                    itemDetails.item_mrp = Convert.ToString(ds.Tables[6].Rows[0]["item_mrp"]);
                                    itemDetails.item_discount = Convert.ToString(ds.Tables[6].Rows[0]["item_discount"]);
                                    itemDetails.item_price = Convert.ToString(ds.Tables[6].Rows[0]["item_price"]);
                                    itemDetails.retail_price = Convert.ToString(ds.Tables[6].Rows[0]["retail_price"]);
                                    itemDetails.dist_price = Convert.ToString(ds.Tables[6].Rows[0]["dist_price"]);
                                    itemDetails.uom = Convert.ToString(ds.Tables[6].Rows[0]["uom"]);
                                    itemDetails.star = Convert.ToString(ds.Tables[6].Rows[0]["star"]);
                                    itemDetails.cart_img = Convert.ToString(ds.Tables[6].Rows[0]["cart_img"]);
                                    itemDetails.img_cart_title = Convert.ToString(ds.Tables[6].Rows[0]["img_cart_title"]);
                                    itemDetails.watch_img = Convert.ToString(ds.Tables[6].Rows[0]["watch_img"]);
                                    itemDetails.wearit_count = Convert.ToString(ds.Tables[6].Rows[0]["wearit_count"]);
                                    itemDetails.wearit_status = Convert.ToString(ds.Tables[6].Rows[0]["wearit_status"]);
                                    itemDetails.wearit_img = Convert.ToString(ds.Tables[6].Rows[0]["wearit_img"]);
                                    itemDetails.wearit_none_img = Convert.ToString(ds.Tables[6].Rows[0]["wearit_none_img"]);
                                    itemDetails.wearit_color = Convert.ToString(ds.Tables[6].Rows[0]["wearit_color"]);
                                    itemDetails.wearit_text = Convert.ToString(ds.Tables[6].Rows[0]["wearit_text"]);
                                    itemDetails.img_wearit_title = Convert.ToString(ds.Tables[6].Rows[0]["img_wearit_title"]);
                                    itemDetails.tryon_count = Convert.ToString(ds.Tables[6].Rows[0]["tryon_count"]);
                                    itemDetails.tryon_status = Convert.ToString(ds.Tables[6].Rows[0]["tryon_status"]);
                                    itemDetails.tryon_img = Convert.ToString(ds.Tables[6].Rows[0]["tryon_img"]);
                                    itemDetails.tryon_none_img = Convert.ToString(ds.Tables[6].Rows[0]["tryon_none_img"]);
                                    itemDetails.tryon_text = Convert.ToString(ds.Tables[6].Rows[0]["tryon_text"]);
                                    itemDetails.tryon_title = Convert.ToString(ds.Tables[6].Rows[0]["tryon_title"]);
                                    itemDetails.tryon_android_path = Convert.ToString(ds.Tables[6].Rows[0]["tryon_android_path"]);
                                    itemDetails.tryon_ios_path = Convert.ToString(ds.Tables[6].Rows[0]["tryon_ios_path"]);
                                    itemDetails.wish_count = Convert.ToString(ds.Tables[6].Rows[0]["wish_count"]);
                                    itemDetails.wish_default_img = Convert.ToString(ds.Tables[6].Rows[0]["wish_default_img"]);
                                    itemDetails.wish_fill_img = Convert.ToString(ds.Tables[6].Rows[0]["wish_fill_img"]);
                                    itemDetails.img_wish_title = Convert.ToString(ds.Tables[6].Rows[0]["img_wish_title"]);
                                    itemDetails.item_review = Convert.ToString(ds.Tables[6].Rows[0]["item_review"]);
                                    itemDetails.item_size = Convert.ToString(ds.Tables[6].Rows[0]["item_size"]);
                                    itemDetails.item_kt = Convert.ToString(ds.Tables[6].Rows[0]["item_kt"]);
                                    itemDetails.item_color = Convert.ToString(ds.Tables[6].Rows[0]["item_color"]);
                                    itemDetails.item_metal = Convert.ToString(ds.Tables[6].Rows[0]["item_metal"]);
                                    itemDetails.item_wt = Convert.ToString(ds.Tables[6].Rows[0]["item_wt"]);
                                    itemDetails.item_stone = Convert.ToString(ds.Tables[6].Rows[0]["item_stone"]);
                                    itemDetails.item_stone_wt = Convert.ToString(ds.Tables[6].Rows[0]["item_stone_wt"]);
                                    itemDetails.item_stone_qty = Convert.ToString(ds.Tables[6].Rows[0]["item_stone_qty"]);
                                    itemDetails.star_color = Convert.ToString(ds.Tables[6].Rows[0]["star_color"]);
                                    itemDetails.ItemMetalCommonID = Convert.ToString(ds.Tables[6].Rows[0]["ItemMetalCommonID"]);
                                    itemDetails.price_text = Convert.ToString(ds.Tables[6].Rows[0]["price_text"]);
                                    itemDetails.cart_price = Convert.ToString(ds.Tables[6].Rows[0]["cart_price"]);
                                    itemDetails.item_color_id = Convert.ToString(ds.Tables[6].Rows[0]["item_color_id"]);
                                    itemDetails.item_details = Convert.ToString(ds.Tables[6].Rows[0]["item_details"]);
                                    itemDetails.item_diamond_details = Convert.ToString(ds.Tables[6].Rows[0]["item_diamond_details"]);
                                    itemDetails.item_text = Convert.ToString(ds.Tables[6].Rows[0]["item_text"]);
                                    itemDetails.more_item_details = Convert.ToString(ds.Tables[6].Rows[0]["more_item_details"]);
                                    itemDetails.item_stock = Convert.ToString(ds.Tables[6].Rows[0]["item_stock"]);
                                    itemDetails.cart_item_qty = Convert.ToString(ds.Tables[6].Rows[0]["cart_item_qty"]);
                                    itemDetails.rupy_symbol = Convert.ToString(ds.Tables[6].Rows[0]["rupy_symbol"]);
                                    itemDetails.variantCount = Convert.ToString(ds.Tables[6].Rows[0]["variantCount"]);
                                    itemDetails.ItemGenderCommonID = Convert.ToString(ds.Tables[6].Rows[0]["ItemGenderCommonID"]);
                                    itemDetails.category_id = Convert.ToString(ds.Tables[6].Rows[0]["category_id"]);
                                    itemDetails.ItemNosePinScrewSts = Convert.ToString(ds.Tables[6].Rows[0]["ItemNosePinScrewSts"]);
                                    itemDetails.metal = Convert.ToString(ds.Tables[6].Rows[0]["metal"]);
                                    itemDetails.kt = Convert.ToString(ds.Tables[6].Rows[0]["kt"]);
                                    itemDetails.quality = Convert.ToString(ds.Tables[6].Rows[0]["quality"]);
                                    itemDetails.shape = Convert.ToString(ds.Tables[6].Rows[0]["shape"]);
                                    itemDetails.brand = Convert.ToString(ds.Tables[6].Rows[0]["brand"]);
                                    itemDetails.stone = Convert.ToString(ds.Tables[6].Rows[0]["stone"]);
                                    itemDetails.diamondcolor = Convert.ToString(ds.Tables[6].Rows[0]["diamondcolor"]);
                                    itemDetails.category = Convert.ToString(ds.Tables[6].Rows[0]["category"]);
                                    itemDetails.ItemAproxDayCommonID = Convert.ToString(ds.Tables[6].Rows[0]["ItemAproxDayCommonID"]);
                                    itemDetails.GrossWt = Convert.ToString(ds.Tables[6].Rows[0]["GrossWt"]);
                                    itemDetails.ItemFranchiseSts = Convert.ToString(ds.Tables[6].Rows[0]["ItemFranchiseSts"]);
                                    itemDetails.priceflag = Convert.ToString(ds.Tables[6].Rows[0]["priceflag"]);
                                    itemDetails.ItemPlainGold = Convert.ToString(ds.Tables[6].Rows[0]["ItemPlainGold"]);
                                    itemDetails.ItemSoliterSts = Convert.ToString(ds.Tables[6].Rows[0]["ItemSoliterSts"]);
                                    itemDetails.ItemSubCtgName = Convert.ToString(ds.Tables[6].Rows[0]["ItemSubCtgName"]);
                                    itemDetails.ItemSubSubCtgName = Convert.ToString(ds.Tables[6].Rows[0]["ItemSubSubCtgName"]);
                                    itemDetails.ItemIsSRP = Convert.ToString(ds.Tables[6].Rows[0]["ItemIsSRP"]);
                                    itemDetails.productTags = pd;
                                    itemDetails.next = Convert.ToString(ds.Tables[6].Rows[0]["next"]);
                                    itemDetails.prev = Convert.ToString(ds.Tables[6].Rows[0]["prev"]);
                                    itemDetails.metalweight = Convert.ToString(ds.Tables[6].Rows[0]["metalweight"]);
                                    itemDetails.diamondweight = Convert.ToString(ds.Tables[6].Rows[0]["diamondweight"]);
                                    itemDetails.approxdelivery = Convert.ToString(ds.Tables[6].Rows[0]["approxdelivery"]);
                                    itemDetails.collections = Convert.ToString(ds.Tables[6].Rows[0]["collections"]);
                                    itemDetails.item_stock_qty = Convert.ToString(ds.Tables[6].Rows[0]["item_stock_qty"]);
                                    itemDetails.item_stock_colorsize_qty = Convert.ToString(ds.Tables[6].Rows[0]["item_stock_colorsize_qty"]);
                                    itemDetails.selectedColor = Convert.ToString(ds.Tables[6].Rows[0]["selectedColor"]);
                                    itemDetails.selectedSize = Convert.ToString(ds.Tables[6].Rows[0]["selectedSize"]);
                                    itemDetails.selectedColor1 = Convert.ToString(ds.Tables[6].Rows[0]["selectedColor1"]);
                                    itemDetails.selectedSize1 = Convert.ToString(ds.Tables[6].Rows[0]["selectedSize1"]);
                                    itemDetails.field_name = Convert.ToString(ds.Tables[6].Rows[0]["field_name"]);
                                    itemDetails.color_name = Convert.ToString(ds.Tables[6].Rows[0]["color_name"]);
                                    itemDetails.default_color_name = Convert.ToString(ds.Tables[6].Rows[0]["default_color_name"]);
                                    itemDetails.default_color_code = Convert.ToString(ds.Tables[6].Rows[0]["default_color_code"]);
                                    itemDetails.default_size_name = Convert.ToString(ds.Tables[6].Rows[0]["default_size_name"]);
                                    itemDetails.fran_diamond_price = Convert.ToString(ds.Tables[6].Rows[0]["fran_diamond_price"]);
                                    itemDetails.diamond_wt = Convert.ToString(ds.Tables[6].Rows[0]["diamond_wt"]);
                                    itemDetails.fran_gold_price = Convert.ToString(ds.Tables[6].Rows[0]["fran_gold_price"]);
                                    itemDetails.stone_qty = Convert.ToString(ds.Tables[6].Rows[0]["stone_qty"]);
                                    itemDetails.fran_mrp_gst = Convert.ToString(ds.Tables[6].Rows[0]["fran_mrp_gst"]);
                                    itemDetails.sizeList = sl;
                                    itemDetails.itemsColorSizeList = Icsl;
                                    itemDetails.colorList = c;
                                    itemDetails.itemOrderInstructionList = ol;
                                    itemDetails.itemOrderCustomInstructionList = olc;
                                    itemDetails.approxDays = ad;
                                    itemDetails.diamondData = dd;

                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 5)
                                {
                                    data myData = new data
                                    {
                                        item_detail = itemDetails,
                                        color_image_details = ItemImages
                                    };

                                    for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                                    {
                                        ItemdetailsStatic.success = Convert.ToString(ds.Tables[5].Rows[i]["SUCCESS"]);
                                        ItemdetailsStatic.message = Convert.ToString(ds.Tables[5].Rows[i]["MESSAGE"]);
                                        ItemdetailsStatic.data = myData;

                                    }
                                }



                            }
                        }
                    }
                }

                if (ItemdetailsStatic != null || ItemdetailsStatic.success == "TRUE")
                {
                    return ItemdetailsStatic;
                }
                else
                {
                    return new ItemDetails_Static
                    {
                        message = "No data available."
                    };
                }

            }
            catch (Exception sqlEx)
            {
                return new ItemDetails_Static
                {
                    message = sqlEx.Message
                };
            }

        }

        public async Task<SoliterView_Static> ViewSoliterDetails(PayloadsItemDetails Details)
        {
            try
            {
                SoliterView_Static SoliterItemdetailsStatic = new SoliterView_Static();
                SoliterView SoliteritemDetails = new SoliterView();

                if (Details != null)
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.VIEWSOLITERITEMDETAILS;
                        dbConnection.Open();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {

                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CategoryId", Details.category_id);
                            cmd.Parameters.AddWithValue("@data_id", Details.data_id);
                            cmd.Parameters.AddWithValue("@data_login_type", Details.data_login_type);
                            cmd.Parameters.AddWithValue("@item_id", Details.item_id);
                            cmd.Parameters.AddWithValue("@master_common_id", Details.master_common_id);

                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = cmd;
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                IList<ProductTags> pd = new List<ProductTags>();
                                IList<SizeListSoliter> sl = new List<SizeListSoliter>();
                                IList<ColorListSoliter> c = new List<ColorListSoliter>();
                                IList<ItemOrderInstructionListSoliter> ol = new List<ItemOrderInstructionListSoliter>();
                                IList<ItemOrderCustomInstructionListSoliter> olc = new List<ItemOrderCustomInstructionListSoliter>();
                                IList<Item_Images_Soliter> ItemImages = new List<Item_Images_Soliter>();
                                IList<Item_Image_Color_Soliter> itemImageColor = new List<Item_Image_Color_Soliter>();

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
                                {
                                    if (ds.Tables[0] != null)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            sl.Add(new SizeListSoliter
                                            {
                                                product_size_mst_id = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_id"]),
                                                product_size_mst_code = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_code"]),
                                                product_size_mst_name = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_name"]),
                                                product_size_mst_desc = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_desc"]),
                                                SortBy = ds.Tables[0].Rows[i]["MstSortBy"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["MstSortBy"])
                                            });
                                        }

                                    }

                                    if (ds.Tables[1] != null)
                                    {
                                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                        {
                                            c.Add(new ColorListSoliter
                                            {
                                                product_color_mst_id = Convert.ToString(ds.Tables[1].Rows[i]["product_color_mst_id"]),
                                                product_color_mst_code = Convert.ToString(ds.Tables[1].Rows[i]["product_color_mst_code"]),
                                                product_color_mst_name = Convert.ToString(ds.Tables[1].Rows[i]["product_color_mst_name"]),
                                                IsDefault = Convert.ToString(ds.Tables[1].Rows[i]["IsDefault"]),

                                            });
                                        }
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 2)
                                {

                                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                    {
                                        ol.Add(new ItemOrderInstructionListSoliter
                                        {
                                            item_instruction_mst_id = Convert.ToString(ds.Tables[2].Rows[i]["item_instruction_mst_id"]),
                                            item_instruction_mst_code = Convert.ToString(ds.Tables[2].Rows[i]["item_instruction_mst_code"]),
                                            item_instruction_mst_name = Convert.ToString(ds.Tables[2].Rows[i]["item_instruction_mst_name"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 3)
                                {

                                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                                    {
                                        olc.Add(new ItemOrderCustomInstructionListSoliter
                                        {
                                            item_instruction_mst_id = Convert.ToString(ds.Tables[3].Rows[i]["item_instruction_mst_id"]),
                                            item_instruction_mst_code = Convert.ToString(ds.Tables[3].Rows[i]["item_instruction_mst_code"]),
                                            item_instruction_mst_name = Convert.ToString(ds.Tables[3].Rows[i]["item_instruction_mst_name"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 7)
                                {
                                    for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                                    {
                                        ItemImages.Add(new Item_Images_Soliter
                                        {
                                            image_view_name = Convert.ToString(ds.Tables[6].Rows[i]["image_view_name"]),
                                            image_path = Convert.ToString(ds.Tables[6].Rows[i]["image_path"]),
                                            filetype = Convert.ToString(ds.Tables[6].Rows[i]["filetype"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 7)
                                {

                                    for (int i = 0; i < 1; i++)
                                    {
                                        itemImageColor.Add(new Item_Image_Color_Soliter
                                        {
                                            color_id = Convert.ToString(ds.Tables[7].Rows[i]["color_id"]),
                                            color_image_details = ItemImages,

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 4)
                                {
                                    SoliteritemDetails.item_id = Convert.ToString(ds.Tables[5].Rows[0]["item_id"]);
                                    SoliteritemDetails.Category_name = Convert.ToString(ds.Tables[5].Rows[0]["Category_name"]);
                                    SoliteritemDetails.item_soliter = Convert.ToString(ds.Tables[5].Rows[0]["item_soliter"]);
                                    SoliteritemDetails.plaingold_status = Convert.ToString(ds.Tables[5].Rows[0]["plaingold_status"]);
                                    SoliteritemDetails.item_name = Convert.ToString(ds.Tables[5].Rows[0]["item_name"]);
                                    SoliteritemDetails.item_sku = Convert.ToString(ds.Tables[5].Rows[0]["item_sku"]);
                                    SoliteritemDetails.item_description = Convert.ToString(ds.Tables[5].Rows[0]["item_description"]);
                                    SoliteritemDetails.item_discount = Convert.ToString(ds.Tables[5].Rows[0]["item_discount"]);
                                    SoliteritemDetails.item_price = Convert.ToString(ds.Tables[5].Rows[0]["item_price"]);
                                    SoliteritemDetails.retail_price = Convert.ToString(ds.Tables[5].Rows[0]["retail_price"]);
                                    SoliteritemDetails.dist_price = Convert.ToString(ds.Tables[5].Rows[0]["dist_price"]);
                                    SoliteritemDetails.uom = Convert.ToString(ds.Tables[5].Rows[0]["uom"]);
                                    SoliteritemDetails.star = Convert.ToString(ds.Tables[5].Rows[0]["star"]);
                                    SoliteritemDetails.cart_img = Convert.ToString(ds.Tables[5].Rows[0]["cart_img"]);
                                    SoliteritemDetails.img_cart_title = Convert.ToString(ds.Tables[5].Rows[0]["img_cart_title"]);
                                    SoliteritemDetails.watch_img = Convert.ToString(ds.Tables[5].Rows[0]["watch_img"]);
                                    SoliteritemDetails.img_watch_title = Convert.ToString(ds.Tables[5].Rows[0]["img_watch_title"]);
                                    SoliteritemDetails.wearit_count = Convert.ToString(ds.Tables[5].Rows[0]["wearit_count"]);
                                    SoliteritemDetails.wearit_status = Convert.ToString(ds.Tables[5].Rows[0]["wearit_status"]);
                                    SoliteritemDetails.wearit_img = Convert.ToString(ds.Tables[5].Rows[0]["wearit_img"]);
                                    SoliteritemDetails.wearit_none_img = Convert.ToString(ds.Tables[5].Rows[0]["wearit_none_img"]);
                                    SoliteritemDetails.wearit_color = Convert.ToString(ds.Tables[5].Rows[0]["wearit_color"]);
                                    SoliteritemDetails.wearit_text = Convert.ToString(ds.Tables[5].Rows[0]["wearit_text"]);
                                    SoliteritemDetails.img_wearit_title = Convert.ToString(ds.Tables[5].Rows[0]["img_wearit_title"]);
                                    SoliteritemDetails.tryon_count = Convert.ToString(ds.Tables[5].Rows[0]["tryon_count"]);
                                    SoliteritemDetails.tryon_status = Convert.ToString(ds.Tables[5].Rows[0]["tryon_status"]);
                                    SoliteritemDetails.tryon_img = Convert.ToString(ds.Tables[5].Rows[0]["tryon_img"]);
                                    SoliteritemDetails.tryon_none_img = Convert.ToString(ds.Tables[5].Rows[0]["tryon_none_img"]);
                                    SoliteritemDetails.tryon_text = Convert.ToString(ds.Tables[5].Rows[0]["tryon_text"]);
                                    SoliteritemDetails.tryon_title = Convert.ToString(ds.Tables[5].Rows[0]["tryon_title"]);
                                    SoliteritemDetails.tryon_android_path = Convert.ToString(ds.Tables[5].Rows[0]["tryon_android_path"]);
                                    SoliteritemDetails.tryon_ios_path = Convert.ToString(ds.Tables[5].Rows[0]["tryon_ios_path"]);
                                    SoliteritemDetails.wish_count = Convert.ToString(ds.Tables[5].Rows[0]["wish_count"]);
                                    SoliteritemDetails.wish_default_img = Convert.ToString(ds.Tables[5].Rows[0]["wish_default_img"]);
                                    SoliteritemDetails.wish_fill_img = Convert.ToString(ds.Tables[5].Rows[0]["wish_fill_img"]);
                                    SoliteritemDetails.img_wish_title = Convert.ToString(ds.Tables[5].Rows[0]["img_wish_title"]);
                                    SoliteritemDetails.item_review = Convert.ToString(ds.Tables[5].Rows[0]["item_review"]);
                                    SoliteritemDetails.item_size = Convert.ToString(ds.Tables[5].Rows[0]["item_size"]);
                                    SoliteritemDetails.item_color = Convert.ToString(ds.Tables[5].Rows[0]["item_color"]);
                                    SoliteritemDetails.item_metal = Convert.ToString(ds.Tables[5].Rows[0]["item_metal"]);
                                    SoliteritemDetails.item_stone = Convert.ToString(ds.Tables[5].Rows[0]["item_stone"]);
                                    SoliteritemDetails.item_stone_wt = Convert.ToString(ds.Tables[5].Rows[0]["item_stone_wt"]);
                                    SoliteritemDetails.item_stone_qty = Convert.ToString(ds.Tables[5].Rows[0]["item_stone_qty"]);
                                    SoliteritemDetails.star_color = Convert.ToString(ds.Tables[5].Rows[0]["star_color"]);
                                    SoliteritemDetails.ItemMetalCommonID = Convert.ToString(ds.Tables[5].Rows[0]["ItemMetalCommonID"]);
                                    SoliteritemDetails.price_text = Convert.ToString(ds.Tables[5].Rows[0]["price_text"]);
                                    SoliteritemDetails.cart_price = Convert.ToString(ds.Tables[5].Rows[0]["cart_price"]);
                                    SoliteritemDetails.item_color_id = Convert.ToString(ds.Tables[5].Rows[0]["item_color_id"]);
                                    SoliteritemDetails.item_details = Convert.ToString(ds.Tables[5].Rows[0]["item_details"]);
                                    SoliteritemDetails.item_text = Convert.ToString(ds.Tables[5].Rows[0]["item_text"]);
                                    SoliteritemDetails.more_item_details = Convert.ToString(ds.Tables[5].Rows[0]["more_item_details"]);
                                    SoliteritemDetails.item_stock = Convert.ToString(ds.Tables[5].Rows[0]["item_stock"]);
                                    SoliteritemDetails.cart_item_qty = Convert.ToString(ds.Tables[5].Rows[0]["cart_item_qty"]);
                                    SoliteritemDetails.rupy_symbol = Convert.ToString(ds.Tables[5].Rows[0]["rupy_symbol"]);
                                    SoliteritemDetails.variantCount = Convert.ToString(ds.Tables[5].Rows[0]["variantCount"]);
                                    SoliteritemDetails.cart_id = Convert.ToString(ds.Tables[5].Rows[0]["cart_id"]);
                                    SoliteritemDetails.ItemGenderCommonID = Convert.ToString(ds.Tables[5].Rows[0]["ItemGenderCommonID"]);
                                    SoliteritemDetails.cart_auto_id = Convert.ToString(ds.Tables[5].Rows[0]["cart_auto_id"]);
                                    SoliteritemDetails.item_stock_qty = Convert.ToString(ds.Tables[5].Rows[0]["item_stock_qty"]);
                                    SoliteritemDetails.item_stock_colorsize_qty = Convert.ToString(ds.Tables[5].Rows[0]["item_stock_colorsize_qty"]);
                                    SoliteritemDetails.category_id = Convert.ToString(ds.Tables[5].Rows[0]["category_id"]);
                                    SoliteritemDetails.ItemNosePinScrewSts = Convert.ToString(ds.Tables[5].Rows[0]["ItemNosePinScrewSts"]);
                                    SoliteritemDetails.ItemAproxDayCommonID = Convert.ToString(ds.Tables[5].Rows[0]["ItemAproxDayCommonID"]);
                                    SoliteritemDetails.ItemBrandCommonID = Convert.ToString(ds.Tables[5].Rows[0]["ItemBrandCommonID"]);
                                    SoliteritemDetails.item_illumine = Convert.ToString(ds.Tables[5].Rows[0]["item_illumine"]);
                                    SoliteritemDetails.productTags = pd;
                                    SoliteritemDetails.selectedColor = Convert.ToString(ds.Tables[5].Rows[0]["selectedColor"]);
                                    SoliteritemDetails.selectedSize = Convert.ToString(ds.Tables[5].Rows[0]["selectedSize"]);
                                    SoliteritemDetails.selectedColor1 = Convert.ToString(ds.Tables[5].Rows[0]["selectedColor1"]);
                                    SoliteritemDetails.selectedSize1 = Convert.ToString(ds.Tables[5].Rows[0]["selectedSize1"]);
                                    SoliteritemDetails.field_name = Convert.ToString(ds.Tables[5].Rows[0]["field_name"]);
                                    SoliteritemDetails.color_name = Convert.ToString(ds.Tables[5].Rows[0]["color_name"]);
                                    SoliteritemDetails.default_color_name = Convert.ToString(ds.Tables[5].Rows[0]["default_color_name"]);
                                    SoliteritemDetails.default_color_code = Convert.ToString(ds.Tables[5].Rows[0]["default_color_code"]);
                                    SoliteritemDetails.default_size_name = Convert.ToString(ds.Tables[5].Rows[0]["default_size_name"]);
                                    SoliteritemDetails.sizeList = sl;
                                    SoliteritemDetails.colorList = c;
                                    SoliteritemDetails.itemOrderInstructionList = ol;
                                    SoliteritemDetails.itemOrderCustomInstructionList = olc;
                                    SoliteritemDetails.item_images_color = itemImageColor;
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 5)
                                {
                                    data2 myData = new data2
                                    {
                                        item_detail = SoliteritemDetails,
                                        color_image_details = ItemImages

                                    };

                                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                                    {
                                        SoliterItemdetailsStatic.success = Convert.ToString(ds.Tables[4].Rows[i]["SUCCESS"]);
                                        SoliterItemdetailsStatic.message = Convert.ToString(ds.Tables[4].Rows[i]["MESSAGE"]);
                                        SoliterItemdetailsStatic.data = myData;
                                    }
                                }
                            }
                        }
                    }
                }

                if (SoliterItemdetailsStatic != null || SoliterItemdetailsStatic.success == "TRUE")
                {
                    return SoliterItemdetailsStatic;
                }
                else
                {
                    return new SoliterView_Static { message = "No data available." };
                }

            }
            catch (Exception sqlEx)
            {
                return new SoliterView_Static { message = sqlEx.Message };
            }
        }

        public async Task<ResponseDetails> AllCategoryListNew(AllCategoryListingParamsNew allcategorylistparams)
        {
            var responseDetails = new ResponseDetails();

            IList<AllCategoryListing> allCategoryListNew = new List<AllCategoryListing>();
            IList<GoldPriceRateWiseResponse> categoryButtonList = new List<GoldPriceRateWiseResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ALLCATEGORYLISTNEW;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        int data_id = allcategorylistparams.dataid > 0 ? allcategorylistparams.dataid : 0;
                        int data_login_type = allcategorylistparams.data_login_type > 0 ? allcategorylistparams.data_login_type : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            int category_id = rowdetails["category_id"] != DBNull.Value ? Convert.ToInt32(rowdetails["category_id"]) : 0;
                                            string category_name = rowdetails["category_name"] != DBNull.Value ? Convert.ToString(rowdetails["category_name"]) : string.Empty;
                                            int master_common_id = rowdetails["master_common_id"] != DBNull.Value ? Convert.ToInt32(rowdetails["master_common_id"]) : 0;
                                            string image_path = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            int total_count = rowdetails["count"] != DBNull.Value ? Convert.ToInt32(rowdetails["count"]) : 0;

                                            allCategoryListNew.Add(new AllCategoryListing
                                            {
                                                category_id = category_id.ToString(),
                                                category_name = category_name,
                                                master_common_id = master_common_id.ToString(),
                                                image_path = image_path,
                                                count = total_count.ToString(),
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (allCategoryListNew.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = allCategoryListNew;
                    if (categoryButtonList.Any())
                    {
                        responseDetails.button = categoryButtonList;
                    }
                    else
                    {
                        responseDetails.button = new List<CategoryButtonListing>();
                    }
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<AllCategoryListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<AllCategoryListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GoldPriceRateWise(GoldPriceRateWise rate)
        {
            var responseDetails = new ResponseDetails();

            IList<GoldPriceRateWiseResponse> goldRate = new List<GoldPriceRateWiseResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GetGoldPriceRateWise;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        int data_id = rate.dataid > 0 ? rate.dataid : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", data_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string name = rowdetails["kt_name"] != DBNull.Value ? Convert.ToString(rowdetails["kt_name"]) : string.Empty;
                                            string goldprice = rowdetails["kt_price"] != DBNull.Value ? Convert.ToString(rowdetails["kt_price"]) : string.Empty;

                                            goldRate.Add(new GoldPriceRateWiseResponse
                                            {
                                                name = name,
                                                price = goldprice,
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (goldRate.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = goldRate;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<GoldPriceRateWiseResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<GoldPriceRateWiseResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> PriceWiseList(AllCategoryListingParamsNew price)
        {
            var responseDetails = new ResponseDetails();

            IList<PriceRateWiseResponse> priceRate = new List<PriceRateWiseResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GetPriceWiseList;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        int data_id = price.dataid > 0 ? price.dataid : 0;
                        int data_login_type = price.data_login_type > 0 ? price.data_login_type : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string range = rowdetails["range"] != DBNull.Value ? Convert.ToString(rowdetails["range"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;

                                            priceRate.Add(new PriceRateWiseResponse
                                            {
                                                range = range,
                                                imagePath = imagePath,
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (priceRate.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = priceRate;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<PriceRateWiseResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<PriceRateWiseResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CategoryButtonListNew(CategoryButtonListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();

            IList<CategoryButtonListResponse> buttonList = new List<CategoryButtonListResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CategoryButtonListNew;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string data_id = string.IsNullOrWhiteSpace(param.dataId) ? "" : param.dataId;
                        string data_login_type = string.IsNullOrWhiteSpace(param.dataLoginId) ? "" : param.dataLoginId;
                        string category_id = string.IsNullOrWhiteSpace(param.categoryId) ? "" : param.categoryId;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataID", data_id);
                        cmd.Parameters.AddWithValue("@DataLoginType", data_login_type);
                        cmd.Parameters.AddWithValue("@CategoryID", category_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string buttonName = rowdetails["button_name"] != DBNull.Value ? Convert.ToString(rowdetails["button_name"]) : string.Empty;
                                            string buttonCode = rowdetails["btn_cd"] != DBNull.Value ? Convert.ToString(rowdetails["btn_cd"]) : string.Empty;
                                            string buttonType = rowdetails["btn_type"] != DBNull.Value ? Convert.ToString(rowdetails["btn_type"]) : string.Empty;
                                            string buttonImage = rowdetails["button_image"] != DBNull.Value ? Convert.ToString(rowdetails["button_image"]) : string.Empty;

                                            buttonList.Add(new CategoryButtonListResponse
                                            {
                                                buttonName = buttonName,
                                                buttonCode = buttonCode,
                                                buttonType = buttonType,
                                                buttonImage = buttonImage,
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (buttonList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = buttonList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<CategoryButtonListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CategoryButtonListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> SplashScreenList([FromBody] SplashScreenListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SplashScreenList;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        int data_id = param.dataid > 0 ? param.dataid : 0;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", data_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            var finalResponse = new ResponseDetails();
                            var userDataJson = new List<Dictionary<string, object>>();

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                var firstRow = ds.Tables[0].Rows[0];
                                var kioskConfigStr = firstRow["KioskConfiguration"] != DBNull.Value
                                    ? Convert.ToString(firstRow["KioskConfiguration"])
                                    : "{}";

                                var kioskConfigDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(kioskConfigStr);

                                userDataJson = new List<Dictionary<string, object>> { kioskConfigDict };
                            }

                            if (userDataJson.Any())
                            {
                                responseDetails.success = true;
                                responseDetails.message = "Successfully";
                                responseDetails.status = "200";
                                responseDetails.data = userDataJson;
                            }
                            else
                            {
                                responseDetails.success = false;
                                responseDetails.message = "No data found";
                                responseDetails.status = "200";
                                responseDetails.data = new List<ResponseDetails>();
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<ResponseDetails>();
            }
            catch (Exception ex)
            {
                responseDetails.success = false;
                responseDetails.message = $"An unexpected error occurred: {ex.Message}";
                responseDetails.status = "500";
                responseDetails.data = new List<ResponseDetails>();
            }
            return responseDetails;
        }

        public async Task<ResponseDetails> PriceWiseCatgoryList(PriceWiseCategoryListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<PriceWiseCategoryListResponse> priceCategoryList = new List<PriceWiseCategoryListResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PriceWiseItemCategory;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string data_id = string.IsNullOrWhiteSpace(param.dataId) ? "" : param.dataId;
                        string data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? "" : param.dataLoginType;
                        string price_range = string.IsNullOrWhiteSpace(param.priceRange) ? "" : param.priceRange;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@price_range", price_range);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string categoryName = rowdetails["category_name"] != DBNull.Value ? Convert.ToString(rowdetails["category_name"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string mstSortBy = rowdetails["MstSortBy"] != DBNull.Value ? Convert.ToString(rowdetails["MstSortBy"]) : string.Empty;

                                            priceCategoryList.Add(new PriceWiseCategoryListResponse
                                            {
                                                categoryId = categoryId,
                                                categoryName = categoryName,
                                                imagePath = imagePath,
                                                mstSortBy = mstSortBy,
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (priceCategoryList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = priceCategoryList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<PriceWiseCategoryListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<PriceWiseCategoryListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> HOStockCategories(HOStockCategoriesRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<HOStockCategoriesResponse> stockCategoryList = new List<HOStockCategoriesResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GETHOSTOCKCATEGORY;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string data_id = string.IsNullOrWhiteSpace(param.dataId) ? "" : param.dataId;
                        string data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? "" : param.dataLoginType;


                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string categoryName = rowdetails["category_name"] != DBNull.Value ? Convert.ToString(rowdetails["category_name"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string mstSortBy = rowdetails["MstSortBy"] != DBNull.Value ? Convert.ToString(rowdetails["MstSortBy"]) : string.Empty;
                                            string categoryCount = rowdetails["category_count"] != DBNull.Value ? Convert.ToString(rowdetails["category_count"]) : string.Empty;

                                            stockCategoryList.Add(new HOStockCategoriesResponse
                                            {
                                                categoryId = categoryId,
                                                categoryName = categoryName,
                                                imagePath = imagePath,
                                                mstSortBy = mstSortBy,
                                                categoryCount = categoryCount
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (stockCategoryList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = stockCategoryList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<HOStockCategoriesResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<HOStockCategoriesResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> SolitaireSortBy(CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<SortByResponse> solitaireSortBy = new List<SortByResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SolitaireSortBy;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string sortId = rowdetails["sort_id"] != DBNull.Value ? Convert.ToString(rowdetails["sort_id"]) : string.Empty;
                                            string sortCode = rowdetails["sort_cd"] != DBNull.Value ? Convert.ToString(rowdetails["sort_cd"]) : string.Empty;
                                            string sortName = rowdetails["sort_name"] != DBNull.Value ? Convert.ToString(rowdetails["sort_name"]) : string.Empty;
                                            string sortDescription = rowdetails["sort_description"] != DBNull.Value ? Convert.ToString(rowdetails["sort_description"]) : string.Empty;
                                            string sortMasterId = rowdetails["sort_master_id"] != DBNull.Value ? Convert.ToString(rowdetails["sort_master_id"]) : string.Empty;

                                            solitaireSortBy.Add(new SortByResponse
                                            {
                                                sortId = sortId,
                                                sortCode = sortCode,
                                                sortName = sortName,
                                                sortDescription = sortDescription,
                                                sortMasterId = sortMasterId
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (solitaireSortBy.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = solitaireSortBy;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<SortByResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<SortByResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> HOStockSortBy(HOStockSortByRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<SortByResponse> solitaireSortBy = new List<SortByResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string data_id = string.IsNullOrWhiteSpace(param.dataId) ? "" : param.dataId;
                    string data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? "" : param.dataLoginType;

                    string cmdQuery = DBCommands.GETHOSTOCKSORTBY;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string sortId = rowdetails["sort_id"] != DBNull.Value ? Convert.ToString(rowdetails["sort_id"]) : string.Empty;
                                            string sortCode = rowdetails["sort_cd"] != DBNull.Value ? Convert.ToString(rowdetails["sort_cd"]) : string.Empty;
                                            string sortName = rowdetails["sort_name"] != DBNull.Value ? Convert.ToString(rowdetails["sort_name"]) : string.Empty;
                                            string sortDescription = rowdetails["sort_description"] != DBNull.Value ? Convert.ToString(rowdetails["sort_description"]) : string.Empty;
                                            string sortMasterId = rowdetails["sort_mster_id"] != DBNull.Value ? Convert.ToString(rowdetails["sort_mster_id"]) : string.Empty;

                                            solitaireSortBy.Add(new SortByResponse
                                            {
                                                sortId = sortId,
                                                sortCode = sortCode,
                                                sortName = sortName,
                                                sortDescription = sortDescription,
                                                sortMasterId = sortMasterId
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (solitaireSortBy.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = solitaireSortBy;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<SortByResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<SortByResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> HOStockItemList(HOStockItemListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            IList<HOStockItemListResponse> hoStockItemList = new List<HOStockItemListResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string? data_id = string.IsNullOrWhiteSpace(param.dataId) ? null : param.dataId;
                    string? data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? null : param.dataLoginType;
                    string? category_id = string.IsNullOrWhiteSpace(param.categoryId) ? null : param.categoryId;
                    string? item_name = string.IsNullOrWhiteSpace(param.itemName) ? null : param.itemName;
                    string? sub_category_id = string.IsNullOrWhiteSpace(param.subCategoryId) ? null : param.subCategoryId;
                    string? dsg_kt = string.IsNullOrWhiteSpace(param.dsgKt) ? null : param.dsgKt;
                    string? amount = string.IsNullOrWhiteSpace(param.amount) ? null : param.amount;
                    string? sort_id = string.IsNullOrWhiteSpace(param.sortId) ? null : param.sortId;
                    string? item_tag = string.IsNullOrWhiteSpace(param.itemTag) ? null : param.itemTag;
                    string? brand = string.IsNullOrWhiteSpace(param.brand) ? null : param.brand;
                    string? gender_id = string.IsNullOrWhiteSpace(param.genderId) ? null : param.genderId;
                    string? delivery_days = string.IsNullOrWhiteSpace(param.deliveryDays) ? null : param.deliveryDays;
                    string? limit = string.IsNullOrWhiteSpace(param.limit) ? null : param.limit;
                    string? item_id = string.IsNullOrWhiteSpace(param.itemId) ? null : param.itemId;
                    string? metal_wt = string.IsNullOrWhiteSpace(param.metalWt) ? null : param.metalWt;
                    string? dia_wt = string.IsNullOrWhiteSpace(param.diaWt) ? null : param.diaWt;
                    string? page = string.IsNullOrWhiteSpace(param.page) ? null : param.page;
                    string? item_sub_category_id = string.IsNullOrWhiteSpace(param.itemSubCategoryId) ? null : param.itemSubCategoryId;
                    string? item_sub_sub_category_id = string.IsNullOrWhiteSpace(param.itemSubSubCategoryId) ? null : param.itemSubSubCategoryId;

                    string cmdQuery = DBCommands.GETHOSTOCKITEMLIST;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 500;

                        cmd.Parameters.AddWithValue("@DataId", data_id);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", data_login_type);
                        cmd.Parameters.AddWithValue("@CategoryID", category_id);
                        cmd.Parameters.AddWithValue("@ItemName", item_name);
                        cmd.Parameters.AddWithValue("@SubCategoryID", sub_category_id);
                        cmd.Parameters.AddWithValue("@DsgKt", dsg_kt);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@SortId", sort_id);
                        cmd.Parameters.AddWithValue("@ItemTag", item_tag);
                        cmd.Parameters.AddWithValue("@Brand", brand);
                        cmd.Parameters.AddWithValue("@GenderID", gender_id);
                        cmd.Parameters.AddWithValue("@DeliveryDays", delivery_days);
                        cmd.Parameters.AddWithValue("@DefaultLimitAppPage", limit);
                        cmd.Parameters.AddWithValue("@Item_ID", item_id);
                        cmd.Parameters.AddWithValue("@MetalWt", metal_wt);
                        cmd.Parameters.AddWithValue("@DiaWt", dia_wt);
                        cmd.Parameters.AddWithValue("@Page", page);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", item_sub_category_id);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", item_sub_sub_category_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemGenderCommonID = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string itemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string itemKt = rowdetails["item_kt"] != DBNull.Value ? Convert.ToString(rowdetails["item_kt"]) : string.Empty;
                                            string plainGoldStatus = rowdetails["plaingold_status"] != DBNull.Value ? Convert.ToString(rowdetails["plaingold_status"]) : string.Empty;
                                            string mostOrder = rowdetails["mostOrder"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder"]) : string.Empty;
                                            string approxDeliveryDate = rowdetails["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["ApproxDeliveryDate"]) : string.Empty;
                                            string itemBrandText = rowdetails["item_brand_text"] != DBNull.Value ? Convert.ToString(rowdetails["item_brand_text"]) : string.Empty;
                                            string itemIsSRP = rowdetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowdetails["ItemIsSRP"]) : string.Empty;
                                            string subCategoryId = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string priceFlag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string isStockFilter = rowdetails["isStockFilter"] != DBNull.Value ? Convert.ToString(rowdetails["isStockFilter"]) : string.Empty;
                                            string isInFranchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            hoStockItemList.Add(new HOStockItemListResponse
                                            {
                                                ItemId = itemId,
                                                CategoryId = categoryId,
                                                ItemDescription = itemDescription,
                                                ItemCode = itemCode,
                                                ItemName = itemName,
                                                ItemGenderCommonID = itemGenderCommonID,
                                                ItemNosePinScrewSts = itemNosePinScrewSts,
                                                ItemKt = itemKt,
                                                PlainGoldStatus = plainGoldStatus,
                                                MostOrder = mostOrder,
                                                ApproxDeliveryDate = approxDeliveryDate,
                                                ItemBrandText = itemBrandText,
                                                ItemIsSRP = itemIsSRP,
                                                SubCategoryId = subCategoryId,
                                                PriceFlag = priceFlag,
                                                ItemMrp = itemMrp,
                                                ImagePath = imagePath,
                                                ProductTags = productTags,
                                                IsStockFilter = isStockFilter,
                                                IsInFranchiseStore = isInFranchiseStore
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (hoStockItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = hoStockItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<HOStockItemListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<HOStockItemListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CustomCollectionSortBy(CustomCollectionSortByRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<SortByResponse> solitaireSortBy = new List<SortByResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string data_id = string.IsNullOrWhiteSpace(param.dataId) ? "" : param.dataId;
                    string data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? "" : param.dataLoginType;
                    string collection_id = string.IsNullOrWhiteSpace(param.collectionId) ? "" : param.collectionId;
                    string category_id = string.IsNullOrWhiteSpace(param.categoryId) ? "" : param.categoryId;

                    string cmdQuery = DBCommands.GETCUSTOMCOLLECTIONSORTBY;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@collection_id", collection_id);
                        cmd.Parameters.AddWithValue("@category_id", category_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string sortId = rowdetails["sort_id"] != DBNull.Value ? Convert.ToString(rowdetails["sort_id"]) : string.Empty;
                                            string sortCode = rowdetails["sort_cd"] != DBNull.Value ? Convert.ToString(rowdetails["sort_cd"]) : string.Empty;
                                            string sortName = rowdetails["sort_name"] != DBNull.Value ? Convert.ToString(rowdetails["sort_name"]) : string.Empty;
                                            string sortDescription = rowdetails["sort_description"] != DBNull.Value ? Convert.ToString(rowdetails["sort_description"]) : string.Empty;
                                            string sortMasterId = rowdetails["sort_master_id"] != DBNull.Value ? Convert.ToString(rowdetails["sort_master_id"]) : string.Empty;

                                            solitaireSortBy.Add(new SortByResponse
                                            {
                                                sortId = sortId,
                                                sortCode = sortCode,
                                                sortName = sortName,
                                                sortDescription = sortDescription,
                                                sortMasterId = sortMasterId
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (solitaireSortBy.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = solitaireSortBy;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<SortByResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<SortByResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> PriceWiseItemSortBy(CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<SortByResponse> solitaireSortBy = new List<SortByResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SolitaireSortBy;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string sortId = rowdetails["sort_id"] != DBNull.Value ? Convert.ToString(rowdetails["sort_id"]) : string.Empty;
                                            string sortCode = rowdetails["sort_cd"] != DBNull.Value ? Convert.ToString(rowdetails["sort_cd"]) : string.Empty;
                                            string sortName = rowdetails["sort_name"] != DBNull.Value ? Convert.ToString(rowdetails["sort_name"]) : string.Empty;
                                            string sortDescription = rowdetails["sort_description"] != DBNull.Value ? Convert.ToString(rowdetails["sort_description"]) : string.Empty;
                                            string sortMasterId = rowdetails["sort_master_id"] != DBNull.Value ? Convert.ToString(rowdetails["sort_master_id"]) : string.Empty;

                                            solitaireSortBy.Add(new SortByResponse
                                            {
                                                sortId = sortId,
                                                sortCode = sortCode,
                                                sortName = sortName,
                                                sortDescription = sortDescription,
                                                sortMasterId = sortMasterId
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (solitaireSortBy.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = solitaireSortBy;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<SortByResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<SortByResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetAllSubCategoryList(AllSubCategoryListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<CustomCollectionSubCategoryListResponse> subCategoryList = new List<CustomCollectionSubCategoryListResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.AllSubCategoryList;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string data_id = string.IsNullOrWhiteSpace(param.dataId) ? "" : param.dataId;
                        string data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? "" : param.dataLoginType;
                        string button_code = string.IsNullOrWhiteSpace(param.buttonCode) ? "" : param.buttonCode;
                        string category_id = string.IsNullOrWhiteSpace(param.categoryId) ? "" : param.categoryId;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@DataID", data_id);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", data_login_type);
                        cmd.Parameters.AddWithValue("@CategoryID", category_id);
                        cmd.Parameters.AddWithValue("@ButtonCode", button_code);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemSubCategiryId = rowdetails["item_sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_sub_category_id"]) : string.Empty;
                                            string itemSubCategiryName = rowdetails["item_sub_category_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_sub_category_name"]) : string.Empty;
                                            string itemSubCategiryCount = rowdetails["item_sub_category_count"] != DBNull.Value ? Convert.ToString(rowdetails["item_sub_category_count"]) : string.Empty;

                                            subCategoryList.Add(new CustomCollectionSubCategoryListResponse
                                            {
                                                itemSubCategoryId = itemSubCategiryId,
                                                itemSubCategoryName = itemSubCategiryName,
                                                itemSubCategoryCount = itemSubCategiryCount,
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (subCategoryList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = subCategoryList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<CustomCollectionSubCategoryListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CustomCollectionSubCategoryListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> PlainGoldItemListFranSIS(PlainGoldItemListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<PlainGoldItemListfranSISResponse> plaingoldList = new List<PlainGoldItemListfranSISResponse>();

            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PlainGoldItemListFranSIS;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? data_id = string.IsNullOrWhiteSpace(param.data_id) ? null : param.data_id;
                        string? data_login_type = string.IsNullOrWhiteSpace(param.data_login_type) ? null : param.data_login_type;
                        string? master_common_id = string.IsNullOrWhiteSpace(param.master_common_id) ? null : param.master_common_id;
                        string? category_id = string.IsNullOrWhiteSpace(param.category_id) ? null : param.category_id;
                        string? sort_id = string.IsNullOrWhiteSpace(param.sort_id) ? null : param.sort_id;
                        string? item_name = string.IsNullOrWhiteSpace(param.item_name) ? null : param.item_name;
                        string? size = string.IsNullOrWhiteSpace(param.size) ? null : param.size;
                        string? color = string.IsNullOrWhiteSpace(param.color) ? null : param.color;
                        string? item_id = string.IsNullOrWhiteSpace(param.item_id) ? null : param.item_id;
                        string? price = string.IsNullOrWhiteSpace(param.price) ? null : param.price;
                        string? dsg_weight = string.IsNullOrWhiteSpace(param.dsg_weight) ? null : param.dsg_weight;
                        string? dsg_westage = string.IsNullOrWhiteSpace(param.dsg_westage) ? null : param.dsg_westage;
                        string? diawt = string.IsNullOrWhiteSpace(param.diawt) ? null : param.diawt;
                        string? dsgkt = string.IsNullOrWhiteSpace(param.dsgKt) ? null : param.dsgKt;
                        string? brand = string.IsNullOrWhiteSpace(param.brand) ? null : param.brand;
                        string? approx_days = string.IsNullOrWhiteSpace(param.approx_days) ? null : param.approx_days;
                        string? gender = string.IsNullOrWhiteSpace(param.gender) ? null : param.gender;
                        string? tags = string.IsNullOrWhiteSpace(param.tags) ? null : param.tags;
                        string? sub_category_id = string.IsNullOrWhiteSpace(param.subCategoryId) ? null : param.subCategoryId;
                        string? item_sub_ctg_ids = string.IsNullOrWhiteSpace(param.itemSubCtgIDs) ? null : param.itemSubCtgIDs;
                        string? item_sub_sub_ctg_ids = string.IsNullOrWhiteSpace(param.itemSubSubCtgIDs) ? null : param.itemSubSubCtgIDs;
                        string? sales_location = string.IsNullOrWhiteSpace(param.sales_location) ? null : param.sales_location;
                        string? design_time_line = string.IsNullOrWhiteSpace(param.design_time_line) ? null : param.design_time_line;
                        string? item_sub_category_id = string.IsNullOrWhiteSpace(param.item_sub_category_id) ? null : param.item_sub_category_id;
                        string? org_type = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;
                        string? page = string.IsNullOrWhiteSpace(param.page) ? null : param.page;
                        string? limit = string.IsNullOrWhiteSpace(param.limit) ? null : param.limit;
                        string? variant = string.IsNullOrWhiteSpace(param.variant) ? null : param.variant;
                        string? stock_av = string.IsNullOrWhiteSpace(param.stock_av) ? null : param.stock_av;
                        string? family_av = string.IsNullOrWhiteSpace(param.family_av) ? null : param.family_av;
                        string? regular_av = string.IsNullOrWhiteSpace(param.regular_av) ? null : param.regular_av;
                        string? wearit = string.IsNullOrWhiteSpace(param.wearit) ? null : param.wearit;
                        string? tryon = string.IsNullOrWhiteSpace(param.tryon) ? null : param.tryon;
                        string? fran_store_av = string.IsNullOrWhiteSpace(param.fran_store_av) ? null : param.fran_store_av;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;

                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@limit", limit);
                        cmd.Parameters.AddWithValue("@DataId", data_id);
                        cmd.Parameters.AddWithValue("@DataLoginType", data_login_type);
                        cmd.Parameters.AddWithValue("@MasterCommonId", master_common_id);
                        cmd.Parameters.AddWithValue("@CategoryID", category_id);
                        cmd.Parameters.AddWithValue("@sortid", sort_id);
                        cmd.Parameters.AddWithValue("@variant", variant);
                        cmd.Parameters.AddWithValue("@itemName", item_name);
                        cmd.Parameters.AddWithValue("@Size", size);
                        cmd.Parameters.AddWithValue("@Color", color);
                        cmd.Parameters.AddWithValue("@Item_ID", item_id);
                        cmd.Parameters.AddWithValue("@Stock_Av", stock_av);
                        cmd.Parameters.AddWithValue("@Family_Av", family_av);
                        cmd.Parameters.AddWithValue("@Regular_Av", regular_av);
                        cmd.Parameters.AddWithValue("@wearit", wearit);
                        cmd.Parameters.AddWithValue("@tryon", tryon);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@dsg_weight", dsg_weight);
                        cmd.Parameters.AddWithValue("@dsg_westage", dsg_westage);
                        cmd.Parameters.AddWithValue("@diawt", diawt);
                        cmd.Parameters.AddWithValue("@Brand", brand);
                        cmd.Parameters.AddWithValue("@DsgKt", dsgkt);
                        cmd.Parameters.AddWithValue("@approx_days", approx_days);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@Tags", tags);
                        cmd.Parameters.AddWithValue("@SubCategoryID", sub_category_id);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", item_sub_ctg_ids);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", item_sub_sub_ctg_ids);
                        cmd.Parameters.AddWithValue("@salesLocation", sales_location);
                        cmd.Parameters.AddWithValue("@designTimeLine", design_time_line);
                        cmd.Parameters.AddWithValue("@item_sub_category_id", item_sub_category_id);
                        cmd.Parameters.AddWithValue("@fran_store_av", fran_store_av);
                        cmd.Parameters.AddWithValue("@org_type", org_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemGenderCommonId = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string itemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string plainGoldStatus = rowdetails["plaingold_status"] != DBNull.Value ? Convert.ToString(rowdetails["plaingold_status"]) : string.Empty;
                                            string subCategoryId = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string itemIsSrp = rowdetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowdetails["ItemIsSRP"]) : string.Empty;
                                            string itemPrice = rowdetails["item_price"] != DBNull.Value ? Convert.ToString(rowdetails["item_price"]) : string.Empty;
                                            string distPrice = rowdetails["dist_price"] != DBNull.Value ? Convert.ToString(rowdetails["dist_price"]) : string.Empty;
                                            string approxDeliveryDate = rowdetails["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["ApproxDeliveryDate"]) : string.Empty;
                                            string priceflag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string maxQtySold = rowdetails["max_qty_sold"] != DBNull.Value ? Convert.ToString(rowdetails["max_qty_sold"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string mostOrder = rowdetails["mostOrder"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder"]) : string.Empty;
                                            string productTagsJson = rowdetails["productTagsJson"] != DBNull.Value ? Convert.ToString(rowdetails["productTagsJson"]) : string.Empty;
                                            string isStockFilter = rowdetails["isStockFilter"] != DBNull.Value ? Convert.ToString(rowdetails["isStockFilter"]) : string.Empty;
                                            string isInFranchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            List<Dictionary<string, object>> productTagsDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(productTagsJson))
                                            {
                                                try
                                                {
                                                    productTagsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(productTagsJson);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing productTagsJson: " + ex.Message);
                                                }
                                            }

                                            plaingoldList.Add(new PlainGoldItemListfranSISResponse
                                            {
                                                itemId = itemId,
                                                categoryId = categoryId,
                                                itemDescription = itemDescription,
                                                itemCode = itemCode,
                                                itemName = itemName,
                                                itemGenderCommonID = itemGenderCommonId,
                                                itemNosePinScrewSts = itemNosePinScrewSts,
                                                plainGoldStatus = plainGoldStatus,
                                                subCategoryId = subCategoryId,
                                                itemIsSRP = itemIsSrp,
                                                itemPrice = itemPrice,
                                                distPrice = distPrice,
                                                approxDeliveryDate = approxDeliveryDate,
                                                priceflag = priceflag,
                                                itemMrp = itemMrp,
                                                maxQtySold = maxQtySold,
                                                imagePath = imagePath,
                                                productTags = productTagsDynamic,
                                                stockFilter = isStockFilter,
                                                franchiseStore = isInFranchiseStore
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (plaingoldList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = plaingoldList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<PlainGoldItemListfranSISResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<PlainGoldItemListfranSISResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> ColorStoneItemListFranSIS(ColorStoneItemListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<ColorStoneItemListResponse> colorStoneList = new List<ColorStoneItemListResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ColorStoneItemListFranSIS;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? page = string.IsNullOrWhiteSpace(param.page) ? null : param.page;
                        string? limit = string.IsNullOrWhiteSpace(param.limit) ? null : param.limit;
                        string? data_id = string.IsNullOrWhiteSpace(param.data_id) ? null : param.data_id;
                        string? data_login_type = string.IsNullOrWhiteSpace(param.data_login_type) ? null : param.data_login_type;
                        string? master_common_id = string.IsNullOrWhiteSpace(param.master_common_id) ? null : param.master_common_id;
                        string? category_id = string.IsNullOrWhiteSpace(param.category_id) ? null : param.category_id;
                        string? sort_id = string.IsNullOrWhiteSpace(param.sort_id) ? null : param.sort_id;
                        string? variant = string.IsNullOrWhiteSpace(param.variant) ? null : param.variant;
                        string? item_name = string.IsNullOrWhiteSpace(param.item_name) ? null : param.item_name;
                        string? sub_category_id = string.IsNullOrWhiteSpace(param.subCategoryId) ? null : param.subCategoryId;
                        string? size = string.IsNullOrWhiteSpace(param.size) ? null : param.size;
                        string? dsg_kt = string.IsNullOrWhiteSpace(param.dsgKt) ? null : param.dsgKt;
                        string? color = string.IsNullOrWhiteSpace(param.color) ? null : param.color;
                        string? price = string.IsNullOrWhiteSpace(param.price) ? null : param.price;
                        string? metalwt = string.IsNullOrWhiteSpace(param.metalwt) ? null : param.metalwt;
                        string? diawt = string.IsNullOrWhiteSpace(param.diawt) ? null : param.diawt;
                        string? item_id = string.IsNullOrWhiteSpace(param.item_id) ? null : param.item_id;
                        string? stock_av = string.IsNullOrWhiteSpace(param.stock_av) ? null : param.stock_av;
                        string? family_av = string.IsNullOrWhiteSpace(param.family_av) ? null : param.family_av;
                        string? regular_av = string.IsNullOrWhiteSpace(param.regular_av) ? null : param.regular_av;
                        string? wearit = string.IsNullOrWhiteSpace(param.wearit) ? null : param.wearit;
                        string? tryon = string.IsNullOrWhiteSpace(param.tryon) ? null : param.tryon;
                        string? gender = string.IsNullOrWhiteSpace(param.gender) ? null : param.gender;
                        string? tags = string.IsNullOrWhiteSpace(param.tags) ? null : param.tags;
                        string? brand = string.IsNullOrWhiteSpace(param.brand) ? null : param.brand;
                        string? approx_days = string.IsNullOrWhiteSpace(param.approx_days) ? null : param.approx_days;
                        string? item_sub_ctg_ids = string.IsNullOrWhiteSpace(param.itemSubCtgIDs) ? null : param.itemSubCtgIDs;
                        string? item_sub_sub_ctg_ids = string.IsNullOrWhiteSpace(param.itemSubSubCtgIDs) ? null : param.itemSubSubCtgIDs;
                        string? sales_location = string.IsNullOrWhiteSpace(param.sales_location) ? null : param.sales_location;
                        string? design_time_line = string.IsNullOrWhiteSpace(param.design_time_line) ? null : param.design_time_line;
                        string? item_sub_category_id = string.IsNullOrWhiteSpace(param.item_sub_category_id) ? null : param.item_sub_category_id;
                        string? fran_store_av = string.IsNullOrWhiteSpace(param.fran_store_av) ? null : param.fran_store_av;
                        string? org_type = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@limit", limit);
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@sort_id", sort_id);
                        cmd.Parameters.AddWithValue("@variant", variant);
                        cmd.Parameters.AddWithValue("@item_name", item_name);
                        cmd.Parameters.AddWithValue("@subcategory_id", sub_category_id);
                        cmd.Parameters.AddWithValue("@size", size);
                        cmd.Parameters.AddWithValue("@dsgkt", dsg_kt);
                        cmd.Parameters.AddWithValue("@color", color);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@metalwt", metalwt);
                        cmd.Parameters.AddWithValue("@diawt", diawt);
                        cmd.Parameters.AddWithValue("@item_id", item_id);
                        cmd.Parameters.AddWithValue("@stock_av", stock_av);
                        cmd.Parameters.AddWithValue("@family_av", family_av);
                        cmd.Parameters.AddWithValue("@regular_av", regular_av);
                        cmd.Parameters.AddWithValue("@wearit", wearit);
                        cmd.Parameters.AddWithValue("@tryon", tryon);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@tags", tags);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@approx_days", approx_days);
                        cmd.Parameters.AddWithValue("@itemsubctgids", item_sub_ctg_ids);
                        cmd.Parameters.AddWithValue("@itemsubsubctgids", item_sub_sub_ctg_ids);
                        cmd.Parameters.AddWithValue("@saleslocation", sales_location);
                        cmd.Parameters.AddWithValue("@designtimeline", design_time_line);
                        cmd.Parameters.AddWithValue("@item_sub_category_id", item_sub_category_id);
                        cmd.Parameters.AddWithValue("@fran_store_av", fran_store_av);
                        cmd.Parameters.AddWithValue("@org_type", org_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemGenderCommonId = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string itemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string subCategoryId = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string itemIsSrp = rowdetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowdetails["ItemIsSRP"]) : string.Empty;
                                            string itemPrice = rowdetails["item_price"] != DBNull.Value ? Convert.ToString(rowdetails["item_price"]) : string.Empty;
                                            string approxDeliveryDate = rowdetails["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["ApproxDeliveryDate"]) : string.Empty;
                                            string priceflag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string mostOrder_1 = rowdetails["mostOrder_1"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder_1"]) : string.Empty;
                                            string maxQtySold = rowdetails["max_qty_sold"] != DBNull.Value ? Convert.ToString(rowdetails["max_qty_sold"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string mostOrder_2 = rowdetails["mostOrder_2"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder_2"]) : string.Empty;
                                            string productTagsJson = rowdetails["productTagsJson"] != DBNull.Value ? Convert.ToString(rowdetails["productTagsJson"]) : string.Empty;
                                            string isStockFilter = rowdetails["isStockFilter"] != DBNull.Value ? Convert.ToString(rowdetails["isStockFilter"]) : string.Empty;
                                            string isInFranchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            List<Dictionary<string, object>> productTagsDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(productTagsJson))
                                            {
                                                try
                                                {
                                                    productTagsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(productTagsJson);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing productTagsJson: " + ex.Message);
                                                }
                                            }

                                            colorStoneList.Add(new ColorStoneItemListResponse
                                            {
                                                itemId = itemId,
                                                categoryId = categoryId,
                                                itemDescription = itemDescription,
                                                itemCode = itemCode,
                                                itemName = itemName,
                                                itemGenderCommonId = itemGenderCommonId,
                                                itemNosePinScrewSts = itemNosePinScrewSts,
                                                subCategoryId = subCategoryId,
                                                itemIsSRP = itemIsSrp,
                                                itemPrice = itemPrice,
                                                approxDeliveryDate = approxDeliveryDate,
                                                priceFlag = priceflag,
                                                itemMrp = itemMrp,
                                                mostOrder_1 = mostOrder_1,
                                                maxQtySold = maxQtySold,
                                                imagePath = imagePath,
                                                productTags = productTagsDynamic,
                                                mostOrder_2 = mostOrder_2,
                                                stockFilter = isStockFilter,
                                                franchiseStore = isInFranchiseStore
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (colorStoneList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = colorStoneList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<ColorStoneItemListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<ColorStoneItemListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> PlatinumItemListFranSIS(PlatinumItemListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<PlatinumItemListResponse> platinumItemList = new List<PlatinumItemListResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PlatinumItemListFranSIS;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? page = string.IsNullOrWhiteSpace(param.page) ? null : param.page;
                        string? limit = string.IsNullOrWhiteSpace(param.limit) ? null : param.limit;
                        string? data_id = string.IsNullOrWhiteSpace(param.data_id) ? null : param.data_id;
                        string? data_login_type = string.IsNullOrWhiteSpace(param.data_login_type) ? null : param.data_login_type;
                        string? master_common_id = string.IsNullOrWhiteSpace(param.master_common_id) ? null : param.master_common_id;
                        string? category_id = string.IsNullOrWhiteSpace(param.category_id) ? null : param.category_id;
                        string? sort_id = string.IsNullOrWhiteSpace(param.sort_id) ? null : param.sort_id;
                        string? variant = string.IsNullOrWhiteSpace(param.variant) ? null : param.variant;
                        string? item_name = string.IsNullOrWhiteSpace(param.item_name) ? null : param.item_name;
                        string? sub_category_id = string.IsNullOrWhiteSpace(param.subCategoryId) ? null : param.subCategoryId;
                        string? size = string.IsNullOrWhiteSpace(param.size) ? null : param.size;
                        string? dsg_kt = string.IsNullOrWhiteSpace(param.dsgKt) ? null : param.dsgKt;
                        string? color = string.IsNullOrWhiteSpace(param.color) ? null : param.color;
                        string? price = string.IsNullOrWhiteSpace(param.price) ? null : param.price;
                        string? metalwt = string.IsNullOrWhiteSpace(param.metalwt) ? null : param.metalwt;
                        string? diawt = string.IsNullOrWhiteSpace(param.diawt) ? null : param.diawt;
                        string? item_id = string.IsNullOrWhiteSpace(param.item_id) ? null : param.item_id;
                        string? stock_av = string.IsNullOrWhiteSpace(param.stock_av) ? null : param.stock_av;
                        string? family_av = string.IsNullOrWhiteSpace(param.family_av) ? null : param.family_av;
                        string? regular_av = string.IsNullOrWhiteSpace(param.regular_av) ? null : param.regular_av;
                        string? wearit = string.IsNullOrWhiteSpace(param.wearit) ? null : param.wearit;
                        string? tryon = string.IsNullOrWhiteSpace(param.tryon) ? null : param.tryon;
                        string? gender = string.IsNullOrWhiteSpace(param.gender) ? null : param.gender;
                        string? tags = string.IsNullOrWhiteSpace(param.tags) ? null : param.tags;
                        string? brand = string.IsNullOrWhiteSpace(param.brand) ? null : param.brand;
                        string? approx_days = string.IsNullOrWhiteSpace(param.approx_days) ? null : param.approx_days;
                        string? item_sub_ctg_ids = string.IsNullOrWhiteSpace(param.itemSubCtgIDs) ? null : param.itemSubCtgIDs;
                        string? item_sub_sub_ctg_ids = string.IsNullOrWhiteSpace(param.itemSubSubCtgIDs) ? null : param.itemSubSubCtgIDs;
                        string? sales_location = string.IsNullOrWhiteSpace(param.sales_location) ? null : param.sales_location;
                        string? design_time_line = string.IsNullOrWhiteSpace(param.design_time_line) ? null : param.design_time_line;
                        string? item_sub_category_id = string.IsNullOrWhiteSpace(param.item_sub_category_id) ? null : param.item_sub_category_id;
                        string? fran_store_av = string.IsNullOrWhiteSpace(param.fran_store_av) ? null : param.fran_store_av;
                        string? org_type = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@limit", limit);
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@sort_id", sort_id);
                        cmd.Parameters.AddWithValue("@variant", variant);
                        cmd.Parameters.AddWithValue("@item_name", item_name);
                        cmd.Parameters.AddWithValue("@subcategory_id", sub_category_id);
                        cmd.Parameters.AddWithValue("@size", size);
                        cmd.Parameters.AddWithValue("@dsgkt", dsg_kt);
                        cmd.Parameters.AddWithValue("@color", color);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@metalwt", metalwt);
                        cmd.Parameters.AddWithValue("@diawt", diawt);
                        cmd.Parameters.AddWithValue("@item_id", item_id);
                        cmd.Parameters.AddWithValue("@stock_av", stock_av);
                        cmd.Parameters.AddWithValue("@family_av", family_av);
                        cmd.Parameters.AddWithValue("@regular_av", regular_av);
                        cmd.Parameters.AddWithValue("@wearit", wearit);
                        cmd.Parameters.AddWithValue("@tryon", tryon);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@tags", tags);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@approx_days", approx_days);
                        cmd.Parameters.AddWithValue("@itemsubctgids", item_sub_ctg_ids);
                        cmd.Parameters.AddWithValue("@itemsubsubctgids", item_sub_sub_ctg_ids);
                        cmd.Parameters.AddWithValue("@saleslocation", sales_location);
                        cmd.Parameters.AddWithValue("@designtimeline", design_time_line);
                        cmd.Parameters.AddWithValue("@item_sub_category_id", item_sub_category_id);
                        cmd.Parameters.AddWithValue("@fran_store_av", fran_store_av);
                        cmd.Parameters.AddWithValue("@org_type", org_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemGenderCommonId = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string itemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string subCategoryId = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string itemIsSrp = rowdetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowdetails["ItemIsSRP"]) : string.Empty;
                                            string itemPrice = rowdetails["item_price"] != DBNull.Value ? Convert.ToString(rowdetails["item_price"]) : string.Empty;
                                            string approxDeliveryDate = rowdetails["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["ApproxDeliveryDate"]) : string.Empty;
                                            string priceflag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string mostOrder_1 = rowdetails["mostOrder_1"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder_1"]) : string.Empty;
                                            string maxQtySold = rowdetails["max_qty_sold"] != DBNull.Value ? Convert.ToString(rowdetails["max_qty_sold"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string mostOrder_2 = rowdetails["mostOrder_2"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder_2"]) : string.Empty;
                                            string productTagsJson = rowdetails["productTagsJson"] != DBNull.Value ? Convert.ToString(rowdetails["productTagsJson"]) : string.Empty;
                                            string isStockFilter = rowdetails["isStockFilter"] != DBNull.Value ? Convert.ToString(rowdetails["isStockFilter"]) : string.Empty;
                                            string isInFranchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            List<Dictionary<string, object>> productTagsDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(productTagsJson))
                                            {
                                                try
                                                {
                                                    productTagsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(productTagsJson);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing productTagsJson: " + ex.Message);
                                                }
                                            }

                                            platinumItemList.Add(new PlatinumItemListResponse
                                            {
                                                itemId = itemId,
                                                categoryId = categoryId,
                                                itemDescription = itemDescription,
                                                itemCode = itemCode,
                                                itemName = itemName,
                                                itemGenderCommonID = itemGenderCommonId,
                                                itemNosePinScrewSts = itemNosePinScrewSts,
                                                subCategoryId = subCategoryId,
                                                itemIsSrp = itemIsSrp,
                                                itemPrice = itemPrice,
                                                approxDeliveryDate = approxDeliveryDate,
                                                priceFlag = priceflag,
                                                itemMrp = itemMrp,
                                                mostOrder_1 = mostOrder_1,
                                                maxQtySold = maxQtySold,
                                                imagePath = imagePath,
                                                productTags = productTagsDynamic,
                                                mostOrder_2 = mostOrder_2,
                                                isStockFilter = isStockFilter,
                                                isInFranchiseStore = isInFranchiseStore
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (platinumItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = platinumItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<PlatinumItemListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<PlatinumItemListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CoupleBandItemListFranSIS(CoupleBandItemListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<CoupleBandItemListResponse> coupleBandItemList = new List<CoupleBandItemListResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CoupleBandItemListFranSIS;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? page = string.IsNullOrWhiteSpace(param.page) ? null : param.page;
                        string? limit = string.IsNullOrWhiteSpace(param.limit) ? null : param.limit;
                        string? data_id = string.IsNullOrWhiteSpace(param.data_id) ? null : param.data_id;
                        string? data_login_type = string.IsNullOrWhiteSpace(param.data_login_type) ? null : param.data_login_type;
                        string? master_common_id = string.IsNullOrWhiteSpace(param.master_common_id) ? null : param.master_common_id;
                        string? category_id = string.IsNullOrWhiteSpace(param.category_id) ? null : param.category_id;
                        string? sort_id = string.IsNullOrWhiteSpace(param.sort_id) ? null : param.sort_id;
                        string? variant = string.IsNullOrWhiteSpace(param.variant) ? null : param.variant;
                        string? item_name = string.IsNullOrWhiteSpace(param.item_name) ? null : param.item_name;
                        string? sub_category_id = string.IsNullOrWhiteSpace(param.subCategoryId) ? null : param.subCategoryId;
                        string? size = string.IsNullOrWhiteSpace(param.size) ? null : param.size;
                        string? dsg_kt = string.IsNullOrWhiteSpace(param.dsgKt) ? null : param.dsgKt;
                        string? color = string.IsNullOrWhiteSpace(param.color) ? null : param.color;
                        string? price = string.IsNullOrWhiteSpace(param.price) ? null : param.price;
                        string? metalwt = string.IsNullOrWhiteSpace(param.metalwt) ? null : param.metalwt;
                        string? diawt = string.IsNullOrWhiteSpace(param.diawt) ? null : param.diawt;
                        string? item_id = string.IsNullOrWhiteSpace(param.item_id) ? null : param.item_id;
                        string? stock_av = string.IsNullOrWhiteSpace(param.stock_av) ? null : param.stock_av;
                        string? family_av = string.IsNullOrWhiteSpace(param.family_av) ? null : param.family_av;
                        string? regular_av = string.IsNullOrWhiteSpace(param.regular_av) ? null : param.regular_av;
                        string? wearit = string.IsNullOrWhiteSpace(param.wearit) ? null : param.wearit;
                        string? tryon = string.IsNullOrWhiteSpace(param.tryon) ? null : param.tryon;
                        string? gender = string.IsNullOrWhiteSpace(param.gender) ? null : param.gender;
                        string? tags = string.IsNullOrWhiteSpace(param.tags) ? null : param.tags;
                        string? brand = string.IsNullOrWhiteSpace(param.brand) ? null : param.brand;
                        string? approx_days = string.IsNullOrWhiteSpace(param.approx_days) ? null : param.approx_days;
                        string? item_sub_ctg_ids = string.IsNullOrWhiteSpace(param.itemSubCtgIDs) ? null : param.itemSubCtgIDs;
                        string? item_sub_sub_ctg_ids = string.IsNullOrWhiteSpace(param.itemSubSubCtgIDs) ? null : param.itemSubSubCtgIDs;
                        string? sales_location = string.IsNullOrWhiteSpace(param.sales_location) ? null : param.sales_location;
                        string? design_time_line = string.IsNullOrWhiteSpace(param.design_time_line) ? null : param.design_time_line;
                        string? item_sub_category_id = string.IsNullOrWhiteSpace(param.item_sub_category_id) ? null : param.item_sub_category_id;
                        string? fran_store_av = string.IsNullOrWhiteSpace(param.fran_store_av) ? null : param.fran_store_av;
                        string? org_type = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@limit", limit);
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@sort_id", sort_id);
                        cmd.Parameters.AddWithValue("@variant", variant);
                        cmd.Parameters.AddWithValue("@item_name", item_name);
                        cmd.Parameters.AddWithValue("@subcategory_id", sub_category_id);
                        cmd.Parameters.AddWithValue("@size", size);
                        cmd.Parameters.AddWithValue("@dsgkt", dsg_kt);
                        cmd.Parameters.AddWithValue("@color", color);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@metalwt", metalwt);
                        cmd.Parameters.AddWithValue("@diawt", diawt);
                        cmd.Parameters.AddWithValue("@item_id", item_id);
                        cmd.Parameters.AddWithValue("@stock_av", stock_av);
                        cmd.Parameters.AddWithValue("@family_av", family_av);
                        cmd.Parameters.AddWithValue("@regular_av", regular_av);
                        cmd.Parameters.AddWithValue("@wearit", wearit);
                        cmd.Parameters.AddWithValue("@tryon", tryon);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@tags", tags);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@approx_days", approx_days);
                        cmd.Parameters.AddWithValue("@itemsubctgids", item_sub_ctg_ids);
                        cmd.Parameters.AddWithValue("@itemsubsubctgids", item_sub_sub_ctg_ids);
                        cmd.Parameters.AddWithValue("@saleslocation", sales_location);
                        cmd.Parameters.AddWithValue("@designtimeline", design_time_line);
                        cmd.Parameters.AddWithValue("@item_sub_category_id", item_sub_category_id);
                        cmd.Parameters.AddWithValue("@fran_store_av", fran_store_av);
                        cmd.Parameters.AddWithValue("@org_type", org_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemGenderCommonId = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string itemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string subCategoryId = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string itemIsSrp = rowdetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowdetails["ItemIsSRP"]) : string.Empty;
                                            string itemPrice = rowdetails["item_price"] != DBNull.Value ? Convert.ToString(rowdetails["item_price"]) : string.Empty;
                                            string approxDeliveryDate = rowdetails["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["ApproxDeliveryDate"]) : string.Empty;
                                            string inStockDeliveryDate = rowdetails["InStockDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["InStockDeliveryDate"]) : string.Empty;
                                            string priceflag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string mostOrder_1 = rowdetails["mostOrder_1"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder_1"]) : string.Empty;
                                            string maxQtySold = rowdetails["max_qty_sold"] != DBNull.Value ? Convert.ToString(rowdetails["max_qty_sold"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string mostOrder_2 = rowdetails["mostOrder_2"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder_2"]) : string.Empty;
                                            string productTagsJson = rowdetails["productTagsJson"] != DBNull.Value ? Convert.ToString(rowdetails["productTagsJson"]) : string.Empty;
                                            string isStockFilter = rowdetails["isStockFilter"] != DBNull.Value ? Convert.ToString(rowdetails["isStockFilter"]) : string.Empty;
                                            string isInFranchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            List<Dictionary<string, object>> productTagsDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(productTagsJson))
                                            {
                                                try
                                                {
                                                    productTagsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(productTagsJson);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing productTagsJson: " + ex.Message);
                                                }
                                            }

                                            coupleBandItemList.Add(new CoupleBandItemListResponse
                                            {
                                                itemId = itemId,
                                                categoryId = categoryId,
                                                itemDescription = itemDescription,
                                                itemCode = itemCode,
                                                itemName = itemName,
                                                itemGenderCommonID = itemGenderCommonId,
                                                itemNosePinScrewSts = itemNosePinScrewSts,
                                                subCategoryId = subCategoryId,
                                                itemIsSrp = itemIsSrp,
                                                itemPrice = itemPrice,
                                                approxDeliveryDate = approxDeliveryDate,
                                                inStockDeliveryDate = inStockDeliveryDate,
                                                priceFlag = priceflag,
                                                itemMrp = itemMrp,
                                                mostOrder_1 = mostOrder_1,
                                                maxQtySold = maxQtySold,
                                                imagePath = imagePath,
                                                productTags = productTagsDynamic,
                                                mostOrder_2 = mostOrder_2,
                                                isStockFilter = isStockFilter,
                                                isInFranchiseStore = isInFranchiseStore
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (coupleBandItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = coupleBandItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<CoupleBandItemListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CoupleBandItemListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> IllumineItemListFranSIS(IllumineItemListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<IllumineItemListResponse> illumineItemList = new List<IllumineItemListResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.IllumineItemListFranSIS;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? page = string.IsNullOrWhiteSpace(param.page) ? null : param.page;
                        string? limit = string.IsNullOrWhiteSpace(param.limit) ? null : param.limit;
                        string? data_id = string.IsNullOrWhiteSpace(param.data_id) ? null : param.data_id;
                        string? data_login_type = string.IsNullOrWhiteSpace(param.data_login_type) ? null : param.data_login_type;
                        string? master_common_id = string.IsNullOrWhiteSpace(param.master_common_id) ? null : param.master_common_id;
                        string? category_id = string.IsNullOrWhiteSpace(param.category_id) ? null : param.category_id;
                        string? sort_id = string.IsNullOrWhiteSpace(param.sort_id) ? null : param.sort_id;
                        string? sub_category_id = string.IsNullOrWhiteSpace(param.subCategoryId) ? null : param.subCategoryId;
                        string? color = string.IsNullOrWhiteSpace(param.color) ? null : param.color;
                        string? gender = string.IsNullOrWhiteSpace(param.gender) ? null : param.gender;
                        string? brand = string.IsNullOrWhiteSpace(param.brand) ? null : param.brand;
                        string? item_sub_category_id = string.IsNullOrWhiteSpace(param.item_sub_category_id) ? null : param.item_sub_category_id;
                        string? org_type = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@limit", limit);
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@sort_id", sort_id);
                        cmd.Parameters.AddWithValue("@subcategory_id", sub_category_id);
                        cmd.Parameters.AddWithValue("@color", color);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@item_sub_category_id", item_sub_category_id);
                        cmd.Parameters.AddWithValue("@org_type", org_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string rupySymbol = rowdetails["rupy_symbol"] != DBNull.Value ? Convert.ToString(rowdetails["rupy_symbol"]) : string.Empty;
                                            string imgWatchTitle = rowdetails["img_watch_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_watch_title"]) : string.Empty;
                                            string imgWishTitle = rowdetails["img_wish_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wish_title"]) : string.Empty;
                                            string wishCount = rowdetails["wish_count"] != DBNull.Value ? Convert.ToString(rowdetails["wish_count"]) : string.Empty;
                                            string mostOrder_1 = rowdetails["mostOrder_1"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder_1"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string mostOrder_2 = rowdetails["mostOrder_2"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder_2"]) : string.Empty;
                                            string productTagsJson = rowdetails["productTagsJson"] != DBNull.Value ? Convert.ToString(rowdetails["productTagsJson"]) : string.Empty;
                                            string isInFranchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            List<Dictionary<string, object>> productTagsDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(productTagsJson))
                                            {
                                                try
                                                {
                                                    productTagsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(productTagsJson);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing productTagsJson: " + ex.Message);
                                                }
                                            }

                                            illumineItemList.Add(new IllumineItemListResponse
                                            {
                                                itemId = itemId,
                                                categoryId = categoryId,
                                                itemDescription = itemDescription,
                                                itemName = itemName,
                                                itemMrp = itemMrp,
                                                mostOrder_1 = mostOrder_1,
                                                imagePath = imagePath,
                                                productTags = productTagsDynamic,
                                                mostOrder_2 = mostOrder_2,
                                                franchiseStore = isInFranchiseStore,
                                                imageWatchTitle = imgWatchTitle,
                                                imageWishTitle = imgWishTitle,
                                                wishCount = wishCount
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (illumineItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = illumineItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<IllumineItemListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<IllumineItemListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GlemSolitaireItemListSIS(GlemSolitaireItemListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<GlemSolitaireItemListResponse> glemSolitaireItemList = new List<GlemSolitaireItemListResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GlemSolitaireItemListSIS;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? page = string.IsNullOrWhiteSpace(param.page) ? null : param.page;
                        string? limit = string.IsNullOrWhiteSpace(param.limit) ? null : param.limit;
                        string? data_id = string.IsNullOrWhiteSpace(param.data_id) ? null : param.data_id;
                        string? data_login_type = string.IsNullOrWhiteSpace(param.data_login_type) ? null : param.data_login_type;
                        string? master_common_id = string.IsNullOrWhiteSpace(param.master_common_id) ? null : param.master_common_id;
                        string? category_id = string.IsNullOrWhiteSpace(param.category_id) ? null : param.category_id;
                        string? sort_id = string.IsNullOrWhiteSpace(param.sort_id) ? null : param.sort_id;
                        string? variant = string.IsNullOrWhiteSpace(param.variant) ? null : param.variant;
                        string? item_name = string.IsNullOrWhiteSpace(param.item_name) ? null : param.item_name;
                        string? sub_category_id = string.IsNullOrWhiteSpace(param.subCategoryId) ? null : param.subCategoryId;
                        string? size = string.IsNullOrWhiteSpace(param.size) ? null : param.size;
                        string? dsg_kt = string.IsNullOrWhiteSpace(param.dsgKt) ? null : param.dsgKt;
                        string? color = string.IsNullOrWhiteSpace(param.color) ? null : param.color;
                        string? price = string.IsNullOrWhiteSpace(param.price) ? null : param.price;
                        string? metalwt = string.IsNullOrWhiteSpace(param.metalwt) ? null : param.metalwt;
                        string? diawt = string.IsNullOrWhiteSpace(param.diawt) ? null : param.diawt;
                        string? item_id = string.IsNullOrWhiteSpace(param.item_id) ? null : param.item_id;
                        string? stock_av = string.IsNullOrWhiteSpace(param.stock_av) ? null : param.stock_av;
                        string? family_av = string.IsNullOrWhiteSpace(param.family_av) ? null : param.family_av;
                        string? regular_av = string.IsNullOrWhiteSpace(param.regular_av) ? null : param.regular_av;
                        string? wearit = string.IsNullOrWhiteSpace(param.wearit) ? null : param.wearit;
                        string? tryon = string.IsNullOrWhiteSpace(param.tryon) ? null : param.tryon;
                        string? gender = string.IsNullOrWhiteSpace(param.gender) ? null : param.gender;
                        string? tags = string.IsNullOrWhiteSpace(param.tags) ? null : param.tags;
                        string? brand = string.IsNullOrWhiteSpace(param.brand) ? null : param.brand;
                        string? approx_days = string.IsNullOrWhiteSpace(param.approx_days) ? null : param.approx_days;
                        string? item_sub_ctg_ids = string.IsNullOrWhiteSpace(param.itemSubCtgIDs) ? null : param.itemSubCtgIDs;
                        string? item_sub_sub_ctg_ids = string.IsNullOrWhiteSpace(param.itemSubSubCtgIDs) ? null : param.itemSubSubCtgIDs;
                        string? sales_location = string.IsNullOrWhiteSpace(param.sales_location) ? null : param.sales_location;
                        string? design_time_line = string.IsNullOrWhiteSpace(param.design_time_line) ? null : param.design_time_line;
                        string? org_type = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@limit", limit);
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@Data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@sort_id", sort_id);
                        cmd.Parameters.AddWithValue("@variant", variant);
                        cmd.Parameters.AddWithValue("@item_name", item_name);
                        cmd.Parameters.AddWithValue("@subcategory_id", sub_category_id);
                        cmd.Parameters.AddWithValue("@size", size);
                        cmd.Parameters.AddWithValue("@dsgkt", dsg_kt);
                        cmd.Parameters.AddWithValue("@color", color);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@metalwt", metalwt);
                        cmd.Parameters.AddWithValue("@diawt", diawt);
                        cmd.Parameters.AddWithValue("@item_id", item_id);
                        cmd.Parameters.AddWithValue("@stock_av", stock_av);
                        cmd.Parameters.AddWithValue("@family_av", family_av);
                        cmd.Parameters.AddWithValue("@regular_av", regular_av);
                        cmd.Parameters.AddWithValue("@wearit", wearit);
                        cmd.Parameters.AddWithValue("@tryon", tryon);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@tags", tags);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@approx_days", approx_days);
                        cmd.Parameters.AddWithValue("@itemsubctgids", item_sub_ctg_ids);
                        cmd.Parameters.AddWithValue("@itemsubsubctgids", item_sub_sub_ctg_ids);
                        cmd.Parameters.AddWithValue("@saleslocation", sales_location);
                        cmd.Parameters.AddWithValue("@designtimeline", design_time_line);
                        cmd.Parameters.AddWithValue("@org_type", org_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemGenderCommonId = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string itemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string subCategoryId = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string itemIsSrp = rowdetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowdetails["ItemIsSRP"]) : string.Empty;
                                            string itemPrice = rowdetails["item_price"] != DBNull.Value ? Convert.ToString(rowdetails["item_price"]) : string.Empty;
                                            string approxDeliveryDate = rowdetails["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["ApproxDeliveryDate"]) : string.Empty;
                                            string priceflag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string mostOrder_1 = rowdetails["mostOrder_1"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder_1"]) : string.Empty;
                                            string maxQtySold = rowdetails["max_qty_sold"] != DBNull.Value ? Convert.ToString(rowdetails["max_qty_sold"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string mostOrder_2 = rowdetails["mostOrder_2"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder_2"]) : string.Empty;
                                            string productTagsJson = rowdetails["productTagsJson"] != DBNull.Value ? Convert.ToString(rowdetails["productTagsJson"]) : string.Empty;
                                            string isStockFilter = rowdetails["isStockFilter"] != DBNull.Value ? Convert.ToString(rowdetails["isStockFilter"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            List<Dictionary<string, object>> productTagsDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(productTagsJson))
                                            {
                                                try
                                                {
                                                    productTagsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(productTagsJson);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing productTagsJson: " + ex.Message);
                                                }
                                            }

                                            glemSolitaireItemList.Add(new GlemSolitaireItemListResponse
                                            {
                                                itemId = itemId,
                                                categoryId = categoryId,
                                                itemDescription = itemDescription,
                                                itemCode = itemCode,
                                                itemName = itemName,
                                                itemGenderCommonID = itemGenderCommonId,
                                                itemNosePinScrewSts = itemNosePinScrewSts,
                                                subCategoryId = subCategoryId,
                                                itemIsSrp = itemIsSrp,
                                                itemPrice = itemPrice,
                                                approxDeliveryDate = approxDeliveryDate,
                                                priceFlag = priceflag,
                                                itemMrp = itemMrp,
                                                mostOrder_1 = mostOrder_1,
                                                maxQtySold = maxQtySold,
                                                imagePath = imagePath,
                                                productTags = productTagsDynamic,
                                                mostOrder_2 = mostOrder_2,
                                                isStockFilter = isStockFilter
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (glemSolitaireItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = glemSolitaireItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<GlemSolitaireItemListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<GlemSolitaireItemListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> RareSolitaireItemListFran(RareSolitaireItemListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<RareSolitairItemListResponse> rareSolitaireItemList = new List<RareSolitairItemListResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.RareSolitaireItemListFran;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? dataId = string.IsNullOrWhiteSpace(param.DataId) ? null : param.DataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.DataLoginType) ? null : param.DataLoginType;
                        string? page = string.IsNullOrWhiteSpace(param.Page) ? null : param.Page;
                        string? limit = string.IsNullOrWhiteSpace(param.Limit) ? null : param.Limit;
                        string? categoryId = string.IsNullOrWhiteSpace(param.CategoryId) ? null : param.CategoryId;
                        string? sortId = string.IsNullOrWhiteSpace(param.SortId) ? null : param.SortId;
                        string? dsgKt = string.IsNullOrWhiteSpace(param.DsgKt) ? null : param.DsgKt;
                        string? dsgSize = string.IsNullOrWhiteSpace(param.DsgSize) ? null : param.DsgSize;
                        string? dsgColor = string.IsNullOrWhiteSpace(param.DsgColor) ? null : param.DsgColor;
                        string? itemName = string.IsNullOrWhiteSpace(param.ItemName) ? null : param.ItemName;
                        string? variant = string.IsNullOrWhiteSpace(param.Variant) ? null : param.Variant;
                        string? masterCommonId = string.IsNullOrWhiteSpace(param.MasterCommonId) ? null : param.MasterCommonId;
                        string? itemTag = string.IsNullOrWhiteSpace(param.ItemTag) ? null : param.ItemTag;
                        string? deliveryDays = string.IsNullOrWhiteSpace(param.DeliveryDays) ? null : param.DeliveryDays;
                        string? amount = string.IsNullOrWhiteSpace(param.Amount) ? null : param.Amount;
                        string? metalWt = string.IsNullOrWhiteSpace(param.MetalWt) ? null : param.MetalWt;
                        string? diawt = string.IsNullOrWhiteSpace(param.DiaWt) ? null : param.DiaWt;
                        string? gender = string.IsNullOrWhiteSpace(param.GenderId) ? null : param.GenderId;
                        string? brand = string.IsNullOrWhiteSpace(param.Brand) ? null : param.Brand;
                        string? buttonCode = string.IsNullOrWhiteSpace(param.ButtonCode) ? null : param.ButtonCode;
                        string? subCategoryId = string.IsNullOrWhiteSpace(param.SubCategoryId) ? null : param.SubCategoryId;
                        string? org_type = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", page);
                        cmd.Parameters.AddWithValue("@data_login_type", limit);
                        cmd.Parameters.AddWithValue("@page", dataId);
                        cmd.Parameters.AddWithValue("@default_limit_app_page", dataLoginType);
                        cmd.Parameters.AddWithValue("@category_id", categoryId);
                        cmd.Parameters.AddWithValue("@sort_id", sortId);
                        cmd.Parameters.AddWithValue("@dsg_kt", dsgKt);
                        cmd.Parameters.AddWithValue("@dsg_size", dsgSize);
                        cmd.Parameters.AddWithValue("@dsg_color", dsgColor);
                        cmd.Parameters.AddWithValue("@item_name", itemName);
                        cmd.Parameters.AddWithValue("@variant", variant);
                        cmd.Parameters.AddWithValue("@master_common_id", masterCommonId);
                        cmd.Parameters.AddWithValue("@item_tag", itemTag);
                        cmd.Parameters.AddWithValue("@delivery_days", deliveryDays);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@metalwt", metalWt);
                        cmd.Parameters.AddWithValue("@diawt", diawt);
                        cmd.Parameters.AddWithValue("@gender_id", gender);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@button_cd", buttonCode);
                        cmd.Parameters.AddWithValue("@sub_category_id", subCategoryId);
                        cmd.Parameters.AddWithValue("@org_type", org_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string itemNames = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string categoryIds = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string itemKt = rowdetails["item_kt"] != DBNull.Value ? Convert.ToString(rowdetails["item_kt"]) : string.Empty;
                                            string isNewCollection = rowdetails["IsNewCollection"] != DBNull.Value ? Convert.ToString(rowdetails["IsNewCollection"]) : string.Empty;
                                            string rupaySymbol = rowdetails["rupy_symbol"] != DBNull.Value ? Convert.ToString(rowdetails["rupy_symbol"]) : string.Empty;
                                            string imgWatchTitle = rowdetails["img_watch_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_watch_title"]) : string.Empty;
                                            string imgWishTitle = rowdetails["img_wish_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wish_title"]) : string.Empty;
                                            string wishCount = rowdetails["wish_count"] != DBNull.Value ? Convert.ToString(rowdetails["wish_count"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string mostOrder = rowdetails["mostOrder"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            List<Dictionary<string, object>> productTagsDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(productTags))
                                            {
                                                try
                                                {
                                                    productTagsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(productTags);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing productTags: " + ex.Message);
                                                }
                                            }

                                            rareSolitaireItemList.Add(new RareSolitairItemListResponse
                                            {
                                                ItemId = itemId,
                                                ItmMrp = itemMrp,
                                                ItemName = itemName,
                                                ItemDescription = itemDescription,
                                                CategoryId = categoryIds,
                                                ItemKt = itemKt,
                                                IsNewCollection = isNewCollection,
                                                RupaySymbol = rupaySymbol,
                                                ImgWatchTitle = imgWatchTitle,
                                                ImgWishTitle = imgWishTitle,
                                                WishCount = wishCount,
                                                ImagePath = imagePath,
                                                MostOrder = mostOrder,
                                                ProductTags = productTagsDynamic,
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (rareSolitaireItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = rareSolitaireItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<RareSolitairItemListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<RareSolitairItemListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> KisnaItemListFran(KisnaItemListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<KisnaItemListResponse> kisnaItemList = new List<KisnaItemListResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.KisnaItemListFran;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? dataId = string.IsNullOrWhiteSpace(param.DataID) ? null : param.DataID;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.DataLoginTypeID) ? null : param.DataLoginTypeID;
                        string? masterCommonId = string.IsNullOrWhiteSpace(param.MasterCommonID) ? null : param.MasterCommonID;
                        string? categoryId = string.IsNullOrWhiteSpace(param.CategoryID) ? null : param.CategoryID;
                        string? sortId = string.IsNullOrWhiteSpace(param.SortIds) ? null : param.SortIds;
                        string? variant = string.IsNullOrWhiteSpace(param.Variant) ? null : param.Variant;
                        string? itemName = string.IsNullOrWhiteSpace(param.ItemName) ? null : param.ItemName;
                        string? subCategoryId = string.IsNullOrWhiteSpace(param.SubCategoryIDs) ? null : param.SubCategoryIDs;
                        string? dsgSize = string.IsNullOrWhiteSpace(param.Sizes) ? null : param.Sizes;
                        string? dsgKt = string.IsNullOrWhiteSpace(param.DsgKts) ? null : param.DsgKts;
                        string? dsgColor = string.IsNullOrWhiteSpace(param.Colors) ? null : param.Colors;
                        string? amount = string.IsNullOrWhiteSpace(param.Price) ? null : param.Price;
                        string? metalWt = string.IsNullOrWhiteSpace(param.MetalWt) ? null : param.MetalWt;
                        string? diawt = string.IsNullOrWhiteSpace(param.DiaWt) ? null : param.DiaWt;
                        string? itemId = string.IsNullOrWhiteSpace(param.ItemID) ? null : param.ItemID;
                        string? stockAv = string.IsNullOrWhiteSpace(param.Stock_Av) ? null : param.Stock_Av;
                        string? familyAv = string.IsNullOrWhiteSpace(param.Family_Av) ? null : param.Family_Av;
                        string? regularAv = string.IsNullOrWhiteSpace(param.Regular_Av) ? null : param.Regular_Av;
                        string? wearIt = string.IsNullOrWhiteSpace(param.Wearit) ? null : param.Wearit;
                        string? tryOn = string.IsNullOrWhiteSpace(param.Tryon) ? null : param.Tryon;
                        string? gender = string.IsNullOrWhiteSpace(param.Genders) ? null : param.Genders;
                        string? itemTag = string.IsNullOrWhiteSpace(param.TagWiseFilters) ? null : param.TagWiseFilters;
                        string? brand = string.IsNullOrWhiteSpace(param.BrandWiseFilters) ? null : param.BrandWiseFilters;
                        string? deliveryDays = string.IsNullOrWhiteSpace(param.DeliveryDays) ? null : param.DeliveryDays;
                        string? itemSubCtgIDs = string.IsNullOrWhiteSpace(param.ItemSubCtgIDs) ? null : param.ItemSubCtgIDs;
                        string? itemSubSubCtgIDs = string.IsNullOrWhiteSpace(param.ItemSubSubCtgIDs) ? null : param.ItemSubSubCtgIDs;
                        string? salesLocation = string.IsNullOrWhiteSpace(param.SalesLocation) ? null : param.SalesLocation;
                        string? designTimeLine = string.IsNullOrWhiteSpace(param.DesignTimeLine) ? null : param.DesignTimeLine;
                        string? itemSubCategoryID = string.IsNullOrWhiteSpace(param.ItemSubCategoryID) ? null : param.ItemSubCategoryID;
                        string? franStoreAV = string.IsNullOrWhiteSpace(param.FranStore_AV) ? null : param.FranStore_AV;
                        string? page = string.IsNullOrWhiteSpace(param.Page) ? null : param.Page;
                        string? limit = string.IsNullOrWhiteSpace(param.Limit) ? null : param.Limit;
                        string? orgType = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;
                        string? mode = string.IsNullOrWhiteSpace(param.Mode) ? null : param.Mode;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@DataID", dataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", dataLoginType);
                        cmd.Parameters.AddWithValue("@MasterCommonID", masterCommonId);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                        cmd.Parameters.AddWithValue("@SortIds", sortId);
                        cmd.Parameters.AddWithValue("@Variant", variant);
                        cmd.Parameters.AddWithValue("@ItemName", itemName);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", subCategoryId);
                        cmd.Parameters.AddWithValue("@Sizes", dsgSize);
                        cmd.Parameters.AddWithValue("@DsgKts", dsgKt);
                        cmd.Parameters.AddWithValue("@Colors", dsgColor);
                        cmd.Parameters.AddWithValue("@Price", amount);
                        cmd.Parameters.AddWithValue("@MetalWt", metalWt);
                        cmd.Parameters.AddWithValue("@DiaWt", diawt);
                        cmd.Parameters.AddWithValue("@ItemID", itemId);
                        cmd.Parameters.AddWithValue("@Stock_Av", stockAv);
                        cmd.Parameters.AddWithValue("@Family_Av", familyAv);
                        cmd.Parameters.AddWithValue("@Regular_Av", regularAv);
                        cmd.Parameters.AddWithValue("@Wearit", wearIt);
                        cmd.Parameters.AddWithValue("@Tryon", tryOn);
                        cmd.Parameters.AddWithValue("@Genders", gender);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", itemTag);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", brand);
                        cmd.Parameters.AddWithValue("@DeliveryDays", deliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", itemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", itemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", salesLocation);
                        cmd.Parameters.AddWithValue("@DesignTimeLine", designTimeLine);
                        cmd.Parameters.AddWithValue("@ItemSubCategoryID", itemSubCategoryID);
                        cmd.Parameters.AddWithValue("@FranStore_AV", franStoreAV);
                        cmd.Parameters.AddWithValue("@Page", page);
                        cmd.Parameters.AddWithValue("@Limit", limit);
                        cmd.Parameters.AddWithValue("@OrgType", orgType);
                        cmd.Parameters.AddWithValue("@Mode", mode);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string ItemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string CategoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string ItemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string ItemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string ItemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string ItemGenderCommonID = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string ItemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string SubCategoryId = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string ItemIsSRP = rowdetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowdetails["ItemIsSRP"]) : string.Empty;
                                            string ItemPrice = rowdetails["item_price"] != DBNull.Value ? Convert.ToString(rowdetails["item_price"]) : string.Empty;
                                            string DistPrice = rowdetails["dist_price"] != DBNull.Value ? Convert.ToString(rowdetails["dist_price"]) : string.Empty;
                                            string ApproxDeliveryDate = rowdetails["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["ApproxDeliveryDate"]) : string.Empty;
                                            string InStockDeliveryDate = rowdetails["InStockDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["InStockDeliveryDate"]) : string.Empty;
                                            string PriceFlag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string ItemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string MaxQtySold = rowdetails["max_qty_sold"] != DBNull.Value ? Convert.ToString(rowdetails["max_qty_sold"]) : string.Empty;
                                            string ImagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string ProductTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string MostOrder = rowdetails["mostOrder"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder"]) : string.Empty;
                                            string ItemColor = rowdetails["item_color"] != DBNull.Value ? Convert.ToString(rowdetails["item_color"]) : string.Empty;
                                            string ItemMetalCommonID = rowdetails["ItemMetalCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemMetalCommonID"]) : string.Empty;
                                            string ItemSoliter = rowdetails["item_soliter"] != DBNull.Value ? Convert.ToString(rowdetails["item_soliter"]) : string.Empty;
                                            string PlainGoldStatus = rowdetails["plaingold_status"] != DBNull.Value ? Convert.ToString(rowdetails["plaingold_status"]) : string.Empty;
                                            string ItemText = rowdetails["item_text"] != DBNull.Value ? Convert.ToString(rowdetails["item_text"]) : string.Empty;
                                            string ItemBrandText = rowdetails["item_brand_text"] != DBNull.Value ? Convert.ToString(rowdetails["item_brand_text"]) : string.Empty;
                                            string RupySymbol = rowdetails["rupy_symbol"] != DBNull.Value ? Convert.ToString(rowdetails["rupy_symbol"]) : string.Empty;
                                            string ImgWatchTitle = rowdetails["img_watch_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_watch_title"]) : string.Empty;
                                            string ImgWearitTitle = rowdetails["img_wearit_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wearit_title"]) : string.Empty;
                                            string ImgWishTitle = rowdetails["img_wish_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wish_title"]) : string.Empty;
                                            string WearitText = rowdetails["wearit_text"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_text"]) : string.Empty;
                                            string CartPrice = rowdetails["cart_price"] != DBNull.Value ? Convert.ToString(rowdetails["cart_price"]) : string.Empty;
                                            string VariantCount = rowdetails["variantCount"] != DBNull.Value ? Convert.ToString(rowdetails["variantCount"]) : string.Empty;
                                            string ItemColorId = rowdetails["item_color_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_color_id"]) : string.Empty;
                                            string CartId = rowdetails["cart_id"] != DBNull.Value ? Convert.ToString(rowdetails["cart_id"]) : string.Empty;
                                            string CartItemQty = rowdetails["cart_item_qty"] != DBNull.Value ? Convert.ToString(rowdetails["cart_item_qty"]) : string.Empty;
                                            string WishCount = rowdetails["wish_count"] != DBNull.Value ? Convert.ToString(rowdetails["wish_count"]) : string.Empty;
                                            string WearitCount = rowdetails["wearit_count"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_count"]) : string.Empty;
                                            string ItemOrderInstructionList = rowdetails["itemOrderInstructionList"] != DBNull.Value ? Convert.ToString(rowdetails["itemOrderInstructionList"]) : string.Empty;
                                            string ItemOrderCustomInstructionList = rowdetails["itemOrderCustomInstructionList"] != DBNull.Value ? Convert.ToString(rowdetails["itemOrderCustomInstructionList"]) : string.Empty;
                                            string IsStockFilter = rowdetails["isStockFilter"] != DBNull.Value ? Convert.ToString(rowdetails["isStockFilter"]) : string.Empty;
                                            string IsInFranchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            List<Dictionary<string, object>> productTagsDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemOrderInstructionListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemOrderCustomInstructionListDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(ProductTags))
                                            {
                                                try
                                                {
                                                    productTagsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ProductTags);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ProductTags: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemOrderInstructionList))
                                            {
                                                try
                                                {
                                                    itemOrderInstructionListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemOrderInstructionList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemOrderInstructionList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemOrderCustomInstructionList))
                                            {
                                                try
                                                {
                                                    itemOrderCustomInstructionListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemOrderCustomInstructionList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemOrderCustomInstructionList: " + ex.Message);
                                                }
                                            }

                                            kisnaItemList.Add(new KisnaItemListResponse
                                            {
                                                ItemId = ItemId,
                                                CategoryId = categoryId,
                                                ItemDescription = ItemDescription,
                                                ItemCode = ItemCode,
                                                ItemName = ItemName,
                                                ItemGenderCommonID = ItemGenderCommonID,
                                                ItemNosePinScrewSts = ItemNosePinScrewSts,
                                                SubCategoryId = SubCategoryId,
                                                ItemIsSRP = ItemIsSRP,
                                                ItemPrice = ItemPrice,
                                                DistPrice = DistPrice,
                                                ApproxDeliveryDate = ApproxDeliveryDate,
                                                InStockDeliveryDate = InStockDeliveryDate,
                                                PriceFlag = PriceFlag,
                                                ItemMrp = ItemMrp,
                                                MaxQtySold = MaxQtySold,
                                                ImagePath = ImagePath,
                                                MostOrder = MostOrder,
                                                ProductTags = productTagsDynamic,
                                                ItemColor = ItemColor,
                                                ItemMetalCommonID = ItemMetalCommonID,
                                                ItemSoliter = ItemSoliter,
                                                PlainGoldStatus = PlainGoldStatus,
                                                ItemText = ItemText,
                                                ItemBrandText = ItemBrandText,
                                                RupySymbol = RupySymbol,
                                                ImgWatchTitle = ImgWatchTitle,
                                                ImgWearitTitle = ImgWearitTitle,
                                                ImgWishTitle = ImgWishTitle,
                                                WearitText = WearitText,
                                                CartPrice = CartPrice,
                                                VariantCount = VariantCount,
                                                ItemColorId = ItemColorId,
                                                CartId = CartId,
                                                CartItemQty = CartItemQty,
                                                WishCount = WishCount,
                                                WearitCount = WearitCount,
                                                ItemOrderInstructionList = itemOrderInstructionListDynamic,
                                                ItemOrderCustomInstructionList = itemOrderCustomInstructionListDynamic,
                                                IsStockFilter = IsStockFilter,
                                                IsInFranchiseStore = IsInFranchiseStore,
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (kisnaItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = kisnaItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<KisnaItemListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<KisnaItemListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CommonItemFilterFranSIS(CommonItemFilterRequest param, CommonHeader header)
        {
            {
                var responseDetails = new ResponseDetails();
                bool hasAnyData = false;
                CommonItemFilterResponse filterDetails = new CommonItemFilterResponse();
                IList<CommonFilterCategoryList> categoryList = new List<CommonFilterCategoryList>();
                IList<CommonFilterDsgKtList> dsgKtList = new List<CommonFilterDsgKtList>();
                IList<CommonFilterProductTagList> productTagList = new List<CommonFilterProductTagList>();
                IList<CommonFilterBrandList> brandList = new List<CommonFilterBrandList>();
                IList<CommonFilterGenderList> genderList = new List<CommonFilterGenderList>();
                IList<CommonFilterApproxDeliveryList> approxDeliveryList = new List<CommonFilterApproxDeliveryList>();
                IList<CommonFilterPriceList> priceList = new List<CommonFilterPriceList>();
                IList<CommonFilterFranchiseStockList> franchiseStockList = new List<CommonFilterFranchiseStockList>();
                IList<CommonFilterHOStockList> stockList = new List<CommonFilterHOStockList>();
                IList<CommonFilterFamilyProductList> familyProductList = new List<CommonFilterFamilyProductList>();
                IList<CommonFilterExcludeDiscontinueList> excludeDisconList = new List<CommonFilterExcludeDiscontinueList>();
                IList<CommonFilterWearItList> wearItList = new List<CommonFilterWearItList>();
                IList<CommonFilterTryOnList> tryOnList = new List<CommonFilterTryOnList>();
                IList<CommonFilterDsgMetalWtList> metalWtList = new List<CommonFilterDsgMetalWtList>();
                IList<CommonFilterDsgDiamondWtList> diamondWtList = new List<CommonFilterDsgDiamondWtList>();
                IList<CommonFilterBestSellerList> bestSellerList = new List<CommonFilterBestSellerList>();
                IList<CommonFilterLatestDesignList> latestDesignList = new List<CommonFilterLatestDesignList>();

                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.CommonItemFilterListFranSIS;
                        await dbConnection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            string? data_id = string.IsNullOrWhiteSpace(param.dataId) ? null : param.dataId;
                            string? data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? null : param.dataLoginType;
                            string? category_id = string.IsNullOrWhiteSpace(param.categoryId) ? null : param.categoryId;
                            string? button_code = string.IsNullOrWhiteSpace(param.buttonCode) ? null : param.buttonCode;
                            string? master_common_id = string.IsNullOrWhiteSpace(param.masterCommonId) ? null : param.masterCommonId;
                            string? item_subcategory_id = string.IsNullOrWhiteSpace(param.itemSubCategoryId) ? null : param.itemSubCategoryId;
                            string? orgtype = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@data_id", data_id);
                            cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                            cmd.Parameters.AddWithValue("@category_id", category_id);
                            cmd.Parameters.AddWithValue("@button_code", button_code);
                            cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
                            cmd.Parameters.AddWithValue("@item_sub_category_id", item_subcategory_id);
                            cmd.Parameters.AddWithValue("@org_type", orgtype);

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                if (ds?.Tables?.Count >= 1)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        hasAnyData = true;
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            categoryList.Add(new CommonFilterCategoryList
                                            {
                                                category_id = Convert.ToString(ds.Tables[0].Rows[i]["category_id"]),
                                                sub_category_id = Convert.ToString(ds.Tables[0].Rows[i]["sub_category_id"]),
                                                sub_category_name = Convert.ToString(ds.Tables[0].Rows[i]["sub_category_name"]),
                                                category_count = Convert.ToString(ds.Tables[0].Rows[i]["category_count"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            dsgKtList.Add(new CommonFilterDsgKtList
                                            {
                                                kt = Convert.ToString(ds.Tables[1].Rows[i]["kt"]),
                                                Kt_count = Convert.ToString(ds.Tables[1].Rows[i]["kt_count"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[2].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            priceList.Add(new CommonFilterPriceList
                                            {
                                                amount = Convert.ToString(ds.Tables[2].Rows[i]["amount"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[3].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            metalWtList.Add(new CommonFilterDsgMetalWtList
                                            {
                                                metalwt = Convert.ToString(ds.Tables[3].Rows[i]["metalwt"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[4].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            diamondWtList.Add(new CommonFilterDsgDiamondWtList
                                            {
                                                diamondwt = Convert.ToString(ds.Tables[4].Rows[i]["diamondwt"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[5].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            productTagList.Add(new CommonFilterProductTagList
                                            {
                                                tag_name = Convert.ToString(ds.Tables[5].Rows[i]["tag_name"]),
                                                tag_count = Convert.ToString(ds.Tables[5].Rows[i]["tag_count"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[6].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            brandList.Add(new CommonFilterBrandList
                                            {
                                                brand_id = Convert.ToString(ds.Tables[6].Rows[i]["brand_id"]),
                                                brand_name = Convert.ToString(ds.Tables[6].Rows[i]["brand_name"]),
                                                brand_count = Convert.ToString(ds.Tables[6].Rows[i]["brand_count"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[7].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            genderList.Add(new CommonFilterGenderList
                                            {
                                                gender_id = Convert.ToString(ds.Tables[7].Rows[i]["gender_id"]),
                                                gender_name = Convert.ToString(ds.Tables[7].Rows[i]["gender_name"]),
                                                gender_count = Convert.ToString(ds.Tables[7].Rows[i]["gender_count"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[8].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            approxDeliveryList.Add(new CommonFilterApproxDeliveryList
                                            {
                                                ItemAproxDay = Convert.ToString(ds.Tables[8].Rows[i]["ItemAproxDay"]),
                                                ItemAproxDay_count = Convert.ToString(ds.Tables[8].Rows[i]["ItemAproxDay_count"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[9].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            stockList.Add(new CommonFilterHOStockList
                                            {
                                                stock_name = Convert.ToString(ds.Tables[9].Rows[i]["stock_name"]),
                                                stock_id = Convert.ToString(ds.Tables[9].Rows[i]["stock_id"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[10].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            franchiseStockList.Add(new CommonFilterFranchiseStockList
                                            {
                                                stock_name = Convert.ToString(ds.Tables[10].Rows[i]["stock_name"]),
                                                stock_id = Convert.ToString(ds.Tables[10].Rows[i]["stock_id"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[11].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[11].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            bestSellerList.Add(new CommonFilterBestSellerList
                                            {
                                                Name = Convert.ToString(ds.Tables[11].Rows[i]["Name"]),
                                                Value = Convert.ToString(ds.Tables[11].Rows[i]["Value"]),
                                            });
                                        }
                                    }
                                    if (ds.Tables[12].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[12].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            latestDesignList.Add(new CommonFilterLatestDesignList
                                            {
                                                Name = Convert.ToString(ds.Tables[12].Rows[i]["Name"]),
                                                Value = Convert.ToString(ds.Tables[12].Rows[i]["Value"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[13].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[13].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            familyProductList.Add(new CommonFilterFamilyProductList
                                            {
                                                familyproduct_name = Convert.ToString(ds.Tables[13].Rows[i]["familyproduct_name"]),
                                                familyproduct_id = Convert.ToString(ds.Tables[13].Rows[i]["familyproduct_id"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[14].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[14].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            excludeDisconList.Add(new CommonFilterExcludeDiscontinueList
                                            {
                                                excludediscontinue_name = Convert.ToString(ds.Tables[14].Rows[i]["excludediscontinue_name"]),
                                                excludediscontinue_id = Convert.ToString(ds.Tables[14].Rows[i]["excludediscontinue_id"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[15].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[15].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            wearItList.Add(new CommonFilterWearItList
                                            {
                                                view_name = Convert.ToString(ds.Tables[15].Rows[i]["view_name"]),
                                                view_id = Convert.ToString(ds.Tables[15].Rows[i]["view_id"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[16].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[16].Rows.Count; i++)
                                        {
                                            hasAnyData = true;
                                            tryOnList.Add(new CommonFilterTryOnList
                                            {
                                                view_name = Convert.ToString(ds.Tables[16].Rows[i]["view_name"]),
                                                view_id = Convert.ToString(ds.Tables[16].Rows[i]["view_id"]),
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (hasAnyData)
                    {
                        filterDetails.categoryList = categoryList;
                        filterDetails.dsgKt = dsgKtList;
                        filterDetails.productTag = productTagList;
                        filterDetails.brand = brandList;
                        filterDetails.gender = genderList;
                        filterDetails.approxDelivery = approxDeliveryList;
                        filterDetails.price = priceList;
                        filterDetails.franchiseStock = franchiseStockList;
                        filterDetails.hoStock = stockList;
                        filterDetails.familyProduct = familyProductList;
                        filterDetails.excludeDiscontinue = excludeDisconList;
                        filterDetails.wearIt = wearItList;
                        filterDetails.tryOn = tryOnList;
                        filterDetails.metalWt = metalWtList;
                        filterDetails.diamondWt = diamondWtList;
                        filterDetails.bestSeller = bestSellerList;
                        filterDetails.latestDesign = latestDesignList;
                    }

                    responseDetails = new ResponseDetails
                    {
                        success = true,
                        status = "200",
                        message = hasAnyData ? "Successfully" : "No data found",
                        data = filterDetails,
                        data1 = null
                    };

                    return responseDetails;
                }
                catch (SqlException ex)
                {
                    return new ResponseDetails
                    {
                        success = false,
                        status = "400",
                        message = $"SQL error: {ex.Message}",
                        data = new List<CommonItemFilterResponse>(),
                        data1 = null
                    };
                }
            }
        }

        public async Task<ResponseDetails> BannerWishList(BannerWishListRequest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<BannerWishListResponse> bannerwishlist = new List<BannerWishListResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.BannerWishList;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? page = string.IsNullOrWhiteSpace(param.page) ? null : param.page;
                        string? limit = string.IsNullOrWhiteSpace(param.limit) ? null : param.limit;
                        string? data_id = string.IsNullOrWhiteSpace(param.data_id) ? null : param.data_id;
                        string? b_id = string.IsNullOrWhiteSpace(param.b_id) ? null : param.b_id;
                        string? item_id = string.IsNullOrWhiteSpace(param.item_id) ? null : param.item_id;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@limit", limit);
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@b_id", b_id);
                        cmd.Parameters.AddWithValue("@item_id", item_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string bannerWishListId = rowdetails["banner_wish_list_id"] != DBNull.Value ? Convert.ToString(rowdetails["banner_wish_list_id"]) : string.Empty;
                                            string bannerId = rowdetails["banner_id"] != DBNull.Value ? Convert.ToString(rowdetails["banner_id"]) : string.Empty;
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string dataId = rowdetails["data_id"] != DBNull.Value ? Convert.ToString(rowdetails["data_id"]) : string.Empty;
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string selectedColor1 = rowdetails["selectedColor1"] != DBNull.Value ? Convert.ToString(rowdetails["selectedColor1"]) : string.Empty;
                                            string selectedSize1 = rowdetails["selectedSize1"] != DBNull.Value ? Convert.ToString(rowdetails["selectedSize1"]) : string.Empty;
                                            string validStatus = rowdetails["valid_status"] != DBNull.Value ? Convert.ToString(rowdetails["valid_status"]) : string.Empty;
                                            string entDt = rowdetails["ent_dt"] != DBNull.Value ? Convert.ToString(rowdetails["ent_dt"]) : string.Empty;
                                            string itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemSku = rowdetails["item_sku"] != DBNull.Value ? Convert.ToString(rowdetails["item_sku"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string itemAproxDay = rowdetails["ItemAproxDay"] != DBNull.Value ? Convert.ToString(rowdetails["ItemAproxDay"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string itemPrice = rowdetails["item_price"] != DBNull.Value ? Convert.ToString(rowdetails["item_price"]) : string.Empty;
                                            string distPrice = rowdetails["dist_price"] != DBNull.Value ? Convert.ToString(rowdetails["dist_price"]) : string.Empty;
                                            string itemSoliter = rowdetails["item_soliter"] != DBNull.Value ? Convert.ToString(rowdetails["item_soliter"]) : string.Empty;
                                            string plaingoldStatus = rowdetails["plaingold_status"] != DBNull.Value ? Convert.ToString(rowdetails["plaingold_status"]) : string.Empty;
                                            string ItemReview = rowdetails["item_review"] != DBNull.Value ? Convert.ToString(rowdetails["item_review"]) : string.Empty;
                                            string ItemSize = rowdetails["item_size"] != DBNull.Value ? Convert.ToString(rowdetails["item_size"]) : string.Empty;
                                            string ItemKt = rowdetails["item_kt"] != DBNull.Value ? Convert.ToString(rowdetails["item_kt"]) : string.Empty;
                                            string ItemColor = rowdetails["item_color"] != DBNull.Value ? Convert.ToString(rowdetails["item_color"]) : string.Empty;
                                            string ItemMetal = rowdetails["item_metal"] != DBNull.Value ? Convert.ToString(rowdetails["item_metal"]) : string.Empty;
                                            string ItemWt = rowdetails["item_wt"] != DBNull.Value ? Convert.ToString(rowdetails["item_wt"]) : string.Empty;
                                            string ItemStone = rowdetails["item_stone"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone"]) : string.Empty;
                                            string ItemStoneWt = rowdetails["item_stone_wt"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone_wt"]) : string.Empty;
                                            string ItemStoneQty = rowdetails["item_stone_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone_qty"]) : string.Empty;
                                            string PriceText = rowdetails["price_text"] != DBNull.Value ? Convert.ToString(rowdetails["price_text"]) : string.Empty;
                                            string CartPrice = rowdetails["cart_price"] != DBNull.Value ? Convert.ToString(rowdetails["cart_price"]) : string.Empty;
                                            string ItemColorId = rowdetails["item_color_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_color_id"]) : string.Empty;
                                            string DsgSfx = rowdetails["dsg_sfx"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_sfx"]) : string.Empty;
                                            string DsgSize = rowdetails["dsg_size"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_size"]) : string.Empty;
                                            string DsgKt = rowdetails["dsg_kt"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_kt"]) : string.Empty;
                                            string DsgColor = rowdetails["dsg_color"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_color"]) : string.Empty;
                                            string Star = rowdetails["star"] != DBNull.Value ? Convert.ToString(rowdetails["star"]) : string.Empty;
                                            string ImagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string CartImg = rowdetails["cart_img"] != DBNull.Value ? Convert.ToString(rowdetails["cart_img"]) : string.Empty;
                                            string ImgCartTitle = rowdetails["img_cart_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_cart_title"]) : string.Empty;
                                            string WatchImg = rowdetails["watch_img"] != DBNull.Value ? Convert.ToString(rowdetails["watch_img"]) : string.Empty;
                                            string ImgWatchTitle = rowdetails["img_watch_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_watch_title"]) : string.Empty;
                                            string WishCount = rowdetails["wish_count"] != DBNull.Value ? Convert.ToString(rowdetails["wish_count"]) : string.Empty;
                                            string WearitCount = rowdetails["wearit_count"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_count"]) : string.Empty;
                                            string WearitStatus = rowdetails["wearit_status"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_status"]) : string.Empty;
                                            string WearitImg = rowdetails["wearit_img"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_img"]) : string.Empty;
                                            string WearitNoneImg = rowdetails["wearit_none_img"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_none_img"]) : string.Empty;
                                            string WearitColor = rowdetails["wearit_color"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_color"]) : string.Empty;
                                            string WearitText = rowdetails["wearit_text"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_text"]) : string.Empty;
                                            string ImgWearitTitle = rowdetails["img_wearit_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wearit_title"]) : string.Empty;
                                            string WishDefaultImg = rowdetails["wish_default_img"] != DBNull.Value ? Convert.ToString(rowdetails["wish_default_img"]) : string.Empty;
                                            string WishFillImg = rowdetails["wish_fill_img"] != DBNull.Value ? Convert.ToString(rowdetails["wish_fill_img"]) : string.Empty;
                                            string ImgWishTitle = rowdetails["img_wish_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wish_title"]) : string.Empty;
                                            string ItemDetails = rowdetails["item_details"] != DBNull.Value ? Convert.ToString(rowdetails["item_details"]) : string.Empty;
                                            string ItemDiamondDetails = rowdetails["item_diamond_details"] != DBNull.Value ? Convert.ToString(rowdetails["item_diamond_details"]) : string.Empty;
                                            string ItemText = rowdetails["item_text"] != DBNull.Value ? Convert.ToString(rowdetails["item_text"]) : string.Empty;
                                            string MoreItemDetails = rowdetails["more_item_details"] != DBNull.Value ? Convert.ToString(rowdetails["more_item_details"]) : string.Empty;
                                            string ItemStock = rowdetails["item_stock"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock"]) : string.Empty;
                                            string CartItemQty = rowdetails["cart_item_qty"] != DBNull.Value ? Convert.ToString(rowdetails["cart_item_qty"]) : string.Empty;
                                            string VariantCount = rowdetails["variantCount"] != DBNull.Value ? Convert.ToString(rowdetails["variantCount"]) : string.Empty;
                                            string CartId = rowdetails["cart_id"] != DBNull.Value ? Convert.ToString(rowdetails["cart_id"]) : string.Empty;
                                            string ItemGenderCommonID = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string ItemTypeCommonID = rowdetails["ItemTypeCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemTypeCommonID"]) : string.Empty;
                                            string ItemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string ItemStockQty = rowdetails["item_stock_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock_qty"]) : string.Empty;
                                            string ItemStockColorSizeQty = rowdetails["item_stock_colorsize_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock_colorsize_qty"]) : string.Empty;
                                            string ProductTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string Weight = rowdetails["weight"] != DBNull.Value ? Convert.ToString(rowdetails["weight"]) : string.Empty;
                                            string ApproxdayDetail = rowdetails["approxday_detail"] != DBNull.Value ? Convert.ToString(rowdetails["approxday_detail"]) : string.Empty;
                                            string SelectedColor = rowdetails["selectedColor"] != DBNull.Value ? Convert.ToString(rowdetails["selectedColor"]) : string.Empty;
                                            string SelectedSize = rowdetails["selectedSize"] != DBNull.Value ? Convert.ToString(rowdetails["selectedSize"]) : string.Empty;
                                            string FieldName = rowdetails["field_name"] != DBNull.Value ? Convert.ToString(rowdetails["field_name"]) : string.Empty;
                                            string ColorName = rowdetails["color_name"] != DBNull.Value ? Convert.ToString(rowdetails["color_name"]) : string.Empty;
                                            string DefaultColorName = rowdetails["default_color_name"] != DBNull.Value ? Convert.ToString(rowdetails["default_color_name"]) : string.Empty;
                                            string SizeList = rowdetails["sizeList"] != DBNull.Value ? Convert.ToString(rowdetails["sizeList"]) : string.Empty;
                                            string ColorList = rowdetails["colorList"] != DBNull.Value ? Convert.ToString(rowdetails["colorList"]) : string.Empty;
                                            string ItemsColorSizeList = rowdetails["itemsColorSizeList"] != DBNull.Value ? Convert.ToString(rowdetails["itemsColorSizeList"]) : string.Empty;
                                            string ItemOrderInstructionList = rowdetails["itemOrderInstructionList"] != DBNull.Value ? Convert.ToString(rowdetails["itemOrderInstructionList"]) : string.Empty;
                                            string ItemOrderCustomInstructionList = rowdetails["itemOrderCustomInstructionList"] != DBNull.Value ? Convert.ToString(rowdetails["itemOrderCustomInstructionList"]) : string.Empty;
                                            string ItemImagesColor = rowdetails["item_images_color"] != DBNull.Value ? Convert.ToString(rowdetails["item_images_color"]) : string.Empty;

                                            // Converting String to Json Format
                                            List<Dictionary<string, object>> sizeListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> colorListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemsColorSizeListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemOrderInstructionListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemOrderCustomInstructionListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemImagesColorDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(SizeList))
                                            {
                                                try
                                                {
                                                    sizeListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(SizeList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing SizeList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ColorList))
                                            {
                                                try
                                                {
                                                    colorListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ColorList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ColorList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemsColorSizeList))
                                            {
                                                try
                                                {
                                                    itemsColorSizeListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemsColorSizeList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemsColorSizeList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemOrderInstructionList))
                                            {
                                                try
                                                {
                                                    itemOrderInstructionListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemOrderInstructionList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemOrderInstructionList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemOrderCustomInstructionList))
                                            {
                                                try
                                                {
                                                    itemOrderCustomInstructionListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemOrderCustomInstructionList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemOrderCustomInstructionList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemImagesColor))
                                            {
                                                try
                                                {
                                                    itemImagesColorDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemImagesColor);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemImagesColor: " + ex.Message);
                                                }
                                            }

                                            bannerwishlist.Add(new BannerWishListResponse
                                            {
                                                BannerWishListId = bannerWishListId,
                                                BannerId = bannerId,
                                                CategoryId = categoryId,
                                                DataId = dataId,
                                                ItemId = itemId,
                                                SelectedColor1 = selectedColor1,
                                                SelectedSize1 = selectedSize1,
                                                ValidStatus = validStatus,
                                                EntDt = entDt,
                                                ItemCode = itemCode,
                                                ItemName = itemName,
                                                ItemSku = itemSku,
                                                ItemDescription = itemDescription,
                                                ItemAproxDay = itemAproxDay,
                                                ItemMrp = itemMrp,
                                                ItemPrice = itemPrice,
                                                DistPrice = distPrice,
                                                PriceText = PriceText,
                                                CartPrice = CartPrice,
                                                ItemSoliter = itemSoliter,
                                                PlaingoldStatus = plaingoldStatus,
                                                ItemReview = ItemReview,
                                                ItemSize = ItemSize,
                                                ItemKt = ItemKt,
                                                ItemColor = ItemColor,
                                                ItemMetal = ItemMetal,
                                                ItemWt = ItemWt,
                                                ItemStone = ItemStone,
                                                ItemStoneWt = ItemStoneWt,
                                                ItemStoneQty = ItemStoneQty,
                                                ItemColorId =  ItemColorId,
                                                Weight = Weight,
                                                DsgSfx = DsgSfx,
                                                DsgSize = DsgSize,
                                                DsgKt = DsgKt,
                                                DsgColor = DsgColor,
                                                Star = Star,
                                                ImagePath = ImagePath,
                                                CartImg = CartImg,
                                                ImgCartTitle = ImgCartTitle,
                                                WatchImg = WatchImg,
                                                ImgWatchTitle = ImgWatchTitle,
                                                WishCount = WishCount,
                                                WearitCount = WearitCount,
                                                WearitStatus = WearitStatus,
                                                WearitImg = WearitImg,
                                                WearitNoneImg = WearitNoneImg,
                                                WearitColor = WearitColor,
                                                WearitText = WearitText,
                                                ImgWearitTitle = ImgWearitTitle,
                                                WishDefaultImg = WishDefaultImg,
                                                WishFillImg = WishFillImg,
                                                ImgWishTitle = ImgWishTitle,
                                                ItemDetails = ItemDetails,
                                                ItemDiamondDetails = ItemDiamondDetails,
                                                ItemText = ItemText,
                                                MoreItemDetails = MoreItemDetails,
                                                ItemStock = ItemStock,
                                                CartItemQty = CartItemQty,
                                                VariantCount = VariantCount,
                                                ItemStockQty = ItemStockQty,
                                                ItemStockColorSizeQty = ItemStockColorSizeQty,
                                                CartId = CartId,
                                                ItemGenderCommonID = ItemGenderCommonID, 
                                                ItemTypeCommonID = ItemTypeCommonID,
                                                ItemNosePinScrewSts = ItemNosePinScrewSts,
                                                ProductTags = ProductTags,
                                                ApproxdayDetail = ApproxdayDetail,
                                                SelectedColor = SelectedColor,
                                                SelectedSize = SelectedSize,
                                                FieldName = FieldName,
                                                ColorName = ColorName,
                                                DefaultColorName  = DefaultColorName,
                                                SizeList = sizeListDynamic,
                                                ColorList = colorListDynamic,
                                                ItemsColorSizeList = itemsColorSizeListDynamic,
                                                ItemOrderInstructionList = itemOrderInstructionListDynamic,
                                                ItemOrderCustomInstructionList = itemOrderCustomInstructionListDynamic,
                                                ItemImagesColor = itemImagesColorDynamic
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (bannerwishlist.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = bannerwishlist;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<BannerWishListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<BannerWishListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> ItemDetailShowFran(ItemDetailsRquest param, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<ItemDetailsResponse> itemDetails = new List<ItemDetailsResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ItemDetailsFran;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? data_id = string.IsNullOrWhiteSpace(param.dataId) ? null : param.dataId;
                        string? data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? null : param.dataLoginType;
                        string? category_id = string.IsNullOrWhiteSpace(param.categoryId) ? null : param.categoryId;
                        string? item_id = string.IsNullOrWhiteSpace(param.itemId) ? null : param.itemId;
                        string? master_common_id = string.IsNullOrWhiteSpace(param.masterCommonId) ? null : param.masterCommonId;
                        string? org_type = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@item_id", item_id);
                        cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
                        cmd.Parameters.AddWithValue("@org_type", org_type);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string ItemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string ItemSoliter = rowdetails["item_soliter"] != DBNull.Value ? Convert.ToString(rowdetails["item_soliter"]) : string.Empty;
                                            string ItemCtgCommonID = rowdetails["ItemCtgCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemCtgCommonID"]) : string.Empty;
                                            string ItemAproxDay = rowdetails["ItemAproxDay"] != DBNull.Value ? Convert.ToString(rowdetails["ItemAproxDay"]) : string.Empty;
                                            string ItemDAproxDay = rowdetails["ItemDAproxDay"] != DBNull.Value ? Convert.ToString(rowdetails["ItemDAproxDay"]) : string.Empty;
                                            string PlaingoldStatus = rowdetails["plaingold_status"] != DBNull.Value ? Convert.ToString(rowdetails["plaingold_status"]) : string.Empty;
                                            string ItemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string ItemSku = rowdetails["item_sku"] != DBNull.Value ? Convert.ToString(rowdetails["item_sku"]) : string.Empty;
                                            string ItemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string ItemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string ItemDiscount = rowdetails["item_discount"] != DBNull.Value ? Convert.ToString(rowdetails["item_discount"]) : string.Empty;
                                            string ItemPrice = rowdetails["item_price"] != DBNull.Value ? Convert.ToString(rowdetails["item_price"]) : string.Empty;
                                            string ItemBrandCommonID = rowdetails["ItemBrandCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemBrandCommonID"]) : string.Empty;
                                            string ItemCd = rowdetails["ItemCd"] != DBNull.Value ? Convert.ToString(rowdetails["ItemCd"]) : string.Empty;
                                            string RetailPrice = rowdetails["retail_price"] != DBNull.Value ? Convert.ToString(rowdetails["retail_price"]) : string.Empty;
                                            string DistPrice = rowdetails["dist_price"] != DBNull.Value ? Convert.ToString(rowdetails["dist_price"]) : string.Empty;
                                            string Uom = rowdetails["uom"] != DBNull.Value ? Convert.ToString(rowdetails["uom"]) : string.Empty;
                                            string Star = rowdetails["star"] != DBNull.Value ? Convert.ToString(rowdetails["star"]) : string.Empty;
                                            string CartImg = rowdetails["cart_img"] != DBNull.Value ? Convert.ToString(rowdetails["cart_img"]) : string.Empty;
                                            string ImgCartTitle = rowdetails["img_cart_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_cart_title"]) : string.Empty;
                                            string WatchImg = rowdetails["watch_img"] != DBNull.Value ? Convert.ToString(rowdetails["watch_img"]) : string.Empty;
                                            string ImgWatchTitle = rowdetails["img_watch_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_watch_title"]) : string.Empty;
                                            string WearitCount = rowdetails["wearit_count"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_count"]) : string.Empty;
                                            string WearitStatus = rowdetails["wearit_status"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_status"]) : string.Empty;
                                            string WearitImg = rowdetails["wearit_img"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_img"]) : string.Empty;
                                            string WearitNoneImg = rowdetails["wearit_none_img"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_none_img"]) : string.Empty;
                                            string WearitColor = rowdetails["wearit_color"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_color"]) : string.Empty;
                                            string WearitText = rowdetails["wearit_text"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_text"]) : string.Empty;
                                            string ImgWearitTitle = rowdetails["img_wearit_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wearit_title"]) : string.Empty;
                                            string TryonCount = rowdetails["tryon_count"] != DBNull.Value ? Convert.ToString(rowdetails["tryon_count"]) : string.Empty;
                                            string TryonStatus = rowdetails["tryon_status"] != DBNull.Value ? Convert.ToString(rowdetails["tryon_status"]) : string.Empty;
                                            string TryonImg = rowdetails["tryon_img"] != DBNull.Value ? Convert.ToString(rowdetails["tryon_img"]) : string.Empty;
                                            string TryonNoneImg = rowdetails["tryon_none_img"] != DBNull.Value ? Convert.ToString(rowdetails["tryon_none_img"]) : string.Empty;
                                            string TryonText = rowdetails["tryon_text"] != DBNull.Value ? Convert.ToString(rowdetails["tryon_text"]) : string.Empty;
                                            string TryonTitle = rowdetails["tryon_title"] != DBNull.Value ? Convert.ToString(rowdetails["tryon_title"]) : string.Empty;
                                            string TryonAndroidPath = rowdetails["tryon_android_path"] != DBNull.Value ? Convert.ToString(rowdetails["tryon_android_path"]) : string.Empty;
                                            string TryonIosPath = rowdetails["tryon_ios_path"] != DBNull.Value ? Convert.ToString(rowdetails["tryon_ios_path"]) : string.Empty;
                                            string WishCount = rowdetails["wish_count"] != DBNull.Value ? Convert.ToString(rowdetails["wish_count"]) : string.Empty;
                                            string WishDefaultImg = rowdetails["wish_default_img"] != DBNull.Value ? Convert.ToString(rowdetails["wish_default_img"]) : string.Empty;
                                            string WishFillImg = rowdetails["wish_fill_img"] != DBNull.Value ? Convert.ToString(rowdetails["wish_fill_img"]) : string.Empty;
                                            string ImgWishTitle = rowdetails["img_wish_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wish_title"]) : string.Empty;
                                            string ItemReview = rowdetails["item_review"] != DBNull.Value ? Convert.ToString(rowdetails["item_review"]) : string.Empty;
                                            string ItemSize = rowdetails["item_size"] != DBNull.Value ? Convert.ToString(rowdetails["item_size"]) : string.Empty;
                                            string ItemKt = rowdetails["item_kt"] != DBNull.Value ? Convert.ToString(rowdetails["item_kt"]) : string.Empty;
                                            string ItemColor = rowdetails["item_color"] != DBNull.Value ? Convert.ToString(rowdetails["item_color"]) : string.Empty;
                                            string ItemMetal = rowdetails["item_metal"] != DBNull.Value ? Convert.ToString(rowdetails["item_metal"]) : string.Empty;
                                            string ItemWt = rowdetails["item_wt"] != DBNull.Value ? Convert.ToString(rowdetails["item_wt"]) : string.Empty;
                                            string ItemStone = rowdetails["item_stone"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone"]) : string.Empty;
                                            string ItemStoneWt = rowdetails["item_stone_wt"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone_wt"]) : string.Empty;
                                            string ItemStoneQty = rowdetails["item_stone_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone_qty"]) : string.Empty;
                                            string StarColor = rowdetails["star_color"] != DBNull.Value ? Convert.ToString(rowdetails["star_color"]) : string.Empty;
                                            string ItemMetalCommonID = rowdetails["ItemMetalCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemMetalCommonID"]) : string.Empty;
                                            string PriceText = rowdetails["price_text"] != DBNull.Value ? Convert.ToString(rowdetails["price_text"]) : string.Empty;
                                            string CartPrice = rowdetails["cart_price"] != DBNull.Value ? Convert.ToString(rowdetails["cart_price"]) : string.Empty;
                                            string ItemColorId = rowdetails["item_color_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_color_id"]) : string.Empty;
                                            string ItemDetails = rowdetails["item_details"] != DBNull.Value ? Convert.ToString(rowdetails["item_details"]) : string.Empty;
                                            string ItemDiamondDetails = rowdetails["item_diamond_details"] != DBNull.Value ? Convert.ToString(rowdetails["item_diamond_details"]) : string.Empty;
                                            string ItemText = rowdetails["item_text"] != DBNull.Value ? Convert.ToString(rowdetails["item_text"]) : string.Empty;
                                            string MoreItemDetails = rowdetails["more_item_details"] != DBNull.Value ? Convert.ToString(rowdetails["more_item_details"]) : string.Empty;
                                            string ItemStock = rowdetails["item_stock"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock"]) : string.Empty;
                                            string CartItemQty = rowdetails["cart_item_qty"] != DBNull.Value ? Convert.ToString(rowdetails["cart_item_qty"]) : string.Empty;
                                            string RupySymbol = rowdetails["rupy_symbol"] != DBNull.Value ? Convert.ToString(rowdetails["rupy_symbol"]) : string.Empty;
                                            string VariantCount = rowdetails["variantCount"] != DBNull.Value ? Convert.ToString(rowdetails["variantCount"]) : string.Empty;
                                            string CartId = rowdetails["cart_id"] != DBNull.Value ? Convert.ToString(rowdetails["cart_id"]) : string.Empty;
                                            string ItemGenderCommonID = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string CategoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string ItemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string Metal = rowdetails["metal"] != DBNull.Value ? Convert.ToString(rowdetails["metal"]) : string.Empty;
                                            string Kt = rowdetails["kt"] != DBNull.Value ? Convert.ToString(rowdetails["kt"]) : string.Empty;
                                            string Quality = rowdetails["quality"] != DBNull.Value ? Convert.ToString(rowdetails["quality"]) : string.Empty;
                                            string Shape = rowdetails["shape"] != DBNull.Value ? Convert.ToString(rowdetails["shape"]) : string.Empty;
                                            string Brand = rowdetails["brand"] != DBNull.Value ? Convert.ToString(rowdetails["brand"]) : string.Empty;
                                            string Stone = rowdetails["stone"] != DBNull.Value ? Convert.ToString(rowdetails["stone"]) : string.Empty;
                                            string Diamondcolor = rowdetails["diamondcolor"] != DBNull.Value ? Convert.ToString(rowdetails["diamondcolor"]) : string.Empty;
                                            string Category = rowdetails["category"] != DBNull.Value ? Convert.ToString(rowdetails["category"]) : string.Empty;
                                            string ItemAproxDayCommonID = rowdetails["ItemAproxDayCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemAproxDayCommonID"]) : string.Empty;
                                            string GrossWt = rowdetails["GrossWt"] != DBNull.Value ? Convert.ToString(rowdetails["GrossWt"]) : string.Empty;
                                            string ItemFranchiseSts = rowdetails["ItemFranchiseSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemFranchiseSts"]) : string.Empty;
                                            string Priceflags = rowdetails["priceflags"] != DBNull.Value ? Convert.ToString(rowdetails["priceflags"]) : string.Empty;
                                            string ItemPlainGold = rowdetails["ItemPlainGold"] != DBNull.Value ? Convert.ToString(rowdetails["ItemPlainGold"]) : string.Empty;
                                            string ItemSoliterSts = rowdetails["ItemSoliterSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemSoliterSts"]) : string.Empty;
                                            string ItemSubCtgName = rowdetails["ItemSubCtgName"] != DBNull.Value ? Convert.ToString(rowdetails["ItemSubCtgName"]) : string.Empty;
                                            string ItemSubSubCtgName = rowdetails["ItemSubSubCtgName"] != DBNull.Value ? Convert.ToString(rowdetails["ItemSubSubCtgName"]) : string.Empty;
                                            string ItemIsSRP = rowdetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowdetails["ItemIsSRP"]) : string.Empty;
                                            string ItemSizeAvailable = rowdetails["ItemSizeAvailable"] != DBNull.Value ? Convert.ToString(rowdetails["ItemSizeAvailable"]) : string.Empty;
                                            string IsEarPiercing = rowdetails["isEarPiercing"] != DBNull.Value ? Convert.ToString(rowdetails["isEarPiercing"]) : string.Empty;
                                            string ProductTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string SubCollection = rowdetails["sub_collection"] != DBNull.Value ? Convert.ToString(rowdetails["sub_collection"]) : string.Empty;
                                            string GemsStone = rowdetails["gems_stone"] != DBNull.Value ? Convert.ToString(rowdetails["gems_stone"]) : string.Empty;
                                            string DataCollection1 = rowdetails["data_collection1"] != DBNull.Value ? Convert.ToString(rowdetails["data_collection1"]) : string.Empty;
                                            string Next = rowdetails["next"] != DBNull.Value ? Convert.ToString(rowdetails["next"]) : string.Empty;
                                            string Prev = rowdetails["prev"] != DBNull.Value ? Convert.ToString(rowdetails["prev"]) : string.Empty;
                                            string Weight = rowdetails["weight"] != DBNull.Value ? Convert.ToString(rowdetails["weight"]) : string.Empty;
                                            string Metalweight = rowdetails["metalweight"] != DBNull.Value ? Convert.ToString(rowdetails["metalweight"]) : string.Empty;
                                            string Diamondweight = rowdetails["diamondweight"] != DBNull.Value ? Convert.ToString(rowdetails["diamondweight"]) : string.Empty;
                                            string Approxdelivery = rowdetails["approxdelivery"] != DBNull.Value ? Convert.ToString(rowdetails["approxdelivery"]) : string.Empty;
                                            string Collections = rowdetails["collections"] != DBNull.Value ? Convert.ToString(rowdetails["collections"]) : string.Empty;
                                            string ItemStockQty = rowdetails["item_stock_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock_qty"]) : string.Empty;
                                            string StockColorId = rowdetails["stock_color_id"] != DBNull.Value ? Convert.ToString(rowdetails["stock_color_id"]) : string.Empty;
                                            string StockSizeId = rowdetails["stock_size_id"] != DBNull.Value ? Convert.ToString(rowdetails["stock_size_id"]) : string.Empty;
                                            string ItemStockColorsizeQty = rowdetails["item_stock_colorsize_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock_colorsize_qty"]) : string.Empty;
                                            string SelectedColor = rowdetails["selectedColor"] != DBNull.Value ? Convert.ToString(rowdetails["selectedColor"]) : string.Empty;
                                            string SelectedSize = rowdetails["selectedSize"] != DBNull.Value ? Convert.ToString(rowdetails["selectedSize"]) : string.Empty;
                                            string SelectedColor1 = rowdetails["selectedColor1"] != DBNull.Value ? Convert.ToString(rowdetails["selectedColor1"]) : string.Empty;
                                            string SelectedSize1 = rowdetails["selectedSize1"] != DBNull.Value ? Convert.ToString(rowdetails["selectedSize1"]) : string.Empty;
                                            string FieldName = rowdetails["field_name"] != DBNull.Value ? Convert.ToString(rowdetails["field_name"]) : string.Empty;
                                            string ColorName = rowdetails["color_name"] != DBNull.Value ? Convert.ToString(rowdetails["color_name"]) : string.Empty;
                                            string DefaultColorName = rowdetails["default_color_name"] != DBNull.Value ? Convert.ToString(rowdetails["default_color_name"]) : string.Empty;
                                            string DefaultColorCode = rowdetails["default_color_code"] != DBNull.Value ? Convert.ToString(rowdetails["default_color_code"]) : string.Empty;
                                            string DefaultSizeName = rowdetails["default_size_name"] != DBNull.Value ? Convert.ToString(rowdetails["default_size_name"]) : string.Empty;
                                            string FranMrpGst = rowdetails["fran_mrp_gst"] != DBNull.Value ? Convert.ToString(rowdetails["fran_mrp_gst"]) : string.Empty;
                                            string ApproxDays = rowdetails["approxDays"] != DBNull.Value ? Convert.ToString(rowdetails["approxDays"]) : string.Empty;
                                            string SizeList = rowdetails["sizeList"] != DBNull.Value ? Convert.ToString(rowdetails["sizeList"]) : string.Empty; 
                                            string ItemsColorSizeList = rowdetails["itemsColorSizeList"] != DBNull.Value ? Convert.ToString(rowdetails["itemsColorSizeList"]) : string.Empty; 
                                            string ItemOrderInstructionList = rowdetails["itemOrderInstructionList"] != DBNull.Value ? Convert.ToString(rowdetails["itemOrderInstructionList"]) : string.Empty; 
                                            string ItemOrderCustomInstructionList = rowdetails["itemOrderCustomInstructionList"] != DBNull.Value ? Convert.ToString(rowdetails["itemOrderCustomInstructionList"]) : string.Empty; 
                                            string ItemImagesColor = rowdetails["item_images_color"] != DBNull.Value ? Convert.ToString(rowdetails["item_images_color"]) : string.Empty; 
                                            string DiamondData = rowdetails["diamondData"] != DBNull.Value ? Convert.ToString(rowdetails["diamondData"]) : string.Empty; 
                                            string ColorList = rowdetails["colorList"] != DBNull.Value ? Convert.ToString(rowdetails["colorList"]) : string.Empty; 
                                            string ItemImages = rowdetails["item_images"] != DBNull.Value ? Convert.ToString(rowdetails["item_images"]) : string.Empty; 
                                            string FranchiseWiseStock = rowdetails["franchise_wise_stock"] != DBNull.Value ? Convert.ToString(rowdetails["franchise_wise_stock"]) : string.Empty; 

                                            // Converting String to Json Format
                                            List<Dictionary<string, object>> sizeListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> colorListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemsColorSizeListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemOrderInstructionListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemOrderCustomInstructionListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemImagesColorDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> diamondDataDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemImagesDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> approxDaysDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(SizeList))
                                            {
                                                try
                                                {
                                                    sizeListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(SizeList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing SizeList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ColorList))
                                            {
                                                try
                                                {
                                                    colorListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ColorList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ColorList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemsColorSizeList))
                                            {
                                                try
                                                {
                                                    itemsColorSizeListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemsColorSizeList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemsColorSizeList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemOrderInstructionList))
                                            {
                                                try
                                                {
                                                    itemOrderInstructionListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemOrderInstructionList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemOrderInstructionList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemOrderCustomInstructionList))
                                            {
                                                try
                                                {
                                                    itemOrderCustomInstructionListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemOrderCustomInstructionList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemOrderCustomInstructionList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemImagesColor))
                                            {
                                                try
                                                {
                                                    itemImagesColorDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemImagesColor);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemImagesColor: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(DiamondData))
                                            {
                                                try
                                                {
                                                    diamondDataDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(DiamondData);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing DiamondData: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ItemImages))
                                            {
                                                try
                                                {
                                                    itemImagesDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemImages);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ItemImages: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(ApproxDays))
                                            {
                                                try
                                                {
                                                    approxDaysDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ApproxDays);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ApproxDays: " + ex.Message);
                                                }
                                            }

                                            itemDetails.Add(new ItemDetailsResponse
                                            {
                                                ItemId = ItemId,
                                                ItemSoliter = ItemSoliter,
                                                ItemCtgCommonID = ItemCtgCommonID,
                                                ItemAproxDay = ItemAproxDay,
                                                ItemDAproxDay = ItemDAproxDay,
                                                PlaingoldStatus = PlaingoldStatus,
                                                ItemName = ItemName,
                                                ItemSku = ItemSku,
                                                ItemDescription = ItemDescription,
                                                ItemMrp = ItemMrp,
                                                ItemDiscount = ItemDiscount,
                                                ItemPrice = ItemPrice,
                                                ItemBrandCommonID = ItemBrandCommonID,
                                                ItemCd = ItemCd,
                                                RetailPrice = RetailPrice,
                                                DistPrice = DistPrice,
                                                Uom = Uom,
                                                Star = Star,
                                                CartImg = CartImg,
                                                ImgCartTitle = ImgCartTitle,
                                                WatchImg = WatchImg,
                                                ImgWatchTitle = ImgWatchTitle,
                                                WearitCount = WearitCount,
                                                WearitStatus = WearitStatus,
                                                WearitImg = WearitImg,
                                                WearitNoneImg = WearitNoneImg,
                                                WearitColor = WearitColor,
                                                WearitText = WearitText,
                                                ImgWearitTitle = ImgWearitTitle,
                                                TryonCount = TryonCount,
                                                TryonStatus = TryonStatus,
                                                TryonImg = TryonImg,
                                                TryonNoneImg = TryonNoneImg,
                                                TryonText = TryonText,
                                                TryonTitle = TryonTitle,
                                                TryonAndroidPath = TryonAndroidPath,
                                                TryonIosPath = TryonIosPath,
                                                WishCount = WishCount,
                                                WishDefaultImg = WishDefaultImg,
                                                WishFillImg = WishFillImg,
                                                ImgWishTitle = ImgWishTitle,
                                                ItemReview = ItemReview,
                                                ItemSize = ItemSize,
                                                ItemKt = ItemKt,
                                                ItemColor = ItemColor,
                                                ItemMetal = ItemMetal,
                                                ItemWt = ItemWt,
                                                ItemStone = ItemStone,
                                                ItemStoneWt = ItemStoneWt,
                                                ItemStoneQty = ItemStoneQty,
                                                StarColor = StarColor,
                                                ItemMetalCommonID = ItemMetalCommonID,
                                                PriceText = PriceText,
                                                CartPrice = CartPrice,
                                                ItemColorId = ItemColorId,
                                                ItemDetails = ItemDetails,
                                                ItemDiamondDetails = ItemDiamondDetails,
                                                ItemText = ItemText,
                                                MoreItemDetails = MoreItemDetails,
                                                ItemStock = ItemStock,
                                                CartItemQty =CartItemQty,
                                                RupySymbol = RupySymbol,
                                                VariantCount = VariantCount,
                                                CartId = CartId,
                                                ItemGenderCommonID = ItemGenderCommonID,
                                                CategoryId = CategoryId,
                                                ItemNosePinScrewSts = ItemNosePinScrewSts,
                                                Metal = Metal,
                                                Kt = Kt,
                                                Quality = Quality,
                                                Shape = Shape,
                                                Brand = Brand,
                                                Stone = Stone,
                                                Diamondcolor = Diamondcolor,
                                                Category = Category,
                                                ItemAproxDayCommonID = ItemAproxDayCommonID,
                                                GrossWt = GrossWt,
                                                ItemFranchiseSts = ItemFranchiseSts,
                                                Priceflags = Priceflags,
                                                ItemPlainGold = ItemPlainGold,
                                                ItemSoliterSts = ItemSoliterSts,
                                                ItemSubCtgName = ItemSubCtgName,
                                                ItemSubSubCtgName = ItemSubSubCtgName,
                                                ItemIsSRP = ItemIsSRP,
                                                ItemSizeAvailable = ItemSizeAvailable,
                                                IsEarPiercing = IsEarPiercing,
                                                ProductTags = ProductTags,
                                                SubCollection = SubCollection,
                                                GemsStone = GemsStone,
                                                DataCollection1 = DataCollection1,
                                                Next = Next,
                                                Prev = Prev,
                                                Weight = Weight,
                                                Metalweight = Metalweight,
                                                Diamondweight = Diamondweight,
                                                Approxdelivery = Approxdelivery,
                                                Collections = Collections,
                                                ItemStockQty = ItemStockQty,
                                                StockColorId = StockColorId,
                                                StockSizeId = StockSizeId,
                                                ItemStockColorsizeQty = ItemStockColorsizeQty,
                                                SelectedColor = SelectedColor,
                                                SelectedSize = SelectedSize,
                                                SelectedColor1 = SelectedColor1,
                                                SelectedSize1 = SelectedSize1,
                                                FieldName = FieldName,
                                                ColorName = ColorName,
                                                DefaultColorName = DefaultColorName,
                                                DefaultColorCode = DefaultColorCode,
                                                DefaultSizeName = DefaultSizeName,
                                                FranMrpGst = FranMrpGst,
                                                ApproxDays = approxDaysDynamic,
                                                SizeList = sizeListDynamic,
                                                ItemsColorSizeList = itemsColorSizeListDynamic,
                                                ItemOrderInstructionList = itemOrderInstructionListDynamic,
                                                ItemOrderCustomInstructionList = itemOrderCustomInstructionListDynamic,
                                                ItemImagesColor = itemImagesColorDynamic,
                                                DiamondData = diamondDataDynamic,
                                                ColorList = colorListDynamic,
                                                ItemImages = itemImagesDynamic
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (itemDetails.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = itemDetails;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<ItemDetailsResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<ItemDetailsResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> ChatCustomerCare(ChatCustomerCareRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<ChatCustomerResponse> chatCustomerCare = new List<ChatCustomerResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ChatCustomerCare;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? data_id = string.IsNullOrWhiteSpace(param.dataId) ? null : param.dataId;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", data_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[1].Rows[i];
                                            string id = rowdetails["Id"] != DBNull.Value ? Convert.ToString(rowdetails["Id"]) : string.Empty;
                                            string name = rowdetails["Name"] != DBNull.Value ? Convert.ToString(rowdetails["Name"]) : string.Empty;
                                            string email = rowdetails["Email"] != DBNull.Value ? Convert.ToString(rowdetails["Email"]) : string.Empty;
                                            string description = rowdetails["Desc"] != DBNull.Value ? Convert.ToString(rowdetails["Desc"]) : string.Empty;
                                            string commonId = rowdetails["comid"] != DBNull.Value ? Convert.ToString(rowdetails["comid"]) : string.Empty;

                                            chatCustomerCare.Add(new ChatCustomerResponse
                                            {
                                                Id = id,
                                                Name = name,
                                                Email = email,
                                                Description = description,
                                                CommonId = commonId
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (chatCustomerCare.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = chatCustomerCare;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<ChatCustomerResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<ChatCustomerResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> FranchiseWiseStock(FranchiseWiseStockRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<FranchiseWiseStockResponse> franchisewisestock = new List<FranchiseWiseStockResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseWiseStock;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? item_code = string.IsNullOrWhiteSpace(param.ItemCode) ? null : param.ItemCode;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@ItemCd", item_code);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string availableStock = rowdetails["available_stock"] != DBNull.Value ? Convert.ToString(rowdetails["available_stock"]) : string.Empty;
                                            string branchCode = rowdetails["branch_code"] != DBNull.Value ? Convert.ToString(rowdetails["branch_code"]) : string.Empty;
                                            string franchiseName = rowdetails["franchise_name"] != DBNull.Value ? Convert.ToString(rowdetails["franchise_name"]) : string.Empty;
                                            string franchiseAddress = rowdetails["franchise_address"] != DBNull.Value ? Convert.ToString(rowdetails["franchise_address"]) : string.Empty;
                                            string details = rowdetails["details"] != DBNull.Value ? Convert.ToString(rowdetails["details"]) : string.Empty;

                                            List<Dictionary<string, object>> detailsDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(details))
                                            {
                                                try
                                                {
                                                    detailsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(details);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing details: " + ex.Message);
                                                }
                                            }

                                            franchisewisestock.Add(new FranchiseWiseStockResponse
                                            {
                                                AvailableStock = availableStock,
                                                BranchCode = branchCode,
                                                FranchiseName = franchiseName,
                                                FranchiseAddress = franchiseAddress,
                                                Details = detailsDynamic
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (franchisewisestock.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = franchisewisestock;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<FranchiseWiseStockResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<FranchiseWiseStockResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CappingSortBy(CappingSortByRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<CappingSortByResponse> cappingSort = new List<CappingSortByResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CappingSortBy;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? dataId = string.IsNullOrWhiteSpace(param.DataId) ? null : param.DataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.DataLoginType) ? null : param.DataLoginType;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string sortId = rowdetails["sort_id"] != DBNull.Value ? Convert.ToString(rowdetails["sort_id"]) : string.Empty;
                                            string sortCode = rowdetails["sort_cd"] != DBNull.Value ? Convert.ToString(rowdetails["sort_cd"]) : string.Empty;
                                            string sortName = rowdetails["sort_name"] != DBNull.Value ? Convert.ToString(rowdetails["sort_name"]) : string.Empty;
                                            string sortMasterId = rowdetails["sort_mster_id"] != DBNull.Value ? Convert.ToString(rowdetails["sort_mster_id"]) : string.Empty;

                                            cappingSort.Add(new CappingSortByResponse
                                            {
                                                SortId = sortId,
                                                SortCode = sortCode,
                                                SortName = sortName,
                                                SortMasterId = sortMasterId
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (cappingSort.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = cappingSort;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<CappingSortByResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CappingSortByResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CappingItemList(CappingItemListRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<CappingItemListResponse> cappingItemDetails = new List<CappingItemListResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CappingItemList;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? dataId = string.IsNullOrWhiteSpace(param.DataId) ? null : param.DataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.DataLoginType) ? null : param.DataLoginType;
                        string? type = string.IsNullOrWhiteSpace(param.Type) ? null : param.Type;
                        string? categoryId = string.IsNullOrWhiteSpace(param.CategoryId) ? null : param.CategoryId;
                        string? itemSubCategoryId = string.IsNullOrWhiteSpace(param.ItemSubCategoryId) ? null : param.ItemSubCategoryId;
                        string? subCategoryId = string.IsNullOrWhiteSpace(param.SubCategoryId) ? null : param.SubCategoryId;
                        string? sizeId = string.IsNullOrWhiteSpace(param.SizeId) ? null : param.SizeId;
                        string? colorCode = string.IsNullOrWhiteSpace(param.ColorCode) ? null : param.ColorCode;
                        string? amount = string.IsNullOrWhiteSpace(param.Amount) ? null : param.Amount;
                        string? deliveryDays = string.IsNullOrWhiteSpace(param.DeliveryDays) ? null : param.DeliveryDays;
                        string? dsgKt = string.IsNullOrWhiteSpace(param.DsgKt) ? null : param.DsgKt;
                        string? sortId = string.IsNullOrWhiteSpace(param.SortId) ? null : param.SortId;
                        string? itemTag = string.IsNullOrWhiteSpace(param.ItemTag) ? null : param.ItemTag;
                        string? genderId = string.IsNullOrWhiteSpace(param.GenderId) ? null : param.GenderId;
                        string? brand = string.IsNullOrWhiteSpace(param.Brand) ? null : param.Brand;
                        string? familyAv = string.IsNullOrWhiteSpace(param.FamilyAv) ? null : param.FamilyAv;
                        string? metalWt = string.IsNullOrWhiteSpace(param.MetalWt) ? null : param.MetalWt;
                        string? diaWt = string.IsNullOrWhiteSpace(param.DiaWt) ? null : param.DiaWt;
                        string? itemSubCtgIDs = string.IsNullOrWhiteSpace(param.ItemSubCtgIDs) ? null : param.ItemSubCtgIDs;
                        string? itemSubSubCtgIDs = string.IsNullOrWhiteSpace(param.ItemSubSubCtgIDs) ? null : param.ItemSubSubCtgIDs;
                        string? page = string.IsNullOrWhiteSpace(param.Page) ? null : param.Page;
                        string? limit = string.IsNullOrWhiteSpace(param.DefaultLimitAppPage) ? null : param.DefaultLimitAppPage;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@category_id", categoryId);
                        cmd.Parameters.AddWithValue("@item_sub_category_id", itemSubCategoryId);
                        cmd.Parameters.AddWithValue("@sub_category_id", subCategoryId);
                        cmd.Parameters.AddWithValue("@size_id", sizeId);
                        cmd.Parameters.AddWithValue("@color_cd", colorCode);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@delivery_days", deliveryDays);
                        cmd.Parameters.AddWithValue("@dsg_kt", dsgKt);
                        cmd.Parameters.AddWithValue("@sort_id", sortId);
                        cmd.Parameters.AddWithValue("@item_tag", itemTag);
                        cmd.Parameters.AddWithValue("@gender_id", genderId);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@Family_Av", familyAv);
                        cmd.Parameters.AddWithValue("@metalwt", metalWt);
                        cmd.Parameters.AddWithValue("@diawt", diaWt);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", itemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", itemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@default_limit_app_page", limit);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string categoryID = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemGenderCommonID = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string itemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string itemKt = rowdetails["item_kt"] != DBNull.Value ? Convert.ToString(rowdetails["item_kt"]) : string.Empty;
                                            string sizeID = rowdetails["size_id"] != DBNull.Value ? Convert.ToString(rowdetails["size_id"]) : string.Empty;
                                            string colorId = rowdetails["color_id"] != DBNull.Value ? Convert.ToString(rowdetails["color_id"]) : string.Empty;
                                            string isStockFilter = rowdetails["isStockFilter"] != DBNull.Value ? Convert.ToString(rowdetails["isStockFilter"]) : string.Empty;
                                            string plainGoldStatus = rowdetails["plaingold_status"] != DBNull.Value ? Convert.ToString(rowdetails["plaingold_status"]) : string.Empty;
                                            string approxDeliveryDate = rowdetails["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["ApproxDeliveryDate"]) : string.Empty;
                                            string itemBrandText = rowdetails["item_brand_text"] != DBNull.Value ? Convert.ToString(rowdetails["item_brand_text"]) : string.Empty;
                                            string itemIsSRP = rowdetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowdetails["ItemIsSRP"]) : string.Empty;
                                            string subCategoryID = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string priceFlag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string mostOrder = rowdetails["mostOrder"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string isInFranchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;

                                            List<Dictionary<string, object>> productTagsDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(productTags))
                                            {
                                                try
                                                {
                                                    productTagsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(productTags);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing productTags: " + ex.Message);
                                                }
                                            }

                                            cappingItemDetails.Add(new CappingItemListResponse
                                            {
                                                ItemId = itemId,
                                                CategoryId = categoryID,
                                                ItemDescription = itemDescription,
                                                ItemCode = itemCode,
                                                ItemName = itemName,
                                                ItemGenderCommonID = itemGenderCommonID,
                                                ItemNosePinScrewSts = itemNosePinScrewSts,
                                                ItemKt = itemKt,
                                                SizeId = sizeID,
                                                ColorId = colorId,
                                                IsStockFilter = isStockFilter,
                                                PlainGoldStatus = plainGoldStatus,
                                                ApproxDeliveryDate = approxDeliveryDate,
                                                ItemBrandText = itemBrandText,
                                                ItemIsSRP = itemIsSRP,
                                                SubCategoryId = subCategoryID,
                                                PriceFlag = priceFlag,
                                                ItemMrp = itemMrp,
                                                ImagePath = imagePath,
                                                MostOrder = mostOrder,
                                                ProductTags = productTagsDynamic,
                                                IsInFranchiseStore = isInFranchiseStore
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (cappingItemDetails.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = cappingItemDetails;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<CappingItemListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CappingItemListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CappingFilter(CappingFilterRequest request)
        {
            var responseDetails = new ResponseDetails();
            bool hasAnyData = false;
            PopularItemsFilterList filterDetails = new PopularItemsFilterList();
            IList<FilterCategoryList> categoryList = new List<FilterCategoryList>();
            IList<FilterDsgKtList> dsgKtList = new List<FilterDsgKtList>();
            IList<FilterDsgMetalWtList> dsgMetalwtList = new List<FilterDsgMetalWtList>();
            IList<FilterDsgDiamondWtList> dsgDiamondList = new List<FilterDsgDiamondWtList>();
            IList<FilterProductTagList> productTagsList = new List<FilterProductTagList>();
            IList<FilterBrandList> brandList = new List<FilterBrandList>();
            IList<FilterGenderList> genderList = new List<FilterGenderList>();
            IList<FilterApproxDeliveryList> approxDeliveryList = new List<FilterApproxDeliveryList>();
            IList<FilterSubCategoryList> itemSubCategoryList = new List<FilterSubCategoryList>();
            IList<FilterSubSubCategoryList> itemSubSubCategoryList = new List<FilterSubSubCategoryList>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CappingFilter;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? dataId = string.IsNullOrWhiteSpace(request.DataId) ? null : request.DataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(request.DataLoginType) ? null : request.DataLoginType;
                        string? type = string.IsNullOrWhiteSpace(request.Type) ? null : request.Type;
                        string? categoryId = string.IsNullOrWhiteSpace(request.CategoryId) ? null : request.CategoryId;
                        string? metalWt = string.IsNullOrWhiteSpace(request.MetalWt) ? null : request.MetalWt;

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@category_id", categoryId);
                        cmd.Parameters.AddWithValue("@metalwt", metalWt);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds?.Tables?.Count >= 1)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    hasAnyData = true;
                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    {
                                        categoryList.Add(new FilterCategoryList
                                        {
                                            categoryId = Convert.ToString(ds.Tables[1].Rows[i]["category_id"]),
                                            subCategoryId = Convert.ToString(ds.Tables[1].Rows[i]["sub_category_id"]),
                                            subCategoryName = Convert.ToString(ds.Tables[1].Rows[i]["sub_category_name"]),
                                            subCategoryCount = Convert.ToString(ds.Tables[1].Rows[i]["category_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        dsgKtList.Add(new FilterDsgKtList
                                        {
                                            KT = Convert.ToString(ds.Tables[2].Rows[i]["kt"]),
                                            KtCount = Convert.ToString(ds.Tables[2].Rows[i]["Kt_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[3].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        dsgMetalwtList.Add(new FilterDsgMetalWtList
                                        {
                                            minMetalweight = Convert.ToString(ds.Tables[3].Rows[i]["minmetalwt"]),
                                            maxMetalWeight = Convert.ToString(ds.Tables[3].Rows[i]["maxmetalwt"]),
                                        });
                                    }
                                }

                                if (ds.Tables[4].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        dsgDiamondList.Add(new FilterDsgDiamondWtList
                                        {
                                            minDiamondWeight = Convert.ToString(ds.Tables[4].Rows[i]["min_diawt"]),
                                            maxDiamondWeight = Convert.ToString(ds.Tables[4].Rows[i]["max_diawt"]),
                                        });
                                    }
                                }

                                if (ds.Tables[5].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        productTagsList.Add(new FilterProductTagList
                                        {
                                            tagName = Convert.ToString(ds.Tables[5].Rows[i]["tag_name"]),
                                            tagCount = Convert.ToString(ds.Tables[5].Rows[i]["tag_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[6].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        brandList.Add(new FilterBrandList
                                        {
                                            brandId = Convert.ToString(ds.Tables[6].Rows[i]["brand_id"]),
                                            brandName = Convert.ToString(ds.Tables[6].Rows[i]["brand_name"]),
                                            brandCount = Convert.ToString(ds.Tables[6].Rows[i]["brand_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[7].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        genderList.Add(new FilterGenderList
                                        {
                                            genderId = Convert.ToString(ds.Tables[7].Rows[i]["gender_id"]),
                                            genderName = Convert.ToString(ds.Tables[7].Rows[i]["gender_name"]),
                                            genderCount = Convert.ToString(ds.Tables[7].Rows[i]["gender_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[8].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        approxDeliveryList.Add(new FilterApproxDeliveryList
                                        {
                                            ItemAproxDays = Convert.ToString(ds.Tables[8].Rows[i]["ItemAproxDay"]),
                                            ItemAproxDayCount = Convert.ToString(ds.Tables[8].Rows[i]["ItemAproxDay_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[9].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        itemSubCategoryList.Add(new FilterSubCategoryList
                                        {
                                            itemSubCategoryId = Convert.ToString(ds.Tables[9].Rows[i]["sub_category_id"]),
                                            itemSubCategoryName = Convert.ToString(ds.Tables[9].Rows[i]["sub_category_name"]),
                                            itemSubCategorycounts = Convert.ToString(ds.Tables[9].Rows[i]["sub_category_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[10].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        itemSubSubCategoryList.Add(new FilterSubSubCategoryList
                                        {
                                            itemSubSubCategoryId = Convert.ToString(ds.Tables[10].Rows[i]["sub_sub_category_id"]),
                                            itemSubSubCategoryName = Convert.ToString(ds.Tables[10].Rows[i]["sub_sub_category_name"]),
                                            itemSubSubCategorycounts = Convert.ToString(ds.Tables[10].Rows[i]["sub_sub_category_count"]),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                if (hasAnyData)
                {
                    filterDetails.categoryList = categoryList;
                    filterDetails.dsgKt = dsgKtList;
                    filterDetails.dsgMetalWeight = dsgMetalwtList;
                    filterDetails.dsgDiamondWeight = dsgDiamondList;
                    filterDetails.productTags = productTagsList;
                    filterDetails.brand = brandList;
                    filterDetails.gender = genderList;
                    filterDetails.approxDelivery = approxDeliveryList;
                    filterDetails.itemSubCategory = itemSubCategoryList;
                    filterDetails.itemSubSubCategory = itemSubSubCategoryList;
                }

                responseDetails = new ResponseDetails
                {
                    success = true,
                    status = "200",
                    message = hasAnyData ? "Successfully" : "No data found",
                    data = filterDetails,
                    data1 = null
                };

                return responseDetails;
            }
            catch (SqlException ex)
            {
                return new ResponseDetails
                {
                    success = false,
                    status = "400",
                    message = $"SQL error: {ex.Message}",
                    data = new List<PopularItemsFilterList>(),
                    data1 = null
                };
            }
        }

        public async Task<ResponseDetails> ConsumerFormStore(ConsumerFormStoreRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<ConsumerFormStoreResponse> details = new List<ConsumerFormStoreResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ConsumerFromStore;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? dataId = string.IsNullOrWhiteSpace(param.DataId) ? null : param.DataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.DataLoginType) ? null : param.DataLoginType;
                        string? name = string.IsNullOrWhiteSpace(param.Name) ? null : param.Name;
                        string? mobile = string.IsNullOrWhiteSpace(param.Mobile) ? null : param.Mobile;
                        string? email = string.IsNullOrWhiteSpace(param.Email) ? null : param.Email;
                        string? address = string.IsNullOrWhiteSpace(param.Address) ? null : param.Address;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@mobile", mobile);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@address", address);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string data = rowdetails["data"] != DBNull.Value ? Convert.ToString(rowdetails["data"]) : string.Empty;

                                            List<Dictionary<string, object>> dataDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(data))
                                            {
                                                try
                                                {
                                                    dataDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing data: " + ex.Message);
                                                }
                                            }

                                            details.Add(new ConsumerFormStoreResponse
                                            {
                                                
                                                Data = dataDynamic
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (details.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = details;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<ConsumerFormStoreResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<ConsumerFormStoreResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> StockWiseItemData(StockWiseItemDataRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<StockWiseItemDataResponse> details = new List<StockWiseItemDataResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.StockWiseItemData;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? itemId = string.IsNullOrWhiteSpace(param.ItemId) ? null : param.ItemId;
                        string? colorId = string.IsNullOrWhiteSpace(param.ColorId) ? null : param.ColorId;
                        string? sizeId = string.IsNullOrWhiteSpace(param.SizeId) ? null : param.SizeId;
                        string? dataId = string.IsNullOrWhiteSpace(param.DataId) ? null : param.DataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.DataLoginType) ? null : param.DataLoginType;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@item_id", itemId);
                        cmd.Parameters.AddWithValue("@color_id", colorId);
                        cmd.Parameters.AddWithValue("@size_id", sizeId);
                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string data = rowdetails["data"] != DBNull.Value ? Convert.ToString(rowdetails["data"]) : string.Empty;

                                            List<Dictionary<string, object>> dataDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(data))
                                            {
                                                try
                                                {
                                                    dataDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing data: " + ex.Message);
                                                }
                                            }

                                            details.Add(new StockWiseItemDataResponse
                                            {

                                                Data = dataDynamic
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (details.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = details;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<StockWiseItemDataResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<StockWiseItemDataResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> PopularItems(PopularItemsRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<PopularItemsResponse> details = new List<PopularItemsResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PopularItems;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? dataId = string.IsNullOrWhiteSpace(param.DataId) ? null : param.DataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.DataLoginType) ? null : param.DataLoginType;
                        string? masterCommonId = string.IsNullOrWhiteSpace(param.MasterCommonId) ? null : param.MasterCommonId;
                        string? page = string.IsNullOrWhiteSpace(param.Page) ? null : param.Page;
                        string? limit = string.IsNullOrWhiteSpace(param.DefaultLimitAppPage) ? null : param.DefaultLimitAppPage;
                        string? variant = string.IsNullOrWhiteSpace(param.Variant) ? null : param.Variant;
                        string? itemName = string.IsNullOrWhiteSpace(param.ItemName) ? null : param.ItemName;
                        string? subcategoryId = string.IsNullOrWhiteSpace(param.SubCategoryId) ? null : param.SubCategoryId;
                        string? dsgSize = string.IsNullOrWhiteSpace(param.DsgSize) ? null : param.DsgSize;
                        string? dsgKt = string.IsNullOrWhiteSpace(param.DsgKt) ? null : param.DsgKt;
                        string? dsgColor = string.IsNullOrWhiteSpace(param.DsgColor) ? null : param.DsgColor;
                        string? amount = string.IsNullOrWhiteSpace(param.Amount) ? null : param.Amount;
                        string? metalWt = string.IsNullOrWhiteSpace(param.MetalWt) ? null : param.MetalWt;
                        string? diaWt = string.IsNullOrWhiteSpace(param.DiaWt) ? null : param.DiaWt;
                        string? itemId = string.IsNullOrWhiteSpace(param.ItemId) ? null : param.ItemId;
                        string? stockAv = string.IsNullOrWhiteSpace(param.StockAv) ? null : param.StockAv;
                        string? familyAv = string.IsNullOrWhiteSpace(param.FamilyAv) ? null : param.FamilyAv;
                        string? regularAv = string.IsNullOrWhiteSpace(param.RegularAv) ? null : param.RegularAv;
                        string? franStoreAv = string.IsNullOrWhiteSpace(param.FranStoreAv) ? null : param.FranStoreAv;
                        string? wearIt = string.IsNullOrWhiteSpace(param.WearIt) ? null : param.WearIt;
                        string? tryOn = string.IsNullOrWhiteSpace(param.TryOn) ? null : param.TryOn;
                        string? genderId = string.IsNullOrWhiteSpace(param.GenderId) ? null : param.GenderId;
                        string? imageTag = string.IsNullOrWhiteSpace(param.ItemTag) ? null : param.ItemTag;
                        string? brand = string.IsNullOrWhiteSpace(param.Brand) ? null : param.Brand;
                        string? deliveryDays = string.IsNullOrWhiteSpace(param.DeliveryDays) ? null : param.DeliveryDays;
                        string? itemSubCategoryIds = string.IsNullOrWhiteSpace(param.ItemSubCtgIDs) ? null : param.ItemSubCtgIDs;
                        string? itemSubSubCategoryId = string.IsNullOrWhiteSpace(param.ItemSubSubCtgIDs) ? null : param.ItemSubSubCtgIDs;
                        string? designTimeLine = string.IsNullOrWhiteSpace(param.DesignTimeline) ? null : param.DesignTimeline;
                        string? itemSubCategoryId = string.IsNullOrWhiteSpace(param.ItemSubCategoryId) ? null : param.ItemSubCategoryId;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);
                        cmd.Parameters.AddWithValue("@master_common_id", masterCommonId);
                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@default_limit_app_page", limit);
                        cmd.Parameters.AddWithValue("@variant", variant);
                        cmd.Parameters.AddWithValue("@item_name", itemName);
                        cmd.Parameters.AddWithValue("@sub_category_id", subcategoryId);
                        cmd.Parameters.AddWithValue("@dsg_size", dsgSize);
                        cmd.Parameters.AddWithValue("@dsg_kt", dsgKt);
                        cmd.Parameters.AddWithValue("@dsg_color", dsgColor);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@metalwt", metalWt);
                        cmd.Parameters.AddWithValue("@diawt", diaWt);
                        cmd.Parameters.AddWithValue("@Item_ID", itemId);
                        cmd.Parameters.AddWithValue("@Stock_Av", stockAv);
                        cmd.Parameters.AddWithValue("@Family_Av", familyAv);
                        cmd.Parameters.AddWithValue("@Regular_Av", regularAv);
                        cmd.Parameters.AddWithValue("@fran_store_av", franStoreAv);
                        cmd.Parameters.AddWithValue("@wearit", wearIt);
                        cmd.Parameters.AddWithValue("@tryon", tryOn);
                        cmd.Parameters.AddWithValue("@gender_id", genderId);
                        cmd.Parameters.AddWithValue("@item_tag", imageTag);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@delivery_days", deliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", itemSubCategoryIds);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", itemSubSubCategoryId);
                        cmd.Parameters.AddWithValue("@design_timeline", designTimeLine);
                        cmd.Parameters.AddWithValue("@item_sub_category_id", itemSubCategoryId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string itemIds = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string itemNames = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemGenderCommonId = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string itemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string subCategoryId = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string priceFlag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string maxQtySold = rowdetails["max_qty_sold"] != DBNull.Value ? Convert.ToString(rowdetails["max_qty_sold"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string mostOrder = rowdetails["mostOrder"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder"]) : string.Empty;
                                            string tagName = rowdetails["tag_name"] != DBNull.Value ? Convert.ToString(rowdetails["tag_name"]) : string.Empty;
                                            string tagColor = rowdetails["tag_color"] != DBNull.Value ? Convert.ToString(rowdetails["tag_color"]) : string.Empty;
                                            string franchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            details.Add(new PopularItemsResponse
                                            {
                                                ItemId = itemIds,
                                                CategoryId = categoryId,
                                                ItemDescription = itemDescription,
                                                ItemCode = itemCode,
                                                ItemName = itemNames,
                                                ItemGenderCommonId = itemGenderCommonId,
                                                ItemNosePinScrewSts = itemNosePinScrewSts,
                                                SubCategoryId = subCategoryId,
                                                PriceFlag = priceFlag,
                                                ItemMrp = itemMrp,
                                                MaxQtySold = maxQtySold,
                                                ImagePath = imagePath,
                                                MostOrder = mostOrder,
                                                TagName = tagName,
                                                TagColor = tagColor,
                                                FranchiseStore = franchiseStore
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (details.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = details;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<PopularItemsResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<PopularItemsResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> PopularItemsFilter(PopularItemsFilterRequest request)
        {
            var responseDetails = new ResponseDetails();
            bool hasAnyData = false;
            PopularItemsFilterList filterDetails = new PopularItemsFilterList();
            IList<FilterCategoryList> categoryList = new List<FilterCategoryList>();
            IList<FilterDsgKtList> dsgKtList = new List<FilterDsgKtList>();
            IList<FilterAmountList> amountList = new List<FilterAmountList>();
            IList<FilterDsgMetalWtList> dsgMetalwtList = new List<FilterDsgMetalWtList>();
            IList<FilterDsgDiamondWtList> dsgDiamondList = new List<FilterDsgDiamondWtList>();
            IList<FilterProductTagList> productTagsList = new List<FilterProductTagList>();
            IList<FilterBrandList> brandList = new List<FilterBrandList>();
            IList<FilterGenderList> genderList = new List<FilterGenderList>();
            IList<FilterApproxDeliveryList> approxDeliveryList = new List<FilterApproxDeliveryList>();
            IList<FilterStockList> stockList = new List<FilterStockList>();
            IList<FilterSubCategoryList> itemSubCategoryList = new List<FilterSubCategoryList>();
            IList<FilterSubSubCategoryList> itemSubSubCategoryList = new List<FilterSubSubCategoryList>();
            IList<FilterFamilyProductList> familyProductList = new List<FilterFamilyProductList>();
            IList<FilterExcludeDiscontinueList> excludeDisconList = new List<FilterExcludeDiscontinueList>();
            IList<FilterWearViewList> wearViewList = new List<FilterWearViewList>();
            IList<FilterTryViewList> tryViewList = new List<FilterTryViewList>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PopularItemsFilter;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? data_id = string.IsNullOrWhiteSpace(request.DataId) ? null : request.DataId;
                        string? button_code = string.IsNullOrWhiteSpace(request.ButtonCode) ? null : request.ButtonCode;
                        string? master_common_id = string.IsNullOrWhiteSpace(request.MasterCommonId) ? null : request.MasterCommonId;
                        string? category_id = string.IsNullOrWhiteSpace(request.CategoryId) ? null : request.CategoryId;

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@button_code", button_code);
                        cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
                        cmd.Parameters.AddWithValue("@category_id", category_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds?.Tables?.Count >= 1)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    hasAnyData = true;
                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    {
                                        categoryList.Add(new FilterCategoryList
                                        {
                                            filterName = Convert.ToString(ds.Tables[1].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[1].Rows[i]["name"]),
                                            subCategoryId = Convert.ToString(ds.Tables[1].Rows[i]["sub_category_id"]),
                                            subCategoryName = Convert.ToString(ds.Tables[1].Rows[i]["sub_category_name"]),
                                            subCategoryCount = Convert.ToString(ds.Tables[1].Rows[i]["category_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        dsgKtList.Add(new FilterDsgKtList
                                        {
                                            filterName = Convert.ToString(ds.Tables[2].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[2].Rows[i]["name"]),
                                            KT = Convert.ToString(ds.Tables[2].Rows[i]["kt"]),
                                            KtCount = Convert.ToString(ds.Tables[2].Rows[i]["Kt_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[3].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        amountList.Add(new FilterAmountList
                                        {
                                            filterName = Convert.ToString(ds.Tables[3].Rows[i]["filter_name"]),
                                            minAmount = Convert.ToString(ds.Tables[3].Rows[i]["min_amount"]),
                                            maxAmount = Convert.ToString(ds.Tables[3].Rows[i]["max_amount"]),
                                        });
                                    }
                                }

                                if (ds.Tables[4].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        dsgMetalwtList.Add(new FilterDsgMetalWtList
                                        {
                                            filterName = Convert.ToString(ds.Tables[4].Rows[i]["filter_name"]),
                                            minMetalweight = Convert.ToString(ds.Tables[4].Rows[i]["min_metalwt"]),
                                            maxMetalWeight = Convert.ToString(ds.Tables[4].Rows[i]["max_metalwt"]),
                                        });
                                    }
                                }

                                if (ds.Tables[5].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        dsgDiamondList.Add(new FilterDsgDiamondWtList
                                        {
                                            filterName = Convert.ToString(ds.Tables[5].Rows[i]["filter_name"]),
                                            minDiamondWeight = Convert.ToString(ds.Tables[5].Rows[i]["min_diamondwt"]),
                                            maxDiamondWeight = Convert.ToString(ds.Tables[5].Rows[i]["max_diamondwt"]),
                                        });
                                    }
                                }

                                if (ds.Tables[6].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        productTagsList.Add(new FilterProductTagList
                                        {
                                            filterName = Convert.ToString(ds.Tables[6].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[6].Rows[i]["name"]),
                                            tagName = Convert.ToString(ds.Tables[6].Rows[i]["tag_name"]),
                                            tagCount = Convert.ToString(ds.Tables[6].Rows[i]["tag_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[7].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        brandList.Add(new FilterBrandList
                                        {
                                            filterName = Convert.ToString(ds.Tables[7].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[7].Rows[i]["name"]),
                                            brandId = Convert.ToString(ds.Tables[7].Rows[i]["brand_id"]),
                                            brandName = Convert.ToString(ds.Tables[7].Rows[i]["brand_name"]),
                                            brandCount = Convert.ToString(ds.Tables[7].Rows[i]["brand_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[8].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        genderList.Add(new FilterGenderList
                                        {
                                            filterName = Convert.ToString(ds.Tables[8].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[8].Rows[i]["name"]),
                                            genderId = Convert.ToString(ds.Tables[8].Rows[i]["gender_id"]),
                                            genderName = Convert.ToString(ds.Tables[8].Rows[i]["gender_name"]),
                                            genderCount = Convert.ToString(ds.Tables[8].Rows[i]["gender_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[9].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        approxDeliveryList.Add(new FilterApproxDeliveryList
                                        {
                                            filterName = Convert.ToString(ds.Tables[9].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[9].Rows[i]["name"]),
                                            ItemAproxDays = Convert.ToString(ds.Tables[9].Rows[i]["ItemAproxDay"]),
                                            ItemAproxDayCount = Convert.ToString(ds.Tables[9].Rows[i]["ItemAproxDay_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[10].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        stockList.Add(new FilterStockList
                                        {
                                            filterName = Convert.ToString(ds.Tables[10].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[10].Rows[i]["name"]),
                                            stockName = Convert.ToString(ds.Tables[10].Rows[i]["stock_name"]),
                                            stockId = Convert.ToString(ds.Tables[10].Rows[i]["stock_id"]),
                                        });
                                    }
                                }

                                if (ds.Tables[11].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[11].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        itemSubCategoryList.Add(new FilterSubCategoryList
                                        {
                                            filterName = Convert.ToString(ds.Tables[11].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[11].Rows[i]["name"]),
                                            itemSubCategoryId = Convert.ToString(ds.Tables[11].Rows[i]["sub_category_id"]),
                                            itemSubCategoryName = Convert.ToString(ds.Tables[11].Rows[i]["sub_category_name"]),
                                            itemSubCategorycounts = Convert.ToString(ds.Tables[11].Rows[i]["sub_category_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[12].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[12].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        itemSubSubCategoryList.Add(new FilterSubSubCategoryList
                                        {
                                            filterName = Convert.ToString(ds.Tables[12].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[12].Rows[i]["name"]),
                                            itemSubSubCategoryId = Convert.ToString(ds.Tables[12].Rows[i]["sub_sub_category_id"]),
                                            itemSubSubCategoryName = Convert.ToString(ds.Tables[12].Rows[i]["sub_sub_category_name"]),
                                            itemSubSubCategorycounts = Convert.ToString(ds.Tables[12].Rows[i]["sub_sub_category_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[13].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[13].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        familyProductList.Add(new FilterFamilyProductList
                                        {
                                            filterName = Convert.ToString(ds.Tables[13].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[13].Rows[i]["name"]),
                                            familyproductName = Convert.ToString(ds.Tables[13].Rows[i]["familyproduct_name"]),
                                            familyproductId = Convert.ToString(ds.Tables[13].Rows[i]["familyproduct_id"]),
                                        });
                                    }
                                }

                                if (ds.Tables[14].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[14].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        excludeDisconList.Add(new FilterExcludeDiscontinueList
                                        {
                                            filterName = Convert.ToString(ds.Tables[14].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[14].Rows[i]["name"]),
                                            excludediscontinueName = Convert.ToString(ds.Tables[14].Rows[i]["excludediscontinue_name"]),
                                            excludediscontinueId = Convert.ToString(ds.Tables[14].Rows[i]["excludediscontinue_id"]),
                                        });
                                    }
                                }

                                if (ds.Tables[15].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[15].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        wearViewList.Add(new FilterWearViewList
                                        {
                                            filterName = Convert.ToString(ds.Tables[15].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[15].Rows[i]["name"]),
                                            viewName = Convert.ToString(ds.Tables[15].Rows[i]["view_name"]),
                                            viewId = Convert.ToString(ds.Tables[15].Rows[i]["view_id"]),
                                        });
                                    }
                                }

                                if (ds.Tables[16].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[16].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        tryViewList.Add(new FilterTryViewList
                                        {
                                            filterName = Convert.ToString(ds.Tables[16].Rows[i]["filter_name"]),
                                            name = Convert.ToString(ds.Tables[16].Rows[i]["name"]),
                                            viewName = Convert.ToString(ds.Tables[16].Rows[i]["view_name"]),
                                            viewId = Convert.ToString(ds.Tables[16].Rows[i]["view_id"]),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                if (hasAnyData)
                {
                    filterDetails.categoryList = categoryList;
                    filterDetails.dsgKt = dsgKtList;
                    filterDetails.dsgAmount = amountList;
                    filterDetails.dsgMetalWeight = dsgMetalwtList;
                    filterDetails.dsgDiamondWeight = dsgDiamondList;
                    filterDetails.productTags = productTagsList;
                    filterDetails.brand = brandList;
                    filterDetails.gender = genderList;
                    filterDetails.approxDelivery = approxDeliveryList;
                    filterDetails.stockFilter = stockList;
                    filterDetails.itemSubCategory = itemSubCategoryList;
                    filterDetails.itemSubSubCategory = itemSubSubCategoryList;
                    filterDetails.familyProduct = familyProductList;
                    filterDetails.excludeDiscontinue = excludeDisconList;
                    filterDetails.wearView = wearViewList;
                    filterDetails.tryView = tryViewList;
                }

                responseDetails = new ResponseDetails
                {
                    success = true,
                    status = "200",
                    message = hasAnyData ? "Successfully" : "No data found",
                    data = filterDetails,
                    data1 = null
                };

                return responseDetails;
            }
            catch (SqlException ex)
            {
                return new ResponseDetails
                {
                    success = false,
                    status = "400",
                    message = $"SQL error: {ex.Message}",
                    data = new List<PopularItemsFilterList>(),
                    data1 = null
                };
            }
        }

        //public async Task<ResponseDetails> PieceVerify(PieceVerifyRequest param)
        //{
        //    var responseDetails = new ResponseDetails();
        //    IList<PopularItemsResponse> details = new List<PopularItemsResponse>();
        //    try
        //    {
        //        using (SqlConnection dbConnection = new SqlConnection(_connection))
        //        {
        //            string cmdQuery = DBCommands.PieceVerify;
        //            await dbConnection.OpenAsync();

        //            using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
        //            {
        //                string? dataId = string.IsNullOrWhiteSpace(param.DataId) ? null : param.DataId;
        //                string? dataLoginType = string.IsNullOrWhiteSpace(param.DataLoginType) ? null : param.DataLoginType;
        //                string? type = string.IsNullOrWhiteSpace(param.Type) ? null : param.Type;
        //                string? search = string.IsNullOrWhiteSpace(param.Search) ? null : param.Search;

        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.CommandTimeout = 120;

        //                cmd.Parameters.AddWithValue("@type", type);
        //                cmd.Parameters.AddWithValue("@search", search);
        //                cmd.Parameters.AddWithValue("@data_id", dataId);
        //                cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);

        //                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                {
        //                    DataSet ds = new DataSet();
        //                    da.Fill(ds);

        //                    if (ds.Tables.Count > 0)
        //                    {
        //                        if (ds.Tables[0].Rows.Count > 0)
        //                        {
        //                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                            {
        //                                try
        //                                {
        //                                    var rowdetails = ds.Tables[0].Rows[i];


        //                                    details.Add(new PopularItemsResponse
        //                                    {

        //                                    });
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    Console.WriteLine(ex.Message);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        if (details.Any())
        //        {
        //            responseDetails.success = true;
        //            responseDetails.message = "Successfully";
        //            responseDetails.status = "200";
        //            responseDetails.current_page = currentPage?.ToString();
        //            responseDetails.last_page = lastPage?.ToString();
        //            responseDetails.total_items = totalItems?.ToString();
        //            responseDetails.data = details;
        //        }
        //        else
        //        {
        //            responseDetails.success = false;
        //            responseDetails.message = "No data found";
        //            responseDetails.status = "200";
        //            responseDetails.data = new List<PopularItemsResponse>();
        //        }
        //        return responseDetails;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        responseDetails.success = false;
        //        responseDetails.message = $"SQL error: {sqlEx.Message}";
        //        responseDetails.status = "400";
        //        responseDetails.data = new List<PopularItemsResponse>();
        //        return responseDetails;
        //    }
        //}

        public async Task<ResponseDetails> TopRecommandedItems(TopRecommandedItemsRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<TopRecommandedItemsResponse> topItemList = new List<TopRecommandedItemsResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.TopRecommandedItems;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? dataId = string.IsNullOrWhiteSpace(param.DataId) ? null : param.DataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.DataLoginType) ? null : param.DataLoginType;
                        string? masterCommonId = string.IsNullOrWhiteSpace(param.MasterCommonId) ? null : param.MasterCommonId;
                        string? page = string.IsNullOrWhiteSpace(param.Page) ? null : param.Page;
                        string? limit = string.IsNullOrWhiteSpace(param.DefaultLimitAppPage) ? null : param.DefaultLimitAppPage;
                        string? variant = string.IsNullOrWhiteSpace(param.Variant) ? null : param.Variant;
                        string? itemName = string.IsNullOrWhiteSpace(param.ItemName) ? null : param.ItemName;
                        string? subCategoryId = string.IsNullOrWhiteSpace(param.SubCategoryId) ? null : param.SubCategoryId;
                        string? dsgSize = string.IsNullOrWhiteSpace(param.DsgSize) ? null : param.DsgSize;
                        string? dsgKt = string.IsNullOrWhiteSpace(param.DsgKt) ? null : param.DsgKt;
                        string? dsgColor = string.IsNullOrWhiteSpace(param.DsgColor) ? null : param.DsgColor;
                        string? amount = string.IsNullOrWhiteSpace(param.Amount) ? null : param.Amount;
                        string? metalWt = string.IsNullOrWhiteSpace(param.MetalWt) ? null : param.MetalWt;
                        string? diawt = string.IsNullOrWhiteSpace(param.DiaWt) ? null : param.DiaWt;
                        string? itemId = string.IsNullOrWhiteSpace(param.ItemId) ? null : param.ItemId;
                        string? stockAv = string.IsNullOrWhiteSpace(param.StockAv) ? null : param.StockAv;
                        string? familyAv = string.IsNullOrWhiteSpace(param.FamilyAv) ? null : param.FamilyAv;
                        string? regularAv = string.IsNullOrWhiteSpace(param.RegularAv) ? null : param.RegularAv;
                        string? wearIt = string.IsNullOrWhiteSpace(param.WearIt) ? null : param.WearIt;
                        string? tryOn = string.IsNullOrWhiteSpace(param.TryOn) ? null : param.TryOn;
                        string? gender = string.IsNullOrWhiteSpace(param.GenderId) ? null : param.GenderId;
                        string? itemTag = string.IsNullOrWhiteSpace(param.ItemTag) ? null : param.ItemTag;
                        string? brand = string.IsNullOrWhiteSpace(param.Brand) ? null : param.Brand;
                        string? deliveryDays = string.IsNullOrWhiteSpace(param.DeliveryDays) ? null : param.DeliveryDays;
                        string? itemSubCtgIDs = string.IsNullOrWhiteSpace(param.ItemSubCtgIDs) ? null : param.ItemSubCtgIDs;
                        string? itemSubSubCtgIDs = string.IsNullOrWhiteSpace(param.ItemSubSubCtgIDs) ? null : param.ItemSubSubCtgIDs;
                        string? salesLocation = string.IsNullOrWhiteSpace(param.SalesLocation) ? null : param.SalesLocation;
                        string? designTimeLine = string.IsNullOrWhiteSpace(param.DesignTimeline) ? null : param.DesignTimeline;
                        string? itemSubCategoryID = string.IsNullOrWhiteSpace(param.ItemSubCategoryId) ? null : param.ItemSubCategoryId;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);
                        cmd.Parameters.AddWithValue("@master_common_id", masterCommonId);
                        cmd.Parameters.AddWithValue("@variant", variant);
                        cmd.Parameters.AddWithValue("@item_name", itemName);
                        cmd.Parameters.AddWithValue("@sub_category_id", subCategoryId);
                        cmd.Parameters.AddWithValue("@dsg_size", dsgSize);
                        cmd.Parameters.AddWithValue("@dsg_kt", dsgKt);
                        cmd.Parameters.AddWithValue("@dsg_color", dsgColor);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@metalwt", metalWt);
                        cmd.Parameters.AddWithValue("@diawt", diawt);
                        cmd.Parameters.AddWithValue("@Item_ID", itemId);
                        cmd.Parameters.AddWithValue("@Stock_Av", stockAv);
                        cmd.Parameters.AddWithValue("@Family_Av", familyAv);
                        cmd.Parameters.AddWithValue("@Regular_Av", regularAv);
                        cmd.Parameters.AddWithValue("@wearit", wearIt);
                        cmd.Parameters.AddWithValue("@tryon", tryOn);
                        cmd.Parameters.AddWithValue("@gender_id", gender);
                        cmd.Parameters.AddWithValue("@item_tag", itemTag);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@delivery_days", deliveryDays);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", itemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", itemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@sales_location", salesLocation);
                        cmd.Parameters.AddWithValue("@design_timeline", designTimeLine);
                        cmd.Parameters.AddWithValue("@item_sub_category_id", itemSubCategoryID);
                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@default_limit_app_page", limit);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string ItemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string CategoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string ItemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string ItemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string ItemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string ItemGenderCommonID = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string ItemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string PlainGoldStatus = rowdetails["plaingold_status"] != DBNull.Value ? Convert.ToString(rowdetails["plaingold_status"]) : string.Empty;
                                            string SubCategoryId = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string PriceFlag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string ItemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string MaxQtySold = rowdetails["max_qty_sold"] != DBNull.Value ? Convert.ToString(rowdetails["max_qty_sold"]) : string.Empty;
                                            string ImagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string ProductTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string MostOrder = rowdetails["mostOrder"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder"]) : string.Empty;
                                            string IsInFranchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            List<Dictionary<string, object>> productTagsDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(ProductTags))
                                            {
                                                try
                                                {
                                                    productTagsDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ProductTags);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing ProductTags: " + ex.Message);
                                                }
                                            }

                                            topItemList.Add(new TopRecommandedItemsResponse
                                            {
                                                ItemId = ItemId,
                                                CategoryId = CategoryId,
                                                ItemDescription = ItemDescription,
                                                ItemCode = ItemCode,
                                                ItemName = ItemName,
                                                ItemGenderCommonId = ItemGenderCommonID,
                                                ItemNosePinScrewSts = ItemNosePinScrewSts,
                                                PlaingoldStatus = PlainGoldStatus,
                                                SubCategoryId = SubCategoryId,
                                                PriceFlag = PriceFlag,
                                                ItemMrp = ItemMrp,
                                                MaxQtySold = MaxQtySold,
                                                ImagePath = ImagePath,
                                                MostOrder = MostOrder,
                                                ProductTags = productTagsDynamic,
                                                IsInFranchiseStore = IsInFranchiseStore,

                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (topItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = topItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<TopRecommandedItemsResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<TopRecommandedItemsResponse>();
                return responseDetails;
            }
        }
    }
}