using System;
using System.Collections.Generic;
using System.Reactive;
using amplitude.tool.Events.Domain.Model;

namespace amplitude.tool.Events.Domain.Repositories
{
    public interface ExpectedEventsRepository
    {
        IObservable<List<ExpectedEvent>> Fetch();
        IObservable<Unit> AddEvents(List<ExpectedEvent> newEvents);
    }
}