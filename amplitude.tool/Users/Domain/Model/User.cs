namespace amplitude.tool.Users.Domain.Model
{
    public struct User
    {
        public readonly UserId UserId;

        public User(UserId userId)
        {
            UserId = userId;
        }
    }
}