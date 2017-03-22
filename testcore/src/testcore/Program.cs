using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var res = Timebox(() =>
            {
                Console.WriteLine("Task started");
                for (int i = 0; i < 10000; i++)
                {
                    Console.WriteLine($"< current i {i} >");
                }
                Console.WriteLine("Task finished on the right time");
            }, 20000);
            var result = res ? "finished" : "broken";
            Console.WriteLine($"job status is {result}");
        }

        public static bool Timebox(Action longRunning, int milisec)
        {
            try
            {
                return Task.Factory.StartNew(longRunning).Wait(milisec);
            }
            catch (AggregateException)
            {
                Console.WriteLine("Task took longer and it is terminated");
                throw;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"somehing else got wrong{ex.GetType()}, {ex.Message}");
                throw;
            }
        }
    }
}
