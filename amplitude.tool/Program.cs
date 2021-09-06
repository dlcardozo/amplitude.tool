using System;
using System.Reactive;
using System.Reactive.Linq;
using amplitude.tool.Users.UnityDelivery.Views;

namespace amplitude.tool
{
    class Program
    {
        static UserView userView;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            userView = new UserView();
            
            userView.Init();
        }
    }
}