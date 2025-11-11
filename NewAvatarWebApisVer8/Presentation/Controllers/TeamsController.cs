using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpGet("get-teams")]
        public ServiceResponse<IList<MasterTeams>> GetAllTeams(string dataid)
        {
            var result = _teamsService.GetAllTeams(dataid);
            return result;
        }

        [HttpPost("add-team")]
        public ServiceResponse<bool> AddItems(MasterTeams team)
        {
            var result = _teamsService.AddItems(team);
            return result;
        }

        [HttpPost("edit-team")]
        public ServiceResponse<bool> EditItems(MasterTeams team)
        {
            var result = _teamsService.EditItems(team);
            return result;
        }

        [HttpPost("disable-team")]
        public ServiceResponse<bool> DisableTeams(MasterTeams team)
        {
            var result = _teamsService.DisableTeams(team);
            return result;
        }
    }
}
