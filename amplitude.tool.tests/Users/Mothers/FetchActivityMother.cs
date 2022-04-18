using System.Reactive.Subjects;
using amplitude.tool.Users.Domain.Actions;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;
using static amplitude.tool.tests.Mothers.EventMother;
using static amplitude.tool.tests.Users.Mothers.UserActivityRepositoryMother;

namespace amplitude.tool.tests.Users.Mothers
{
    public static class FetchActivityMother
    {
        public static FetchActivity AFetchActivity(
            ISubject<UserActivity> withActivitiesFetched = null,
            UserActivityRepository withUserActivityRepository = null
        ) => new FetchActivity(
            withActivitiesFetched ?? AnEvent<UserActivity>(),
            withUserActivityRepository ?? AnUserActivityRepository()
        );
    }
}