using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using amplitude.tool.Events.Domain.Model;
using amplitude.tool.Events.Presentation.Presenters;
using amplitude.tool.Events.Presentation.Views;
using amplitude.tool.Events.Shared;
using amplitude.tool.Utilities;
using CsvHelper;

namespace amplitude.tool.Events.UnityDelivery.Views
{
    public class EventView : IEventView
    {
        readonly string expectedEventsPath;
        EventPresenter presenter;

        public EventView(string expectedEventsPath)
        {
            this.expectedEventsPath = expectedEventsPath;
            presenter = new EventPresenter(this);
        }

        public void AskForExpectedEvents()
        {
            Console.WriteLine("Provide event names in a CSV with an Event header, path to csv file:");

            using var reader = new StreamReader(string.IsNullOrEmpty(expectedEventsPath) ? Console.ReadLine() : expectedEventsPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
         
            var expectedEvents = csv.GetRecords<CsvRow>()
                .Select(record => new SharedExpectedEvent(record.Event, ConvertEventPropertiesFrom(record)))
                .ToArray();
            presenter.AddExpectedEvents(expectedEvents);
        }

        public void ShowExpectedEventsAdded() => Console.WriteLine("Expected events added.");

        public void ShowValidationResult(List<Validation> validations)
        {
            Console.WriteLine("### Searching for events matching on User activity: ");
            
            validations.ForEach(WriteValidationOutput);
            
            PrintValidationSummary(validations);
        }

        void WriteValidationOutput(Validation validation) =>
            ConsoleExtensions.WriteLineWith(
                validation.EventProperties.Count == 0
                    ? $" # {validation.EventName} - {DisplayResult(validation)}"
                    : $" # {validation.EventName} - {DisplayResult(validation)} - Properties: {validation.EventPropertiesToString()}",
                ValidationColor(validation));


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

        static ConsoleColor ValidationColor(Validation validation) =>
            !validation.IsValid
                ? ConsoleColor.Red
                : ConsoleColor.Green;

        static Dictionary<string, object> ConvertEventPropertiesFrom(CsvRow csvRow)
        {
            if (string.IsNullOrEmpty(csvRow.Event_Properties))
                return new Dictionary<string, object>();
            
            var tuple = csvRow.Event_Properties.Split(':');
            return new Dictionary<string, object>{{tuple[0].Trim(), CastExtension.CastToValue(tuple[1].Trim())}};
        }
    }
}