using CheckInSKP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.DeviceEvents
{
    public class DeviceDeauthorizedEvent : DomainEvent
    {
        public DeviceDeauthorizedEvent(Guid deviceId)
        {
            DeviceId = deviceId;
        }
        public Guid DeviceId { get; }
    }
}
