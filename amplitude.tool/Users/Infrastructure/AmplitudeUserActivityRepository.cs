using System;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;

namespace amplitude.tool.Users.Infrastructure
{
    public class AmplitudeUserActivityRepository : UserActivityRepository
    {
        public IObservable<UserActivity> Fetch(UserId userId) => throw new NotImplementedException();
    }
}