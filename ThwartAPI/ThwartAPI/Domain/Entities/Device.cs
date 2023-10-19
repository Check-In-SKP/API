namespace ThwartAPI.Domain.Entities
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public bool Authorized { get; set; }

        public Device(string label, bool authorized)
        {
            if (string.IsNullOrEmpty(label) || label.Length > 64)
            {
                throw new ArgumentException("Invalid device label.");
            }

            Label = label;
            Authorized = authorized;
        }

        public void UpdateLabel(string newLabel)
        {
            if (string.IsNullOrEmpty(newLabel) || newLabel.Length > 64)
            {
                throw new ArgumentException("Invalid new device label.");
            }

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
