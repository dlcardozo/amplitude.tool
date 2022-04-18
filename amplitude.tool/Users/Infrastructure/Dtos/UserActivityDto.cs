using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Utilities;

namespace amplitude.tool.Users.Infrastructure.Dtos
{
    [Serializable]
    public class UserActivityDto
    {
        public IList<EventDto> events { get; set; }

        public UserActivity ToUserActivity() => 
            new UserActivity(events
                .Select(x => new TrackedEvent(x.event_type, CastProperties(x)))
                .ToList()
            );

        static Dictionary<string, object> CastProperties(EventDto eventDto) =>
            eventDto.event_properties
                .Select(tuple => new KeyValuePair<string, object>(tuple.Key, CastToValue(tuple)))
                .ToDictionary(x => x.Key, x => x.Value);

        static object CastToValue(KeyValuePair<string, object> tuple) => CastExtension.CastToValue(tuple.Value.ToString());
    }
}