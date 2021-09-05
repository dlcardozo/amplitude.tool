using System.Reactive.Subjects;
using amplitude.tool.tests.Mothers;
using amplitude.tool.Users.Domain.Actions;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;

namespace amplitude.tool.tests.Users.Mothers
{
    public static class FetchActivityMother
    {
        public static FetchActivity AFetchActivity(
            ISubject<UserActivity> withActivitiesFetched = null,
            UserActivityRepository withUserActivityRepository = null
        ) => new FetchActivity(
            withActivitiesFetched ?? EventMother.AnEvent<UserActivity>(),
            withUserActivityRepository ?? UserActivityRepositoryMother.AnUserActivityRepository()
        );
    }
}