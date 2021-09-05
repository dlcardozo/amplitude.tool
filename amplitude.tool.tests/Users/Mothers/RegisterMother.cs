using System.Reactive.Subjects;
using amplitude.tool.tests.Mothers;
using amplitude.tool.Users.Domain.Actions;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;
using amplitude.tool.Users.Infrastructure;

namespace amplitude.tool.tests.Users.Mothers
{
    public static class RegisterMother
    {
        public static Register ARegister(
            ISubject<User> withUserRegistered = null,
            UsersRepository withUsersRepository = null
        ) {
            return new Register(
                withUserRegistered ?? EventMother.AnEvent<User>(),
                withUsersRepository ?? new InMemoryUsersRepository());
        }
    }
}