using System.Collections.Generic;
using amplitude.tool.Users.Domain.Model;
using static amplitude.tool.tests.Users.Mothers.TrackedEventMother;

namespace amplitude.tool.tests.Users.Mothers
{
    public static class UserActivityMother
    {
        public static UserActivity AnUserActivity(List<TrackedEvent> withTrackedEvents = null) => 
            new UserActivity(
                withTrackedEvents ?? new List<TrackedEvent>()
            );

        public static UserActivity AnUserActivityWithOne(TrackedEvent? withTrackedEvent = null) =>
            AnUserActivity(
                new List<TrackedEvent> { withTrackedEvent ?? ATrackedEvent() }
            );
    }
}