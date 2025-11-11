using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class ItemViewService : IItemViewService
    {
        public string _Connection = DBCommands.CONNECTION_STRING;

        public async Task<ItemViewItemListResponse> ItemList(ItemViewItemListParams request)
        {
            var response = new ItemViewItemListResponse();

            try
            {
                response = new ItemViewItemListResponse
                {
                    success = true,
                    message = "Successfully",
                    current_page = 1,
                    last_page = 175,
                    total_items = 20,
                    data = new List<ItemViewItemData>()
                };

                response.data.Add(new ItemViewItemData
                {
                    view_id = "12168010",
                    item_soliter = "N",
                    ItemDisLabourPer = "0.0",
                    ItemAproxDay = "15 DAYS",
                    labour_per = "0.0",
                    plaingold_status = "N",
                    cat_id = "4",
                    data_id = "4596",
                    item_id = "352624",
                    item_code = "KR12882",
                    item_name = "KR12882",
                    item_sku = "KR12882KSN-FG-R8",
                    item_description = "Sun Spring Ring",
                    item_mrp = "35522",
                    item_price = "38983",
                    dist_price = "31233",
                    image_path = "https://assets.kisna.com/public/uploads/Product/KR12882.png",
                    image_thumb_path = "https://assets.kisna.com/public/uploads/image.png",
                    dsg_sfx = "KSN-FG-R8",
                    dsg_size = "",
                    dsg_kt = "18KT",
                    dsg_color = "P",
                    star = "0",
                    cart_img = "https://force.kisna.com/HKDB/public/assets/Icons/cart.png",
                    img_cart_title = "Cart",
                    watch_img = "https://force.kisna.com/HKDB/public/assets/Icons/watch.png",
                    img_watch_title = "Watch",
                    wish_count = "0",
                    wearit_count = "0",
                    wearit_status = "N",
                    wearit_img = "https://force.kisna.com/HKDB/public/assets/Icons/wearit.png",
                    wearit_none_img = "https://force.kisna.com/HKDB/public/assets/Icons/wearitnone.png",
                    wearit_color = "#3fa63f",
                    wearit_text = "Image not available",
                    img_wearit_title = "Wear",
                    wish_default_img = "https://force.kisna.com/HKDB/public/assets/Icons/Wishlist_hand default.png",
                    wish_fill_img = "https://force.kisna.com/HKDB/public/assets/Icons/Wishlist_hand fill.png",
                    img_wish_title = "Wish",
                    item_review = "0",
                    item_size = "",
                    item_kt = "18KT",
                    item_color = "P",
                    item_metal = "Gold",
                    item_wt = "2.3799999999999999",
                    item_stone = "Diamond",
                    item_stone_wt = "0.10000000000000001",
                    item_stone_qty = "21",
                    price_text = "MRP: ₹ 46000/-",
                    cart_price = "46000.0",
                    item_color_id = "1575",
                    item_details = "Metal: Gold,  KT: 18KT",
                    item_diamond_details = "Stone: Diamond, Qty: 21,  Quality: VVS-FG,  Shape: ROUND",
                    item_text = "KR12882 (Sun Spring Ring)",
                    more_item_details = "Stock detail Coming Soon...",
                    item_stock = "Metal: Gold,  KT: 18KT, Brand: KISNA FG\nStone: Diamond, Quality: VVS-FG,  Shape: ROUND",
                    item_removecart_img = "https://force.kisna.com/HKDB/public/assets/Icons/remove item from cart new.png",
                    item_removecard_title = "Remove",
                    rupy_symbol = "₹",
                    recent_view = " Yesterday ",
                    category_id = "4",
                    cart_id = null,
                    ItemGenderCommonID = "252",
                    item_stock_qty = "0",
                    item_stock_colorsize_qty = "0",
                    variantCount = "5",
                    ItemTypeCommonID = "112",
                    ItemNosePinScrewSts = "N",
                    ItemFranchiseSts = "N",
                    item_illumine = "N",
                    isSolitaireOtherCollection = "N",
                    productTags = new List<ItemViewProductTag> { new ItemViewProductTag { tag_name = "Esme", tag_color = "#f542da" } },
                    weight = "Metal Weight: 2.38 , Diamond Weight :0.1 , Approx Delivery: 15 DAYS.",
                    totalLabourPer = "",
                    selectedColor = 1573,
                    selectedSize = 1522,
                    selectedColor1 = "",
                    selectedSize1 = "",
                    field_name = "Size",
                    color_name = "Color",
                    default_color_name = "Default",
                    sizeList = new List<ItemViewSizeList>{
                        new ItemViewSizeList{ product_size_mst_id="2092", product_size_mst_code="3", product_size_mst_name="R3", product_size_mst_desc="S3  |  CP-5  |  P-03" }
                    },
                    colorList = new List<ItemViewColorList>{
                        new ItemViewColorList{ product_color_mst_id="1573", product_color_mst_code="Y", product_color_mst_name="Yellow" },
                        new ItemViewColorList{ product_color_mst_id="1574", product_color_mst_code="W", product_color_mst_name="White" },
                        new ItemViewColorList{ product_color_mst_id="1575", product_color_mst_code="P", product_color_mst_name="Pink" }
                    },
                    itemsColorSizeList = new List<ItemViewColorSizeList>(),
                    itemOrderInstructionList = new List<ItemViewItemInstruction>{
                        new ItemViewItemInstruction{ item_instruction_mst_id="2175", item_instruction_mst_code="Big Loop", item_instruction_mst_name="Big Loop" }
                    },
                    itemOrderCustomInstructionList = new List<ItemViewItemInstruction>{
                        new ItemViewItemInstruction{ item_instruction_mst_id="2192", item_instruction_mst_code="Mangalsutra", item_instruction_mst_name="Mangalsutra" }
                    },
                    item_images_color = new List<ItemViewItemImagesColor>{
                        new ItemViewItemImagesColor{
                            color_id="1575",
                            color_image_details=new List<ItemViewColorImageDetails>{
                                new ItemViewColorImageDetails{
                                    image_view_name="Image",
                                    image_path="https://assets.kisna.com/public/uploads/Product/KR12882.png",
                                    image_thumb_path="https://assets.kisna.com/public/uploads/image.png",
                                    color_id="1575"
                                }
                            }
                        }
                    }
                });

                return response;
            }
            catch (SqlException sqlEx)
            {
                response.success = false;
                response.message = $"SQL error: {sqlEx.Message}";
                response.current_page = 1;
                response.last_page = 1;
                response.total_items = 0;
                response.data = new List<ItemViewItemData>();
                return response;
            }
        }
    }
}
