using System.ComponentModel.DataAnnotations;

namespace CheckInSKP.Domain.Entities
{
    public class Device
    {
        private readonly Guid _id;
        public Guid Id => _id;

        [Required, StringLength(64)]
        public string Label { get; private set; }

        public bool Authorized { get; private set; }

        // Constructor for new Device
        public Device(string label, bool authorized)
        {
            ValidateInput(label);

            Label = label;
            Authorized = authorized;
        }

        // Constructor for existing Device
        public Device(Guid id, string label, bool authorized)
        {
            ValidateInput(label);

            _id = id;
            Label = label;
            Authorized = authorized;
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
            Authorized = true;
        }

        public void Deauthorize()
        {
            Authorized = false;
        }
    }
}
