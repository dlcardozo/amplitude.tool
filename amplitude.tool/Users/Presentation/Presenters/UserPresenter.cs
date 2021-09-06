using amplitude.tool.Users.Domain.Actions;
using amplitude.tool.Users.Infrastructure;
using amplitude.tool.Users.Presentation.Views;
using System;
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

        public UserPresenter(IUserView userView)
        {
            view = userView;

            usersRepository = new InMemoryUsersRepository();
            userActivityRepository = new AmplitudeUserActivityRepository();
            
            register = new Register(Context.Instance.OnUserRegistered, usersRepository);
            fetchActivity = new FetchActivity(Context.Instance.OnActivitiesFetched, userActivityRepository);

            Context.Instance
                .OnUserRegistered
                .Subscribe(user => view.ShowUserRegistered(user.UserId.Value));
        }

        public void Init() => 
            view.AskForUserId();

        public void RegisterUserId(string userId) => 
            register.Do(userId);
    }
}