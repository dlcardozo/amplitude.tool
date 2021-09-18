using amplitude.tool.Users.Domain.Model;

namespace amplitude.tool.Users.Presentation.Views
{
    public interface IUserActivityView
    {
        void ShowUserActivitiesFetched(UserActivity userActivities);
    }
}