using System;
using amplitude.tool.Users.Domain.Model;

namespace amplitude.tool.Users.Domain.Repositories
{
    public interface UserActivityRepository
    {
        IObservable<UserActivity> Fetch(UserId userId);
    }
}