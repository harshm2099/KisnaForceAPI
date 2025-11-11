using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using Newtonsoft.Json;
using System.Data;
using System.Web.Http;
using static NewAvatarWebApis.Core.Application.DTOs.DiamondCertificatePriceFilter;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class SolitaireService : ISolitaireService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        public async Task<ResponseDetails> SoliCartStore(SoliCartStoreParams solicartstore_params)
        {
            var responseDetails = new ResponseDetails();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataId = solicartstore_params.data_id > 0 ? solicartstore_params.data_id : 0;
                    string NetIp = string.IsNullOrWhiteSpace(solicartstore_params.net_ip) ? "" : solicartstore_params.net_ip;
                    string CartRemark = string.IsNullOrWhiteSpace(solicartstore_params.cart_remark) ? "" : solicartstore_params.cart_remark;
                    int CartId = solicartstore_params.cart_id > 0 ? solicartstore_params.cart_id : 0;
                    int ItemId = solicartstore_params.item_id > 0 ? solicartstore_params.item_id : 0;
                    decimal CartPrice = solicartstore_params.cart_price > 0 ? solicartstore_params.cart_price : 0;
                    int CartQTY = solicartstore_params.cart_qty > 0 ? solicartstore_params.cart_qty : 0;
                    int soli_refrence_id = solicartstore_params.soli_refrence_id > 0 ? solicartstore_params.soli_refrence_id : 0;
                    decimal CartMRPrice = solicartstore_params.cart_mrprice > 0 ? solicartstore_params.cart_mrprice : 0;
                    decimal CartRPrice = solicartstore_params.cart_rprice > 0 ? solicartstore_params.cart_rprice : 0;
                    decimal CartDPrice = solicartstore_params.cart_dprice > 0 ? solicartstore_params.cart_dprice : 0;
                    int CartColorCommonID = solicartstore_params.product_color_mst_id > 0 ? solicartstore_params.product_color_mst_id : 0;
                    int CartConfCommonID = solicartstore_params.product_size_mst_id > 0 ? solicartstore_params.product_size_mst_id : 0;
                    string CartItemInfoselect = string.IsNullOrWhiteSpace(solicartstore_params.product_item_remarks) ? "" : solicartstore_params.product_item_remarks;
                    string CartItemInfoselectIDS = string.IsNullOrWhiteSpace(solicartstore_params.product_item_remarks_ids) ? "" : solicartstore_params.product_item_remarks_ids;
                    decimal ExtraGold = solicartstore_params.extra_gold > 0 ? solicartstore_params.extra_gold : 0;
                    decimal ExtraGoldPrice = solicartstore_params.extra_gold_price > 0 ? solicartstore_params.extra_gold_price : 0;
                    int ItemAproxDayCommonID = solicartstore_params.ItemAproxDayCommonID > 0 ? solicartstore_params.ItemAproxDayCommonID : 0;

                    CommonHelpers objHelpers = new CommonHelpers();
                    string
                        MstType = "",
                        resmessage = "";
                    int
                        ItemCtgCommonID = 0,
                        resstatus = 0,
                        resstatuscode = 400,
                        cart_auto_id = 0;
                    decimal
                        D_Price = 0,
                        R_Price = 0;

                    string cmdQuery = DBCommands.GET_ITEMDETAILS_BYITEMID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@ItemId", ItemId);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    ItemCtgCommonID = dataReader["ItemCtgCommonID"] as int? ?? 0;
                                    MstType = dataReader["MstType"] as string ?? string.Empty;
                                }
                            }
                        }
                    }

                    cmdQuery = DBCommands.SOLICARTSTORE;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        if (MstType == "F")
                        {
                            CartItemDPRPCALCListingParams cartitemDPRPCALClistparams = new CartItemDPRPCALCListingParams();
                            IList<CartItemDPRPCALCListing> cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

                            cartitemDPRPCALClistparams.DataID = DataId;
                            cartitemDPRPCALClistparams.MRP = CartPrice;
                            cartItemDPRPCALCList = objHelpers.Get_DP_RP_Calculation(cartitemDPRPCALClistparams);

                            if (cartItemDPRPCALCList.Count > 0)
                            {
                                D_Price = cartItemDPRPCALCList[0].D_Price;
                                R_Price = cartItemDPRPCALCList[0].R_Price;
                            }
                        }

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@NetIp", NetIp);
                        cmd.Parameters.AddWithValue("@CartRemark", CartRemark);
                        cmd.Parameters.AddWithValue("@CartId", CartId);
                        cmd.Parameters.AddWithValue("@ItemId", ItemId);
                        cmd.Parameters.AddWithValue("@CartPrice", CartPrice);
                        cmd.Parameters.AddWithValue("@CartQTY", CartQTY);
                        cmd.Parameters.AddWithValue("@soli_refrence_id", soli_refrence_id);
                        cmd.Parameters.AddWithValue("@CartMRPrice", CartMRPrice);
                        cmd.Parameters.AddWithValue("@CartRPrice", CartRPrice);
                        cmd.Parameters.AddWithValue("@CartDPrice", CartDPrice);
                        cmd.Parameters.AddWithValue("@CartColorCommonID", CartColorCommonID);
                        cmd.Parameters.AddWithValue("@CartConfCommonID", CartConfCommonID);
                        cmd.Parameters.AddWithValue("@CartItemInfoselect", CartItemInfoselect);
                        cmd.Parameters.AddWithValue("@CartItemInfoselectIDS", CartItemInfoselectIDS);
                        cmd.Parameters.AddWithValue("@ExtraGold", ExtraGold);
                        cmd.Parameters.AddWithValue("@ExtraGoldPrice", ExtraGoldPrice);
                        cmd.Parameters.AddWithValue("@ItemAproxDayCommonID", ItemAproxDayCommonID);
                        cmd.Parameters.AddWithValue("@SourceType", "APP");
                        cmd.Parameters.AddWithValue("@D_Price", D_Price);
                        cmd.Parameters.AddWithValue("@R_Price", R_Price);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    try
                                    {
                                        var firstRow = ds.Tables[0].Rows[0];
                                        resstatus = firstRow["resstatus"] as int? ?? 0;
                                        resstatuscode = firstRow["resstatuscode"] as int? ?? 0;
                                        resmessage = firstRow["resmessage"] as string ?? string.Empty;
                                        cart_auto_id = firstRow["cart_auto_id"] as int? ?? 0;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                            }
                        }
                    }

                    responseDetails.success = (resstatus == 1 ? true : false);
                    responseDetails.message = resmessage;
                    responseDetails.cart_auto_id = cart_auto_id.ToString();
                    responseDetails.status = resstatuscode.ToString();
                    return responseDetails;
                }
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> SoliterSortByList()
        {
            var responseDetails = new ResponseDetails();
            IList<SoliterSortByListing> soliterSortByList = new List<SoliterSortByListing>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(DBCommands.SOLITER_SORTBY, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                soliterSortByList.Add(new SoliterSortByListing
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

                responseDetails.success = soliterSortByList.Any();
                responseDetails.message = soliterSortByList.Any() ? "Successfully" : "No data found";
                responseDetails.status = "200";
                responseDetails.data = soliterSortByList;
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<SoliterSortByListing>();
                return responseDetails;
            }
        }

        public async Task<ViewDiamondDetailNewResponse> ViewDiamondDetail_New(ViewDiamondDetailNewParams viewdiamonddetailnew_params)
        {
            var response = new ViewDiamondDetailNewResponse();
            var diamonddetailList = new List<DiamondDetailNew>();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    using (var cmd = new SqlCommand(DBCommands.VIEWDIAMONDDETAIL_NEW, dbConnection))
                    {
                        string StockNos = string.IsNullOrWhiteSpace(viewdiamonddetailnew_params.solietr_stoke_no) ? "" : viewdiamonddetailnew_params.solietr_stoke_no;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@StockNos", StockNos);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                diamonddetailList.Add(new DiamondDetailNew
                                {
                                    view_detail =
                                        $"Diamond Stock No.: {reader.GetSafeString("DiaStkNO")}\n" +
                                        $"Diamond Shape: {reader.GetSafeString("DiaStkShape")}\n" +
                                        $"Diamond Carat: {Math.Round(reader.GetSafeDecimal("DiaStkCarat"), 2)}\n" +
                                        $"Diamond Clarity: {reader.GetSafeString("DiaStkClarity")}\n" +
                                        $"Diamond Color: {reader.GetSafeString("DiaStkColor")}\n" +
                                        $"Diamond Shade Color: {reader.GetSafeString("DiaStkColor_Shade")}\n" +
                                        $"Diamond Cut: {reader.GetSafeString("DiaStkCut")}\n" +
                                        $"Diamond Polish: {reader.GetSafeString("DiaStkPolish")}\n" +
                                        $"Diamond Symmetry: {reader.GetSafeString("DiaStkSymmetry")}\n" +
                                        $"Diamond Fluorescent: {reader.GetSafeString("DiaStkFluorescent")}\n" +
                                        $"Diamond Measurement: {reader.GetSafeString("DiaStkMeasurement")}",
                                    view_diamond_image = reader.GetSafeString("DiaStkImageLink"),
                                    view_diamond_certificate = reader.GetSafeString("DiaStkCertificateLink"),
                                    view_diamond_video = reader.GetSafeString("DiaStkVideoLink")
                                });
                            }
                        }
                    }
                }

                response = new ViewDiamondDetailNewResponse
                {
                    success = diamonddetailList.Any(),
                    message = diamonddetailList.Any() ? "Successfully" : "No data found",
                    data = diamonddetailList
                };

                return response;
            }
            catch (SqlException ex)
            {
                response = new ViewDiamondDetailNewResponse
                {
                    success = false,
                    message = $"SQL error: {ex.Message}",
                    data = new List<DiamondDetailNew>()
                };
                return response;
            }
        }

        public async Task<SoliCertificateMaster> SoliterdiamondCertificatesPriceFilter(PayloadsSoliterCertificate payloadsolitercertificate)
        {
            try
            {
                SoliCertificateMaster SoliCertificateStatic = new SoliCertificateMaster();
                IList<SoliCertificateDetail> SoliCertificateDetails = new List<SoliCertificateDetail>();
                if (payloadsolitercertificate != null)
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.DiamondCertificatesPriceFilter;
                        dbConnection.Open();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@diaquality_id", payloadsolitercertificate.diaquality_id);
                            cmd.Parameters.AddWithValue("@category_id", payloadsolitercertificate.category_id);
                            cmd.Parameters.AddWithValue("@sort_type", payloadsolitercertificate.sort_type);
                            cmd.Parameters.AddWithValue("@item_id", payloadsolitercertificate.item_id);
                            cmd.Parameters.AddWithValue("@diacolor_id", payloadsolitercertificate.diacolor_id);
                            cmd.Parameters.AddWithValue("@diacut_id", payloadsolitercertificate.diacut_id);
                            cmd.Parameters.AddWithValue("@diafrom_carat", payloadsolitercertificate.diafrom_carat);
                            cmd.Parameters.AddWithValue("@diato_carat", payloadsolitercertificate.diato_carat);
                            cmd.Parameters.AddWithValue("@diaflurocense_id", payloadsolitercertificate.diaflurocense_id);
                            cmd.Parameters.AddWithValue("@diasymmentry_id", payloadsolitercertificate.diasymmentry_id);
                            cmd.Parameters.AddWithValue("@diapolish_id", payloadsolitercertificate.diapolish_id);
                            cmd.Parameters.AddWithValue("@diacertificate_id", payloadsolitercertificate.diacertificate_id);
                            cmd.Parameters.AddWithValue("@color_id", payloadsolitercertificate.color_id);
                            cmd.Parameters.AddWithValue("@cart_quantity", payloadsolitercertificate.cart_quantity);
                            cmd.Parameters.AddWithValue("@design_kt", payloadsolitercertificate.design_kt);
                            cmd.Parameters.AddWithValue("@size_id", payloadsolitercertificate.size_id);
                            cmd.Parameters.AddWithValue("@diashap_id", payloadsolitercertificate.diashap_id);
                            cmd.Parameters.AddWithValue("@shape", payloadsolitercertificate.shape);
                            cmd.Parameters.AddWithValue("@diaprice", payloadsolitercertificate.diaprice);

                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = cmd;
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    {
                                        SoliCertificateDetails.Add(new SoliCertificateDetail
                                        {
                                            RowNumber = Convert.ToString(ds.Tables[1].Rows[i]["RowNumber"]),
                                            price = Convert.ToString(ds.Tables[1].Rows[i]["price"]),
                                            goldwt = Convert.ToString(ds.Tables[1].Rows[i]["goldwt"]),
                                            stock_no = Convert.ToString(ds.Tables[1].Rows[i]["stock_no"]),
                                            certificate = Convert.ToString(ds.Tables[1].Rows[i]["certificate_no"]),
                                            certificate_link = Convert.ToString(ds.Tables[1].Rows[i]["certificate_link"]),
                                            stock_image = Convert.ToString(ds.Tables[1].Rows[i]["stock_image"]),
                                            stock_video = Convert.ToString(ds.Tables[1].Rows[i]["stock_video"]),
                                            dia_carat = Convert.ToString(ds.Tables[1].Rows[i]["dia_carat"]),
                                            tab_per = Convert.ToString(ds.Tables[1].Rows[i]["tab_per"]),
                                            diamond_cut = Convert.ToString(ds.Tables[1].Rows[i]["diamond_cut"]),
                                            diamond_polish = Convert.ToString(ds.Tables[1].Rows[i]["diamond_polish"]),
                                            diamond_symmentry = Convert.ToString(ds.Tables[1].Rows[i]["diamond_symmentry"]),
                                            diamond_flurence = Convert.ToString(ds.Tables[1].Rows[i]["diamond_flurence"]),
                                            diamond_maindetail = Convert.ToString(ds.Tables[1].Rows[i]["diamond_maindetail"]),
                                            diamond_extradetail = Convert.ToString(ds.Tables[1].Rows[i]["diamond_extradetail"]),
                                            TotalCount = Convert.ToString(ds.Tables[1].Rows[i]["TotalCount"]),
                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        SoliCertificateStatic.success = Convert.ToString(ds.Tables[0].Rows[i]["SUCCESS"]);
                                        SoliCertificateStatic.message = Convert.ToString(ds.Tables[0].Rows[i]["MESSAGE"]);
                                        SoliCertificateStatic.data = SoliCertificateDetails;
                                    }
                                }
                            }
                        }
                    }
                }

                if (SoliCertificateStatic != null || SoliCertificateStatic.success == "TRUE")
                {
                    return SoliCertificateStatic;
                }
                else
                {
                    return new SoliCertificateMaster
                    {
                        success = "false",
                        message = "No items found."
                    };
                }

            }
            catch (Exception ex)
            {

                return new SoliCertificateMaster
                {
                    message = ex.Message
                };
            }
        }

        public async Task<SoliterPriceBreakup_Static> SoliterPriceBreakup(SoliterPriceBreakupPayload SoliterPriceBreakupPayload)
        {
            try
            {
                SoliterPriceBreakup_Static SoliterPriceBreakupStatic = new SoliterPriceBreakup_Static();
                IList<SoliterPriceBreakupResponse> SoliterPriceBreakupResponse = new List<SoliterPriceBreakupResponse>();

                if (SoliterPriceBreakupPayload != null)
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.SoliterPriceBreakup;
                        dbConnection.Open();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@gold_wt", SoliterPriceBreakupPayload.gold_wt);
                            cmd.Parameters.AddWithValue("@cart_quantity", SoliterPriceBreakupPayload.cart_quantity);
                            cmd.Parameters.AddWithValue("@category_id", SoliterPriceBreakupPayload.category_id);
                            cmd.Parameters.AddWithValue("@design_kt", SoliterPriceBreakupPayload.design_kt);
                            cmd.Parameters.AddWithValue("@data_id", SoliterPriceBreakupPayload.data_id);
                            cmd.Parameters.AddWithValue("@item_id", SoliterPriceBreakupPayload.item_id);
                            cmd.Parameters.AddWithValue("@Stoke_no_arr", SoliterPriceBreakupPayload.Stoke_no_arr);

                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = cmd;
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        SoliterPriceBreakupResponse.Add(new SoliterPriceBreakupResponse
                                        {
                                            SolitaireWt_Price = Convert.ToString(ds.Tables[0].Rows[i]["Solitaire Wt. / Price"]),
                                            GoldWt_Price = Convert.ToString(ds.Tables[0].Rows[i]["Gold Wt. / Price"]),
                                            Labour_Price = Convert.ToString(ds.Tables[0].Rows[i]["Labour Price"]),
                                            GST = Convert.ToString(ds.Tables[0].Rows[i]["GST"]),
                                            Total = Convert.ToString(ds.Tables[0].Rows[i]["Total"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        SoliterPriceBreakupStatic.success = Convert.ToString(ds.Tables[1].Rows[i]["SUCCESS"]);
                                        SoliterPriceBreakupStatic.message = Convert.ToString(ds.Tables[1].Rows[i]["MESSAGE"]);
                                        SoliterPriceBreakupStatic.data = SoliterPriceBreakupResponse;
                                    }
                                }

                            }

                        }
                    }

                }

                if (SoliterPriceBreakupStatic != null || SoliterPriceBreakupStatic.success.ToUpper() == "TRUE")
                {
                    return SoliterPriceBreakupStatic;
                }
                else
                {
                    return new SoliterPriceBreakup_Static
                    {
                        success = "false",
                        message = "No items found."
                    };
                }

            }
            catch (Exception ex)
            {
                return new SoliterPriceBreakup_Static
                {
                    message = ex.Message
                };
            }

        }

        public async Task<FinalMRPNew_Static> FinalMrpNew(FinalMrpNewPayload FinalMrpNewPayload)
        {
            try
            {
                FinalMRPNew_Static FinalMRPNewStatic = new FinalMRPNew_Static();
                IList<FinalMRPNewResponse> FinalMRPNewResponse = new List<FinalMRPNewResponse>();

                if (FinalMrpNewPayload != null)
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.FinalMRPNew;
                        dbConnection.Open();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@gold_wt", FinalMrpNewPayload.gold_wt);
                            cmd.Parameters.AddWithValue("@cart_quantity", FinalMrpNewPayload.cart_quantity);
                            cmd.Parameters.AddWithValue("@category_id", FinalMrpNewPayload.category_id);
                            cmd.Parameters.AddWithValue("@design_kt", FinalMrpNewPayload.design_kt);
                            cmd.Parameters.AddWithValue("@data_id", FinalMrpNewPayload.data_id);
                            cmd.Parameters.AddWithValue("@item_id", FinalMrpNewPayload.item_id);
                            cmd.Parameters.AddWithValue("@Stoke_no_arr", FinalMrpNewPayload.Stoke_no_arr);

                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = cmd;
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        FinalMRPNewResponse.Add(new FinalMRPNewResponse
                                        {
                                            gold_wt = Convert.ToString(ds.Tables[0].Rows[i]["gold_wt"]),
                                            gold_price = Convert.ToString(ds.Tables[0].Rows[i]["gold_price"]),
                                            dia_wt = Convert.ToString(ds.Tables[0].Rows[i]["dia_wt"]),
                                            dia_price = Convert.ToString(ds.Tables[0].Rows[i]["dia_price"]),
                                            labour = Convert.ToString(ds.Tables[0].Rows[i]["labour"]),
                                            tax = Convert.ToString(ds.Tables[0].Rows[i]["tax"]),
                                            other = Convert.ToString(ds.Tables[0].Rows[i]["other"]),
                                            Final_Mrp = Convert.ToString(ds.Tables[0].Rows[i]["Final_Mrp"]),
                                            Final_Mrp_text = Convert.ToString(ds.Tables[0].Rows[i]["Final_Mrp_text"]),
                                            Final_DMrp = Convert.ToString(ds.Tables[0].Rows[i]["Final_DMrp"]),
                                            Final_RMrp = Convert.ToString(ds.Tables[0].Rows[i]["Final_RMrp"]),
                                            Soli_refrence_id = Convert.ToString(ds.Tables[0].Rows[i]["Soli_refrence_id"]),
                                            soliter_stock_no = Convert.ToString(ds.Tables[0].Rows[i]["soliter_stock_no"]),

                                        });
                                    }
                                }

                                if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        FinalMRPNewStatic.success = Convert.ToString(ds.Tables[1].Rows[i]["SUCCESS"]);
                                        FinalMRPNewStatic.message = Convert.ToString(ds.Tables[1].Rows[i]["MESSAGE"]);
                                        FinalMRPNewStatic.data = FinalMRPNewResponse;
                                    }
                                }
                            }
                        }
                    }
                }

                if (FinalMRPNewStatic != null || FinalMRPNewStatic.success.ToUpper() == "TRUE")
                {
                    return FinalMRPNewStatic;
                }
                else
                {
                    return new FinalMRPNew_Static
                    {
                        success = "false",
                        message = "No items found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new FinalMRPNew_Static
                {
                    message = ex.Message
                };
            }
        }

        public async Task<DiamondCertificatePriceFilter_Static> Diamond_Certificates_price_filter_dist(DiamondCertificatePriceFilter DCFP_Payload)
        {
            DiamondCertificatePriceFilter_Static DCFP_Static = new DiamondCertificatePriceFilter_Static();
            IList<DiamondCertificatePriceFilter_Records> DCFP_Records = new List<DiamondCertificatePriceFilter_Records>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DIAMOND_CERTIFICATES_PRICE_FILTER_DIST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sort_type", DCFP_Payload.sort_type);
                        cmd.Parameters.AddWithValue("@category_id", DCFP_Payload.category_id);
                        cmd.Parameters.AddWithValue("@cart_quantity", DCFP_Payload.cart_quantity);
                        cmd.Parameters.AddWithValue("@item_id", DCFP_Payload.item_id);
                        cmd.Parameters.AddWithValue("@size_id", DCFP_Payload.size_id);
                        cmd.Parameters.AddWithValue("@diashap_id", DCFP_Payload.diashap_id);
                        cmd.Parameters.AddWithValue("@page", DCFP_Payload.page);
                        cmd.Parameters.AddWithValue("@DEFAULT_LIMIT_APP_PAGE", DCFP_Payload.DEFAULT_LIMIT_APP_PAGE);
                        cmd.Parameters.AddWithValue("@design_kt", DCFP_Payload.design_kt);


                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 300;
                            da.SelectCommand = cmd;
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
                            {
                                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                {
                                    DCFP_Records.Add(new DiamondCertificatePriceFilter_Records
                                    {
                                        RowNumber = Convert.ToString(ds.Tables[0].Rows[j]["RowNumber"]),
                                        TotalPrice = Convert.ToString(ds.Tables[0].Rows[j]["Price"]),
                                        price = Convert.ToString(ds.Tables[0].Rows[j]["price"]),
                                        goldwt = Convert.ToString(ds.Tables[0].Rows[j]["goldwt"]),
                                        stock_no = Convert.ToString(ds.Tables[0].Rows[j]["stock_no"]),
                                        certificate_no = Convert.ToString(ds.Tables[0].Rows[j]["certificate_no"]),
                                        certificate_link = Convert.ToString(ds.Tables[0].Rows[j]["certificate_link"]),
                                        stock_image = Convert.ToString(ds.Tables[0].Rows[j]["stock_image"]),
                                        stock_video = Convert.ToString(ds.Tables[0].Rows[j]["stock_video"]),
                                        dia_carat = Convert.ToString(ds.Tables[0].Rows[j]["dia_carat"]),
                                        tab_per = Convert.ToString(ds.Tables[0].Rows[j]["tab_per"]),
                                        diamond_cut = Convert.ToString(ds.Tables[0].Rows[j]["diamond_cut"]),
                                        diamond_polish = Convert.ToString(ds.Tables[0].Rows[j]["diamond_polish"]),
                                        diamond_symmentry = Convert.ToString(ds.Tables[0].Rows[j]["diamond_symmentry"]),
                                        diamond_flurence = Convert.ToString(ds.Tables[0].Rows[j]["diamond_flurence"]),
                                        diamond_maindetail = Convert.ToString(ds.Tables[0].Rows[j]["diamond_maindetail"]),
                                        diamond_extradetail = Convert.ToString(ds.Tables[0].Rows[j]["diamond_extradetail"]),
                                        TotalCount = Convert.ToString(ds.Tables[0].Rows[j]["TotalCount"]),
                                        price_text = "MRP : ₹ " + Convert.ToString(ds.Tables[0].Rows[j]["price_text"]) // Convert.ToDecimal(ds.Tables[0].Rows[j]["price"]).ToString("#,0"),
                                    });
                                }

                                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                {
                                    DCFP_Static.success = Convert.ToString(ds.Tables[1].Rows[i]["SUCCESS"]);
                                    DCFP_Static.message = Convert.ToString(ds.Tables[1].Rows[i]["MESSAGE"]);
                                    DCFP_Static.last_page = Convert.ToString(ds.Tables[1].Rows[i]["last_page"]);  //Convert.ToString(Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]) / Convert.ToInt32(DCFP_Payload.DEFAULT_LIMIT_APP_PAGE));
                                    DCFP_Static.total_items = Convert.ToString(ds.Tables[1].Rows[i]["total_items"]);  //Convert.ToString(ds.Tables[0].Rows.Count);
                                    DCFP_Static.current_page = Convert.ToString(ds.Tables[1].Rows[i]["current_page"]); // DCFP_Payload.page;
                                    DCFP_Static.data = DCFP_Records;
                                }
                            }
                        }

                    }
                }
                if (DCFP_Records.Count > 0)
                {
                    return DCFP_Static;
                }
                else
                {
                    DCFP_Static.success = "false";
                    DCFP_Static.message = "No Records Found.";
                    return DCFP_Static;
                }
            }
            catch (SqlException sqlEx)
            {
                return new DiamondCertificatePriceFilter_Static
                {
                    message = sqlEx.Message
                };
            }
        }

        public async Task<DiamondFilter_Static> DiamondFiltersList(DiamondFiltersList DUser)
        {
            Diamond D = new Diamond();
            DiamondFilter_Static D_Static = new DiamondFilter_Static();
            DiamondFilter_Records D_Records = new DiamondFilter_Records();
            IList<Diamond_Shapelist> D_Shapelist = new List<Diamond_Shapelist>();
            IList<Diamond_Colorlist> D_Colorlist = new List<Diamond_Colorlist>();
            IList<Diamond_Qualitylist> D_Qualitylist = new List<Diamond_Qualitylist>();
            IList<Diamond_Cutlist> D_Cutlist = new List<Diamond_Cutlist>();
            IList<Diamond_Polishlis> D_Polishlis = new List<Diamond_Polishlis>();
            IList<Diamond_Symmetrylist> D_Symmetrylist = new List<Diamond_Symmetrylist>();
            IList<Diamond_Fluorlist> D_Fluorlist = new List<Diamond_Fluorlist>();
            IList<Diamond_Lablist> D_Lablist = new List<Diamond_Lablist>();
            Diamond_Caratlist D_Caratlist = new Diamond_Caratlist();
            IList<Diamond_Caratlist_detail> D_Caratlist_Detail = new List<Diamond_Caratlist_detail>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DIAMONDFILTERSLIST;
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cart_quantity", DUser.cart_quantity);
                        cmd.Parameters.AddWithValue("@category_id", DUser.category_id);
                        cmd.Parameters.AddWithValue("@color_id", DUser.color_id);
                        cmd.Parameters.AddWithValue("@item_id", DUser.item_id);
                        cmd.Parameters.AddWithValue("@size_id", DUser.size_id);

                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            DiamondFilter_Records DR = new DiamondFilter_Records();
                            if (ds != null && ds.Tables != null && ds.Tables.Count > 8)
                            {
                                if (ds.Tables[ds.Tables.Count - 1].Rows[0]["SUCCESS"].ToString().ToUpper() == "TRUE")
                                {
                                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                                    {
                                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                        {
                                            D_Shapelist.Add(new Diamond_Shapelist
                                            {
                                                DiaStkShape = Convert.ToString(ds.Tables[0].Rows[j]["DiaStkShape"]),
                                            });
                                        }

                                    }
                                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                                    {
                                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                        {
                                            D_Colorlist.Add(new Diamond_Colorlist
                                            {
                                                DiaStkColor = Convert.ToString(ds.Tables[1].Rows[j]["DiaStkColor"]),
                                            });
                                        }
                                    }
                                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                                    {
                                        for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                                        {
                                            D_Qualitylist.Add(new Diamond_Qualitylist
                                            {
                                                DiaStkClarity = Convert.ToString(ds.Tables[2].Rows[j]["DiaStkClarity"]),
                                            });
                                        }
                                    }
                                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                                    {
                                        for (int j = 0; j < ds.Tables[3].Rows.Count; j++)
                                        {
                                            D_Cutlist.Add(new Diamond_Cutlist
                                            {
                                                DiaStkCut = Convert.ToString(ds.Tables[3].Rows[j]["DiaStkCut"]),
                                            });
                                        }
                                    }
                                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                                    {
                                        for (int j = 0; j < ds.Tables[4].Rows.Count; j++)
                                        {
                                            D_Polishlis.Add(new Diamond_Polishlis
                                            {
                                                DiaStkPolish = Convert.ToString(ds.Tables[4].Rows[j]["DiaStkPolish"]),
                                                DiaStkPolishVal = Convert.ToString(ds.Tables[4].Rows[j]["DiaStkPolishVal"]),
                                            });
                                        }
                                    }
                                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                                    {
                                        for (int j = 0; j < ds.Tables[5].Rows.Count; j++)
                                        {
                                            D_Symmetrylist.Add(new Diamond_Symmetrylist
                                            {
                                                DiaStkSymmetry = Convert.ToString(ds.Tables[5].Rows[j]["DiaStkSymmetry"]),
                                                DiaStkSymmetryfull = Convert.ToString(ds.Tables[5].Rows[j]["DiaStkSymmetryfull"]),
                                            });
                                        }
                                    }
                                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                                    {
                                        for (int j = 0; j < ds.Tables[6].Rows.Count; j++)
                                        {
                                            D_Fluorlist.Add(new Diamond_Fluorlist
                                            {
                                                DiaStkFluorescent = Convert.ToString(ds.Tables[6].Rows[j]["DiaStkFluorescent"]),
                                            });
                                        }
                                    }
                                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                                    {
                                        for (int j = 0; j < ds.Tables[7].Rows.Count; j++)
                                        {
                                            D_Lablist.Add(new Diamond_Lablist
                                            {
                                                DiaStkLab = Convert.ToString(ds.Tables[7].Rows[j]["DiaStkLab"]),
                                            });
                                        }

                                    }
                                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                                    {
                                        for (int j = 0; j < ds.Tables[8].Rows.Count; j++)
                                        {
                                            D_Caratlist_Detail.Add(new Diamond_Caratlist_detail
                                            {
                                                ID = ds.Tables[8].Columns["ID"] != null ? Convert.ToString(ds.Tables[8].Rows[j]["ID"]) : "",
                                                DiaStkCarat = ds.Tables[8].Columns["DiaStkCarat"] != null ? Convert.ToString(ds.Tables[8].Rows[j]["DiaStkCarat"]) : "",
                                                itemDiaWtFr = ds.Tables[8].Columns["itemDiaWtFr"] != null ? Convert.ToString(ds.Tables[8].Rows[j]["itemDiaWtFr"]) : "",
                                                itemDiaWtTo = ds.Tables[8].Columns["itemDiaWtTo"] != null ? Convert.ToString(ds.Tables[8].Rows[j]["itemDiaWtTo"]) : "",
                                                fromto_carat = ds.Tables[8].Columns["fromto_carat"] != null ? Convert.ToString(ds.Tables[8].Rows[j]["fromto_carat"]) : "",
                                                itemGoldWt = ds.Tables[8].Columns["itemGoldWt"] != null ? Convert.ToString(ds.Tables[8].Rows[j]["itemGoldWt"]) : "",
                                                itemId = ds.Tables[8].Columns["DiaStkShape"] != null ? Convert.ToString(ds.Tables[8].Rows[j]["itemId"]) : "",
                                                itemSize = ds.Tables[8].Columns["DiaStkShape"] != null ? Convert.ToString(ds.Tables[8].Rows[j]["itemSize"]) : "",
                                                RowNum = ds.Tables[8].Columns["DiaStkShape"] != null ? Convert.ToString(ds.Tables[8].Rows[j]["RowNum"]) : "",

                                            });
                                        }
                                        D_Caratlist.caratlist = D_Caratlist_Detail;
                                    }//

                                    if (ds.Tables[ds.Tables.Count - 1].Rows[0]["SUCCESS"].ToString().ToUpper() == "TRUE")
                                    {
                                        D_Static.success = ds.Tables[ds.Tables.Count - 1].Rows[0]["SUCCESS"].ToString();
                                        D_Static.message = ds.Tables[ds.Tables.Count - 1].Rows[0]["MESSAGE"].ToString();
                                    }

                                    if (D_Static.success == "TRUE") //D_Records != null || 
                                    {
                                        D_Records.Diamond_Shapelist = D_Shapelist;
                                        D_Records.Diamond_Colorlist = D_Colorlist;
                                        D_Records.Diamond_Qualitylist = D_Qualitylist;
                                        D_Records.Diamond_Cutlist = D_Cutlist;
                                        D_Records.Diamond_Polishlis = D_Polishlis;
                                        D_Records.Diamond_Symmetrylist = D_Symmetrylist;
                                        D_Records.Diamond_Fluorlist = D_Fluorlist;
                                        D_Records.Diamond_Lablist = D_Lablist;
                                        D_Records.Diamond_Caratlist = D_Caratlist;

                                        D_Static.data = D_Records;
                                    }
                                    else
                                    {
                                        return new DiamondFilter_Static
                                        {
                                            success = "false",
                                            message = "No items found."
                                        };
                                    }
                                    return D_Static;
                                }
                                else
                                {
                                    return new DiamondFilter_Static
                                    {
                                        success = "false",
                                        message = "No items found."
                                    };
                                }
                            }
                            else
                            {
                                return new DiamondFilter_Static
                                {
                                    success = "false",
                                    message = "No items found."
                                };
                            }
                        }

                    }

                }

            }
            catch (Exception sqlEx)
            {
                return new DiamondFilter_Static
                {
                    message = sqlEx.Message
                };
            }
        }

        public async Task<ResponseDetails> ViewDiamondDetail(Diamond param)
        {
            var responseDetails = new ResponseDetails();
            IList<DiamondDetailsResponse> details = new List<DiamondDetailsResponse>();

            try
            {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {

                        string cmdQuery = DBCommands.VIEWDIAMONDDETAIL;
                        dbConnection.Open();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            string? SoliterStockNo = string.IsNullOrWhiteSpace(param.solietr_stoke_no) ? null : param.solietr_stoke_no;

                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@solietr_stoke_no", SoliterStockNo);

                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = cmd;
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                if (ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            details.Add(new DiamondDetailsResponse
                                            {
                                                ViewDetail = Convert.ToString(ds.Tables[0].Rows[i]["view_detail"]),
                                                ViewDiamondImage = Convert.ToString(ds.Tables[0].Rows[i]["view_diamond_image"]),
                                                ViewDiamondCertificate = Convert.ToString(ds.Tables[0].Rows[i]["view_diamond_certificate"]),
                                                ViewDiamondVideo = Convert.ToString(ds.Tables[0].Rows[i]["view_diamond_video"]),
                                            });
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
                    responseDetails.data = new List<DiamondDetailsResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<DiamondDetailsResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> DiamondGoldCappingGet(DiamondGoldCappingRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<DiamondGoldCappingResponse> details = new List<DiamondGoldCappingResponse>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {

                    string cmdQuery = DBCommands.DiamondGoldCapping;
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? dataId = string.IsNullOrWhiteSpace(param.dataId) ? null : param.dataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.dataLoginType) ? null : param.dataLoginType;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);

                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        details.Add(new DiamondGoldCappingResponse
                                        {
                                            Type = Convert.ToString(ds.Tables[0].Rows[i]["Type"]),
                                            CategoryID = Convert.ToString(ds.Tables[0].Rows[i]["CategoryID"]),
                                            CategoryName = Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]),
                                            SubCategoryID = Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryID"]),
                                            SubCategoryName = Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]),
                                            ComplexityID = Convert.ToString(ds.Tables[0].Rows[i]["ComplexityID"]),
                                            ComplexityName = Convert.ToString(ds.Tables[0].Rows[i]["ComplexityName"]),
                                            SizeID = Convert.ToString(ds.Tables[0].Rows[i]["SizeID"]),
                                            SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]),
                                            ColorID = Convert.ToString(ds.Tables[0].Rows[i]["ColorID"]),
                                            ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]),
                                            MinQuantity = Convert.ToString(ds.Tables[0].Rows[i]["MinQuantity"]),
                                            Quantity = Convert.ToString(ds.Tables[0].Rows[i]["Quantity"]),
                                            FromGrams = Convert.ToString(ds.Tables[0].Rows[i]["FromGrams"]),
                                            ToGrams = Convert.ToString(ds.Tables[0].Rows[i]["ToGrams"]),
                                            FromPrice = Convert.ToString(ds.Tables[0].Rows[i]["FromPrice"]),
                                            ToPrice = Convert.ToString(ds.Tables[0].Rows[i]["ToPrice"]),
                                            Amount = Convert.ToString(ds.Tables[0].Rows[i]["Amount"]),
                                            StockQtyFlag = Convert.ToString(ds.Tables[0].Rows[i]["StockQtyFlag"]),
                                            AvailableQty = Convert.ToString(ds.Tables[0].Rows[i]["availableQty"]),
                                            RemainCapping = Convert.ToString(ds.Tables[0].Rows[i]["RemainCapping"]),
                                        });
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
                    responseDetails.data = new List<DiamondGoldCappingResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<DiamondGoldCappingResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> SolitaireSubCategoryNewFranSIS(SolitaireFilterRequest request, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            bool hasAnyData = false;
            SolitaireItemFilterList solitaireFilter = new SolitaireItemFilterList();
            IList<FilterSubCategory> subCategoryList = new List<FilterSubCategory>();
            IList<FilterColor> colorList = new List<FilterColor>();
            IList<FilterBrand> brandList = new List<FilterBrand>();
            IList<FilterGender> genderList = new List<FilterGender>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SolitaireSubCategoryNewFranSIS;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? category_id = string.IsNullOrWhiteSpace(request.category_id) ? null : request.category_id;
                        string? button_code = string.IsNullOrWhiteSpace(request.button_code) ? null : request.button_code;
                        string? master_common_id = string.IsNullOrWhiteSpace(request.master_common_id) ? null : request.master_common_id;
                        string? data_login_type = string.IsNullOrWhiteSpace(request.data_login_type) ? null : request.data_login_type;
                        string? orgtype = string.IsNullOrWhiteSpace(header.orgtype) ? null : header.orgtype;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@btn_cd", button_code);
                        cmd.Parameters.AddWithValue("@master_common_id", master_common_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@org_type", orgtype);

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
                                        subCategoryList.Add(new FilterSubCategory
                                        {
                                            categoryId = Convert.ToString(ds.Tables[1].Rows[i]["category_id"]),
                                            subCategoryId = Convert.ToString(ds.Tables[1].Rows[i]["sub_category_id"]),
                                            subCategoryName = Convert.ToString(ds.Tables[1].Rows[i]["sub_category_name"]),
                                            categoryCount = Convert.ToString(ds.Tables[1].Rows[i]["category_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        colorList.Add(new FilterColor
                                        {
                                            color = Convert.ToString(ds.Tables[2].Rows[i]["color"]),
                                        });
                                    }
                                }

                                if (ds.Tables[3].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        brandList.Add(new FilterBrand
                                        {
                                            brandId = Convert.ToString(ds.Tables[3].Rows[i]["brand_id"]),
                                            brandName = Convert.ToString(ds.Tables[3].Rows[i]["brand_name"]),
                                            brandCount = Convert.ToString(ds.Tables[3].Rows[i]["brand_count"]),
                                        });
                                    }
                                }

                                if (ds.Tables[4].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        genderList.Add(new FilterGender
                                        {
                                            genderId = Convert.ToString(ds.Tables[4].Rows[i]["gender_id"]),
                                            genderName = Convert.ToString(ds.Tables[4].Rows[i]["gender_name"]),
                                            genderCount = Convert.ToString(ds.Tables[4].Rows[i]["gender_count"]),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                if (hasAnyData)
                {
                    solitaireFilter.subCategory = subCategoryList;
                    solitaireFilter.color = colorList;
                    solitaireFilter.brand = brandList;
                    solitaireFilter.gender = genderList;
                }

                responseDetails = new ResponseDetails
                {
                    success = true,
                    status = "200",
                    message = hasAnyData ? "Successfully" : "No data found",
                    data = solitaireFilter,
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
                    data = new List<SolitaireItemFilterList>(),
                    data1 = null
                };
            }
        }

        public async Task<ResponseDetails> SolitaireDetailsFranSIS(SolitaireDetailsRequest request, CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            bool hasAnyData = false;
            SolitaireDetailList solitaireDetailList = new SolitaireDetailList();
            IList<DetailList> details = new List<DetailList>();
            IList<ImageList> images = new List<ImageList>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SolitaireDetailsFranSIS;
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
                        string? orgtype = string.IsNullOrWhiteSpace(header?.orgtype) ? null : header?.orgtype;

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

                                        // JSON DESERIALIZATION
                                        List<Dictionary<string, object>> sizeListDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> colorListDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> itemsColorSizeListDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> itemOrderInstructionListDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> itemOrderCustomInstructionListDynamic = new List<Dictionary<string, object>>();
                                        List<Dictionary<string, object>> itemImagesColorDynamic = new List<Dictionary<string, object>>();

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
                                        details.Add(new DetailList
                                        {
                                            ItemId = Convert.ToString(ds.Tables[0].Rows[i]["item_id"]),
                                            CategoryName = Convert.ToString(ds.Tables[0].Rows[i]["category_name"]),
                                            ItemSoliter = Convert.ToString(ds.Tables[0].Rows[i]["item_soliter"]),
                                            PlaingoldStatus = Convert.ToString(ds.Tables[0].Rows[i]["plaingold_status"]),
                                            ItemName = Convert.ToString(ds.Tables[0].Rows[i]["item_name"]),
                                            ItemSku = Convert.ToString(ds.Tables[0].Rows[i]["item_sku"]),
                                            ItemDescription = Convert.ToString(ds.Tables[0].Rows[i]["item_description"]),
                                            ItemMrp = Convert.ToString(ds.Tables[0].Rows[i]["item_mrp"]),
                                            ItemDiscount = Convert.ToString(ds.Tables[0].Rows[i]["item_discount"]),
                                            ItemPrice = Convert.ToString(ds.Tables[0].Rows[i]["item_price"]),
                                            RetailPrice = Convert.ToString(ds.Tables[0].Rows[i]["retail_price"]),
                                            DistPrice = Convert.ToString(ds.Tables[0].Rows[i]["dist_price"]),
                                            Uom = Convert.ToString(ds.Tables[0].Rows[i]["uom"]),
                                            Star = Convert.ToString(ds.Tables[0].Rows[i]["star"]),
                                            CartImg = Convert.ToString(ds.Tables[0].Rows[i]["cart_img"]),
                                            ImgCartTitle = Convert.ToString(ds.Tables[0].Rows[i]["img_cart_title"]),
                                            WatchImg = Convert.ToString(ds.Tables[0].Rows[i]["watch_img"]),
                                            ImgWatchTitle = Convert.ToString(ds.Tables[0].Rows[i]["img_watch_title"]),
                                            WearitCount = Convert.ToString(ds.Tables[0].Rows[i]["wearit_count"]),
                                            WearitStatus = Convert.ToString(ds.Tables[0].Rows[i]["wearit_status"]),
                                            WearitImg = Convert.ToString(ds.Tables[0].Rows[i]["wearit_img"]),
                                            WearitNoneImg = Convert.ToString(ds.Tables[0].Rows[i]["wearit_none_img"]),
                                            WearitColor = Convert.ToString(ds.Tables[0].Rows[i]["wearit_color"]),
                                            WearitText = Convert.ToString(ds.Tables[0].Rows[i]["wearit_text"]),
                                            ImgWearitTitle = Convert.ToString(ds.Tables[0].Rows[i]["img_wearit_title"]),
                                            TryonCount = Convert.ToString(ds.Tables[0].Rows[i]["tryon_count"]),
                                            TryonStatus = Convert.ToString(ds.Tables[0].Rows[i]["tryon_status"]),
                                            TryonImg = Convert.ToString(ds.Tables[0].Rows[i]["tryon_img"]),
                                            TryonNoneImg = Convert.ToString(ds.Tables[0].Rows[i]["tryon_none_img"]),
                                            TryonText = Convert.ToString(ds.Tables[0].Rows[i]["tryon_text"]),
                                            TryonTitle = Convert.ToString(ds.Tables[0].Rows[i]["tryon_title"]),
                                            TryonAndroidPath = Convert.ToString(ds.Tables[0].Rows[i]["tryon_android_path"]),
                                            TryonIosPath = Convert.ToString(ds.Tables[0].Rows[i]["tryon_ios_path"]),
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
                                            ItemMetalCommonId = Convert.ToString(ds.Tables[0].Rows[i]["ItemMetalCommonID"]),
                                            PriceText = Convert.ToString(ds.Tables[0].Rows[i]["price_text"]),
                                            CartPrice = Convert.ToString(ds.Tables[0].Rows[i]["cart_price"]),
                                            ItemColorId = Convert.ToString(ds.Tables[0].Rows[i]["item_color_id"]),
                                            ItemDetails = Convert.ToString(ds.Tables[0].Rows[i]["item_details"]),
                                            ItemText = Convert.ToString(ds.Tables[0].Rows[i]["item_text"]),
                                            MoreItemDetails = Convert.ToString(ds.Tables[0].Rows[i]["more_item_details"]),
                                            ItemStock = Convert.ToString(ds.Tables[0].Rows[i]["item_stock"]),
                                            CartItemQty = Convert.ToString(ds.Tables[0].Rows[i]["cart_item_qty"]),
                                            RupySymbol = Convert.ToString(ds.Tables[0].Rows[i]["rupy_symbol"]),
                                            VariantCount = Convert.ToString(ds.Tables[0].Rows[i]["variantCount"]),
                                            CartId = Convert.ToString(ds.Tables[0].Rows[i]["cart_id"]),
                                            ItemGenderCommonId = Convert.ToString(ds.Tables[0].Rows[i]["ItemGenderCommonID"]),
                                            CartAutoId = Convert.ToString(ds.Tables[0].Rows[i]["cart_auto_id"]),
                                            ItemStockQty = Convert.ToString(ds.Tables[0].Rows[i]["item_stock_qty"]),
                                            ItemStockColorsizeQty = Convert.ToString(ds.Tables[0].Rows[i]["item_stock_colorsize_qty"]),
                                            CategoryId = Convert.ToString(ds.Tables[0].Rows[i]["category_id"]),
                                            ItemNosePinScrewSts = Convert.ToString(ds.Tables[0].Rows[i]["ItemNosePinScrewSts"]),
                                            ItemAproxDayCommonId = Convert.ToString(ds.Tables[0].Rows[i]["ItemAproxDayCommonID"]),
                                            ItemBrandCommonId = Convert.ToString(ds.Tables[0].Rows[i]["ItemBrandCommonID"]),
                                            ItemIllumine = Convert.ToString(ds.Tables[0].Rows[i]["item_illumine"]),
                                            CustomNote = Convert.ToString(ds.Tables[0].Rows[i]["custom_note"]),
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
                                            SizeList = sizeListDynamic,
                                            ColorList = colorListDynamic,
                                            ItemsColorSizeList = itemsColorSizeListDynamic,
                                            ItemOrderInstructionList = itemOrderInstructionListDynamic,
                                            ItemOrderCustomInstructionList = itemOrderCustomInstructionListDynamic,
                                            ItemImagesColor = itemImagesColorDynamic
                                        });
                                    }
                                }

                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    {
                                        hasAnyData = true;
                                        images.Add(new ImageList
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
                    solitaireDetailList.details = details;
                    solitaireDetailList.images = images;
                }

                responseDetails = new ResponseDetails
                {
                    success = true,
                    status = "200",
                    message = hasAnyData ? "Successfully" : "No data found",
                    data = solitaireDetailList,
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
                    data = new List<SolitaireDetailList>(),
                    data1 = null
                };
            }
        }
    }
}