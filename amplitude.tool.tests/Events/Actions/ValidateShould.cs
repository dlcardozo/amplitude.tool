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
        public void METHOD()
        {
            var onValidated = AnEvent<ValidatedEvents>();
            var expected = new ValidatedEvents();

            Given(new Validate(onValidated))
                .When(action => action.Do())
                .Then(_ => onValidated, it => it.Receives(expected))
                .Run();
        }
    }

    public struct ValidatedEvents
    {
    }

    public class Validate
    {
        readonly ISubject<ValidatedEvents> onValidated;

        public Validate(ISubject<ValidatedEvents> onValidated)
        {
            this.onValidated = onValidated;
        }

        public void Do()
        {
            onValidated.OnNext(new ValidatedEvents());
        }
    }
}