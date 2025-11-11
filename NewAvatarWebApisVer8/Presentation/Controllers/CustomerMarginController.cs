using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CustomerMarginController : ControllerBase
    {
        private readonly ICustomerMarginService _customerMarginService;

        public CustomerMarginController(ICustomerMarginService customerMarginService)
        {
            _customerMarginService = customerMarginService;
        }

        [HttpGet("get-franchise-list")]
        public ServiceResponse<ResponseDetails> GetFranchiseList()
        {
            var result = _customerMarginService.GetFranchiseList();
            return result;
        }

        [HttpGet("get-customer-margin-data")]
        public ServiceResponse<ResponseDetails> GetCustomerMarginData(int dataid)
        {
            var result = _customerMarginService.GetCustomerMarginData(dataid);
            return result;
        }

        [HttpPost("customer-margin-add-update")]
        public ServiceResponse<ResponseDetails> CustomerMargin_AddUpdate(CustomerMarginAddUpdateParams customermargincrud_params)
        {
            var result = _customerMarginService.CustomerMargin_AddUpdate(customermargincrud_params);
            return result;
        }

        [HttpGet("get-usertypes-for-customer-margin-dist")]
        public ServiceResponse<ResponseDetails> GetUserTypesForCustomerMarginDist()
        {
            var result = _customerMarginService.GetUserTypesForCustomerMarginDist();
            return result;
        }

        [HttpGet("get-pptags-for-customer-margin-dist")]
        public ServiceResponse<ResponseDetails> GetPPTagsForCustomerMarginDist()
        {
            var result = _customerMarginService.GetPPTagsForCustomerMarginDist();
            return result;
        }

        [HttpGet("get-isdisplay-new-margin-for-customer-margin-dist")]
        public ServiceResponse<ResponseDetails> GetIsDisplayNewMarginForCustomerMarginDist(int current_admin_dataid)
        {
            var result = _customerMarginService.GetIsDisplayNewMarginForCustomerMarginDist(current_admin_dataid);
            return result;
        }

        [HttpGet("get-users-from-usertype")]
        public ServiceResponse<ResponseDetails> GetUsersFromUserType(int usertypeid)
        {
            var result = _customerMarginService.GetUsersFromUserType(usertypeid);
            return result;
        }

        [HttpGet("get-customer-margin-dist-data")]
        public ServiceResponse<ResponseDetails> GetCustomerMarginDistData(int dataid)
        {
            var result = _customerMarginService.GetCustomerMarginDistData(dataid);
            return result;
        }

        [HttpPost("customer-margin-dist-add-update")]
        public ServiceResponse<ResponseDetails> CustomerMargindist_AddUpdate(CustomerMarginDistAddUpdateParams customermargindistcrud_params)
        {
            var result = _customerMarginService.CustomerMargindist_AddUpdate(customermargindistcrud_params);
            return result;
        }
    }
}
