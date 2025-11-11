using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Models;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface ICustomerMarginService
    {
        public ServiceResponse<ResponseDetails> GetFranchiseList();

        public ServiceResponse<ResponseDetails> GetCustomerMarginData(int dataid);

        public ServiceResponse<ResponseDetails> CustomerMargin_AddUpdate(CustomerMarginAddUpdateParams customermargincrud_params);

        public ServiceResponse<ResponseDetails> GetUserTypesForCustomerMarginDist();

        public ServiceResponse<ResponseDetails> GetPPTagsForCustomerMarginDist();

        public ServiceResponse<ResponseDetails> GetIsDisplayNewMarginForCustomerMarginDist(int current_admin_dataid);

        public ServiceResponse<ResponseDetails> GetUsersFromUserType(int usertypeid);

        public ServiceResponse<ResponseDetails> GetCustomerMarginDistData(int dataid);

        public ServiceResponse<ResponseDetails> CustomerMargindist_AddUpdate(CustomerMarginDistAddUpdateParams customermargindistcrud_params);
    }
}
