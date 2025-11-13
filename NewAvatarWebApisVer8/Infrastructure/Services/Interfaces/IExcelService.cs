using NewAvatarWebApis.Core.Application.Responses;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IExcelService
    {
        Task<FileResponse> CreatePieceVerifyExcelAsync(IList<PieceVerifyExcelResponse> data);
    }
}
