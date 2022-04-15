using System.Collections.Generic;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Domain.Repositories;
using amplitude.tool.Events.Infrastructure;
using amplitude.tool.tests.Events.Actions;

namespace amplitude.tool.tests.Events.Mothers
{
    public static class ExpectedEventsRepositoryMother
    {
        public static ExpectedEventsRepository AnInMemoryExpectedEventsRepository(
            List<ExpectedEvent> withExpectedEvents = null
        ) => 
            new InMemoryExpectedEventsRepository(
                withExpectedEvents ?? new List<ExpectedEvent>()
            );
    }
}