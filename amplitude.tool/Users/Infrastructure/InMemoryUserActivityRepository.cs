using System;
using System.Reactive.Linq;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;

namespace amplitude.tool.Users.Infrastructure
{
    public class InMemoryUserActivityRepository : UserActivityRepository
    {
        readonly UserActivity userActivity;

        public InMemoryUserActivityRepository(UserActivity userActivity)
        {
            this.userActivity = userActivity;
        }

        public IObservable<UserActivity> Fetch(UserId userId) => Observable.Return(userActivity);
    }
}