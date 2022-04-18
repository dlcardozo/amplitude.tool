using System.Collections.Generic;
using amplitude.tool.Events.Domain.Model;

namespace amplitude.tool.tests.Events.Actions
{
    public static class ValidationMother
    {
        public static Validation AValidation(string withEventName = "",
            bool withIsValid = false, 
            Dictionary<string, object> withEventProperties = null
        ) => new Validation(withEventName, withIsValid, withEventProperties);
    }
}