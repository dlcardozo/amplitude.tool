using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace BDD
{
    public class TestEvent<T> : ISubject<T>
    {
        readonly List<IObserver<T>> subscriptions = new List<IObserver<T>>();
        
        public void OnCompleted() => throw new EventShouldNotComplete();

        public void OnError(Exception error) => throw new EventShouldNotFail();

        public void OnNext(T value) => 
            subscriptions.ForEach(subscription => subscription.OnNext(value));

        public IDisposable Subscribe(IObserver<T> observer)
        {
            subscriptions.Add(observer);

            return new Disposer(() => subscriptions.Remove(observer));
        }
    }
}