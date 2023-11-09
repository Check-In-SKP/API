using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Infrastructure.Services.Interfaces
{
    public interface ITokenValidationService
    {
        Task<bool> UserHasValidRole(int userId, params int[] roleIds);
        Task<bool> ValidateUserClaims(int userId, string username, int roleClaim);
        Task<bool> DeviceIsAuthorized(Guid deviceId);
    }
}
