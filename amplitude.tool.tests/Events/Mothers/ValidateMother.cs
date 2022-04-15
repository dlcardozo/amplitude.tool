using System.Reactive.Subjects;
using amplitude.tool.Events.Domain.Actions;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Domain.Repositories;
using amplitude.tool.tests.Events.Actions;
using static amplitude.tool.tests.Events.Mothers.ExpectedEventsRepositoryMother;

namespace amplitude.tool.tests.Events.Mothers
{
    public static class ValidateMother
    {
        public static Validate AValidate(
            ISubject<ValidatedEvents> withOnValidated = null,
            ExpectedEventsRepository withExpectedEventsRepository = null
        ) =>
            new Validate(
                withOnValidated ?? new Subject<ValidatedEvents>(),
                withExpectedEventsRepository ?? AnInMemoryExpectedEventsRepository()
            );
    }
}