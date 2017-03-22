using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;

namespace selfhost
{
    class Program
    {
        static void Main(string[] args)
        {
            //StartServer();
            sendALotOfMessage();
        }

        private static void sendALotOfMessage()
        {
            string queuePath = @".\private$\messageq";
            var queue = new System.Messaging.MessageQueue(queuePath);
            queue.Formatter = new System.Messaging.XmlMessageFormatter();
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                var data = $"{{id:{i}}}";
                queue.Send(data, "Data at " + DateTime.Now.ToString());
            }
            queue.Close();
            stopwatch.Stop();
            Console.WriteLine($"10000 messages in: {stopwatch.ElapsedMilliseconds}");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        static void StartServer()
        {
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://localhost:9001");

            config.Routes.MapHttpRoute(
                "webapidefault", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            
            // Do it when controllers are availalble in another assembly
            //WebAPILoader loader = new WebAPILoader();
            //config.Services.Replace(typeof(IAssembliesResolver), loader);

            Console.WriteLine("Web Server is listening on 9001 ...");

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                System.Windows.Forms.Application.Run();
            }
        }
    }
}
