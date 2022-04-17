using System;
using System.Linq;
using System.Reactive.Linq;
using amplitude.tool.CrossEvents;
using amplitude.tool.Users.Domain.Actions;
using amplitude.tool.Users.Domain.Model;
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
                .Do(Notify)
                .Subscribe();
        }

        static void Notify(UserActivity userActivities) => 
            EventBus.Instance.Send(new CrossUserEvents(userActivities.TrackedEvents.Select(x => x.Name).ToList()));
    }
}