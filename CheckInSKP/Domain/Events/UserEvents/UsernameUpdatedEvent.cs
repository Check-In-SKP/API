using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.UserEvents
{
    public class UsernameUpdatedEvent
    {
        public int UserId { get; }
        public string Username { get; }
        public UsernameUpdatedEvent(int userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}
