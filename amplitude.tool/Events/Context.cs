using System.Collections.Generic;
using System.Reactive.Subjects;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Domain.Repositories;
using amplitude.tool.Events.Infrastructure;

namespace amplitude.tool.Events
{
    public class Context
    {
        public readonly ISubject<ValidatedEvents> OnValidated;
        public readonly ExpectedEventsRepository ExpectedEventsRepository;

        public static Context Instance => instance ??= CreateNewContext();
        
        static Context CreateNewContext() => new Context(new Subject<ValidatedEvents>());
        static Context instance;
        
        Context(ISubject<ValidatedEvents> onValidated)
        {
            OnValidated = onValidated;
            ExpectedEventsRepository = new InMemoryExpectedEventsRepository(new List<ExpectedEvent>());
        }
    }
}