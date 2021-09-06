using System.Reactive.Subjects;
using amplitude.tool.Users.Domain.Model;

namespace amplitude.tool.Users
{
    public class Context
    {
        public readonly ISubject<UserActivity> OnActivitiesFetched;
        public readonly ISubject<User> OnUserRegistered;
        public static Context Instance => instance ??= CreateNewContext();

        static Context CreateNewContext() => new Context(new Subject<UserActivity>(), new Subject<User>());
        static Context instance;

        Context(ISubject<UserActivity> onActivitiesFetched, ISubject<User> onUserRegistered)
        {
            OnActivitiesFetched = onActivitiesFetched;
            OnUserRegistered = onUserRegistered;
        }
    }
}