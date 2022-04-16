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
            .Select(expectedEvents => ValidateEvents(expectedEvents, RemoveDuplicatesFrom(events)))
            .Subscribe(validatedEvents => onValidated.OnNext(new ValidatedEvents(validatedEvents)));

        static List<Validation> ValidateEvents(List<ExpectedEvent> expectedEvents, Dictionary<string, string> events) =>
            expectedEvents
                .Select(expectedEvent => new Validation(expectedEvent.EventName, events.ContainsKey(expectedEvent.EventName)))
                .ToList();

        static Dictionary<string, string> RemoveDuplicatesFrom(List<string> events) =>
            events
                .GroupBy(eventName => eventName)
                .Select(eventDuple => eventDuple.Key)
                .ToDictionary(eventName => eventName);
    }
}