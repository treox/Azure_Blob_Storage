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
            //String strorageconn = System.Configuration.ConfigurationSettings.AppSettings.Get("DefaultEndpointsProtocol=https;AccountName=evryhackathon2017storage;AccountKey=PwSmSHZ0OmdBOLyz3Ygd4o8n7e5PKVxtM5Zk7fBLad0bhQM0FISpFADiXzGl2SVYupV4VKI9WJHgQwXIkYiMmg==;EndpointSuffix=core.windows.net");
            CloudStorageAccount storageacc = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evryhackathon2017storage;AccountKey=PwSmSHZ0OmdBOLyz3Ygd4o8n7e5PKVxtM5Zk7fBLad0bhQM0FISpFADiXzGl2SVYupV4VKI9WJHgQwXIkYiMmg==;EndpointSuffix=core.windows.net");

            //Create Reference to Azure Blob
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            //The next 2 lines create if not exists a container named "democontainer"
            CloudBlobContainer container = blobClient.GetContainerReference("democontainer");

            container.CreateIfNotExists();

            //The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("Images");
            Console.Write("Image name: ");
            string imageName = Console.ReadLine();
            string image = path + imageName;
            
            using (var filestream = File.OpenRead(image))
            {
                blockBlob.UploadFromStream(filestream);
            }
            
            Download(container, path);

            Console.ReadLine();

        }

        static void Download(CloudBlobContainer container, string str)
        {
            Console.WriteLine("Ladda ner image, ge namn på filen: ");
            var namn = Console.ReadLine();
            var img = str + namn;
            //The next 6 lines download the file test.txt with the name test.txt from the container "democontainer"
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("Images");
            using (var filestream = File.OpenWrite(img))
            {
                blockBlob.DownloadToStream(filestream);
            }
        }
    }
}

