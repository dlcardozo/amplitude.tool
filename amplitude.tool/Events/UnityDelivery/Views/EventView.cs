using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("### Searching for events matching on User activity: ");
            validations.ForEach(validation =>
                Console.WriteLine($" # {validation.EventName} - {DisplayResult(validation)}"));
            var resultColor = validations.Any(validation => !validation.IsValid)
                ? ConsoleColor.Red
                : ConsoleColor.Green;
            var currentConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = resultColor;
            Console.WriteLine($"### Found {validations.Count(validation => validation.IsValid)} of {validations.Count} expected events", resultColor);
            Console.ForegroundColor = currentConsoleColor;
        }

        public void ShowExpectedEventsAdded() => Console.WriteLine("Expected events added.");

        string DisplayResult(Validation validation) => validation.IsValid ? "Found" : "Not Found";

        public void AskForExpectedEvents()
        {
            Console.WriteLine("Provide event names to validate, comma separated:");
            presenter.AddExpectedEvents(Console.ReadLine()?.Split(','));
        }
    }

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