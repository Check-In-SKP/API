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
        public int StaffId { get; }
        public TimeOnly MeetingTime { get; }

        public StaffMeetingTimeUpdatedEvent(int staffId, TimeOnly meetingTime)
        {
            StaffId = staffId;
            MeetingTime = meetingTime;
        }
    }
}
