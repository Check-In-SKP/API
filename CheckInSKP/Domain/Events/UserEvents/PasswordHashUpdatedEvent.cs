using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.UserEvents
{
    public class PasswordHashUpdatedEvent
    {
        public int UserId { get; }
        public string PasswordHash { get; }
        public PasswordHashUpdatedEvent(int userId, string passwordHash)
        {
            UserId = userId;
            PasswordHash = passwordHash;
        }
    }
}
