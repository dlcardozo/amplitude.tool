using System;

namespace amplitude.tool
{
    public class UserActivityView : IUserActivityView
    {
        readonly UserActivityPresenter presenter;

        public UserActivityView()
        {
            presenter = new UserActivityPresenter(this);
        }

        public void ShowUserActivitiesFetched() => Console.WriteLine($"User activities fetched");
    }
}