using System;

namespace BDD
{
    public class Given<T>
    {
        readonly T action;
        public Given(T action) => this.action = action;
        
        public When<T> When(Action<T> doAction) => new When<T>(action, doAction);
    }

    public class Disposer : IDisposable
    {
        readonly Action disposer;
        public Disposer(Action disposer) => this.disposer = disposer;

        public void Dispose() => disposer();
    }

    public class EventShouldNotFail : Exception
    {
    }

    public class EventShouldNotComplete : Exception
    {
    }
}