using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.UserEvents
{
    public class RoleUpdatedEvent
    {
        public int UserId { get; }
        public string Role { get; }
        public RoleUpdatedEvent(int userId, string role)
        {
            UserId = userId;
            Role = role;
        }
    }
}
