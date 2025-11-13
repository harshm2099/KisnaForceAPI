using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        public ExcelService()
        {
            // CORRECT WAY: Use OfficeOpenXml.LicenseContext directly
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<FileResponse> CreatePieceVerifyExcelAsync(IList<PieceVerifyExcelResponse> data)
        {
            try
            {
                var fileName = $"PieceVerify_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.xlsx";
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "PieceVerify");

                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var filePath = Path.Combine(uploadsPath, fileName);

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet");

                    var headers = new string[]
                    {
                        "Date", "Item Name", "Item Desc", "Bag Diamond Quality", "Current Diamond Quality",
                        "Bag MRP", "Current MRP", "Bag Brand", "Current Brand", "Bag Quantity",
                        "Current Quantity", "Bag Total Weight", "Current Total Weight",
                        "Bag Diamond Weight", "Current Diamond Weight", "IGINo", "Bagno",
                        "HUID", "Lab", "ItemIsSRP", "COCD"
                    };

                    // Add headers with styling
                    for (int i = 0; i < headers.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = headers[i];
                        worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                    // Add data
                    if (data != null && data.Any())
                    {
                        int row = 2;
                        foreach (var item in data)
                        {
                            worksheet.Cells[row, 1].Value = item.Date;
                            worksheet.Cells[row, 2].Value = item.ItemName;
                            worksheet.Cells[row, 3].Value = item.ItemDesc;
                            worksheet.Cells[row, 4].Value = item.BagDiamondQuality;
                            worksheet.Cells[row, 5].Value = item.CurrentDiamondQuality;
                            worksheet.Cells[row, 6].Value = item.BagMRP;
                            worksheet.Cells[row, 7].Value = item.CurrentMRP;
                            worksheet.Cells[row, 8].Value = item.BagBrand;
                            worksheet.Cells[row, 9].Value = item.CurrentBrand;
                            worksheet.Cells[row, 10].Value = item.BagQuantity;
                            worksheet.Cells[row, 11].Value = item.CurrentQuantity;
                            worksheet.Cells[row, 12].Value = item.BagTotalWeight;
                            worksheet.Cells[row, 13].Value = item.CurrentTotalWeight;
                            worksheet.Cells[row, 14].Value = item.BagDiamondWeight;
                            worksheet.Cells[row, 15].Value = item.CurrentDiamondWeight;
                            worksheet.Cells[row, 16].Value = item.IGINo;
                            worksheet.Cells[row, 17].Value = item.Bagno;
                            worksheet.Cells[row, 18].Value = item.HUID;
                            worksheet.Cells[row, 19].Value = item.Lab;
                            worksheet.Cells[row, 20].Value = item.ItemIsSRP;
                            worksheet.Cells[row, 21].Value = item.COCD;
                            row++;
                        }

                        // Auto-fit columns
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    }

                    // Save file
                    var fileBytes = package.GetAsByteArray();
                    await File.WriteAllBytesAsync(filePath, fileBytes);
                }

                return new FileResponse
                {
                    Success = true,
                    Message = "Excel file created successfully",
                    FilePath = $"/uploads/PieceVerify/{fileName}",
                    FileName = fileName
                };
            }
            catch (Exception ex)
            {
                return new FileResponse
                {
                    Success = false,
                    Message = $"Error creating Excel file: {ex.Message}",
                    FilePath = string.Empty,
                    FileName = string.Empty
                };
            }
        }
    }
}