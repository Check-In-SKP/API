using ThwartAPI.Domain.Entities;

namespace ThwartAPI.Domain.Factories
{
    public class DeviceFactory
    {
        public Device CreateDevice(Guid id, string label, bool authorized)
        {
            return new Device(id, label, authorized);
        }

        public Device CreateNewDevice(string label, bool authorized)
        {
            return new Device(label, authorized);
        }
    }
}
