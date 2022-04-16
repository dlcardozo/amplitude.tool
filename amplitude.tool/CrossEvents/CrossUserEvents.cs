using System.Collections.Generic;

namespace amplitude.tool.CrossEvents
{
    public struct CrossUserEvents
    {
        public List<string> Events { get; }

        public CrossUserEvents(List<string> events)
        {
            Events = events;
        }
    }
}