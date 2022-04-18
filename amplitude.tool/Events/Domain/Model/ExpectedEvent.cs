using System.Collections.Generic;
using System.Linq;
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

        public bool HasExpectedProperties(Dictionary<string, object> eventProperties) =>
            EventProperties.All(x => eventProperties.ContainsKey(x.Key) && eventProperties[x.Key].Equals(x.Value));
    }
}