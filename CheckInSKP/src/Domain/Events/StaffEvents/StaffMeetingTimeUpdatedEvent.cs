using CheckInSKP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.StaffEvents
{
    public class StaffMeetingTimeUpdatedEvent : DomainEvent
    {
        public Guid UserId { get; }
        public TimeOnly MeetingTime { get; }

        public StaffMeetingTimeUpdatedEvent(Guid userId, TimeOnly meetingTime)
        {
            UserId = userId;
            MeetingTime = meetingTime;
        }
    }
}
