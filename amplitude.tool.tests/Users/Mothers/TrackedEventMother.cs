using System.Collections.Generic;
using amplitude.tool.Users.Domain.Model;

namespace amplitude.tool.tests.Users.Mothers
{
    public static class TrackedEventMother
    {
        public static TrackedEvent ATrackedEvent(string withName = null, Dictionary<string, object> withProperties = null) => 
            new TrackedEvent(withName ?? "", withProperties ?? new Dictionary<string, object>());
    }
}