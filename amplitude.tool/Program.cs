using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using amplitude.tool.Users.UnityDelivery.Views;

namespace amplitude.tool
{
    class Program
    {
        static UserView userView;
        static bool stuck = true;

        static void Main(string[] args)
        {
            userView = new UserView();
            userView.Init();

            CloseOn();
            ExitWhenNotStuck();   
        }

        static void CloseOn() =>
            Observable.Return(Unit.Default)
                .Delay(TimeSpan.FromSeconds(10))
                .SubscribeOn(Scheduler.CurrentThread)
                .Subscribe(_ => stuck = false);

        static void ExitWhenNotStuck()
        {
            while (stuck) 
                Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}