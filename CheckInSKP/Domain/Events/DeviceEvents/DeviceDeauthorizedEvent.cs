using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.DeviceEvents
{
    public class DeviceDeauthorizedEvent
    {
        public DeviceDeauthorizedEvent(string deviceId)
        {
            DeviceId = deviceId;
        }
        public string DeviceId { get; }
    }
}
