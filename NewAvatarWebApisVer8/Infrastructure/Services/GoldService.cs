using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using Newtonsoft.Json;
using System.Data;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class GoldService : IGoldService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        public async Task<GoldView_Static> ViewPlainGoldDetails(PayloadGoldDetails Details)
        {
            try
            {
                GoldView_Static GoldItemdetailsStatic = new GoldView_Static();

                if (Details != null)
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.VIEWGOLDITEMDETAILS;
                        dbConnection.Open();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {

                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CategoryId", Details.category_id);
                            cmd.Parameters.AddWithValue("@data_id", Details.data_id);
                            cmd.Parameters.AddWithValue("@data_login_type", Details.data_login_type);
                            cmd.Parameters.AddWithValue("@ItemId", Details.item_id);
                            cmd.Parameters.AddWithValue("@master_common_id", Details.master_common_id);

                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = cmd;
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                IList<SizeListGold> sl = new List<SizeListGold>();
                                IList<ColorListGold> c = new List<ColorListGold>();
                                IList<ItemOrderInstructionListGold> ol = new List<ItemOrderInstructionListGold>();
                                IList<ItemOrderCustomInstructionListGold> olc = new List<ItemOrderCustomInstructionListGold>();
                                PlainGoldDetails GolditemDetails = new PlainGoldDetails();
                                IList<ProductTags> pd = new List<ProductTags>();
                                IList<Item_Image_Color_Gold> itemImageColor = new List<Item_Image_Color_Gold>();
                                IList<ApproxDays> ad = new List<ApproxDays>();

                                IList<Item_Images_Gold> ItemImages = new List<Item_Images_Gold>();

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
                                {
                                    if (ds.Tables[0] != null)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            sl.Add(new SizeListGold
                                            {
                                                product_size_mst_id = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_id"]),
                                                product_size_mst_code = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_code"]),
                                                product_size_mst_name = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_name"]),
                                                product_size_mst_desc = Convert.ToString(ds.Tables[0].Rows[i]["product_size_mst_desc"]),
                                                //SortBy = Convert.ToDecimal(ds.Tables[0].Rows[i]["SortBy"]),
                                            });
                                        }

                                    }

                                    if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
                                    {
                                        if (ds.Tables[1] != null)
                                        {
                                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                            {
                                                c.Add(new ColorListGold
                                                {
                                                    product_color_mst_id = Convert.ToString(ds.Tables[1].Rows[i]["product_color_mst_id"]),
                                                    product_color_mst_code = Convert.ToString(ds.Tables[1].Rows[i]["product_color_mst_code"]),
                                                    product_color_mst_name = Convert.ToString(ds.Tables[1].Rows[i]["product_color_mst_name"]),
                                                    IsDefault = Convert.ToString(ds.Tables[1].Rows[i]["IsDefault"]),

                                                });
                                            }
                                        }

                                    }

                                    if (ds.Tables[2] != null)
                                    {
                                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                        {
                                            ol.Add(new ItemOrderInstructionListGold
                                            {
                                                item_instruction_mst_id = Convert.ToString(ds.Tables[2].Rows[i]["item_instruction_mst_id"]),
                                                item_instruction_mst_code = Convert.ToString(ds.Tables[2].Rows[i]["item_instruction_mst_code"]),
                                                item_instruction_mst_name = Convert.ToString(ds.Tables[2].Rows[i]["item_instruction_mst_name"]),
                                            });
                                        }
                                    }

                                    if (ds.Tables[3] != null)
                                    {
                                        for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                                        {
                                            olc.Add(new ItemOrderCustomInstructionListGold
                                            {
                                                item_instruction_mst_id = Convert.ToString(ds.Tables[3].Rows[i]["item_instruction_mst_id"]),
                                                item_instruction_mst_code = Convert.ToString(ds.Tables[3].Rows[i]["item_instruction_mst_code"]),
                                                item_instruction_mst_name = Convert.ToString(ds.Tables[3].Rows[i]["item_instruction_mst_name"]),

                                            });
                                        }
                                    }

                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 6)
                                {
                                    if (ds.Tables[6] != null)
                                    {
                                        for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                                        {
                                            pd.Add(new ProductTags
                                            {
                                                tag_name = Convert.ToString(ds.Tables[6].Rows[i]["tag_name"]),
                                                tag_color = Convert.ToString(ds.Tables[6].Rows[i]["tag_color"]),
                                                StruItemCommonID = Convert.ToString(ds.Tables[6].Rows[i]["StruItemCommonID"]),
                                            });
                                        }
                                    }

                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 8)
                                {
                                    for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                                    {
                                        ad.Add(new ApproxDays
                                        {
                                            manufactureStartDate = Convert.ToString(ds.Tables[8].Rows[i]["manufactureStartDate"]),
                                            manufactureEndDate = Convert.ToString(ds.Tables[8].Rows[i]["manufactureEndDate"]),
                                            deliveryStartDate = Convert.ToString(ds.Tables[8].Rows[i]["deliveryStartDate"]),
                                            deliveryEndDate = Convert.ToString(ds.Tables[8].Rows[i]["deliveryEndDate"]),
                                            deliveryInDays = Convert.ToString(ds.Tables[8].Rows[i]["deliveryInDays"]),
                                        });
                                    }
                                }
                                if (ds != null && ds.Tables != null && ds.Tables.Count > 7 && ds.Tables[7] != null)
                                {
                                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                                    {
                                        itemImageColor.Add(new Item_Image_Color_Gold
                                        {
                                            color_id = Convert.ToString(ds.Tables[7].Rows[i]["color_id"]),
                                            color_image_details = ItemImages,

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 9)
                                {
                                    for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                                    {
                                        ItemImages.Add(new Item_Images_Gold
                                        {
                                            image_view_name = Convert.ToString(ds.Tables[9].Rows[i]["image_view_name"]),
                                            image_path = Convert.ToString(ds.Tables[9].Rows[i]["image_path"]),
                                            filetype = Convert.ToString(ds.Tables[9].Rows[i]["filetype"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 5)
                                {
                                    GolditemDetails.item_id = Convert.ToString(ds.Tables[5].Rows[0]["item_id"]);
                                    GolditemDetails.category = Convert.ToString(ds.Tables[5].Rows[0]["category"]);
                                    GolditemDetails.item_soliter = Convert.ToString(ds.Tables[5].Rows[0]["item_soliter"]);
                                    GolditemDetails.plaingold_status = Convert.ToString(ds.Tables[5].Rows[0]["plaingold_status"]);
                                    GolditemDetails.item_name = Convert.ToString(ds.Tables[5].Rows[0]["item_name"]);
                                    GolditemDetails.item_sku = Convert.ToString(ds.Tables[5].Rows[0]["item_sku"]);
                                    GolditemDetails.item_description = Convert.ToString(ds.Tables[5].Rows[0]["item_description"]);
                                    GolditemDetails.item_discount = Convert.ToString(ds.Tables[5].Rows[0]["item_discount"]);
                                    GolditemDetails.item_price = Convert.ToString(ds.Tables[5].Rows[0]["item_price"]);
                                    GolditemDetails.retail_price = Convert.ToString(ds.Tables[5].Rows[0]["retail_price"]);
                                    GolditemDetails.dist_price = Convert.ToString(ds.Tables[5].Rows[0]["dist_price"]);
                                    //GolditemDetails.uom = Convert.ToString(ds.Tables[5].Rows[0]["uom"]);
                                    GolditemDetails.star = Convert.ToString(ds.Tables[5].Rows[0]["star"]);
                                    GolditemDetails.cart_img = Convert.ToString(ds.Tables[5].Rows[0]["cart_img"]);
                                    GolditemDetails.img_cart_title = Convert.ToString(ds.Tables[5].Rows[0]["img_cart_title"]);
                                    GolditemDetails.watch_img = Convert.ToString(ds.Tables[5].Rows[0]["watch_img"]);
                                    GolditemDetails.img_watch_title = Convert.ToString(ds.Tables[5].Rows[0]["img_watch_title"]);
                                    GolditemDetails.wearit_count = Convert.ToString(ds.Tables[5].Rows[0]["wearit_count"]);
                                    GolditemDetails.wearit_status = Convert.ToString(ds.Tables[5].Rows[0]["wearit_status"]);
                                    GolditemDetails.wearit_img = Convert.ToString(ds.Tables[5].Rows[0]["wearit_img"]);
                                    GolditemDetails.wearit_none_img = Convert.ToString(ds.Tables[5].Rows[0]["wearit_none_img"]);
                                    GolditemDetails.wearit_color = Convert.ToString(ds.Tables[5].Rows[0]["wearit_color"]);
                                    GolditemDetails.wearit_text = Convert.ToString(ds.Tables[5].Rows[0]["wearit_text"]);
                                    GolditemDetails.img_wearit_title = Convert.ToString(ds.Tables[5].Rows[0]["img_wearit_title"]);
                                    GolditemDetails.tryon_count = Convert.ToString(ds.Tables[5].Rows[0]["tryon_count"]);
                                    GolditemDetails.tryon_status = Convert.ToString(ds.Tables[5].Rows[0]["tryon_status"]);
                                    GolditemDetails.tryon_img = Convert.ToString(ds.Tables[5].Rows[0]["tryon_img"]);
                                    GolditemDetails.tryon_none_img = Convert.ToString(ds.Tables[5].Rows[0]["tryon_none_img"]);
                                    GolditemDetails.tryon_text = Convert.ToString(ds.Tables[5].Rows[0]["tryon_text"]);
                                    GolditemDetails.tryon_title = Convert.ToString(ds.Tables[5].Rows[0]["tryon_title"]);
                                    GolditemDetails.tryon_android_path = Convert.ToString(ds.Tables[5].Rows[0]["tryon_android_path"]);
                                    GolditemDetails.tryon_ios_path = Convert.ToString(ds.Tables[5].Rows[0]["tryon_ios_path"]);
                                    GolditemDetails.wish_count = Convert.ToString(ds.Tables[5].Rows[0]["wish_count"]);
                                    GolditemDetails.wish_default_img = Convert.ToString(ds.Tables[5].Rows[0]["wish_default_img"]);
                                    GolditemDetails.wish_fill_img = Convert.ToString(ds.Tables[5].Rows[0]["wish_fill_img"]);
                                    GolditemDetails.img_wish_title = Convert.ToString(ds.Tables[5].Rows[0]["img_wish_title"]);
                                    GolditemDetails.item_review = Convert.ToString(ds.Tables[5].Rows[0]["item_review"]);
                                    GolditemDetails.item_size = Convert.ToString(ds.Tables[5].Rows[0]["item_size"]);
                                    GolditemDetails.item_color = Convert.ToString(ds.Tables[5].Rows[0]["item_color"]);
                                    GolditemDetails.item_metal = Convert.ToString(ds.Tables[5].Rows[0]["item_metal"]);
                                    GolditemDetails.item_stone = Convert.ToString(ds.Tables[5].Rows[0]["item_stone"]);
                                    GolditemDetails.item_stone_wt = Convert.ToString(ds.Tables[5].Rows[0]["item_stone_wt"]);
                                    GolditemDetails.item_stone_qty = Convert.ToString(ds.Tables[5].Rows[0]["item_stone_qty"]);
                                    //GolditemDetails.star_color = Convert.ToString(ds.Tables[5].Rows[0]["star_color"]);
                                    GolditemDetails.ItemMetalCommonID = Convert.ToString(ds.Tables[5].Rows[0]["ItemMetalCommonID"]);
                                    //GolditemDetails.price_text = Convert.ToString(ds.Tables[5].Rows[0]["price_text"]);
                                    GolditemDetails.cart_price = Convert.ToString(ds.Tables[5].Rows[0]["cart_price"]);
                                    GolditemDetails.item_color_id = Convert.ToString(ds.Tables[5].Rows[0]["item_color_id"]);
                                    GolditemDetails.item_details = Convert.ToString(ds.Tables[5].Rows[0]["item_details"]);
                                    GolditemDetails.item_text = Convert.ToString(ds.Tables[5].Rows[0]["item_text"]);
                                    GolditemDetails.more_item_details = Convert.ToString(ds.Tables[5].Rows[0]["more_item_details"]);
                                    GolditemDetails.item_stock = Convert.ToString(ds.Tables[5].Rows[0]["item_stock"]);
                                    GolditemDetails.cart_item_qty = Convert.ToString(ds.Tables[5].Rows[0]["cart_item_qty"]);
                                    GolditemDetails.rupy_symbol = Convert.ToString(ds.Tables[5].Rows[0]["rupy_symbol"]);
                                    GolditemDetails.variantCount = Convert.ToString(ds.Tables[5].Rows[0]["variantCount"]);
                                    GolditemDetails.cart_id = Convert.ToString(ds.Tables[5].Rows[0]["cart_id"]);
                                    GolditemDetails.ItemGenderCommonID = Convert.ToString(ds.Tables[5].Rows[0]["ItemGenderCommonID"]);
                                    //GolditemDetails.cart_auto_id = Convert.ToString(ds.Tables[5].Rows[0]["cart_auto_id"]);
                                    GolditemDetails.item_stock_qty = Convert.ToString(ds.Tables[5].Rows[0]["item_stock_qty"]);
                                    GolditemDetails.item_stock_colorsize_qty = Convert.ToString(ds.Tables[5].Rows[0]["item_stock_colorsize_qty"]);
                                    GolditemDetails.category_id = Convert.ToString(ds.Tables[5].Rows[0]["category_id"]);
                                    GolditemDetails.ItemNosePinScrewSts = Convert.ToString(ds.Tables[5].Rows[0]["ItemNosePinScrewSts"]);
                                    GolditemDetails.ItemAproxDayCommonID = Convert.ToString(ds.Tables[5].Rows[0]["ItemAproxDayCommonID"]);
                                    GolditemDetails.ItemBrandCommonID = Convert.ToString(ds.Tables[5].Rows[0]["ItemBrandCommonID"]);
                                    //GolditemDetails.item_illumine = Convert.ToString(ds.Tables[5].Rows[0]["item_illumine"]);
                                    GolditemDetails.productTags = pd;
                                    GolditemDetails.selectedColor = Convert.ToString(ds.Tables[5].Rows[0]["selectedColor"]);
                                    GolditemDetails.selectedSize = Convert.ToString(ds.Tables[5].Rows[0]["selectedSize"]);
                                    // GolditemDetails.selectedColor1 = Convert.ToString(ds.Tables[5].Rows[0]["selectedColor1"]);
                                    //GolditemDetails.selectedSize1 = Convert.ToString(ds.Tables[5].Rows[0]["selectedSize1"]);
                                    GolditemDetails.field_name = Convert.ToString(ds.Tables[5].Rows[0]["field_name"]);
                                    GolditemDetails.color_name = Convert.ToString(ds.Tables[5].Rows[0]["color_name"]);
                                    GolditemDetails.default_color_name = Convert.ToString(ds.Tables[5].Rows[0]["default_color_name"]);
                                    GolditemDetails.default_color_code = Convert.ToString(ds.Tables[5].Rows[0]["default_color_code"]);
                                    GolditemDetails.default_size_name = Convert.ToString(ds.Tables[5].Rows[0]["default_size_name"]);
                                    GolditemDetails.sizeList = sl;
                                    GolditemDetails.colorList = c;
                                    GolditemDetails.itemOrderInstructionList = ol;
                                    GolditemDetails.itemOrderCustomInstructionList = olc;
                                    GolditemDetails.item_images_color = itemImageColor;

                                }


                                if (ds != null && ds.Tables != null && ds.Tables.Count > 5)
                                {
                                    data1 myData = new data1
                                    {
                                        item_detail = GolditemDetails,
                                        color_image_details = ItemImages

                                    };

                                    for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                                    {
                                        GoldItemdetailsStatic.success = Convert.ToString(ds.Tables[4].Rows[i]["SUCCESS"]);
                                        GoldItemdetailsStatic.message = Convert.ToString(ds.Tables[4].Rows[i]["MESSAGE"]);
                                        GoldItemdetailsStatic.data = myData;
                                    }
                                }
                            }
                        }
                    }
                }

                if (GoldItemdetailsStatic != null || GoldItemdetailsStatic.success == "TRUE")
                {
                    return GoldItemdetailsStatic;
                }
                else
                {
                    return new GoldView_Static { message = "No data available." };
                }
            }
            catch (Exception sqlEx)
            {
                return new GoldView_Static { message = sqlEx.Message };
            }
        }

        public async Task<ResponseDetails> PlainGoldItemFilterFranSIS(PlainGoldFilter request, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            bool hasAnyData = false;
            PlaingoldItemFilterList filterDetails = new PlaingoldItemFilterList();
            IList<FilterCategoryList> categoryList = new List<FilterCategoryList>();
            IList<FilterDsgKtList> dsgKtList = new List<FilterDsgKtList>();
            IList<FilterProductTagList> productTagList = new List<FilterProductTagList>();
            IList<FilterBrandList> brandList = new List<FilterBrandList>();
            IList<FilterGenderList> genderList = new List<FilterGenderList>();
            IList<FilterApproxDeliveryList> approxDeliveryList = new List<FilterApproxDeliveryList>();
            IList<FilterPriceList> priceList = new List<FilterPriceList>();
            IList<FilterDsgWeightList> dsgWeightList = new List<FilterDsgWeightList>();
            IList<FilterStockList> stockList = new List<FilterStockList>();
            IList<FilterFamilyProductList> familyProductList = new List<FilterFamilyProductList>();
            IList<FilterExcludeDiscontinueList> excludeDisconList = new List<FilterExcludeDiscontinueList>();
            IList<FilterWearItList> wearItList = new List<FilterWearItList>();
            IList<FilterTryOnList> tryOnList = new List<FilterTryOnList>();
            IList<FilterImageAvailList> imageAvailableList = new List<FilterImageAvailList>();
            IList<FilterSubCategoryList> subCategoryList = new List<FilterSubCategoryList>();
            IList<FilterSubSubCategoryList> subSubCategoryList = new List<FilterSubSubCategoryList>();
            IList<FilterDsgMetalWtList> metalWtList = new List<FilterDsgMetalWtList>();
            IList<FilterDsgDiamondWtList> diamondWtList = new List<FilterDsgDiamondWtList>();
            IList<FilterBestSellerList> bestSellerList = new List<FilterBestSellerList>();
            IList<FilterLatestDesignList> latestDesignList = new List<FilterLatestDesignList>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PlainGoldFilterFranSIS;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? category_id = string.IsNullOrWhiteSpace(request.category_id) ? null : request.category_id;
                        string? data_id = string.IsNullOrWhiteSpace(request.data_id) ? null : request.data_id;
                        string? button_code = string.IsNullOrWhiteSpace(request.button_code) ? null : request.button_code;
                        string? master_common_id = string.IsNullOrWhiteSpace(request.master_common_id) ? null : request.master_common_id;
                        string? data_login_type = string.IsNullOrWhiteSpace(request.data_login_type) ? null : request.data_login_type;
                        string? orgtype = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@button_code", button_code);
                        cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
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
                                        categoryList.Add(new FilterCategoryList
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
                                        dsgKtList.Add(new FilterDsgKtList
                                        {
                                            kt = Convert.ToString(ds.Tables[1].Rows[i]["kt"]),
                                            kt_count = Convert.ToString(ds.Tables[1].Rows[i]["kt_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        productTagList.Add(new FilterProductTagList
                                        {
                                            tag_name = Convert.ToString(ds.Tables[2].Rows[i]["tag_name"]),
                                            tag_count = Convert.ToString(ds.Tables[2].Rows[i]["tag_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[3].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        brandList.Add(new FilterBrandList
                                        {
                                            ItemBrandCommonID = Convert.ToString(ds.Tables[3].Rows[i]["ItemBrandCommonID"]),
                                            brand_id = Convert.ToString(ds.Tables[3].Rows[i]["brand_id"]),
                                            brand_name = Convert.ToString(ds.Tables[3].Rows[i]["brand_name"]),
                                            brand_count = Convert.ToString(ds.Tables[3].Rows[i]["brand_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[4].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        genderList.Add(new FilterGenderList
                                        {
                                            gender_id = Convert.ToString(ds.Tables[4].Rows[i]["gender_id"]),
                                            gender_name = Convert.ToString(ds.Tables[4].Rows[i]["gender_name"]),
                                            gender_count = Convert.ToString(ds.Tables[4].Rows[i]["gender_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[5].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        approxDeliveryList.Add(new FilterApproxDeliveryList
                                        {
                                            ItemAproxDay = Convert.ToString(ds.Tables[5].Rows[i]["ItemAproxDay"]),
                                            ItemAproxDay_count = Convert.ToString(ds.Tables[5].Rows[i]["ItemAproxDay_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[6].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        priceList.Add(new FilterPriceList
                                        {
                                            minprice = Convert.ToString(ds.Tables[6].Rows[i]["minprice"]),
                                            maxprice = Convert.ToString(ds.Tables[6].Rows[i]["maxprice"]),
                                        });
                                    }
                                }

                                if (ds.Tables[7].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        dsgWeightList.Add(new FilterDsgWeightList
                                        {
                                            minWt = Convert.ToString(ds.Tables[7].Rows[i]["minWt"]),
                                            maxWt = Convert.ToString(ds.Tables[7].Rows[i]["maxWt"]),
                                        });
                                    }
                                }

                                if (ds.Tables[8].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        stockList.Add(new FilterStockList
                                        {
                                            stock_name = Convert.ToString(ds.Tables[8].Rows[i]["stock_name"]),
                                            stock_id = Convert.ToString(ds.Tables[8].Rows[i]["stock_id"]),
                                        });
                                    }
                                }

                                if (ds.Tables[9].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        familyProductList.Add(new FilterFamilyProductList
                                        {
                                            familyproduct_name = Convert.ToString(ds.Tables[9].Rows[i]["familyproduct_name"]),
                                            familyproduct_id = Convert.ToString(ds.Tables[9].Rows[i]["familyproduct_id"]),
                                        });
                                    }
                                }

                                if (ds.Tables[10].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        excludeDisconList.Add(new FilterExcludeDiscontinueList
                                        {
                                            excludediscontinue_name = Convert.ToString(ds.Tables[10].Rows[i]["excludediscontinue_name"]),
                                            excludediscontinue_id = Convert.ToString(ds.Tables[10].Rows[i]["excludediscontinue_id"]),
                                        });
                                    }
                                }

                                if (ds.Tables[11].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[11].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        wearItList.Add(new FilterWearItList
                                        {
                                            view_name = Convert.ToString(ds.Tables[11].Rows[i]["view_name"]),
                                            view_id = Convert.ToString(ds.Tables[11].Rows[i]["view_id"]),
                                        });
                                    }
                                }

                                if (ds.Tables[12].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[12].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        tryOnList.Add(new FilterTryOnList
                                        {
                                            view_name = Convert.ToString(ds.Tables[12].Rows[i]["view_name"]),
                                            view_id = Convert.ToString(ds.Tables[12].Rows[i]["view_id"]),
                                        });
                                    }
                                }

                                if (ds.Tables[13].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[13].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        imageAvailableList.Add(new FilterImageAvailList
                                        {
                                            imageavail_name = Convert.ToString(ds.Tables[13].Rows[i]["imageavail_name"]),
                                            imageavail_id = Convert.ToString(ds.Tables[13].Rows[i]["imageavail_id"]),
                                        });
                                    }
                                }

                                if (ds.Tables[14].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[14].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        subCategoryList.Add(new FilterSubCategoryList
                                        {
                                            sub_category_id = Convert.ToString(ds.Tables[14].Rows[i]["sub_category_id"]),
                                            sub_category_name = Convert.ToString(ds.Tables[14].Rows[i]["sub_category_name"]),
                                            sub_category_count = Convert.ToString(ds.Tables[14].Rows[i]["sub_category_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[15].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[15].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        subSubCategoryList.Add(new FilterSubSubCategoryList
                                        {
                                            data = Convert.ToString(ds.Tables[15].Rows[i]["data"]),
                                        });
                                    }
                                }

                                if (ds.Tables[16].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[16].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        metalWtList.Add(new FilterDsgMetalWtList
                                        {
                                            metalwt = Convert.ToString(ds.Tables[16].Rows[i]["data"]),
                                        });
                                    }
                                }

                                if (ds.Tables[17].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[17].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        diamondWtList.Add(new FilterDsgDiamondWtList
                                        {
                                            diamondwt = Convert.ToString(ds.Tables[17].Rows[i]["data"]),
                                        });
                                    }
                                }

                                if (ds.Tables[18].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[18].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        bestSellerList.Add(new FilterBestSellerList
                                        {
                                            Name = Convert.ToString(ds.Tables[18].Rows[i]["Name"]),
                                            Value = Convert.ToString(ds.Tables[18].Rows[i]["Value"]),
                                        });
                                    }
                                }
                                if (ds.Tables[19].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[19].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        latestDesignList.Add(new FilterLatestDesignList
                                        {
                                            Name = Convert.ToString(ds.Tables[19].Rows[i]["Name"]),
                                            Value = Convert.ToString(ds.Tables[19].Rows[i]["Value"]),
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
                    filterDetails.dsgWeight = dsgWeightList;
                    filterDetails.hoStock = stockList;
                    filterDetails.familyProduct = familyProductList;
                    filterDetails.excludeDiscontinue = excludeDisconList;
                    filterDetails.wearIt = wearItList;
                    filterDetails.tryOn = tryOnList;
                    filterDetails.imageAvailable = imageAvailableList;
                    filterDetails.subCategory = subCategoryList;
                    filterDetails.subSubCategory = subSubCategoryList;
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
                    data = new List<PlaingoldItemFilterList>(),
                    data1 = null
                };
            }
        }

        public async Task<ResponseDetails> PlainGoldItemDetailsFran(PlainGoldItemDetailsRequest request, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            bool hasAnyData = false;
            PlainGoldItemDetailList plainGoldDetailList = new PlainGoldItemDetailList();
            IList<PlainGoldDetailList> plainGoldDetails = new List<PlainGoldDetailList>();
            IList<PlainGoldImageList> plainGoldImages = new List<PlainGoldImageList>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PlainGoldItemDetailsFran;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? data_id = string.IsNullOrWhiteSpace(request.dataId) ? null : request.dataId;
                        string? data_login_type = string.IsNullOrWhiteSpace(request.dataLoginType) ? null : request.dataLoginType;
                        string? category_id = string.IsNullOrWhiteSpace(request.categoryId) ? null : request.categoryId;
                        string? item_id = string.IsNullOrWhiteSpace(request.itemId) ? null : request.itemId;
                        string? variant = string.IsNullOrWhiteSpace(request.variant) ? null : request.variant;
                        string? item_name = string.IsNullOrWhiteSpace(request.itemName) ? null : request.itemName;
                        string? master_common_id = string.IsNullOrWhiteSpace(request.masterCommonId) ? null : request.masterCommonId;
                        string? orgtype = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@item_id", item_id);
                        cmd.Parameters.AddWithValue("@variant", variant);
                        cmd.Parameters.AddWithValue("@item_name", item_name);
                        cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
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
                                        // Pull the raw string values that may contain JSON
                                        string SizeList = Convert.ToString(ds.Tables[0].Rows[i]["sizeList"]);
                                        string ColorList = Convert.ToString(ds.Tables[0].Rows[i]["colorList"]);
                                        string ItemsColorSizeList = Convert.ToString(ds.Tables[0].Rows[i]["itemsColorSizeList"]);
                                        string ItemOrderInstructionList = Convert.ToString(ds.Tables[0].Rows[i]["itemOrderInstructionList"]);
                                        string ItemOrderCustomInstructionList = Convert.ToString(ds.Tables[0].Rows[i]["itemOrderCustomInstructionList"]);
                                        string ItemImagesColor = Convert.ToString(ds.Tables[0].Rows[i]["item_images_color"]);
                                        string ApproxDays = Convert.ToString(ds.Tables[0].Rows[i]["approxDays"]); 

                                        // JSON DESERIALIZATION
                                        List<Dictionary<string, object>> sizeListDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> colorListDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> itemsColorSizeListDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> itemOrderInstructionListDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> itemOrderCustomInstructionListDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> itemImagesColorDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> approxDaysDynamic = new List<Dictionary<string, object>>();

                                        if (!string.IsNullOrEmpty(SizeList))
                                        {
                                            try { sizeListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(SizeList); }
                                            catch (Exception ex) { Console.WriteLine("Error deserializing SizeList: " + ex.Message); }
                                        }
                                        if (!string.IsNullOrEmpty(ColorList))
                                        {
                                            try { colorListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ColorList); }
                                            catch (Exception ex) { Console.WriteLine("Error deserializing ColorList: " + ex.Message); }
                                        }
                                        if (!string.IsNullOrEmpty(ItemsColorSizeList))
                                        {
                                            try { itemsColorSizeListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemsColorSizeList); }
                                            catch (Exception ex) { Console.WriteLine("Error deserializing ItemsColorSizeList: " + ex.Message); }
                                        }
                                        if (!string.IsNullOrEmpty(ItemOrderInstructionList))
                                        {
                                            try { itemOrderInstructionListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemOrderInstructionList); }
                                            catch (Exception ex) { Console.WriteLine("Error deserializing ItemOrderInstructionList: " + ex.Message); }
                                        }
                                        if (!string.IsNullOrEmpty(ItemOrderCustomInstructionList))
                                        {
                                            try { itemOrderCustomInstructionListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemOrderCustomInstructionList); }
                                            catch (Exception ex) { Console.WriteLine("Error deserializing ItemOrderCustomInstructionList: " + ex.Message); }
                                        }
                                        if (!string.IsNullOrEmpty(ItemImagesColor))
                                        {
                                            try { itemImagesColorDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ItemImagesColor); }
                                            catch (Exception ex) { Console.WriteLine("Error deserializing ItemImagesColor: " + ex.Message); }
                                        }
                                        if (!string.IsNullOrEmpty(ApproxDays))
                                        {
                                            try { approxDaysDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ApproxDays); }
                                            catch (Exception ex) { Console.WriteLine("Error deserializing ApproxDays: " + ex.Message); }
                                        }

                                        plainGoldDetails.Add(new PlainGoldDetailList
                                        {
                                            ItemId = Convert.ToString(ds.Tables[0].Rows[i]["item_id"]),
                                            ItemDisLabourPer = Convert.ToString(ds.Tables[0].Rows[i]["ItemDisLabourPer"]),
                                            ItemCtgCommonID = Convert.ToString(ds.Tables[0].Rows[i]["ItemCtgCommonID"]),
                                            ItemCd = Convert.ToString(ds.Tables[0].Rows[i]["ItemCd"]),
                                            LabourPer = Convert.ToString(ds.Tables[0].Rows[i]["labour_per"]),
                                            ItemSoliter = Convert.ToString(ds.Tables[0].Rows[i]["item_soliter"]),
                                            ItemAproxDay = Convert.ToString(ds.Tables[0].Rows[i]["ItemAproxDay"]),
                                            ItemDAproxDay = Convert.ToString(ds.Tables[0].Rows[i]["ItemDAproxDay"]),
                                            PlainGoldStatus = Convert.ToString(ds.Tables[0].Rows[i]["plaingold_status"]),
                                            ItemName = Convert.ToString(ds.Tables[0].Rows[i]["item_name"]),
                                            ItemSku = Convert.ToString(ds.Tables[0].Rows[i]["item_sku"]),
                                            ItemDescription = Convert.ToString(ds.Tables[0].Rows[i]["item_description"]),
                                            DsgKt = Convert.ToString(ds.Tables[0].Rows[i]["dsg_kt"]),
                                            ItemDiscount = Convert.ToString(ds.Tables[0].Rows[i]["item_discount"]),
                                            ItemPrice = Convert.ToString(ds.Tables[0].Rows[i]["item_price"]),
                                            RetailPrice = Convert.ToString(ds.Tables[0].Rows[i]["retail_price"]),
                                            DistPrice = Convert.ToString(ds.Tables[0].Rows[i]["dist_price"]),
                                            Uom = Convert.ToString(ds.Tables[0].Rows[i]["uom"]),
                                            ItemBrandCommonID = Convert.ToString(ds.Tables[0].Rows[i]["ItemBrandCommonID"]),
                                            Star = Convert.ToString(ds.Tables[0].Rows[i]["star"]),
                                            CartImg = Convert.ToString(ds.Tables[0].Rows[i]["cart_img"]),
                                            ImgCartTitle = Convert.ToString(ds.Tables[0].Rows[i]["img_cart_title"]),
                                            WatchImg = Convert.ToString(ds.Tables[0].Rows[i]["watch_img"]),
                                            ImgWatchTitle = Convert.ToString(ds.Tables[0].Rows[i]["img_watch_title"]),
                                            WearItCount = Convert.ToString(ds.Tables[0].Rows[i]["wearit_count"]),
                                            WearItStatus = Convert.ToString(ds.Tables[0].Rows[i]["wearit_status"]),
                                            WearItImg = Convert.ToString(ds.Tables[0].Rows[i]["wearit_img"]),
                                            WearItNoneImg = Convert.ToString(ds.Tables[0].Rows[i]["wearit_none_img"]),
                                            WearItColor = Convert.ToString(ds.Tables[0].Rows[i]["wearit_color"]),
                                            WearItText = Convert.ToString(ds.Tables[0].Rows[i]["wearit_text"]),
                                            ImgWearItTitle = Convert.ToString(ds.Tables[0].Rows[i]["img_wearit_title"]),
                                            TryOnCount = Convert.ToString(ds.Tables[0].Rows[i]["tryon_count"]),
                                            TryOnStatus = Convert.ToString(ds.Tables[0].Rows[i]["tryon_status"]),
                                            TryOnImg = Convert.ToString(ds.Tables[0].Rows[i]["tryon_img"]),
                                            TryOnNoneImg = Convert.ToString(ds.Tables[0].Rows[i]["tryon_none_img"]),
                                            TryOnText = Convert.ToString(ds.Tables[0].Rows[i]["tryon_text"]),
                                            TryOnTitle = Convert.ToString(ds.Tables[0].Rows[i]["tryon_title"]),
                                            TryOnAndroidPath = Convert.ToString(ds.Tables[0].Rows[i]["tryon_android_path"]),
                                            TryOnIosPath = Convert.ToString(ds.Tables[0].Rows[i]["tryon_ios_path"]),
                                            WishCount = Convert.ToString(ds.Tables[0].Rows[i]["wish_count"]),
                                            WishDefaultImg = Convert.ToString(ds.Tables[0].Rows[i]["wish_default_img"]),
                                            WishFillImg = Convert.ToString(ds.Tables[0].Rows[i]["wish_fill_img"]),
                                            ImgWishTitle = Convert.ToString(ds.Tables[0].Rows[i]["img_wish_title"]),
                                            ItemReview = Convert.ToString(ds.Tables[0].Rows[i]["item_review"]),
                                            ItemSize = Convert.ToString(ds.Tables[0].Rows[i]["item_size"]),
                                            ItemKt = Convert.ToString(ds.Tables[0].Rows[i]["item_kt"]),
                                            ItemColor = Convert.ToString(ds.Tables[0].Rows[i]["item_color"]),
                                            ItemMetal = Convert.ToString(ds.Tables[0].Rows[i]["item_metal"]),
                                            ItemWt = Convert.ToString(ds.Tables[0].Rows[i]["item_wt"]),
                                            ItemStone = Convert.ToString(ds.Tables[0].Rows[i]["item_stone"]),
                                            ItemStoneWt = Convert.ToString(ds.Tables[0].Rows[i]["item_stone_wt"]),
                                            ItemStoneQty = Convert.ToString(ds.Tables[0].Rows[i]["item_stone_qty"]),
                                            StarColor = Convert.ToString(ds.Tables[0].Rows[i]["star_color"]),
                                            ItemMetalCommonID = Convert.ToString(ds.Tables[0].Rows[i]["ItemMetalCommonID"]),
                                            CartPrice = Convert.ToString(ds.Tables[0].Rows[i]["cart_price"]),
                                            ItemDPrice = Convert.ToString(ds.Tables[0].Rows[i]["ItemDPrice"]),
                                            ItemColorId = Convert.ToString(ds.Tables[0].Rows[i]["item_color_id"]),
                                            ItemDetails = Convert.ToString(ds.Tables[0].Rows[i]["item_details"]),
                                            ItemDiamondDetails = Convert.ToString(ds.Tables[0].Rows[i]["item_diamond_details"]),
                                            ItemText = Convert.ToString(ds.Tables[0].Rows[i]["item_text"]),
                                            MoreItemDetails = Convert.ToString(ds.Tables[0].Rows[i]["more_item_details"]),
                                            ItemStock = Convert.ToString(ds.Tables[0].Rows[i]["item_stock"]),
                                            CartItemQty = Convert.ToString(ds.Tables[0].Rows[i]["cart_item_qty"]),
                                            RupySymbol = Convert.ToString(ds.Tables[0].Rows[i]["rupy_symbol"]),
                                            VariantCount = Convert.ToString(ds.Tables[0].Rows[i]["variantCount"]),
                                            CartId = Convert.ToString(ds.Tables[0].Rows[i]["cart_id"]),
                                            ItemGenderCommonID = Convert.ToString(ds.Tables[0].Rows[i]["ItemGenderCommonID"]),
                                            ItemStockQty = Convert.ToString(ds.Tables[0].Rows[i]["item_stock_qty"]),
                                            ItemStockColorSizeQty = Convert.ToString(ds.Tables[0].Rows[i]["item_stock_colorsize_qty"]),
                                            CategoryId = Convert.ToString(ds.Tables[0].Rows[i]["category_id"]),
                                            ItemNosePinScrewSts = Convert.ToString(ds.Tables[0].Rows[i]["ItemNosePinScrewSts"]),
                                            Brand = Convert.ToString(ds.Tables[0].Rows[i]["brand"]),
                                            Category = Convert.ToString(ds.Tables[0].Rows[i]["category"]),
                                            ItemAproxDayCommonID = Convert.ToString(ds.Tables[0].Rows[i]["ItemAproxDayCommonID"]),
                                            ItemPlainGold = Convert.ToString(ds.Tables[0].Rows[i]["ItemPlainGold"]),
                                            ItemSoliterSts = Convert.ToString(ds.Tables[0].Rows[i]["ItemSoliterSts"]),
                                            ItemSubCtgName = Convert.ToString(ds.Tables[0].Rows[i]["ItemSubCtgName"]),
                                            ItemSubCtgId = Convert.ToString(ds.Tables[0].Rows[i]["ItemSubCtgID"]),
                                            ItemSubCtgNm = Convert.ToString(ds.Tables[0].Rows[i]["ItemSubCtgNm"]),
                                            ItemSubSubCtgName = Convert.ToString(ds.Tables[0].Rows[i]["ItemSubSubCtgName"]),
                                            Prev = Convert.ToString(ds.Tables[0].Rows[i]["prev"]),
                                            Next = Convert.ToString(ds.Tables[0].Rows[i]["next"]),
                                            DataCollection1 = Convert.ToString(ds.Tables[0].Rows[i]["data_collection1"]),
                                            ProductTags = Convert.ToString(ds.Tables[0].Rows[i]["productTags"]),
                                            SelectedColor = Convert.ToString(ds.Tables[0].Rows[i]["selectedColor"]),
                                            SelectedSize = Convert.ToString(ds.Tables[0].Rows[i]["selectedSize"]),
                                            SelectedColor1 = Convert.ToString(ds.Tables[0].Rows[i]["selectedColor1"]),
                                            SelectedSize1 = Convert.ToString(ds.Tables[0].Rows[i]["selectedSize1"]),
                                            FieldName = Convert.ToString(ds.Tables[0].Rows[i]["field_name"]),
                                            ColorName = Convert.ToString(ds.Tables[0].Rows[i]["color_name"]),
                                            DefaultColorName = Convert.ToString(ds.Tables[0].Rows[i]["default_color_name"]),
                                            DefaultColorCode = Convert.ToString(ds.Tables[0].Rows[i]["default_color_code"]),
                                            DefaultSizeName = Convert.ToString(ds.Tables[0].Rows[i]["default_size_name"]),
                                            FinalDMrp = Convert.ToString(ds.Tables[0].Rows[i]["FinalDMrp"]),
                                            ItemMrp = Convert.ToString(ds.Tables[0].Rows[i]["item_mrp"]),
                                            ItemMrpWithoutGst = Convert.ToString(ds.Tables[0].Rows[i]["item_mrp_without_gst"]),
                                            GoldPrice = Convert.ToString(ds.Tables[0].Rows[i]["goldprice"]),
                                            GoldPriceNew = Convert.ToString(ds.Tables[0].Rows[i]["goldprice_new"]),
                                            Collections = Convert.ToString(ds.Tables[0].Rows[i]["collections"]),
                                            ApproxDelivery = Convert.ToString(ds.Tables[0].Rows[i]["approxdelivery"]),
                                            SubCollection = Convert.ToString(ds.Tables[0].Rows[i]["sub_collection"]),
                                            Weight = Convert.ToString(ds.Tables[0].Rows[i]["weight"]),
                                            TotalLabourPer = Convert.ToString(ds.Tables[0].Rows[i]["totalLabourPer"]),
                                            //GstPrice = Convert.ToString(ds.Tables[0].Rows[i]["gst_price"]),
                                            //MetalPrice = Convert.ToString(ds.Tables[0].Rows[i]["metal_price"]),
                                            TotalLabourPerDist = Convert.ToString(ds.Tables[0].Rows[i]["totalLabourPerDist"]),
                                            GstPriceDist = Convert.ToString(ds.Tables[0].Rows[i]["gst_priceDist"]),
                                            MetalPriceDist = Convert.ToString(ds.Tables[0].Rows[i]["metal_priceDist"]),
                                            SizeList = sizeListDynamic,
                                            ColorList = colorListDynamic,
                                            ItemsColorSizeList = itemsColorSizeListDynamic,
                                            ItemOrderInstructionList = itemOrderInstructionListDynamic,
                                            ItemOrderCustomInstructionList = itemOrderCustomInstructionListDynamic,
                                            ItemImagesColor = itemImagesColorDynamic,
                                            ApproxDays = approxDaysDynamic,
                                            StockColorId = Convert.ToString(ds.Tables[0].Rows[i]["stock_color_id"]),
                                            StockSizeId = Convert.ToString(ds.Tables[0].Rows[i]["stock_size_id"]),
                                            FranchiseWiseStock = Convert.ToString(ds.Tables[0].Rows[i]["franchise_wise_stock"])
                                        });
                                    }
                                }

                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        plainGoldImages.Add(new PlainGoldImageList
                                        {
                                            imageViewName = Convert.ToString(ds.Tables[1].Rows[i]["image_view_name"]),
                                            imagePath = Convert.ToString(ds.Tables[1].Rows[i]["image_path"]),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                if (hasAnyData)
                {
                    plainGoldDetailList.details = plainGoldDetails;
                    plainGoldDetailList.images = plainGoldImages;
                }

                responseDetails = new ResponseDetails
                {
                    success = true,
                    status = "200",
                    message = hasAnyData ? "Successfully" : "No data found",
                    data = plainGoldDetailList,
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
                    data = new List<PlainGoldItemDetailList>(),
                    data1 = null
                };
            }
        }

        public async Task<ResponseDetails> TotalGoldDiaaWeight(TotalGoldDiaaWeightRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<TotalGoldData> totalGold = new List<TotalGoldData>();
            IList<TotalGoldData1> totalGold1 = new List<TotalGoldData1>();
            bool hasAnyData = false;

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.TotalGoldDiaaWeight;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? cartId = string.IsNullOrWhiteSpace(param.cartId) ? null : param.cartId;

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@cart_id", cartId);

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
                                        // Pull the raw string values that may contain JSON
                                        string itemOdSfx = Convert.ToString(ds.Tables[1].Rows[i]["ItemOdSfx"]);
                                        string goldWeight = Convert.ToString(ds.Tables[1].Rows[i]["Gold_Weight"]);
                                        string metalWeight = Convert.ToString(ds.Tables[1].Rows[i]["Diamond_Weight"]);

                                        totalGold.Add(new TotalGoldData
                                        {
                                            ItemOdSfx = itemOdSfx,
                                            Gold_weight = goldWeight,
                                            Metal_weight = metalWeight
                                        });
                                    }
                                }

                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    hasAnyData = true;
                                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                    {
                                            // Pull the raw string values that may contain JSON
                                            string label = Convert.ToString(ds.Tables[2].Rows[i]["Label"]);
                                            string metalWeight = Convert.ToString(ds.Tables[2].Rows[i]["Metal_Weight"]);
                                            string diamondWeight = Convert.ToString(ds.Tables[2].Rows[i]["Diamond_Weight"]);

                                            totalGold1.Add(new TotalGoldData1
                                            {
                                                Label = label,
                                                Metal_weight = metalWeight,
                                                Diamond_weight = diamondWeight
                                            });
                                    }
                                }
                            }
                        }
                    }
                }
                responseDetails = new ResponseDetails
                {
                    success = true,
                    status = "200",
                    message = hasAnyData ? "Successfully" : "No data found",
                    data = totalGold,
                    data1 = totalGold1
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
                    data = new List<TotalGoldData>(),
                    data1 = null
                };
            }
        }

        public async Task<ResponseDetails> ExtraGoldRateWiseRate(ExtraGoldRateWiseRateRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<ExtraGoldRateWiseRateResponse> details = new List<ExtraGoldRateWiseRateResponse>();
            bool hasAnyData = false;

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ExtraGoldRateWiseRate;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? godlWeight = string.IsNullOrWhiteSpace(param.goldWeight) ? null : param.goldWeight;
                        string? dataId = string.IsNullOrWhiteSpace(param.dataId) ? null : param.dataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.dataLogicType) ? null : param.dataLogicType;
                        string? designKt = string.IsNullOrWhiteSpace(param.designKt) ? null : param.designKt;

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@gold_weight", godlWeight);
                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);
                        cmd.Parameters.AddWithValue("@design_kt", designKt);

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
                                        // Pull the raw string values that may contain JSON
                                        string data = Convert.ToString(ds.Tables[0].Rows[i]["data"]);

                                        details.Add(new ExtraGoldRateWiseRateResponse
                                        {
                                            Data = data,
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                responseDetails = new ResponseDetails
                {
                    success = true,
                    status = "200",
                    message = hasAnyData ? "Successfully" : "No data found",
                    data = details
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
                    data = new List<ExtraGoldRateWiseRateResponse>(),
                };
            }
        }
    }
}
