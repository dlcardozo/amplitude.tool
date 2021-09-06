namespace amplitude.tool.Users.Presentation.Views
{
    public interface IUserView
    {
        void ShowUserRegistered(string userId);
        void AskForUserId();
    }
}