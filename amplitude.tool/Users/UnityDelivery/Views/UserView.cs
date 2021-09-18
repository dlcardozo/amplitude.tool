using System;
using amplitude.tool.Users.Presentation.Presenters;
using amplitude.tool.Users.Presentation.Views;

namespace amplitude.tool.Users.UnityDelivery.Views
{
    public class UserView : IUserView
    {
        UserPresenter presenter;

        public UserView(string amplitudeKey, string amplitudeSecretKey)
        {
            presenter = new UserPresenter(this, amplitudeKey, amplitudeSecretKey);
        }

        public void Init() =>
            presenter.Init();

        public void ShowUserRegistered(string userId) => 
            Console.WriteLine($"User registered with ID: {userId}");

        public void AskForUserId()
        {
            Console.WriteLine($"User Id required, please provide one:");
            presenter.RegisterUserId(Console.ReadLine());
        }
    }
}