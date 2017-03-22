using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading;

namespace azuretests
{
    class Program
    {
        public static string connectionstring = CloudConfigurationManager.GetSetting("Microsoft.Storage.ConnectionString");
        static void Main(string[] args)
        {
            BlobHelper.CreateBlob("abcd");
            //var ts = new TableStore();

            //ts.ModifyTopRows(300, 200);
            //ts.DeleteAll();
            //testPosition();
            //var now = DateTime.Now;
            //Utility.ShowFakeProgress(() => Thread.Sleep(500), () => { return done(now); });
            //UseProgressBar();


            Console.WriteLine("Press any key ...");
            Console.ReadKey();

            //TestEnumerable();
            //Blob.CreateBlob();
            //return;
            //GenerateFakeEOLMessage();
            //Console.WriteLine("Press any key...");
            //Console.ReadKey();
            //sendBigFile();
        }

        private static void UseProgressBar()
        {
            ProgressBar pb = new ProgressBar("Counting", ProgressType.BarDashType4);
            pb.Start();
            var r = new Random();
            for (int i = 0; i < 100; i++)
            {
                pb.Pulse((100 - i).ToString());
                Thread.Sleep(100);
            }
            pb.Stop();
        }

        static bool done(DateTime started)
        {
            return DateTime.Now.Subtract(started).TotalSeconds > 10;
        }

        static void testPosition()
        {
            for (int i = 0; i < 100; i++)
            {
                Utility.WriteAtPosition($"{i}", 10, 10);
                Thread.Sleep(500);
            }
        }



        private static void TestEnumerable()
        {
            List<Model> list = TestEnumerables.GetAll().ToList();
            foreach (var item in list)
            {
                item.Name = $"Bahman{DateTime.Now.Ticks}";
            }

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            IEnumerable<Model> list2 = TestEnumerables.GetAll();
            IEnumerable<Model> list3 = TestEnumerables.GetAll2();


            foreach (var model in TestEnumerables.GetAll())
            {
                Console.WriteLine(model);
            }
        }

        static void GenerateFakeEOLMessage()
        {
            var sasConstraints = new SharedAccessBlobPolicy()
            {
                SharedAccessStartTime = new DateTimeOffset?(DateTime.UtcNow.AddMinutes(-5.0)),
                SharedAccessExpiryTime = new DateTimeOffset?(DateTime.UtcNow.Add(TimeSpan.FromHours(24.0))),
                Permissions = (SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Delete)
            };
            int counter = 0;
            foreach (var blob in Blob.ReadAll())
            {
                Console.WriteLine($"{counter++} Message created.");
                var signiture = blob.GetSharedAccessSignature(sasConstraints);

                ServiceBus.CreateMessage(new Uri(blob.Uri, signiture));
            }
        }

        static void sendBigFile()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionstring);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("somethingbig");
            container.CreateIfNotExists();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(@"D:\code\temp\azuretests\azuretests\bigfile.bmp"))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
    }
}
