using System;
using System.Collections.Generic;
using amplitude.tool.CrossEvents;
using amplitude.tool.Events.Domain.Actions;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Presentation.Views;

namespace amplitude.tool.Events.UnityDelivery.Views
{
    public class EventView : IEventView
    {
        EventPresenter presenter;

        public EventView() => presenter = new EventPresenter(this);
        public void ShowValidationResult(List<Validation> validations)
        {
            validations.ForEach(validation => Console.WriteLine($"Validation: {validation.EventName} - {DisplayResult(validation)}"));
        }

        string DisplayResult(Validation validation) => validation.IsValid ? "Found" : "Not Found";
    }

    public class EventPresenter
    {
        readonly IEventView view;
        Validate validate;

        public EventPresenter(IEventView view)
        {
            this.view = view;
            validate = new Validate(Context.Instance.OnValidated, Context.Instance.ExpectedEventsRepository);
            
            EventBus.Instance
                .On<CrossUserEvents>()
                .Subscribe(crossUserEvents => validate.Do(crossUserEvents.Events));

            Context.Instance.OnValidated
                .Subscribe(validatedEvents => view.ShowValidationResult(validatedEvents.Validations));
        }
    }
}