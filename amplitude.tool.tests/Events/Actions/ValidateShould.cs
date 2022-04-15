using System.Collections.Generic;
using amplitude.tool.Events.Domain.Model;
using NUnit.Framework;
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
                .When(action => action.Do(new List<string>()))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }
        
        [Test]
        public void ProcessAListOfEvents()
        {
            var events = new List<string> { "Events" };
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents(new List<Validation>{new Validation("Events", false)});

            Given(AValidate(onValidated))
                .When(action => action.Do(events))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }

        [Test]
        public void CompareAPreLoadedListOfEvents()
        {
            var events = new List<string> { "Events" };
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents(new List<Validation>{new Validation("Events", true)});

            Given(AValidate(onValidated, AnInMemoryExpectedEventsRepository(new List<ExpectedEvent>{new ExpectedEvent("Events")})))
                .When(action => action.Do(events))
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }
    }
}