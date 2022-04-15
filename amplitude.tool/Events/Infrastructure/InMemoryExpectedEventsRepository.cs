using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Domain.Repositories;

namespace amplitude.tool.Events.Infrastructure
{
    public class InMemoryExpectedEventsRepository : ExpectedEventsRepository
    {
        List<ExpectedEvent> expectedEvents;

        public InMemoryExpectedEventsRepository(List<ExpectedEvent> expectedEvents)
        {
            this.expectedEvents = expectedEvents;
        }

        public IObservable<List<ExpectedEvent>> Fetch() => Observable.Return(expectedEvents);
    }
}