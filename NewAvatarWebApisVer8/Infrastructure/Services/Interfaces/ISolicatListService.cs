using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using static NewAvatarWebApis.Core.Application.DTOs.SoliCatList;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface ISolicatListService
    {
        public Task<SoliCatList_Static> GetSolitaireCategoryList(SoliCatList SCUser);
    }
}
