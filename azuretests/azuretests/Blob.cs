using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuretests
{
    public class Blob
    {
        static string sasUrl = "DefaultEndpointsProtocol=https;AccountName=eolsalesordereuwpstoacc1;AccountKey=KQCUSxh0HDblTxf1LsOQtP6nRR6xRovXX8+EKn/+H33W7n2GqpqYn45I0+N94iQ3JsU1SIcMZbwVeBSi+AW8/A==";

        public static IEnumerable<CloudBlob> ReadAll()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("eolsalesordersshippingready");


            var sasConstraints = new SharedAccessBlobPolicy()
            {
                SharedAccessStartTime = new DateTimeOffset?(DateTime.UtcNow.AddMinutes(-5.0)),
                SharedAccessExpiryTime = new DateTimeOffset?(DateTime.UtcNow.Add(TimeSpan.FromHours(24.0))),
                Permissions = (SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Delete)
            };
            
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                var blobName = string.Empty;
                var blobUri = item.Uri.ToString();
                int index = blobUri.LastIndexOf("/");
                if (index != -1)
                {
                    blobName = blobUri.Substring(index + 1);
                }
                CloudBlockBlob blob = container.GetBlockBlobReference(blobName );
                yield return blob;
            }
        }

        public static string CreateBlob()
        {
            CloudBlobContainer _cloudBolbContainer;
            _cloudBolbContainer = CloudStorageAccount.Parse(sasUrl)
                .CreateCloudBlobClient().GetContainerReference("eolsalesordersshippingready");
            _cloudBolbContainer.CreateIfNotExists();
            byte[] contentBytes = Encoding.Unicode.GetBytes("Test Data");
            var blockBlob = _cloudBolbContainer.GetBlockBlobReference("ATestReference");
            blockBlob.UploadFromByteArray(contentBytes, 0, contentBytes.Length, null, null, null);
            var sasConstraints = new SharedAccessBlobPolicy()
            {
                SharedAccessStartTime = new DateTimeOffset?(DateTime.UtcNow.AddMinutes(-5.0)),
                SharedAccessExpiryTime = new DateTimeOffset?(DateTime.UtcNow.Add(TimeSpan.FromHours(24.0))),
                Permissions = (SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Delete)
            };
            var sasBlobToken = blockBlob.Uri.ToString() + blockBlob.GetSharedAccessSignature(sasConstraints);
            return sasBlobToken;
        }
    }
}
