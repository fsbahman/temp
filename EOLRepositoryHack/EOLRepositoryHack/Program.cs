using EOLRepositoryHack.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOLRepositoryHack
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //TestEventInConsole();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /*private static void TestEventInConsole()
        {
            var reporter = new ReportReader(@"D:\code\eoldev\RepositoryHack\WebApp\Sources\Report-2017-2-6-18-47.csv");
            var data = reporter.ReadFile();

            var generator = new EventGenerator();

            var tree = generator.GetEvents(data);


            foreach (var node in tree)
            {
                DrawNode(node);
            }
            //Console.WriteLine(Environment.ca);
        }*/

        private static void DrawNode(EventModel model, int depth = 0)
        {
            var spaces = new string('-', 2 * depth);
            Console.WriteLine($"{spaces}{model.BCName}->{model.Metadata}: {model.Name} duration {model.Duration.TotalMilliseconds}" );
            foreach (var m in model.ChildEvents)
            {
                DrawNode(m, depth + 1);
            }
            
        }
    }
}
