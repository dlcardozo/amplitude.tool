using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;
using amplitude.tool.Users.Infrastructure;

namespace amplitude.tool.tests.Users.Mothers
{
    public static class UserActivityRepositoryMother
    {
        public static UserActivityRepository AnUserActivityRepository(UserActivity? withUserActivity = null) => 
            new InMemoryUserActivityRepository(
                withUserActivity ?? UserActivityMother.AnUserActivity()
            );
    }
}