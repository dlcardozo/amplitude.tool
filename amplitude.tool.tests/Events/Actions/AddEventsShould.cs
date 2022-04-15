using System.Collections.Generic;
using System.Reactive.Linq;
using amplitude.tool.Events.Domain.Actions;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Domain.Repositories;
using NSubstitute;
using NUnit.Framework;
using static BDD.Context;

namespace amplitude.tool.tests.Events.Actions
{
    [TestFixture]
    public class AddEventsShould
    {
        [Test]
        public void AddNewEventsOnRepository()
        {
            var expectedEventsRepository = Substitute.For<ExpectedEventsRepository>();
            
            Given(new AddEvents(expectedEventsRepository))
                .When(action => action.Do(new List<ExpectedEvent>()))
                .Then(() => expectedEventsRepository.Received(1).AddEvents(Arg.Any<List<ExpectedEvent>>()))
                .Run();
        }
    }
}