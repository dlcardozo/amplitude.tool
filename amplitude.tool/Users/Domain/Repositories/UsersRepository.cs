using System;
using System.Reactive;
using amplitude.tool.Users.Domain.Model;

namespace amplitude.tool.Users.Domain.Repositories
{
    public interface UsersRepository
    {
        IObservable<Unit> Register(User newUser);
    }
}