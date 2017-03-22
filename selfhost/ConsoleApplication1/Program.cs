using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static MessageQueue queue;
        static void Main(string[] args)
        {
            string queuePath = @".\private$\messageq";
            var queue = new System.Messaging.MessageQueue(queuePath);
            queue.Formatter = new System.Messaging.XmlMessageFormatter();
            var stopwatch = Stopwatch.StartNew();
            int counter = 0;
            while (true)
            {
                var msg = queue.Receive();
                msg.Formatter = new XmlMessageFormatter(new string[] { "System.String, mscorlib" });
                string data = msg.Body.ToString();
                var d = JsonConvert.DeserializeObject(data);
                counter++;
                if (counter > 999)
                {
                    stopwatch.Stop();
                    Console.WriteLine($"10000 messages received in {stopwatch.ElapsedMilliseconds}");
                    break;
                }
            }
            queue.Close();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
            //ATechnicqueToReceive();
        }

        private static void ATechnicqueToReceive()
        {
            string queuePath = @".\private$\messageq";
            queue = new MessageQueue(queuePath);
            queue.ReceiveCompleted += Queue_ReceiveCompleted;
            queue.BeginReceive();
            Console.WriteLine("Start listening...");
            //Receive();
            Console.ReadKey();
            //System.Windows.Forms.Application.Run();
        }

        private static void Receive()
        {
            while (true)
            {
                //MessageQueue messageQueue = new MessageQueue(@".\Private$\SomeTestName");
                System.Messaging.Message[] messages = queue.GetAllMessages();

                foreach (System.Messaging.Message message in messages)
                {
                    //Do something with the message
                    message.Formatter = new XmlMessageFormatter(new string[] { "System.String, mscorlib" });
                    string data = message.Body.ToString();
                    Console.WriteLine($"Message Received: {data}");
                }
                // after all processing, delete all the messages
                queue.Purge();
                System.Threading.Thread.Sleep(100);
            }
        }

        private static void Queue_ReceiveCompleted(object sender, System.Messaging.ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = queue.EndReceive(e.AsyncResult);
                msg.Formatter = new XmlMessageFormatter(new string[] { "System.String, mscorlib" });
                string data = msg.Body.ToString();
                Console.WriteLine($"Message Received: {data}");
                queue.BeginReceive();

            }
            catch (MessageQueueException qex)
            {
                Console.WriteLine(qex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
