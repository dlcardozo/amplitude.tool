using System.Collections.Generic;
using amplitude.tool.Utilities;

namespace amplitude.tool.Events.Domain.Model
{
    public struct ExpectedEvent
    {
        public string EventName { get; }
        public Dictionary<string, object> EventProperties { get; }

        public ExpectedEvent(string eventName, Dictionary<string, object> eventProperties)
        {
            EventName = eventName;
            EventProperties = eventProperties;
        }

        public bool HasEventProperties() => EventProperties.Count <= 0;

        public bool HasSameProperties(Dictionary<string, object> eventProperties) => 
            EventProperties.SafeSequenceEqual(eventProperties);
    }
}