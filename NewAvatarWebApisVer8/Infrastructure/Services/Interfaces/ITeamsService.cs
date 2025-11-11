using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface ITeamsService
    {
        public ServiceResponse<IList<MasterTeams>> GetAllTeams(string dataid);

        public ServiceResponse<bool> AddItems(MasterTeams team);

        public ServiceResponse<bool> EditItems(MasterTeams team);

        public ServiceResponse<bool> DisableTeams(MasterTeams team);
    }
}
