using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Domain.Repositories;
using amplitude.tool.Events.Shared;
using amplitude.tool.Utilities;

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

        public void Do(List<SharedValidateEvent> events) => expectedEventsRepository.Fetch()
            .Select(expectedEvents => ValidateEvents(expectedEvents, events))
            .Subscribe(validatedEvents => onValidated.OnNext(new ValidatedEvents(validatedEvents)));

        static List<Validation> ValidateEvents(List<ExpectedEvent> expectedEvents, List<SharedValidateEvent> events) =>
            expectedEvents
                .Select(expectedEvent => CreateValidation(events, expectedEvent))
                .ToList();

        static Validation CreateValidation(List<SharedValidateEvent> events, ExpectedEvent expectedEvent) =>
            new Validation(
                expectedEvent.EventName,
                events.Any(@event =>
                    @event.EventName.Equals(expectedEvent.EventName) && HasExpectedProperties(expectedEvent, @event)),
                expectedEvent.EventProperties
            );

        static bool HasExpectedProperties(ExpectedEvent expectedEvent, SharedValidateEvent @event) =>
            expectedEvent.HasEventProperties() || expectedEvent.HasExpectedProperties(@event.EventProperties);
    }
}