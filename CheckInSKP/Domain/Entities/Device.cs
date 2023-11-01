using CheckInSKP.Domain.Common;
using CheckInSKP.Domain.Events.DeviceEvents;
using System.ComponentModel.DataAnnotations;

namespace CheckInSKP.Domain.Entities
{
    public class Device : DomainEntity
    {
        private readonly Guid _id;
        public Guid Id => _id;

        [Required, StringLength(64)]
        public string Label { get; private set; }

        public bool IsAuthorized { get; private set; }

        // Constructor for new Device
        public Device(string? label)
        {
            label ??= "Unknown";

            ValidateInput(label);

            Label = label;
            IsAuthorized = false;
        }

        // Constructor for existing Device
        public Device(Guid id, string label, bool authorized)
        {
            ValidateInput(label);

            _id = id;
            Label = label;
            IsAuthorized = authorized;
        }

        private void ValidateInput(string label)
        {
            if (string.IsNullOrWhiteSpace(label) || label.Length > 64)
                throw new ArgumentException("Invalid label.", nameof(label));
        }

        public void UpdateLabel(string newLabel)
        {
            if (string.IsNullOrWhiteSpace(newLabel) || newLabel.Length > 64)
                throw new ArgumentException("Invalid new device label.", nameof(newLabel));

            Label = newLabel;
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
