using System.Reactive.Subjects;
using BDD;

namespace amplitude.tool.tests.Mothers
{
    public static class EventMother
    {
        public static ISubject<T> AnEvent<T>(ISubject<T> @event = null) => @event ?? new TestEvent<T>();
    }
}