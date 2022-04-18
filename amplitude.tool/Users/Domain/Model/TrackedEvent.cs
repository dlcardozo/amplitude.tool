using System;
using System.Collections.Generic;
using amplitude.tool.Utilities;

namespace amplitude.tool.Users.Domain.Model
{
    public struct TrackedEvent
    {
        public readonly string Name;
        public readonly Dictionary<string, object> Properties;

        public TrackedEvent(string name, Dictionary<string, object> properties)
        {
            Name = name;
            Properties = properties;
        }

        bool Equals(TrackedEvent other) => 
            Name == other.Name && 
            Properties.SafeSequenceEqual(other.Properties);

        public override bool Equals(object obj) => obj is TrackedEvent other && Equals(other);
    }
}