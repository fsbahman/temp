using selfhost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace selfhost.Controllers
{
    public class CustomerController : ApiController
    {
        public Customer GetCustomerByID(string id)
        {
            return new Customer {
                id = Guid.NewGuid(),
                name = "Name1"
            };
        }

        //[HttpPost]
        //[ActionName("SendData")]
        //public void SendData(HttpRequestMessage request)
        //{
        //    var data = request.Content.ReadAsStringAsync().Result;
        //    Console.WriteLine(data);
        //    //return null;
        //}

        [HttpPost]
        [ActionName("Enqueu")]
        public void Enqueu(HttpRequestMessage request)
        {
            string queuePath = @".\private$\messageq";
            var queue = new System.Messaging.MessageQueue(queuePath);
            var data = request.Content.ReadAsStringAsync().Result;
            if (MessageQueue.Exists(queue.Path))
            {

            }
            else
            {
                MessageQueue.Create(queue.Path);
            }
            EnqueuCore(data, queue);
        }

        private void EnqueuCore(string data, MessageQueue queue)
        {
            //var msg = new Message();
            //msg.Body = data;
            //msg.Label = "Data at " +DateTime.Now.ToString();
            queue.Formatter = new System.Messaging.XmlMessageFormatter();
            //queue.ReceiveCompleted += billingQ _ReceiveCompleted;
            queue.Send(data, "Data at " + DateTime.Now.ToString());
            //queue.BeginReceive();
            queue.Close();
        }

    }
}
