using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BDD
{
    public class TestObserver<T> : IObserver<T>
    {
        readonly List<T> receivedEvents = new List<T>();

        Exception error;
        bool completed;
        
        public void OnCompleted() => completed = true;

        public void OnError(Exception receivedError) => error = receivedError;

        public void OnNext(T receivedEvent) => receivedEvents.Add(receivedEvent);

        public TestObserver<T> Receives(T expected)
        {
            Assert.Contains(expected, receivedEvents,
                $"Expected event: {expected}, but received [{string.Join(", ", receivedEvents)}]");
            
            return this;
        }

        public TestObserver<T> Receives(Func<T, bool> comparer)
        {
            Assert.IsTrue(receivedEvents.Any(comparer));
            return this;
        }

        public TestObserver<T> ReceivesSomething()
        {
            Assert.IsTrue(receivedEvents.Any());
            return this;
        }

        public TestObserver<T> DoesntFail()
        {
            Assert.IsNull(error, "Expected: No failures. Actual: It failed.");
            return this;
        }

        public TestObserver<T> Completes()
        {
            Assert.IsTrue(completed, "Expected: Observable completed. Actual: Observable did not complete.");
            return this;
        }

        public TestObserver<T> DoesntCompletes()
        {
            Assert.IsFalse(completed, "Expected: Observable did not complete. Actual: Observable completed.");
            return this;
        }

        public TestObserver<T> ReceivesNothing()
        {
            Assert.IsEmpty(receivedEvents, "Expected: No events received. Actual: Events were received.");
            return this;
        }

        public TestObserver<T> Fails()
        {
            Assert.NotNull(error, "Expected: A failure. Actual: It did not fail.");
            return this;
        }

        public TestObserver<T> FailsWith<TError>() where TError : Exception
        {
            Assert.IsInstanceOf<TError>(error);
            return this;
        }
    }
}