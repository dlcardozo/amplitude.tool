using System;
using System.Collections.Generic;
using System.Linq;
using amplitude.tool.CrossEvents;
using amplitude.tool.Events.Domain.Actions;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Presentation.Views;
using amplitude.tool.Events.Shared;

namespace amplitude.tool.Events.Presentation.Presenters
{
    public class EventPresenter
    {
        readonly IEventView view;
        Validate validate;
        AddEvents addEvents;

        public EventPresenter(IEventView view)
        {
            this.view = view;
            validate = new Validate(Context.Instance.OnValidated, Context.Instance.ExpectedEventsRepository);
            addEvents = new AddEvents(Context.Instance.ExpectedEventsRepository);
            
            EventBus.Instance
                .On<CrossUserEvents>()
                .Subscribe(crossUserEvents => validate.Do(CreateSharedValidateEventsFrom(crossUserEvents)));

            Context.Instance.OnValidated
                .Subscribe(validatedEvents => view.ShowValidationResult(validatedEvents.Validations));
        }

        static List<SharedValidateEvent> CreateSharedValidateEventsFrom(CrossUserEvents crossUserEvents) => 
            crossUserEvents.Events
                .Select(@event => new SharedValidateEvent(@event.EventName, @event.EventProperties))
                .ToList();

        public void AddExpectedEvents(SharedExpectedEvent[] expectedEvents) =>
            addEvents
                .Do(expectedEvents
                    .Select(expectedEvent => new ExpectedEvent(expectedEvent.EventName, expectedEvent.EventProperties))
                    .ToList())
                .Subscribe(_ => view.ShowExpectedEventsAdded());
    }
}