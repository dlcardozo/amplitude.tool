using System.Collections.Generic;

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
    }
}