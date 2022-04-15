using System;
using System.Collections.Generic;
using System.Reactive;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Domain.Repositories;

namespace amplitude.tool.Events.Domain.Actions
{
    public class AddEvents
    {
        readonly ExpectedEventsRepository expectedEventsRepository;

        public AddEvents(ExpectedEventsRepository expectedEventsRepository)
        {
            this.expectedEventsRepository = expectedEventsRepository;
        }

        public IObservable<Unit> Do(List<ExpectedEvent> newEvents) => 
            expectedEventsRepository.AddEvents(newEvents);
    }
}