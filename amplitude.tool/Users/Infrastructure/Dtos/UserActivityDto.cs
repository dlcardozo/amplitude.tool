using System;
using System.Collections.Generic;
using System.Linq;
using amplitude.tool.Users.Domain.Model;

namespace amplitude.tool.Users.Infrastructure.Dtos
{
    [Serializable]
    public class UserActivityDto
    {
        public IList<EventDto> events { get; set; }

        public UserActivity ToUserActivity() => 
            new UserActivity(events
                .Select(x => new TrackedEvent(x.event_type))
                .ToList()
            );
    }
}