using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Domain.Repositories;

namespace amplitude.tool.Events.Domain.Actions
{
    public class Validate
    {
        readonly ISubject<ValidatedEvents> onValidated;
        readonly ExpectedEventsRepository expectedEventsRepository;

        public Validate(ISubject<ValidatedEvents> onValidated,
            ExpectedEventsRepository expectedEventsRepository)
        {
            this.onValidated = onValidated;
            this.expectedEventsRepository = expectedEventsRepository;
        }

        public void Do(List<string> events) => expectedEventsRepository.Fetch()
            .Select(expectedEvents => expectedEvents.ToDictionary(expectedEvent => expectedEvent.EventName))
            .Select(expectedEvents => ValidateEvents(events, expectedEvents))
            .Subscribe(validatedEvents => onValidated.OnNext(new ValidatedEvents(validatedEvents)));

        static List<Validation> ValidateEvents(List<string> events, Dictionary<string, ExpectedEvent> expectedEvents) =>
            events
                .Select(@event => new Validation(@event, expectedEvents.ContainsKey(@event)))
                .ToList();
    }
}