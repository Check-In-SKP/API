using CheckInSKP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.UserEvents
{
    public class AdminLoggedInEvent : DomainEvent
    {
        public Guid UserId { get; }
        public int RoleId { get; }
        public AdminLoggedInEvent(Guid userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
