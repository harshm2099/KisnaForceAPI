using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using System.Data;
using static NewAvatarWebApis.Core.Application.DTOs.SoliCatList;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class SolicatListService : ISolicatListService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        public async Task<SoliCatList_Static> GetSolitaireCategoryList(SoliCatList SCUser)
        {
            SoliCatList SC = new SoliCatList();
            SoliCatList_Static SC_Static = new SoliCatList_Static();
            IList<SoliCatList_Records> SC_Records = new List<SoliCatList_Records>();

            try
            {
                if (SCUser != null)
                {
                    if (string.IsNullOrEmpty(SCUser.CATEGORY_DEFAULT_COMMON_ID))
                        SCUser.CATEGORY_DEFAULT_COMMON_ID = SC.CATEGORY_DEFAULT_COMMON_ID;
                    if (string.IsNullOrEmpty(SCUser.AWS_IMAGE_DEFAULT_COMMON_ID))
                        SCUser.AWS_IMAGE_DEFAULT_COMMON_ID = SC.AWS_IMAGE_DEFAULT_COMMON_ID;
                    if (string.IsNullOrEmpty(SCUser.STATE_COMMON_ID))
                        SCUser.STATE_COMMON_ID = SC.STATE_COMMON_ID;
                    if (string.IsNullOrEmpty(SCUser.ILLUMINE_COLLECTION))
                        SCUser.ILLUMINE_COLLECTION = SC.ILLUMINE_COLLECTION;
                }
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SOLITAIRECATEGORYLIST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_id", SCUser.data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", SCUser.data_login_type);
                        cmd.Parameters.AddWithValue("@device_type", SCUser.device_type);
                        cmd.Parameters.AddWithValue("@display", SCUser.display);
                        cmd.Parameters.AddWithValue("@CATEGORY_DEFAULT_COMMON_ID", SCUser.CATEGORY_DEFAULT_COMMON_ID);
                        cmd.Parameters.AddWithValue("@STATE_COMMON_ID", SCUser.STATE_COMMON_ID);
                        cmd.Parameters.AddWithValue("@ILLUMINE_COLLECTION", SCUser.ILLUMINE_COLLECTION);

                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
                            {
                                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                {
                                    SC_Records.Add(new SoliCatList_Records
                                    {
                                        category_id = Convert.ToString(ds.Tables[1].Rows[j]["category_id"]),
                                        category_name = Convert.ToString(ds.Tables[1].Rows[j]["category_name"]),
                                        master_common_id = Convert.ToString(ds.Tables[1].Rows[j]["master_common_id"]),
                                        image_path = Convert.ToString(ds.Tables[1].Rows[j]["image_path"]),
                                        count = Convert.ToString(ds.Tables[1].Rows[j]["count"])
                                    });
                                }
                            }

                            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    SC_Static.success = Convert.ToString(ds.Tables[0].Rows[i]["SUCCESS"]);
                                    SC_Static.message = Convert.ToString(ds.Tables[0].Rows[i]["MESSAGE"]);
                                    SC_Static.data = SC_Records;
                                }
                            }
                        }
                    }
                }
                if (SC_Records.Count > 0 || SC_Static.success == "TRUE")
                {
                    return SC_Static;
                }
                else
                {
                    return new SoliCatList_Static { success = "false", message = "Something went wrong. Please check the data" };
                }
            }
            catch (SqlException sqlEx)
            {
                return new SoliCatList_Static { success = "false", message = sqlEx.Message };
            }
        }
    }
}
