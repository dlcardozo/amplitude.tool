using System;
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
                .Subscribe(crossUserEvents => validate.Do(crossUserEvents.Events));

            Context.Instance.OnValidated
                .Subscribe(validatedEvents => view.ShowValidationResult(validatedEvents.Validations));
        }

        public void AddExpectedEvents(SharedExpectedEvent[] expectedEvents) =>
            addEvents
                .Do(expectedEvents
                    .Select(expectedEvent => new ExpectedEvent(expectedEvent.EventName, expectedEvent.EventProperties))
                    .ToList())
                .Subscribe(_ => view.ShowExpectedEventsAdded());
    }
}