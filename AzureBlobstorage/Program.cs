using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace AzureBlobstorage
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\aslan.DESKTOP-T1ACL15\Documents\Min studie\Hackarton\Azure_Blob_Storage\AzureBlobstorage\";
            CloudStorageAccount storageacc = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evryhackathon2017storage;AccountKey=PwSmSHZ0OmdBOLyz3Ygd4o8n7e5PKVxtM5Zk7fBLad0bhQM0FISpFADiXzGl2SVYupV4VKI9WJHgQwXIkYiMmg==;EndpointSuffix=core.windows.net");

            //Create Reference to Azure Blob
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            container.CreateIfNotExists();

            Console.Write("Image name: ");
            string imageName = Console.ReadLine();
            string image = path + imageName;


            Upload(container, image, imageName);

            Console.WriteLine("Ladda ner image, ge namn på filen: ");
            var name = Console.ReadLine();
            var outputPath = path + name;

            Download(container, outputPath, imageName);

            Console.ReadLine();

        }

        static void Upload(CloudBlobContainer container, string image, string imageName)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
            try
            {
                using (var filestream = File.OpenRead(image))
                {
                    blockBlob.UploadFromStream(filestream);
                }
            }
            catch (Exception e)
            {
                // Exception
            }
        }

        static void Download(CloudBlobContainer container, string outputPath, string blobName)
        {

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            using (var filestream = File.OpenWrite(outputPath))
            {
                blockBlob.DownloadToStream(filestream);
            }
        }
    }
}

