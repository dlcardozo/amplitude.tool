using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace amplitude.tool
{
    public class EventBus
    {
        public static EventBus Instance => instance ??= new EventBus();
        static EventBus instance;

        readonly ISubject<object> bus;

        EventBus()
        {
            bus = new Subject<object>();
        }

        public void Send<T>(T @event) where T : struct => bus.OnNext(@event);

        public IObservable<T> On<T>() where T : struct =>
            bus
                .Where(x => x is T)
                .Select(x => (T)x);
    }
}