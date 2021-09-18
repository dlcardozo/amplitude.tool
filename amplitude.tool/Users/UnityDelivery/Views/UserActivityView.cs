using System;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Presentation.Presenters;
using amplitude.tool.Users.Presentation.Views;

namespace amplitude.tool.Users.UnityDelivery.Views
{
    public class UserActivityView : IUserActivityView
    {
        readonly UserActivityPresenter presenter;

        public UserActivityView()
        {
            presenter = new UserActivityPresenter(this);
        }

        public void ShowUserActivitiesFetched(UserActivity userActivities) =>
            userActivities
                .TrackedEvents
                .ForEach(trackedEvent => Console.WriteLine(trackedEvent.Name));
    }
}