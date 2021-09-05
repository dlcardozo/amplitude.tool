using System;
using System.Reactive;
using System.Reactive.Linq;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;

namespace amplitude.tool.Users.Infrastructure
{
    public class InMemoryUsersRepository : UsersRepository
    {
        public IObservable<Unit> Register(User expected) => Observable.Return(Unit.Default);
    }
}