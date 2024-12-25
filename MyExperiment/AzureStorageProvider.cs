using Azure;
using Azure.Storage.Queues;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCloudProject.Common;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MyExperiment
{
    public class AzureStorageProvider : IStorageProvider
    {
        /// <summary>
        /// Represents a collection of private readonly fields used for various services and configurations.
        /// Each field is initialized in the class constructor and is used to interact with specific components or services.
        /// </summary>
        /// <field name="_config">An instance of the configuration class providing settings for the application.</field>
        /// <field name="_blobServiceClient">A client used to interact with Azure Blob Storage service.</field>
        /// <field name="_queueClient">A client used to interact with Azure Queue Storage service.</field>
        /// <field name="_tableServiceClient">A client used to interact with Azure Table Storage service.</field>
        /// <field name="_tempDirectory">A string representing the path to the temporary directory used for file storage.</field>
        /// <field name="_logger">An instance of a logging interface used for logging application activities and errors.</field>

        private readonly MyConfig _config;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly QueueClient _queueClient;
        private readonly TableServiceClient _tableServiceClient;
        private readonly string _tempDirectory;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorageProvider"/> class.
        /// This constructor sets up the Azure storage clients and logging infrastructure by:
        /// 1. Binding configuration settings from the provided <paramref name="configSection"/> to a <see cref="MyConfig"/> instance.
        /// 2. Creating clients for Blob Storage, Queue Storage, and Table Storage using the connection string and settings from the configuration.
        /// 3. Initializing the <paramref name="logger"/> for logging purposes.
        /// 4. Setting up a temporary directory for storing input and output files.
        /// </summary>
        /// <param name="configSection">The configuration section containing the settings for Azure storage services.</param>
        /// <param name="logger">The logger instance used for logging application activities and errors.</param>
        public AzureStorageProvider(IConfigurationSection configSection, ILogger logger)
        {
            _config = new MyConfig();
            configSection.Bind(_config);
            _blobServiceClient = new BlobServiceClient(_config.StorageConnectionString);
            _queueClient = new QueueClient(_config.StorageConnectionString, _config.Queue);
            _tableServiceClient = new TableServiceClient(_config.StorageConnectionString);
            _logger = logger; // Initialize the logger

            // Create a temporary directory for input and output files
            _tempDirectory = Path.Combine(Path.GetTempPath(), "ExperimentTemp");
            Directory.CreateDirectory(_tempDirectory);
        }

        /// <summary>
        /// Asynchronously commits a request by deleting a message from the queue.
        /// The method attempts to delete the message identified by the specified <paramref name="messageId"/> and <paramref name="popReceipt"/>.
        /// It includes retry logic to handle transient failures, with exponential backoff for retries. The method:
        /// 1. Logs the message ID being committed.
        /// 2. Attempts to delete the message from the queue. If the message is successfully deleted, the method exits the retry loop.
        /// 3. Handles specific exceptions where the message is not found in the queue and other request-related errors, retrying up to a maximum number of attempts.
        /// 4. Logs and rethrows unexpected exceptions for further handling if necessary.
        /// </summary>
        /// <param name="messageId">The unique identifier of the message to be deleted from the queue.</param>
        /// <param name="popReceipt">The receipt associated with the message, used for authentication when deleting the message.</param>
        /// <returns>A task representing the asynchronous operation.</returns>

        public async Task CommitRequestAsync(string messageId, string popReceipt)
        {
            int retryCount = 0;
            int maxRetries = 3;
            TimeSpan delay = TimeSpan.FromSeconds(2);

            while (retryCount < maxRetries)
            {
                try
                {
                    // Log the message being committed
                    Console.WriteLine($"Committing request with MessageId = {messageId}");

                    // Delete the processed message from the queue
                    await _queueClient.DeleteMessageAsync(messageId, popReceipt);
                    Console.WriteLine($"Message deleted from the queue: MessageId = {messageId}");
                    break; // Exit loop on success
                }
                catch (RequestFailedException ex) when (ex.ErrorCode == "MessageNotFound")
                {
                    // Log and handle the case where the message is not found in the queue
                    Console.WriteLine($"Message with MessageId = {messageId} not found in the queue.");
                    break;
                }
                catch (RequestFailedException ex)
                {
                    // Log and handle other queue-related exceptions
                    Console.WriteLine($"Error deleting message from queue: {ex.Message}");

                    retryCount++;
                    if (retryCount >= maxRetries)
                    {
                        throw; // Rethrow the exception if max retries reached
                    }

                    await Task.Delay(delay); // Wait before retrying
                    delay = delay * 2; // Exponential backoff
                }
                catch (Exception ex)
                {
                    // Log and handle unexpected exceptions
                    Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                    throw; // Rethrow the exception for further handling if necessary
                }
            }
        }
        /// <summary>
        /// Asynchronously downloads blobs from a specified container and directory to a local temporary directory.
        /// The method:
        /// 1. Retrieves the blob container client for the specified <paramref name="containerName"/>.
        /// 2. Lists the blobs in the specified <paramref name="inputDirectory"/> within the container.
        /// 3. Creates a unique temporary directory to store the downloaded blobs.
        /// 4. Downloads each blob to the temporary directory, using the blob name to determine the local file path.
        /// 5. Returns the path to the temporary directory where the blobs are stored.
        /// 6. Handles and logs any exceptions that occur during the download process.
        /// </summary>
        /// <param name="containerName">The name of the blob container from which to download the blobs.</param>
        /// <param name="inputDirectory">The directory within the container to list and download blobs from.</param>
        /// <returns>A task representing the asynchronous operation, with a string result indicating the path to the temporary directory where the blobs were downloaded.</returns>

        public async Task<string> DownloadInputAsync(string containerName, string inputDirectory)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobs = containerClient.GetBlobsAsync(prefix: inputDirectory);

                string tempDir = Path.Combine(_tempDirectory, Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempDir);

                await foreach (var blobItem in blobs)
                {
                    var blobClient = containerClient.GetBlobClient(blobItem.Name);
                    string localFilePath = Path.Combine(tempDir, Path.GetFileName(blobItem.Name));

                    using (var fileStream = File.OpenWrite(localFilePath))
                    {
                        await blobClient.DownloadToAsync(fileStream);
                    }
                }

                Console.WriteLine($"Downloaded input to: {tempDir}");
                return tempDir;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading input: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Asynchronously receives and processes a message from the queue.
        /// Retrieves a message, logs its content, and attempts to deserialize it into an <see cref="ExerimentRequestMessage"/>.
        /// Returns a tuple containing the deserialized request, message ID, and pop receipt. Handles and logs errors as needed.
        /// </summary>
        /// <param name="token">Cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. Returns a tuple with the deserialized request, message ID, and pop receipt.</returns>


        public async Task<(IExerimentRequest request, string messageId, string popReceipt)> ReceiveExperimentRequestAsync(CancellationToken token)
        {
            try
            {
                // Receive message from queue
                QueueMessage message = await _queueClient.ReceiveMessageAsync(cancellationToken: token);

                if (message != null)
                {
                    // Log raw message for debugging purposes
                    Console.WriteLine($"Raw message: {message.MessageText}");

                    // Since messages are plain text JSON, no need for Base64 decoding
                    string messageText = message.MessageText;

                    Console.WriteLine($"Message text: {messageText}");
                    Console.WriteLine($"Status: Experiment Request is being processed! It will take some time!");

                    // Deserialize the JSON message
                    try
                    {
                        var requestMessage = JsonSerializer.Deserialize<ExerimentRequestMessage>(messageText, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (requestMessage == null)
                        {
                            Console.WriteLine("Failed to deserialize the message. The deserialized object is null.");
                            return (null, message.MessageId, message.PopReceipt);
                        }

                        // Return the deserialized request along with message ID and pop receipt
                        return (requestMessage, message.MessageId, message.PopReceipt);
                    }
                    catch (JsonException jsonEx)
                    {
                        // Log specific JSON deserialization errors
                        Console.WriteLine($"JSON Deserialization error: {jsonEx.Message}");
                        return (null, message.MessageId, message.PopReceipt);
                    }
                }

                return (null, null, null);
            }
            catch (Exception ex)
            {
                // Log general errors
                Console.WriteLine($"Error receiving message: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously uploads files from specified folders based on the experiment type to a storage service.
        /// Validates inputs, processes the folders, and returns a list of uploaded file URLs.
        /// Logs progress and errors during the upload process.
        /// </summary>
        /// <param name="result">The experiment result defining which folders to upload.</param>
        /// <param name="baseOutputDir">The base directory of the files to be uploaded.</param>
        /// <returns>A task representing the asynchronous operation with a list of URLs for the uploaded files.</returns>

        public async Task<List<string>> UploadExperimentResult(IExperimentResult result, string baseOutputDir)
        {
            if (result == null || string.IsNullOrEmpty(baseOutputDir))
            {
                throw new ArgumentException("Invalid result or output directory");
            }

            if (_logger == null)
            {
                throw new InvalidOperationException("Logger is not initialized.");
            }

            baseOutputDir = Path.GetFullPath(baseOutputDir);
            _logger.LogInformation($"Starting upload process. Base output directory: {baseOutputDir}");

            if (!Directory.Exists(baseOutputDir))
            {
                _logger.LogError($"Base output directory not found: {baseOutputDir}");
                throw new DirectoryNotFoundException($"Base output directory not found: {baseOutputDir}");
            }

            var uploadedFileUrls = new List<string>();

            try
            {
                var foldersToUpload = new List<string>();

                if (result.ExperimentType.Equals("imagebinarizerspatialpattern", StringComparison.OrdinalIgnoreCase))
                {
                    foldersToUpload.Add("SimilarityPlots_Image_Inputs");
                    foldersToUpload.Add("1DHeatMap_Image_Inputs");
                    foldersToUpload.Add("DifferenceHeatmapImages_ImageInput");
                }
                else if (result.ExperimentType.Equals("spatialpatternlearning", StringComparison.OrdinalIgnoreCase))
                {
                    foldersToUpload.Add("SimilarityPlots");
                    foldersToUpload.Add("1DHeatMap");
                    foldersToUpload.Add("DifferenceHeatmapImages");
                }
                else
                {
                    throw new ArgumentException($"Unknown experiment type: {result.ExperimentType}", nameof(result.ExperimentType));
                }

                foreach (var folder in foldersToUpload)
                {
                    var folderPath = Path.Combine(baseOutputDir, folder);
                    if (Directory.Exists(folderPath))
                    {
                        _logger.LogInformation($"Processing folder: {folderPath}");
                        await UploadFolderAsync(folderPath, folder, uploadedFileUrls);
                    }
                    else
                    {
                        _logger.LogWarning($"Folder not found: {folderPath}");
                    }
                }

                _logger.LogInformation("All specified folders uploaded successfully.");
                return uploadedFileUrls;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during the upload process. Base output directory: {baseOutputDir}");
                throw;
            }
        }
        /// <summary>
        /// Recursively uploads all files and subfolders from a specified folder to a storage service.
        /// The method:
        /// 1. Uploads each file in the folder and its subfolders, appending the folder name to the blob name.
        /// 2. Updates the list of file URLs with the uploaded file URLs.
        /// </summary>
        /// <param name="folderPath">The path of the folder to upload.</param>
        /// <param name="folderName">The name of the folder used to construct blob names.</param>
        /// <param name="fileUrls">A list to store URLs of uploaded files.</param>
        /// <returns>A task representing the asynchronous operation.</returns>

        private async Task UploadFolderAsync(string folderPath, string folderName, List<string> fileUrls)
        {
            foreach (var file in Directory.GetFiles(folderPath))
            {
                var relativePath = Path.GetRelativePath(folderPath, file);
                var blobName = $"{folderName}/{relativePath}";
                await UploadFileAsync(file, blobName, fileUrls);
            }

            foreach (var subFolder in Directory.GetDirectories(folderPath))
            {
                var subFolderName = Path.GetRelativePath(folderPath, subFolder);
                await UploadFolderAsync(subFolder, $"{folderName}/{subFolderName}", fileUrls);
            }
        }
        /// <summary>
        /// Asynchronously uploads a file to a specified blob storage container and logs the process.
        /// The method:
        /// 1. Uploads the file from <paramref name="filePath"/> to the blob specified by <paramref name="blobName"/>.
        /// 2. Adds the file's URL to the <paramref name="fileUrls"/> list upon successful upload.
        /// 3. Logs information about the upload and errors if the upload fails.
        /// </summary>
        /// <param name="filePath">The local path of the file to upload.</param>
        /// <param name="blobName">The name of the blob where the file will be uploaded.</param>
        /// <param name="fileUrls">A list to store URLs of the uploaded files.</param>
        /// <returns>A task representing the asynchronous operation.</returns>


        private async Task UploadFileAsync(string filePath, string blobName, List<string> fileUrls)
        {
            try
            {
                var blobClient = new BlobClient(_config.StorageConnectionString, _config.ResultContainer, blobName);

                using (var fileStream = File.OpenRead(filePath))
                {
                    await blobClient.UploadAsync(fileStream, overwrite: true);
                }

                _logger.LogInformation($"File uploaded: {filePath}");

                var fileUrl = $"https://{_config.StorageConnectionString}.blob.core.windows.net/{_config.ResultContainer}/{blobName}";
                fileUrls.Add(fileUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to upload file: {filePath}");
                throw;
            }
        }
        /// <summary>
        /// Asynchronously creates or updates a table entry with experiment results and metadata.
        /// The method:
        /// 1. Ensures the table exists, creating it if necessary.
        /// 2. Creates a new <see cref="TableEntity"/> with experiment and request details, including URLs of uploaded files.
        /// 3. Adds the entity to the table and logs the outcome, handling and logging any errors that occur.
        /// </summary>
        /// <param name="result">The experiment result to store in the table.</param>
        /// <param name="fileUrls">The URLs of files associated with the experiment result.</param>
        /// <param name="request">The request details related to the experiment.</param>
        /// <returns>A task representing the asynchronous operation.</returns>

        public async Task CreateResultTableAsync(IExperimentResult result, List<string> fileUrls, IExerimentRequest request)
        {
            var tableClient = _tableServiceClient.GetTableClient(_config.ResultTable);
            await tableClient.CreateIfNotExistsAsync();

            // Ensure PartitionKey and RowKey are set
            var entity = new TableEntity
            {
                // Use ExperimentId or another suitable value
                PartitionKey = result.ExperimentId,
                // Generate a unique RowKey
                RowKey = Guid.NewGuid().ToString(), 
                Timestamp = DateTime.UtcNow,
                // Add additional properties as needed
                ["ExperimentId"] = request.ExperimentId,
                ["InputFile"] = request.InputFile,
                ["InputDirectory"] = request.InputDirectory,
                ["ExperimentType"] = request.ExperimentType,
                ["RequestedBy"] = request.RequestedBy,
                ["MaxValue"] = request.MaxValue,
                ["Description"] = request.Description,
                ["MessageId"] = request.MessageId,
                ["MessageReceipt"] = request.MessageReceipt,
                ["ResultData"] = JsonSerializer.Serialize(result),
                ["OutputUrls"] = JsonSerializer.Serialize(fileUrls),
                // Add a timestamp when the result was created
                ["CompletionTimestamp"] = DateTime.UtcNow 
            };

            try
            {
                await tableClient.AddEntityAsync(entity);
                Console.WriteLine("Result table updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating result table: {ex.Message}");
                throw;
            }
        }

    }
}
