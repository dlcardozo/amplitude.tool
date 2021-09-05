using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using amplitude.tool.Users.Domain.Model;
using NUnit.Framework;
using static amplitude.tool.tests.Mothers.EventMother;
using static BDD.Context;

namespace amplitude.tool.tests.Users.Actions
{
    [TestFixture]
    public class FetchActivityShould
    {
        [Test]
        public void Emit_Activities()
        {
            var onActivitiesFetched = AnEvent<UserActivity>();
            var userActivityRepository = new InMemoryUserActivityRepository(new UserActivity(new List<TrackedEvent>{ new TrackedEvent("install_push")}));
            var expected = new UserActivity(new List<TrackedEvent>{ new TrackedEvent("install_push")});
            
            Given(new FetchActivity(onActivitiesFetched, userActivityRepository))
                .When(action => action.Do(new UserId("SomeUser")))
                .Then(_ => onActivitiesFetched, it => it.Receives(expected))
                .Run();
        }

        [Test]
        public void Emit_Activities_2()
        {
            var onActivitiesFetched = AnEvent<UserActivity>();
            var userActivityRepository = new InMemoryUserActivityRepository(new UserActivity(new List<TrackedEvent>{ new TrackedEvent("session_end")})); 
            var expected = new UserActivity(new List<TrackedEvent>{ new TrackedEvent("session_end")});
            
            Given(new FetchActivity(onActivitiesFetched, userActivityRepository))
                .When(action => action.Do(new UserId("AnotherUser")))
                .Then(_ => onActivitiesFetched, it => it.Receives(expected))
                .Run();
        }
    }

    public class InMemoryUserActivityRepository : UserActivityRepository
    {
        readonly UserActivity userActivity;

        public InMemoryUserActivityRepository(UserActivity userActivity)
        {
            this.userActivity = userActivity;
        }

        public IObservable<UserActivity> Fetch(UserId userId) => Observable.Return(userActivity);
    }

    public interface UserActivityRepository
    {
        IObservable<UserActivity> Fetch(UserId userId);
    }

    public struct TrackedEvent
    {
        public readonly string Name;

        public TrackedEvent(string name)
        {
            Name = name;
        }
    }

    public class FetchActivity
    {
        readonly ISubject<UserActivity> onActivitiesFetched;
        readonly UserActivityRepository userActivityRepository;

        public FetchActivity(
            ISubject<UserActivity> onActivitiesFetched,
            UserActivityRepository userActivityRepository
        ) {
            this.onActivitiesFetched = onActivitiesFetched;
            this.userActivityRepository = userActivityRepository;
        }

        public void Do(UserId userId) =>
            userActivityRepository.Fetch(userId)
                .Subscribe(onActivitiesFetched.OnNext);
    }

    public struct UserActivity
    {
        public readonly List<TrackedEvent> TrackedEvents;

        public UserActivity(List<TrackedEvent> trackedEvents)
        {
            TrackedEvents = trackedEvents;
        }

        public override bool Equals(object obj) => 
            obj is UserActivity other && Equals(other);

        bool Equals(UserActivity other) =>
            TrackedEvents.SafeSequenceEqual(other.TrackedEvents);
    }

    public static class EnumerableExtensions
    {
        public static bool SafeSequenceEqual<T>(this List<T> source, List<T> other) =>
            (source ?? new List<T>()).SequenceEqual(other ?? new List<T>());
    }
}