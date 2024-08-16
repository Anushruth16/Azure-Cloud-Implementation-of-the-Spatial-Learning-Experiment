using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// Retrieve the connection string for use with the application. 
string connectionString = "DefaultEndpointsProtocol=https;AccountName=labccex5;AccountKey=Div12BAgl5KqnAH5zX3OfKYcA28+Rn6saVJ48kQ3/A3ri8gqoHXsaBlSEG1aD+c0I8X21XBWh9z9+AStsZPb2Q==;EndpointSuffix=core.windows.net";

// Create a BlobServiceClient object 
var blobServiceClient = new BlobServiceClient(connectionString);

//Create a unique name for the container
string containerName = "outputfile" + Guid.NewGuid().ToString();

// Create the container and return a container client object
BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

// Create a local file in the ./data/ directory for uploading and downloading
string localPath = "data";
Directory.CreateDirectory(localPath);
string fileName = "photo" + Guid.NewGuid().ToString() + ".jpg";
string localFilePath = Path.Combine(localPath, fileName);

// Here, you should have an existing photo file to upload
string photoFilePath = "C:/Users/ANUSHRUTH/Documents/GitHub/se-cloud-2023-2024/CC_MyWork/Exercise5/ConsoleApp1/ConsoleApp1/bin/Debug/net8.0/data/Anushruth.jpg";

// Copy the photo to the localFilePath to simulate a new file in the./ data / directory
File.Copy(photoFilePath, localFilePath, true);

// Write text to the file
//await File.WriteAllTextAsync(localFilePath, "Hello, World");

// Get a reference to a blob
BlobClient blobClient = containerClient.GetBlobClient(fileName);

Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

// Upload data from the local file, overwrite the blob if it already exists
//await blobClient.UploadAsync(localFilePath, true);

using FileStream uploadFileStream = File.OpenRead(localFilePath);
await blobClient.UploadAsync(localFilePath, true);
uploadFileStream.Close();

Console.WriteLine("Listing blobs...");

// List all blobs in the container
await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
{
    Console.WriteLine("\t" + blobItem.Name);
}


// Download the blob to a local file
// Append the string "DOWNLOADED" before the .txt extension 
// so you can compare the files in the data directory
string downloadFilePath = localFilePath.Replace(".jpg", "DOWNLOADED.jpg");

Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

// Download the blob's contents and save it to a file
//await blobClient.DownloadToAsync(downloadFilePath);

BlobDownloadInfo download = await blobClient.DownloadAsync();

using (FileStream downloadFileStream = File.OpenWrite(downloadFilePath))
{
    await download.Content.CopyToAsync(downloadFileStream);
    downloadFileStream.Close();
}