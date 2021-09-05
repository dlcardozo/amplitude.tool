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
    }
}