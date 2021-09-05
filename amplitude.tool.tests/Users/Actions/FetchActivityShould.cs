using amplitude.tool.Users.Domain.Actions;
using amplitude.tool.Users.Domain.Model;
using NUnit.Framework;
using static amplitude.tool.tests.Mothers.EventMother;
using static amplitude.tool.tests.Users.Mothers.TrackedEventMother;
using static amplitude.tool.tests.Users.Mothers.UserActivityMother;
using static amplitude.tool.tests.Users.Mothers.UserActivityRepositoryMother;
using static BDD.Context;

namespace amplitude.tool.tests.Users.Actions
{
    [TestFixture]
    public class FetchActivityShould
    {
        [Test]
        public void Emit_Activities()
        {
            var onActivitiesFetched = AnEvent<UserActivity>();
            var expected = AnUserActivityWithOne(ATrackedEvent("install_push"));
            
            Given(new FetchActivity(
                    onActivitiesFetched,
                    AnUserActivityRepository(withUserActivity: AnUserActivityWithOne(ATrackedEvent("install_push")))
                ))
                .When(action => action.Do(new UserId("SomeUser")))
                .Then(_ => onActivitiesFetched, it => it.Receives(expected))
                .Run();
        }
    }
}