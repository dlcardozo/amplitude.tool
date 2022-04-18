using System.Collections.Generic;
using amplitude.tool.Events.Domain.Model;

namespace amplitude.tool.tests.Events.Mothers
{
    public static class ExpectedEventMother
    {
        public static ExpectedEvent AnExpectedEvent(
            string withEventName = "",
            Dictionary<string, object> withEventProperties = null
        ) =>
            new ExpectedEvent(withEventName, withEventProperties ?? new Dictionary<string, object>());
    }
}