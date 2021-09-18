using System;
using amplitude.tool.Users.Domain.Actions;
using amplitude.tool.Users.Infrastructure;
using amplitude.tool.Users.Presentation.Views;

namespace amplitude.tool.Users.Presentation.Presenters
{
    public class UserActivityPresenter
    {
        readonly IUserActivityView view;
        readonly AmplitudeUserActivityRepository userActivityRepository;
        readonly FetchActivity fetchActivity;

        public UserActivityPresenter(IUserActivityView view, string amplitudeKey, string amplitudeSecretKey)
        {
            this.view = view;
            
            userActivityRepository = new AmplitudeUserActivityRepository(amplitudeKey, amplitudeSecretKey);
            fetchActivity = new FetchActivity(Context.Instance.OnActivitiesFetched, userActivityRepository);

            Context.Instance
                .OnUserRegistered
                .Subscribe(user => fetchActivity.Do(user.UserId));
            
            Context.Instance
                .OnActivitiesFetched
                .Subscribe(userActivities => this.view.ShowUserActivitiesFetched(userActivities));
        }
    }
}