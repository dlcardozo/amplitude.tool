using System;
using System.Collections.Generic;

namespace BDD
{
    public class When<T>
    {
        readonly T action;
        readonly Action<T> doAction;
        readonly List<Action> assertions = new List<Action>();
        
        public When(T action, Action<T> doAction)
        {
            this.action = action;
            this.doAction = doAction;
        }

        public When<T> Then<S>(Func<T, IObservable<S>> eventSelector, Action<TestObserver<S>> assert)
        {
            var selectedEvent = eventSelector(action);
            var eventObserver = new TestObserver<S>();

            selectedEvent.Subscribe(eventObserver);
            
            assertions.Add(() => assert(eventObserver));

            return this;
        }

        public When<T> Then(Action assert)
        {
            assertions.Add(assert);
            return this;
        }

        public void Run()
        {
            doAction(action);
            assertions.ForEach(assertion => assertion());
        }
    }
}