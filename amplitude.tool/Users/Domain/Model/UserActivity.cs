using System.Collections.Generic;
using amplitude.tool.Utilities;

namespace amplitude.tool.Users.Domain.Model
{
    public struct UserActivity
    {
        public readonly List<TrackedEvent> TrackedEvents;

        public UserActivity(List<TrackedEvent> trackedEvents)
        {
            TrackedEvents = trackedEvents;
        }

        public override bool Equals(object obj) => 
            obj is UserActivity other && Equals(other);

        bool Equals(UserActivity other) =>
            TrackedEvents.SafeSequenceEqual(other.TrackedEvents);
    }
}