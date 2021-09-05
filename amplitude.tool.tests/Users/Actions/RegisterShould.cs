using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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

    public interface UsersRepository
    {
        IObservable<Unit> Register(User expected);
    }
    
    public class InMemoryUsersRepository : UsersRepository
    {
        public IObservable<Unit> Register(User expected) => Observable.Return(Unit.Default);
    }

    public struct User
    {
        public readonly UserId UserId;

        public User(UserId userId)
        {
            UserId = userId;
        }
    }

    public struct UserId
    {
        public readonly string Value;

        public UserId(string value)
        {
            Value = value;
        }
    }

    public class Register
    {
        readonly ISubject<User> onUserRegistered;
        readonly UsersRepository usersRepository;

        public Register(ISubject<User> onUserRegistered, UsersRepository usersRepository)
        {
            this.onUserRegistered = onUserRegistered;
            this.usersRepository = usersRepository;
        }

        public void Do(string userid)
        {
            var newUser = new User(new UserId(userid));
            onUserRegistered.OnNext(newUser);
            usersRepository.Register(newUser)
                .Subscribe();
        }
    }
}