using System;
using System.Collections.Generic;
using System.Linq;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Presentation.Presenters;
using amplitude.tool.Events.Presentation.Views;
using amplitude.tool.Utilities;

namespace amplitude.tool.Events.UnityDelivery.Views
{
    public class EventView : IEventView
    {
        EventPresenter presenter;

        public EventView() => presenter = new EventPresenter(this);

        public void AskForExpectedEvents()
        {
            Console.WriteLine("Provide event names to validate, comma separated:");
            presenter.AddExpectedEvents(Console.ReadLine()?.Split(','));
        }

        public void ShowExpectedEventsAdded() => Console.WriteLine("Expected events added.");

        public void ShowValidationResult(List<Validation> validations)
        {
            Console.WriteLine("### Searching for events matching on User activity: ");
            
            validations.ForEach(validation =>
                Console.WriteLine($" # {validation.EventName} - {DisplayResult(validation)}"));
            
            PrintValidationSummary(validations);
        }


        string DisplayResult(Validation validation) => validation.IsValid ? "Found" : "Not Found";

        static void PrintValidationSummary(List<Validation> validations) =>
            ConsoleExtensions.WriteLineWith(
                $"### Found {validations.Count(validation => validation.IsValid)} of {validations.Count} expected events",
                ValidationsColor(validations)
            );

        static ConsoleColor ValidationsColor(List<Validation> validations) =>
            validations.Any(validation => !validation.IsValid)
                ? ConsoleColor.Red
                : ConsoleColor.Green;
    }
}