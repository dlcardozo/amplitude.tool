using System;
using System.Collections.Generic;

namespace amplitude.tool.tests.Events.Actions
{
    public interface ExpectedEventsRepository
    {
        IObservable<List<ExpectedEvent>> Fetch();
    }
}