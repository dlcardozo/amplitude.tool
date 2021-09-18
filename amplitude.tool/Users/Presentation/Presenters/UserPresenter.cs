using amplitude.tool.Users.Domain.Actions;
using amplitude.tool.Users.Infrastructure;
using amplitude.tool.Users.Presentation.Views;
using System;
using System.Reactive.Linq;
using amplitude.tool.Users.Domain.Repositories;

namespace amplitude.tool.Users.Presentation.Presenters
{
    public class UserPresenter
    {
        readonly IUserView view;
        Register register;
        FetchActivity fetchActivity;
        UsersRepository usersRepository;
        UserActivityRepository userActivityRepository;

        public UserPresenter(IUserView userView, string amplitudeKey, string amplitudeSecretKey)
        {
            view = userView;

            usersRepository = new InMemoryUsersRepository();
            userActivityRepository = new AmplitudeUserActivityRepository(amplitudeKey, amplitudeSecretKey);
            register = new Register(Context.Instance.OnUserRegistered, usersRepository);
            fetchActivity = new FetchActivity(Context.Instance.OnActivitiesFetched, userActivityRepository);

            Context.Instance
                .OnUserRegistered
                .Do(user => view.ShowUserRegistered(user.UserId.Value))
                .Subscribe(user => fetchActivity.Do(user.UserId));
        }

        public void Init() => 
            view.AskForUserId();

        public void RegisterUserId(string userId) => 
            register.Do(userId);
    }
}