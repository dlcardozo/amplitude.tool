using System;
using System.Reactive.Subjects;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;

namespace amplitude.tool.Users.Domain.Actions
{
    public class FetchActivity
    {
        readonly ISubject<UserActivity> onActivitiesFetched;
        readonly UserActivityRepository userActivityRepository;

        public FetchActivity(
            ISubject<UserActivity> onActivitiesFetched,
            UserActivityRepository userActivityRepository
        ) {
            this.onActivitiesFetched = onActivitiesFetched;
            this.userActivityRepository = userActivityRepository;
        }

        public void Do(UserId userId) =>
            userActivityRepository.Fetch(userId)
                .Subscribe(onActivitiesFetched.OnNext);
    }
}