using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckInSKP.Domain.Entities;

namespace CheckInSKP.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(Domain.Entities.User user, Domain.Entities.Device device);
    }
}
