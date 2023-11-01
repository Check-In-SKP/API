using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Services.Interfaces
{
    public interface IDeviceAuthorizationService
    {
        Task AuthorizeDeviceAsync(Guid deviceId, int userId);
        Task DeauthorizeDeviceAsync(Guid deviceId, int userId);
    }
}
