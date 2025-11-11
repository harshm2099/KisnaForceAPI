using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Models;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IItemViewService
    {
        public Task<ResponseDetails> ItemList(ItemViewItemListParams request);
    }
}
