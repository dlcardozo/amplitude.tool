using amplitude.tool.Users.Presentation.Views;

namespace amplitude.tool.Users.Presentation.Presenters
{
    public class UserPresenter
    {
        readonly IUserView userView;

        public UserPresenter(IUserView userView)
        {
            this.userView = userView;
        }
    }
}