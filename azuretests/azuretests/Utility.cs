using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuretests
{
    public class Utility
    {
        public static bool AskConsoleIfSure()
        {
            WriteColored("Are you sure? (y/n)", ConsoleColor.Red);
            string data = Console.ReadLine();
            switch (data)
            {
                case "y":
                case "Y":
                    return true;
                case "n":
                case "N":
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
            WriteColored(text, color);
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

        public static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
