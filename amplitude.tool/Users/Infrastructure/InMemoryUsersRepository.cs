using System;
using System.Reactive;
using System.Reactive.Linq;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;

namespace amplitude.tool.Users.Infrastructure
{
    public class InMemoryUsersRepository : UsersRepository
    {
        User currentUser;

        public IObservable<Unit> Register(User newUser) => 
            Observable.Return(Unit.Default)
                .Do(_ => currentUser = newUser);
    }
}