using System;
using System.Linq;
using amplitude.tool.CrossEvents;
using amplitude.tool.Events.Domain.Actions;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Presentation.Views;

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

        public void AddExpectedEvents(string[] expectedEvents) =>
            addEvents
                .Do(expectedEvents
                    .Select(eventName => new ExpectedEvent(eventName))
                    .ToList())
                .Subscribe(_ => view.ShowExpectedEventsAdded());
    }
}