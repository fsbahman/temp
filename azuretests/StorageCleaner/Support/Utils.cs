using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageCleaner.Support
{
    public class Utils
    {
        public static void ExecuteAndMeasure(Action action, ConsoleColor color = ConsoleColor.DarkCyan)
        {
            var start = System.Diagnostics.Stopwatch.StartNew();
            action();
            start.Stop();
            WriteLineColored((start.ElapsedMilliseconds / 1000).ToString("00:00 s"), color);
        }

        public static T ExecuteAndMeasure<T>(Func<T> func, ConsoleColor color = ConsoleColor.DarkCyan)
        {
            var start = System.Diagnostics.Stopwatch.StartNew();
            var res = func();
            start.Stop();
            WriteLineColored((start.ElapsedMilliseconds / 1000).ToString("00:00 s"), color);
            return res;
        }

        public static T2 ExecuteAndMeasure<T1, T2>(Func<T1, T2> func, T1 param1, ConsoleColor color = ConsoleColor.DarkCyan)
        {
            var start = System.Diagnostics.Stopwatch.StartNew();
            var res = func(param1);
            start.Stop();
            WriteLineColored((start.ElapsedMilliseconds / 1000).ToString("00:00 s"), color);
            return res;
        }

        public static T3 ExecuteAndMeasure<T1, T2, T3>(Func<T1, T2, T3> func, T1 param1, T2 param2, ConsoleColor color = ConsoleColor.DarkCyan)
        {
            var start = System.Diagnostics.Stopwatch.StartNew();
            var res = func(param1, param2);
            start.Stop();
            WriteLineColored((start.ElapsedMilliseconds / 1000).ToString("00:00 s"), color);
            return res;
        }

        public static bool AskConsoleIfSure()
        {
            WriteLineColored("Are you sure? (y/n)", ConsoleColor.Red);
            string data = Console.ReadLine();
            switch (data)
            {
                case "y":
                case "Y":
                    WriteLineColored("Started...", ConsoleColor.Green);
                    return true;
                case "n":
                case "N":
                    WriteLineColored("Aborted!", ConsoleColor.Magenta);
                    return false;
                default:
                    return AskConsoleIfSure();
            }
        }

        public static void WriteAtPosition(string text, int x, int y, ConsoleColor color)
        {
            var oldX = Console.CursorLeft;
            var oldY = Console.CursorTop;
            Console.SetCursorPosition(x, y);
            WriteLineColored(text, color);
            Console.SetCursorPosition(oldX, oldY);
        }


        public static void WriteAtPosition(string text, int x, int y)
        {
            var oldX = Console.CursorLeft;
            var oldY = Console.CursorTop;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
            Console.SetCursorPosition(oldX, oldY);
        }

        public static void WriteLineColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
