using System;
using amplitude.tool.Users.Presentation.Presenters;
using amplitude.tool.Users.Presentation.Views;

namespace amplitude.tool.Users.UnityDelivery.Views
{
    public class UserView : IUserView
    {
        UserPresenter presenter;

        public UserView()
        {
            presenter = new UserPresenter(this);
        }

        public void Init() =>
            presenter.Init();

        public void ShowUserRegistered(string userId) => 
            Console.WriteLine($"Testing started on User ID: {userId}");

        public void AskForUserId()
        {
            Console.WriteLine($"User Id required, please provide one:");
            presenter.RegisterUserId(Console.ReadLine()?.Trim());
        }
    }
}