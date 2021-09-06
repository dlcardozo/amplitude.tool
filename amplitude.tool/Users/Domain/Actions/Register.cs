using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using amplitude.tool.Users.Domain.Model;
using amplitude.tool.Users.Domain.Repositories;

namespace amplitude.tool.Users.Domain.Actions
{
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
            usersRepository.Register(newUser)
                .Do(_ => onUserRegistered.OnNext(newUser))
                .Subscribe();
        }
    }
}