using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.StaffEvents
{
    public class MeetingTimeUpdatedEvent
    {
        public int StaffId { get; }
        public DateTime MeetingTime { get; }

        public MeetingTimeUpdatedEvent(int staffId, DateTime meetingTime)
        {
            StaffId = staffId;
            MeetingTime = meetingTime;
        }
    }
}
