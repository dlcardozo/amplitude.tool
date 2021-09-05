using amplitude.tool.Users.Domain.Actions;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;
using amplitude.tool.Users.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using static amplitude.tool.tests.Mothers.EventMother;
using static BDD.Context;

namespace amplitude.tool.tests.Users.Actions
{
    [TestFixture]
    public class RegisterShould
    {
        [Test]
        public void Emit_An_User_Registered()
        {
            var onUserRegistered = AnEvent<User>();
            var usersRepository = new InMemoryUsersRepository();
            var expected = new User(new UserId("UserId"));
            
            Given(new Register(onUserRegistered, usersRepository))
                .When(action => action.Do("UserId"))
                .Then(_ => onUserRegistered, it => it.Receives(expected))
                .Run();
        }

        [Test]
        public void Save_User_Registered()
        {
            var onUserRegistered = AnEvent<User>();
            var repository = Substitute.For<UsersRepository>();
            var expected = new User(new UserId("UserId"));
            
            Given(new Register(onUserRegistered, repository))
                .When(action => action.Do("UserId"))
                .Then(() => repository.Received(1).Register(expected))
                .Run();
        }
    }
}