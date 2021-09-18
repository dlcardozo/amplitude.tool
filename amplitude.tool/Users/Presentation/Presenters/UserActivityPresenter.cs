using System;
using amplitude.tool.Users;

namespace amplitude.tool
{
    public class UserActivityPresenter
    {
        readonly IUserActivityView view;

        public UserActivityPresenter(IUserActivityView view)
        {
            this.view = view;

            Context.Instance
                .OnActivitiesFetched
                .Subscribe(userActivities => this.view.ShowUserActivitiesFetched());
        }
    }
}