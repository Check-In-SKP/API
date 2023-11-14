using CheckInSKP.Domain.Common;
using CheckInSKP.Domain.Events.DeviceEvents;
using System.ComponentModel.DataAnnotations;

namespace CheckInSKP.Domain.Entities
{
    public class Device : DomainEntity
    {
        private readonly Guid _id;
        public Guid Id => _id;

        [MaxLength(64)]
        public string Label { get; private set; }

        public bool IsAuthorized { get; private set; }

        // Constructor for new Device
        public Device(string? label)
        {
            Label = ValidateInput(label);
            IsAuthorized = false;
        }

        // Constructor for existing Device
        public Device(Guid id, string label, bool authorized)
        {
            _id = id;
            Label = label;
            IsAuthorized = authorized;
        }

        private string ValidateInput(string? label)
        {
            if (string.IsNullOrWhiteSpace(label) || label.Length > 64)
                return "unknown";

            return label;
        }

        public void UpdateLabel(string newLabel)
        {
            Label = ValidateInput(newLabel);
        }

        public void Authorize()
        {
            IsAuthorized = true;

            AddDomainEvent(new DeviceAuthorizedEvent(Id));
        }

        public void Deauthorize()
        {
            IsAuthorized = false;

            AddDomainEvent(new DeviceDeauthorizedEvent(Id));
        }
    }
}
