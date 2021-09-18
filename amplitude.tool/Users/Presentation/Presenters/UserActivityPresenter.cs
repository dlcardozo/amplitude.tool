using System;
using amplitude.tool.Users.Presentation.Views;

namespace amplitude.tool.Users.Presentation.Presenters
{
    public class UserActivityPresenter
    {
        readonly IUserActivityView view;

        public UserActivityPresenter(IUserActivityView view)
        {
            this.view = view;

            Context.Instance
                .OnActivitiesFetched
                .Subscribe(userActivities => this.view.ShowUserActivitiesFetched(userActivities));
        }
    }
}