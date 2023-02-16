

namespace ValheimMod
{
    public struct Pin
    {
        public enum PinType
        {
            Mineral,
            Structure,
            Spawner,
            Undefined = default
        }

        public Pin(string label, string compName, bool save, bool name, bool enabled, PinType type)
        {
            Label = label;
            ComponentName = compName;
            Save = save;
            PrintName = name;
            Enabled = enabled;
            Type = type;
        }

        public string Label { get; }
        public string ComponentName { get; }
        public bool Save { get; set; }
        public bool PrintName { get; set; }
        public bool Enabled { get; set; }
        public PinType Type { get; set; }

    }
}