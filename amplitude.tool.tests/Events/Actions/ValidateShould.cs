using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NUnit.Framework;
using static amplitude.tool.tests.Mothers.EventMother;
using static BDD.Context;

namespace amplitude.tool.tests.Events.Actions
{
    [TestFixture]
    public class ValidateShould
    {
        [Test]
        public void ProcessAnEmptyListOfEvents()
        {
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents();
            var expectedEventsRepository = new InMemoryExpectedEventsRepository(new List<ExpectedEvent>());

            Given(new Validate(onValidated, expectedEventsRepository))
                .When(action => action.Do(new List<string>()))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }
        
        [Test]
        public void ProcessAListOfEvents()
        {
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents(new List<Validation>{new Validation("Events", false)});
            var events = new List<string> { "Events" };
            var expectedEventsRepository = new InMemoryExpectedEventsRepository(new List<ExpectedEvent>());
            
            Given(new Validate(onValidated, expectedEventsRepository))
                .When(action => action.Do(events))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }

        [Test]
        public void CompareAPreLoadedListOfEvents()
        {
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents(new List<Validation>{new Validation("Events", true)});
            var events = new List<string> { "Events" };
            var expectedEventsRepository = new InMemoryExpectedEventsRepository(new List<ExpectedEvent>{new ExpectedEvent("Events")});
            
            Given(new Validate(onValidated, expectedEventsRepository))
                .When(action => action.Do(events))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }
    }

    public class InMemoryExpectedEventsRepository : ExpectedEventsRepository
    {
        List<ExpectedEvent> expectedEvents;

        public InMemoryExpectedEventsRepository(List<ExpectedEvent> expectedEvents)
        {
            this.expectedEvents = expectedEvents;
        }

        public IObservable<List<ExpectedEvent>> Fetch() => Observable.Return(expectedEvents);
    }

    public struct ExpectedEvent
    {
        public string EventName { get; }

        public ExpectedEvent(string eventName)
        {
            EventName = eventName;
        }
    }

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

        static List<Validation> ValidateEvents(List<string> events, Dictionary<string, ExpectedEvent> e) =>
            events
                .Select(@event => new Validation(@event, e.ContainsKey(@event)))
                .ToList();
    }
}