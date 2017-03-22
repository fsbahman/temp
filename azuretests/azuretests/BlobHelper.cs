using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuretests
{
    public class BlobHelper
    {
        public static string CreateBlob(string id)
        {
            var storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=fsbahman;AccountKey=wiVmrKH6KdML24nMso35n+ASil1XtMhpRPFVRmvoZdFcHg13zkCipkjW8M9n+OVFAbTDd/HND7jxTczIFzFmtw==";
            var _cloudBolbContainer = CloudStorageAccount.Parse(storageConnectionString)
                .CreateCloudBlobClient().GetContainerReference("hanif");
            byte[] contentBytes = Encoding.Unicode.GetBytes("salam");
            var blockBlob = _cloudBolbContainer.GetBlockBlobReference(id);
            //blockBlob.UploadFromByteArray(contentBytes, 0, contentBytes.Length, null, null, null);
            //var sasConstraints = new SharedAccessBlobPolicy()
            //{
            //    SharedAccessStartTime = new DateTimeOffset?(DateTime.UtcNow.AddMinutes(-5.0)),
            //    SharedAccessExpiryTime = new DateTimeOffset?(DateTime.UtcNow.Add(TimeSpan.FromHours(24.0))),
            //    Permissions = (SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Delete)
            //};
            //var sasBlobToken = blockBlob.Uri.ToString() + blockBlob.GetSharedAccessSignature(sasConstraints);
            //blockBlob.UploadFromByteArray(contentBytes, 0, contentBytes.Length, null, null, null);
            var sasConstraints = new SharedAccessBlobPolicy()
            {
                SharedAccessStartTime = new DateTimeOffset?(DateTime.UtcNow.AddMinutes(-5.0)),
                SharedAccessExpiryTime = new DateTimeOffset?(DateTime.UtcNow.Add(TimeSpan.FromHours(24.0))),
                Permissions = (SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Delete | SharedAccessBlobPermissions.Create)
            };
            var sasBlobToken = blockBlob.Uri.ToString() + blockBlob.GetSharedAccessSignature(sasConstraints);

            var sasBlockBlob = new CloudBlockBlob(new Uri(sasBlobToken));
            sasBlockBlob.UploadFromByteArray(contentBytes, 0, contentBytes.Length, null, null, null);

            return sasBlobToken;
        }
    }
}
