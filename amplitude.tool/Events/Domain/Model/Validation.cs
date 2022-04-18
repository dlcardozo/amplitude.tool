using System;
using System.Collections.Generic;
using System.Linq;
using amplitude.tool.Utilities;

namespace amplitude.tool.Events.Domain.Model
{
    public struct Validation
    {
        public readonly string EventName;
        public readonly bool IsValid;
        public Dictionary<string, object> EventProperties { get; }

        public Validation(string eventName, bool isValid, Dictionary<string, object> eventProperties)
        {
            EventName = eventName;
            IsValid = isValid;
            EventProperties = eventProperties;
        }

        public bool Equals(Validation other) => 
            EventName == other.EventName && 
            IsValid == other.IsValid && 
            EventProperties.SafeSequenceEqual(other.EventProperties);

        public override bool Equals(object obj) => obj is Validation other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(EventName, IsValid, EventProperties);

        public override string ToString() => 
            $"Validation: " +
            $"{EventName}, " +
            $"{IsValid}, " +
            $"[{EventPropertiesToString()}]";

        public string EventPropertiesToString() => 
            EventProperties != null 
                ? String.Join(", ", EventProperties.Select(x => $"{x.Key} - {x.Value}").ToList()) 
                : "[]";
    }
}