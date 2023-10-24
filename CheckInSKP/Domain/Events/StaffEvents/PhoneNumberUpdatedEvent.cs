using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.StaffEvents
{
    public class PhoneNumberUpdatedEvent
    {
        public int StaffId { get; }
        public string PhoneNumber { get; }
        public PhoneNumberUpdatedEvent(int staffId, string phoneNumber)
        {
            StaffId = staffId;
            PhoneNumber = phoneNumber;
        }
    }
}
