using System;
using System.Collections.Generic;

namespace amplitude.tool.Users.Infrastructure.Dtos
{
    [Serializable]
    public class EventDto
    {
        public long event_id { get; set; }
        public string event_type { get; set; }
        // public Dictionary<string, string> event_properties { get; set; }
        // public Dictionary<string, string> user_properties { get; set; }
    }
}