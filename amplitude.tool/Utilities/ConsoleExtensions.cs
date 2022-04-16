using System;

namespace amplitude.tool.Utilities
{
    public static class ConsoleExtensions
    {
        public static void WriteLineWith(string message, ConsoleColor color)
        {
            var currentConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message, color);
            Console.ForegroundColor = currentConsoleColor;
        }
    }
}