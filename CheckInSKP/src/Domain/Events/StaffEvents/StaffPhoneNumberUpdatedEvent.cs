using CheckInSKP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.StaffEvents
{
    public class StaffPhoneNumberUpdatedEvent : DomainEvent
    {
        public int UserId { get; }
        public string PhoneNumber { get; }
        public StaffPhoneNumberUpdatedEvent(int userId, string phoneNumber)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
        }
    }
}
