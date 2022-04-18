using System.Collections.Generic;

namespace amplitude.tool.CrossEvents
{
    public struct CrossUserEvents
    {
        public List<CrossEvent> Events { get; }

        public CrossUserEvents(List<CrossEvent> events)
        {
            Events = events;
        }
    }

    public struct CrossEvent
    {
        public string EventName { get; set; }

        public Dictionary<string, object> EventProperties { get; set; }

        public CrossEvent(string eventName, Dictionary<string, object> eventProperties)
        {
            EventName = eventName;
            EventProperties = eventProperties;
        }
    }
}