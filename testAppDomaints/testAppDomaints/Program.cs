using System;


namespace testAppDomaints
{
    class Program
    {
        [LoaderOptimization( LoaderOptimization.MultiDomain)]
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.WriteLine("Give me a command.");
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.F1:
                        Console.WriteLine("Do you need help? Sorry!!!");
                        break;
                    case ConsoleKey.F2:
                        //var c1 = new FirstDependency.Class1();
                        //Console.WriteLine($"For you 2 + 2 is: {c1.Sum(2, 3)} ;-)");
                        var sl = new SomeLogic();
                        sl.DoMagic();
                        break;
                }

            } while (keyInfo.Key != ConsoleKey.Escape);
        }
    }
}
