using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using amplitude.tool.CrossEvents;
using amplitude.tool.Events;
using amplitude.tool.Events.UnityDelivery.Views;
using amplitude.tool.Users.UnityDelivery.Views;

namespace amplitude.tool
{
    class Program
    {
        static UserView userView;
        static UserActivityView userActivityView;
        static EventView eventView;
        static bool stuck = true;
        static string amplitude_key = string.Empty;
        static string amplitude_secret_key = string.Empty;
        static string expected_events_path = string.Empty;

        static void Main(string[] args)
        {
            amplitude_key = args[0].Trim();
            amplitude_secret_key = args[1].Trim();
            expected_events_path = args.Length > 2 ? args[2].Trim() : string.Empty;

            eventView = new EventView(expected_events_path);
            userView = new UserView();
            userActivityView = new UserActivityView(amplitude_key, amplitude_secret_key);

            Context.Instance
                .OnValidated.Subscribe(_ => Close());
            
            eventView.AskForExpectedEvents();
            
            userView.Init();
            ExitWhenNotStuck();   
        }

        static void ExitWhenNotStuck()
        {
            while (stuck) 
                Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        static void Close() => stuck = false;
    }
}