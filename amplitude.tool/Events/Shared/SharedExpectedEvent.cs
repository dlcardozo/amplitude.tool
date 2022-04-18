using System.Collections.Generic;

namespace amplitude.tool.Events.Shared
{
    public struct SharedExpectedEvent
    {
        public string EventName { get; set; }

        public Dictionary<string, object> EventProperties { get; set; }

        public SharedExpectedEvent(string eventName, Dictionary<string, object> eventProperties)
        {
            EventName = eventName;
            EventProperties = eventProperties;
        }
    }
    
    public struct SharedValidateEvent
    {
        public string EventName { get; set; }

        public Dictionary<string, object> EventProperties { get; set; }

        public SharedValidateEvent(string eventName, Dictionary<string, object> eventProperties)
        {
            EventName = eventName;
            EventProperties = eventProperties;
        }
    }
}