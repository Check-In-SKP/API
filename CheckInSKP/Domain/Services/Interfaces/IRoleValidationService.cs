using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Services.Interfaces
{
    public interface IRoleValidationService
    {
        Task<bool> UserHasValidRole(int userId, int roleId);
    }
}
