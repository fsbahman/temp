using Microsoft.Azure;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuretests
{
    public static class ServiceBus
    {
        static string SubscriptionName = "salesorder-subscriber";
        static string TopicName = "salesorderevents";
        static string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        static TopicClient client;

        static ServiceBus()
        {
            client = TopicClient.CreateFromConnectionString(connectionString, TopicName);
        }

        public static void CreateMessage(Uri blobUrl)
        {
            var message = new BrokeredMessage() { MessageId = Guid.NewGuid().ToString("N") };
            message.Properties["Diverted"] = blobUrl;
            message.Properties["ExecTime"] = DateTime.UtcNow;
            client.Send(message);
        }
    }
}
