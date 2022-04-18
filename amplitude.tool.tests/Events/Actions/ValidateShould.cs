using System.Collections.Generic;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Shared;
using NUnit.Framework;
using static amplitude.tool.tests.Events.Actions.ValidationMother;
using static amplitude.tool.tests.Events.Mothers.ExpectedEventMother;
using static amplitude.tool.tests.Events.Mothers.ExpectedEventsRepositoryMother;
using static amplitude.tool.tests.Events.Mothers.ValidateMother;
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

            Given(AValidate(onValidated))
                .When(action => action.Do(new List<SharedValidateEvent>()))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }
        
        [Test]
        public void ProcessAListOfEvents()
        {
            var events = new List<SharedValidateEvent>
            {
                new SharedValidateEvent("Events", new Dictionary<string, object>()),
            };
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents(new List<Validation>());

            Given(AValidate(onValidated))
                .When(action => action.Do(events))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }

        [Test]
        public void CompareAPreLoadedListOfEvents()
        {
            var events = new List<SharedValidateEvent>
            {
                new SharedValidateEvent("Events", new Dictionary<string, object>())
            };
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents(new List<Validation>{AValidation("Events", true)});

            Given(AValidate(onValidated, AnInMemoryExpectedEventsRepository(new List<ExpectedEvent>{AnExpectedEvent("Events")})))
                .When(action => action.Do(events))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }
        
        [Test]
        public void CompareAPreLoadedListOfEventsWithNoMatchingEvents()
        {
            var events = new List<SharedValidateEvent>
            {
                new SharedValidateEvent("watch_tutorial", new Dictionary<string, object>()),
                new SharedValidateEvent("random_event", new Dictionary<string, object>()),
                new SharedValidateEvent("fine", new Dictionary<string, object>()),
            };
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents(new List<Validation>
            {
                AValidation("watch_tutorial", true),
            });

            Given(AValidate(onValidated, AnInMemoryExpectedEventsRepository(new List<ExpectedEvent>{AnExpectedEvent("watch_tutorial")})))
                .When(action => action.Do(events))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }
        
        [Test]
        public void FailForAnEventAndWithNoMatchingProperties()
        {
            var events = new List<SharedValidateEvent>
            {
                new SharedValidateEvent("watch_tutorial", new Dictionary<string, object>{{"is_true", true}}),
                new SharedValidateEvent("random_event", new Dictionary<string, object>()),
                new SharedValidateEvent("fine", new Dictionary<string, object>()),
            };
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents(new List<Validation>
            {
                AValidation("watch_tutorial", false, new Dictionary<string, object>{{"is_true", false}}),
            });

            Given(AValidate(onValidated, AnInMemoryExpectedEventsRepository(new List<ExpectedEvent>{AnExpectedEvent("watch_tutorial", new Dictionary<string, object>{{"is_true", false}})})))
                .When(action => action.Do(events))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }
        
        [Test]
        public void SucceedForAnEventAndItsProperties()
        {
            var events = new List<SharedValidateEvent>
            {
                new SharedValidateEvent("watch_tutorial", new Dictionary<string, object>{{"is_true", true}}),
                new SharedValidateEvent("random_event", new Dictionary<string, object>()),
                new SharedValidateEvent("fine", new Dictionary<string, object>()),
            };
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents(new List<Validation>
            {
                AValidation("watch_tutorial", true, new Dictionary<string, object>{{"is_true", true}}),
            });

            Given(AValidate(onValidated, AnInMemoryExpectedEventsRepository(new List<ExpectedEvent>{AnExpectedEvent("watch_tutorial", new Dictionary<string, object>{{"is_true", true}})})))
                .When(action => action.Do(events))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }
    }
    
}