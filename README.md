# Implementation of the Visualization of Permanence Values
## _Cloud Computing Project 2024_
#### _Tech Used_

| ![Image 1](https://ddobric.github.io/neocortexapi/images/logo-NeoCortexAPI.svg) | ![Image 2](https://upload.wikimedia.org/wikipedia/commons/a/a8/Microsoft_Azure_Logo.svg) |
|:------------------------------:|:------------------------------:|

# Introduction
Our project delves into the Reconstruct() function found in HTM's NeoCortexAPI, with the goal of uncovering its full potential. This function, which operates as the inverse of the Spatial Pooler (SP), is central to our study. Entitled "Visualization of Reconstructed Permanence Values," our project aims to illustrate how the Reconstruct() function facilitates the rebuilding of input sequences within HTM's Spatial Pooler. By using Numbers and images as inputs, we seek to clarify the connection between inputs and outputs in HTM. Through this investigation, we aim to enhance the understanding and application of HTM technology.

# Goal of Cloud Computing Project

Our goal is to leverage cloud computing technologies to enhance the deployment and scalability of our project focused on HTM's Reconstruct() function. By utilizing Docker containers and Azure cloud services, we aim to efficiently run and manage our project in the cloud environment. This approach will not only streamline the execution of our experiments but also facilitate scalable and flexible analysis of HTM's Reconstruct() function. Through this integration, we seek to improve the accessibility and performance of our visualization and analysis tools, ultimately contributing to a more robust understanding of HTM technology and its applications in cloud-based environments.

# Details Implementation Available in Software Engineering Project

For a better understanding of the implementation, running, and how it works, please have a look at the Software Engineering project:

- **[Repository Link](https://github.com/BidhanPaul/neocortexapi_team.bji)**: This is the main repository where you can find the source code and other related files.
- **[Readme](https://github.com/BidhanPaul/neocortexapi_team.bji/blob/master/source/Docomentation%20neocortexapi_Team.bji/Readme.md)**: The README file contains detailed information about the project, including how to run and use it.

Feel free to explore and let us know if you have any questions!

## Features
- Separate Experiments for Numbers and Images
- Reconstructions of Encoded Inputs
- Visualizing of Reconstructed Input with Heatmap and Number and Compareing with Original Encoded inputs with Comparable graphical Representation using Combined Image.
- Calculating Similarity
- Plotting Similarity Graph.
## New Adaptation and Implemented Feature for Cloud Computing
- **Difference of Heatmap** has introduced a graphical visualization technique to understand visually where the comparable changes with the original and Reconstructed Inputs. 
- Runnig Image Experiment with more data with compare to Last time in Software Engineering.
##  Basic Workflow of Processing Experiment

![Chart](https://github.com/user-attachments/assets/bcfb020b-ca09-4b72-b3b7-1ff576c1a65e)

*Fig1. Basic workflow: This diagram illustrates the basic workflow of how the project is running.
##  The Cloud Environment Workflow and Architecture of Processing Experiment
![Architecture](https://github.com/user-attachments/assets/a6f9e943-c892-4d83-9881-30f6a2d0b4fa)

*Fig2. Cloud workflow: This diagram illustrates the cloud architecture workflow and how the project is running.
## Cloud Project Program Files Links:
- **[AzureStorageProvider.cs Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2023-2024/blob/Team.bji/Source/MyCloudProjectSample/MyExperiment/AzureStorageProvider.cs)**
- **[Experiment.cs Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2023-2024/blob/Team.bji/Source/MyCloudProjectSample/MyExperiment/Experiment.cs)**
- **[Program.cs Link](https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2023-2024/blob/Team.bji/Source/MyCloudProjectSample/MyCloudProject/Program.cs)**
## Components for Running the Project
**Note:** The project is currently not active on Azure Cloud. However, you can re-launch it by following the steps outlined below and configuring the components specified in Azure Cloud.
## Key Components

| **Component**                                                                                  | **Name**             | **Description**                                                       |
|------------------------------------------------------------------------------------------------|----------------------|-----------------------------------------------------------------------|
| [Resource Group](https://portal.azure.com/#@msdndaenet.onmicrosoft.com/resource/subscriptions/d60f2036-12f5-499d-af22-ef3afc698896/resourceGroups/RG_Team.bji/overview) | RG_Team.bji           | ---                                                                   |
| [Container Registry](https://portal.azure.com/#@msdndaenet.onmicrosoft.com/resource/subscriptions/d60f2036-12f5-499d-af22-ef3afc698896/resourceGroups/RG_Team.bji/providers/Microsoft.ContainerRegistry/registries/teambjicontainer/overview) | teambjiconatiner           | ---                                                                   |
| [Container Registry Server](teambjicontainer.azurecr.io)                                    |teambjicontainer.azurecr.io| ---                                                                   |
| [Repository](https://portal.azure.com/#view/Microsoft_Azure_ContainerRegistries/RepositoryBlade/id/%2Fsubscriptions%2Fd60f2036-12f5-499d-af22-ef3afc698896%2FresourceGroups%2FRG_Team.bji%2Fproviders%2FMicrosoft.ContainerRegistry%2Fregistries%2Fteambjicontainer/repository/mycloudproject) | mycloudproject:v1.1.0 | Name of my repository                                                 |
| [Container Instance](https://portal.azure.com/#@msdndaenet.onmicrosoft.com/resource/subscriptions/d60f2036-12f5-499d-af22-ef3afc698896/resourceGroups/RG_Team.bji/providers/Microsoft.ContainerInstance/containerGroups/reconstructionexperimentcontainer/overview) | reconstructionexperimentcontainer | Name of the container instance where experiment runs                  |
| [Storage Account](https://portal.azure.com/#@msdndaenet.onmicrosoft.com/resource/subscriptions/d60f2036-12f5-499d-af22-ef3afc698896/resourceGroups/RG_Team.bji/providers/Microsoft.Storage/storageAccounts/cloudteambjiproject/overview) | cloudteambjiproject          | ---                                                                   |
| [Queue Storage](https://portal.azure.com/#@msdndaenet.onmicrosoft.com/resource/subscriptions/d60f2036-12f5-499d-af22-ef3afc698896/resourceGroups/RG_Team.bji/providers/Microsoft.Storage/storageAccounts/cloudteambjiproject/storagebrowser) | trigger-queue     | Queue where trigger message is passed to initiate experiment           |
| [Blob Container](https://portal.azure.com/#blade/HubsExtension/Resources/resourceType/Microsoft.Storage%2FstorageAccounts) | training-files    | Containing the Input Images and they are already uploaded |
| [Blob Container](https://portal.azure.com/#@msdndaenet.onmicrosoft.com/resource/subscriptions/d60f2036-12f5-499d-af22-ef3afc698896/resourceGroups/RG_Team.bji/providers/Microsoft.Storage/storageAccounts/cloudteambjiproject/storagebrowser) | result-files      | Containing the output Images genarated by the Experiment       |
| [Table Storage](https://portal.azure.com/#@msdndaenet.onmicrosoft.com/resource/subscriptions/d60f2036-12f5-499d-af22-ef3afc698896/resourceGroups/RG_Team.bji/providers/Microsoft.Storage/storageAccounts/cloudteambjiproject/storagebrowser) | results     | Table used to store results of the experiment                         |


Table-1: The table below lists the key components utilized in the cloud-based project.
## Processing the queue Message for Two Different Experiments
## Experiment Queue For Spatialpatternlearning
**Note**: MaxValue is the input for Experiment: SpatialPatternLearning and For Experiment: imagebinarizerspatialpattern Input is Image and it will be downloaded from Azure Blob Storage.
```json

  {
    "ExperimentId": "exp-1",
    "InputFile": "training-files",
    "InputDirectory": "TestFiles/",
    "ExperimentType": "spatialpatternlearning",
    "RequestedBy": "bidhanpaul7@gmail.com",
    "MaxValue": 100,
    "Description": "This is a test experiment to analyze data.",
    "MessageId": "msg-1",
    "MessageReceipt": "receipt-1"
  }
  {
    "ExperimentId": "exp-2",
    "InputFile": "training-files",
    "InputDirectory": "TestFiles/",
    "ExperimentType": "imagebinarizerspatialpattern",
    "RequestedBy": "bidhanpaul7@gmail.com",
    "MaxValue": 100,
    "Description": "This is a test experiment to analyze data.",
    "MessageId": "msg-2",
    "MessageReceipt": "receipt-1"
  }
```
## Description of the Queue Message
| Field            | Description                                           | Example Value                            |
|------------------|-------------------------------------------------------|------------------------------------------|
| `ExperimentId`   | Unique identifier for the experiment                 | `exp-2`                                  |
| `InputFile`      | Name of the input file or dataset                     | `training-files`                         |
| `InputDirectory` | Directory path where the input files are located     | `TestFiles/`                             |
| `ExperimentType` | Type of experiment being conducted                   | `imagebinarizerspatialpattern`           |
| `RequestedBy`    | Email address of the person who requested the experiment | `bidhanpaul7@gmail.com`                  |
| `MaxValue`       | Maximum value used in the experiment                  | `100`                                    |
| `Description`    | Description of the experiment                        | `This is a test experiment to analyze data.` |
| `MessageId`      | Identifier for the message associated with the experiment | `msg-2`                                  |
| `MessageReceipt` | Receipt or acknowledgment of the message             | `receipt-1`                              |

Table-2: Description of the Queue Message.
### Implementation Details
## Method Documentation: `ReceiveExperimentRequestAsync`
```csharp
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
```
### Purpose
The `ReceiveExperimentRequestAsync` method is designed to handle receiving and processing messages from an Azure queue. The messages are expected to be JSON formatted, representing experiment requests. This method performs the following tasks:
1. Retrieves a message from the queue.
2. Logs the message for debugging.
3. Deserializes the JSON message into an `IExerimentRequest` object.
4. Returns the deserialized object along with the message's ID and pop receipt.

### Method Signature
```csharp
public async Task<(IExerimentRequest request, string messageId, string popReceipt)> ReceiveExperimentRequestAsync(CancellationToken token)
```
##  `DownloadInputAsync`
```csharp
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
```
### Purpose
The `DownloadInputAsync` method is designed to download files from an Azure Blob Storage container to a local temporary directory. The method performs the following tasks:
1. Connects to the specified blob container.
2. Lists and downloads blobs matching the specified directory prefix.
3. Saves the downloaded files to a local temporary directory.
4. Returns the path to the temporary directory containing the downloaded files.

### Method Signature
```csharp
public async Task<string> DownloadInputAsync(string containerName, string inputDirectory)
```
##  `UploadExperimentResult`
```csharp
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
```
### Purpose
The `UploadExperimentResult` method uploads the results of an experiment to Azure Blob Storage. It processes folders of results based on the experiment type and uploads the files within these folders to a specified storage container. The method also logs the progress and errors encountered during the upload process.

### Method Signatures
```csharp
public async Task<List<string>> UploadExperimentResult(IExperimentResult result, string baseOutputDir)
```

```csharp
private async Task UploadFolderAsync(string folderPath, string folderName, List<string> fileUrls)
```
```csharp
private async Task UploadFolderAsync(string folderPath, string folderName, List<string> fileUrls)
```
##  `CreateResultTableAsync`
```csharp
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
```
### Purpose
The `CreateResultTableAsync` method creates or updates a table in Azure Table Storage with the results of an experiment. It constructs a table entity with various properties including experiment details and result URLs, and adds this entity to the specified table.

### Method Signature
```csharp
public async Task CreateResultTableAsync(IExperimentResult result, List<string> fileUrls, IExerimentRequest request)
```
##  `CommitRequestAsync`
```csharp
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
```
### Purpose
The `CommitRequestAsync` method commits (or deletes) a message from the queue. It handles potential errors with retries and exponential backoff, ensuring that the message is successfully removed or that appropriate action is taken in case of failure.

### Method Signature
```csharp
public async Task CommitRequestAsync(string messageId, string popReceipt)
```
##  `RunAsync`
```csharp
Now for this one 
public async Task<IExperimentResult> RunAsync(
    string containerName,
    string inputDirectory,
    string experimentType,
    double? maxValue,
    IExerimentRequest request)
{
    var result = new ExperimentResult(Guid.NewGuid().ToString())
    {
        ExperimentType = experimentType,
        StartTimeUtc = DateTime.UtcNow
    };

    string localInputDirectory = string.Empty;
    // Base path where output folders are created
    string baseOutputDirectory = Environment.CurrentDirectory; 
    bool operationsSuccessful = false;
    // To keep track of uploaded files
    var uploadedFiles = new List<string>(); 

    try
    {
        // Ensure base output directory exists
        if (!Directory.Exists(baseOutputDirectory))
        {
            Directory.CreateDirectory(baseOutputDirectory);
            logger.LogInformation($"Base output directory created: {baseOutputDirectory}");
        }
        else
        {
            logger.LogInformation($"Base output directory already exists: {baseOutputDirectory}");
        }

        // Step 1: Download input files
        localInputDirectory = await storageProvider.DownloadInputAsync(containerName, inputDirectory);
        logger.LogInformation($"Downloaded input to: {localInputDirectory}");

        // Step 2: Run the experiment
        switch (experimentType.ToLower())
        {
            case "imagebinarizerspatialpattern":
                var binarizer = new ImageBinarizerSpatialPattern();
                binarizer.LoadFromDirectory(localInputDirectory);
                binarizer.Run();

                // Wait to ensure processing completion
                // Adjust delay if necessary
                await Task.Delay(2000); 

                // Handle specific output folders for ImageBinarizerSpatialPattern
                var imageBinarizerFolders = new[]
                {
                    "1DHeatMap_Image_Inputs",
                    "SimilarityPlots_Image_Inputs",
                    "DifferenceHeatmapImages_ImageInput"
                };

                var outputFiles = new List<string>();

                foreach (var folder in imageBinarizerFolders)
                {
                    var folderPath = Path.Combine(baseOutputDirectory, folder);
                    if (Directory.Exists(folderPath))
                    {
                        var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                        outputFiles.AddRange(files);
                        // Track files for deletion
                        uploadedFiles.AddRange(files); 
                    }
                    else
                    {
                        logger.LogWarning($"Folder does not exist: {folderPath}");
                    }
                }

                if (outputFiles.Any())
                {
                    logger.LogInformation($"Output directory contains files, proceeding with upload: {baseOutputDirectory}");

                    // Set output files for the result
                    result.OutputFiles = outputFiles.Select(filePath =>
                        $"https://cloudteambjiproject.blob.core.windows.net/result-files/{config.ResultContainer}/{Path.GetFileName(filePath)}"
                    ).ToArray();

                    // Step 3: Upload the results
                    await storageProvider.UploadExperimentResult(result, baseOutputDirectory);

                    // Step 4: Create result table entry
                    await storageProvider.CreateResultTableAsync(result, result.OutputFiles.ToList(), request);
                    // Mark operations as successful
                    operationsSuccessful = true; 
                }
                else
                {
                    logger.LogWarning($"Output directory is empty, no files to upload: {baseOutputDirectory}");
                }
                break;

            case "spatialpatternlearning":
                if (maxValue.HasValue)
                {
                    var patternLearning = new SpatialPatternLearning(maxValue.Value);
                    patternLearning.Run();

                    // Wait to ensure processing completion
                    // Adjust delay if necessary
                    await Task.Delay(2000); 

                    // Handle specific output folders for SpatialPatternLearning
                    var spatialPatternFolders = new[]
                    {
                        "1DHeatMap",
                        "SimilarityPlots",
                        "DifferenceHeatmapImages"
                    };

                    var patternFiles = new List<string>();

                    foreach (var folder in spatialPatternFolders)
                    {
                        var folderPath = Path.Combine(baseOutputDirectory, folder);
                        if (Directory.Exists(folderPath))
                        {
                            var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                            patternFiles.AddRange(files);
                            // Track files for deletion
                            uploadedFiles.AddRange(files); 
                        }
                        else
                        {
                            logger.LogWarning($"Folder does not exist: {folderPath}");
                        }
                    }

                    if (patternFiles.Any())
                    {
                        logger.LogInformation($"Output directory contains files, proceeding with upload: {baseOutputDirectory}");

                        // Set output files for the result
                        result.OutputFiles = patternFiles.Select(filePath =>
                            $"https://cloudteambjiproject.blob.core.windows.net/result-files/{config.ResultContainer}/{Path.GetFileName(filePath)}"
                        ).ToArray();

                        // Step 3: Upload the results
                        await storageProvider.UploadExperimentResult(result, baseOutputDirectory);

                        // Step 4: Create result table entry
                        await storageProvider.CreateResultTableAsync(result, result.OutputFiles.ToList(), request);

                        operationsSuccessful = true; // Mark operations as successful
                    }
                    else
                    {
                        logger.LogWarning($"Output directory is empty, no files to upload: {baseOutputDirectory}");
                    }
                }
                else
                {
                    throw new ArgumentException("MaxValue must be provided for SpatialPatternLearning.");
                }
                break;

            default:
                throw new ArgumentException($"Unknown experiment type: {experimentType}", nameof(experimentType));
        }

        result.EndTimeUtc = DateTime.UtcNow;
        result.Duration = result.EndTimeUtc.Value - result.StartTimeUtc.Value;
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error running the experiment");
        result.EndTimeUtc = DateTime.UtcNow;
        result.Duration = result.EndTimeUtc.Value - result.StartTimeUtc.Value;
    }
    finally
    {
        // Preserve directories and files for debugging
        logger.LogInformation($"Preserving input directory: {localInputDirectory}");
        logger.LogInformation($"Preserving output directory: {baseOutputDirectory}");

        // Delete only the files that were uploaded
        if (operationsSuccessful)
        {
            try
            {
                // Delete uploaded files in the specific output folders
                foreach (var file in uploadedFiles)
                {
                    try
                    {
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                            logger.LogInformation($"Output file deleted: {file}");
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        logger.LogError(ex, $"Access denied while deleting file: {file}");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error deleting file: {file}");
                    }
                }

                // Optionally delete the output directories if they are empty
                var imageBinarizerFolders = new[]
                {
                    "1DHeatMap_Image_Inputs",
                    "SimilarityPlots_Image_Inputs",
                    "DifferenceHeatmapImages_ImageInput"
                };

                var spatialPatternFolders = new[]
                {
                    "1DHeatMap",
                    "SimilarityPlots",
                    "DifferenceHeatmapImages"
                };

                foreach (var folder in imageBinarizerFolders.Concat(spatialPatternFolders))
                {
                    var folderPath = Path.Combine(baseOutputDirectory, folder);
                    try
                    {
                        if (Directory.Exists(folderPath))
                        {
                            // Ensure the directory is empty before attempting to delete
                            if (!Directory.GetFiles(folderPath).Any())
                            {
                                Directory.Delete(folderPath, recursive: true);
                                logger.LogInformation($"Output folder deleted: {folderPath}");
                            }
                            else
                            {
                                logger.LogWarning($"Directory not empty, skipping deletion: {folderPath}");
                            }
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        logger.LogError(ex, $"Access denied while deleting directory: {folderPath}");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error deleting directory: {folderPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting directories");
            }

            // Delete the downloaded input directory
            try
            {
                if (Directory.Exists(localInputDirectory))
                {
                    Directory.Delete(localInputDirectory, recursive: true);
                    logger.LogInformation($"Input directory deleted: {localInputDirectory}");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogError(ex, $"Access denied while deleting input directory: {localInputDirectory}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error deleting input directory: {localInputDirectory}");
            }
        }
    }

    // Ensure a result is always returned
    if (result.EndTimeUtc == null)
    {
        result.EndTimeUtc = DateTime.UtcNow;
        result.Duration = result.EndTimeUtc.Value - result.StartTimeUtc.Value;
    }
    return result;
}
 ```
### Purpose
The `RunAsync` method orchestrates the execution of an experiment by downloading input files, running the experiment based on the type, uploading results, and creating a result table entry. It handles different experiment types, manages temporary directories and files, and performs cleanup.

### Method Signature
```csharp
public async Task<IExperimentResult> RunAsync(
    string containerName,
    string inputDirectory,
    string experimentType,
    double? maxValue,
    IExerimentRequest request)
 ```
### Parameters

- **`containerName`**:
  - **Type**: `string`
  - **Description**: The name of the blob container where input files are stored.

- **`inputDirectory`**:
  - **Type**: `string`
  - **Description**: The directory within the blob container where input files are located.

- **`experimentType`**:
  - **Type**: `string`
  - **Description**: The type of experiment to run (e.g., "imagebinarizerspatialpattern" or "spatialpatternlearning").

- **`maxValue`**:
  - **Type**: `double?`
  - **Description**: The maximum value to use for "spatialpatternlearning" experiments. Optional for other types.

- **`request`**:
  - **Type**: `IExerimentRequest`
  - **Description**: The experiment request details.

### Returns

- **`Task<IExperimentResult>`**:
  - **Type**: `Task<IExperimentResult>`
  - **Description**: An asynchronous operation representing the completion of the experiment, returning the result.

### Process Flow

1. **Initialization**:
   - Creates a new `ExperimentResult` object with a unique ID and start time.
   - Defines the base output directory and prepares a list to track uploaded files.

2. **Ensure Directory Exists**:
   - Checks if the base output directory exists; if not, creates it.

3. **Download Input Files**:
   - Uses `storageProvider.DownloadInputAsync` to download input files from the blob container to a local directory.

4. **Run Experiment**:
   - **For `imagebinarizerspatialpattern`**:
     - Initializes and runs the `ImageBinarizerSpatialPattern` experiment.
     - Handles specific output folders and updates the result with output file URLs.
   - **For `spatialpatternlearning`**:
     - Validates `maxValue`, initializes, and runs the `SpatialPatternLearning` experiment.
     - Handles specific output folders and updates the result with output file URLs.
   - **Unknown Experiment Type**:
     - Throws an `ArgumentException` for unrecognized experiment types.

5. **Upload Results**:
   - Uses `storageProvider.UploadExperimentResult` to upload results to the blob storage.

6. **Create Result Table Entry**:
   - Uses `storageProvider.CreateResultTableAsync` to create an entry in the result table with experiment details and file URLs.

7. **Cleanup**:
   - Preserves directories and files for debugging.
   - Deletes uploaded files and output directories if operations were successful.
   - Deletes the local input directory.

### Exception Handling

- Catches and logs errors during the experiment execution.
- Ensures the result object is returned with updated end time and duration even if an error occurs.
##  `Main`
```csharp
static async Task Main(string[] args)
{
}
```
### Overview

The `Main` method is an asynchronous entry point for the application that handles the lifecycle of an experiment. It initializes configurations, logging, and the experiment environment. It then continuously processes experiment requests from a queue, executes the experiments, and manages the results.

### Process Flow

1. **Setup Cancellation Token**:
   - Creates a `CancellationTokenSource` to handle application shutdown and cancellation requests.

2. **Handle Console Cancellation**:
   - Subscribes to the `Console.CancelKeyPress` event to cancel the token when a console cancel key (Ctrl+C) is pressed.

3. **Initialize Configuration**:
   - Loads the configuration settings using `InitHelpers.InitConfiguration(args)`.
   - Retrieves the relevant configuration section (`MyConfig`).

4. **Initialize Logging**:
   - Sets up the logging factory and creates a logger for the application using `InitHelpers.InitLogging(cfgRoot)`.

5. **Initialize Storage Provider and Experiment**:
   - Creates an `IStorageProvider` instance (`AzureStorageProvider`) with the configuration section and logger.
   - Creates an `IExperiment` instance (`Experiment`) using the configuration section, storage provider, and logger.

6. **Process Experiment Requests**:
   - Enters a loop that runs until the cancellation token is triggered.
   - Receives an experiment request from the queue using `storageProvider.ReceiveExperimentRequestAsync`.

7. **Handle Experiment Request**:
   - If a request is received:
     - Downloads input files to a local directory using `storageProvider.DownloadInputAsync`.
     - Runs the experiment using `experiment.RunAsync` and gets the result.
     - If the output directory exists:
       - Uploads results and retrieves file URLs using `storageProvider.UploadExperimentResult`.
       - Creates a result table entry with `storageProvider.CreateResultTableAsync`.
       - Deletes the output directory after a successful upload.
     - If the output directory is not found:
       - Logs information and indicates successful upload to blob storage.

8. **Commit Request**:
   - Commits the request and deletes the message from the queue using `storageProvider.CommitRequestAsync`.

9. **Exception Handling**:
   - Catches and logs exceptions that occur during request processing and queue handling.
   - Handles specific errors in committing requests and deleting directories.

10. **Exit**:
    - Logs the exit message when the application shuts down.

### Parameters

- **`args`**:
  - **Type**: `string[]`
  - **Description**: Command-line arguments passed to the application.

### Returns

- **`Task`**:
  - **Type**: `Task`
  - **Description**: An asynchronous operation representing the completion of the `Main` method.

### Exception Handling

- Catches and logs exceptions during request processing and the queue processing loop.
- Handles specific errors related to queue operations and file deletion.

### Notes

- Ensure proper handling of temporary files and directories to avoid issues during experiment execution.
- Implement appropriate error handling and logging to diagnose issues during experiment processing and queue management.

# Configuration Documentation

## Logging

- **IncludeScopes**: `false`
  - Determines whether to include scopes in log messages.
- **LogLevel**:
  - **Default**: `Debug`
    - Sets the default logging level to `Debug`.
  - **System**: `Information`
    - Logs system-related messages at the `Information` level.
  - **Microsoft**: `Information`
    - Logs Microsoft-related messages at the `Information` level.

## MyConfig

- **GroupId**: `Team.bji`
  - Identifier for the team or project group.

- **StorageConnectionString**: `your-connection-string-here`
  - Connection string for accessing Azure Storage. Replace `your-connection-string-here` with your actual connection string.

- **TrainingContainer**: `training-files`
  - Name of the Azure Storage container where training files are stored.

- **ResultContainer**: `result-files`
  - Name of the Azure Storage container where result files are stored.

- **ResultTable**: `results`
  - Name of the Azure Table where experiment results are stored.

- **Queue**: `trigger-queue`
  - Name of the Azure Queue used for triggering experiment requests.
## New Method: `GenerateDifferenceHeatmaps`

### Parameters
- **`normalizedDataList`** (`List<int[]>`): A list of arrays containing normalized data.
- **`encodedDataList`** (`List<int[]>`): A list of arrays containing encoded data.
- **`outputFolderPath`** (`string`): The folder path where the generated heatmaps will be saved.
- **`bmpWidth`** (`int`, optional): The width of the bitmap. Defaults to 1024.
- **`bmpHeight`** (`int`, optional): The height of the bitmap. Defaults to 1024.
- **`enlargementFactor`** (`int`, optional): The factor by which to enlarge the bitmap. Defaults to 4.

### Overview
The method generates difference heatmaps comparing `normalizedDataList` and `encodedDataList` and saves them as PNG files in the specified folder.

### Process Flow

1. **Validate Data Lists**
   - Ensure both `normalizedDataList` and `encodedDataList` have the same number of elements.
   - Throw an exception if they do not match.

2. **Create Output Folder**
   - Check if the `outputFolderPath` exists; if not, create it.

3. **Helper Method: `GetHeatmapColor`**
   - Determines the color based on the difference between `normalizedValue` and `encodedValue` using XOR logic:
     - Red if there's a difference.
     - Green if there's no difference.

4. **Generate Heatmaps**
   - Loop through each pair of normalized and encoded data.
   - Create a new bitmap for each pair:
     - Size the bitmap based on the `enlargementFactor`.
     - Clear the canvas with a white background.
     - Calculate scale factors for enlarging.
     - Draw the heatmap by setting pixel colors according to the difference between normalized and encoded data.

5. **Save Heatmaps**
   - Save each generated heatmap as a PNG file in the `outputFolderPath`.

6. **Logging**
   - Log messages to indicate the successful creation and saving of heatmaps.

### Example

To call this method:

```csharp
GenerateDifferenceHeatmaps(
    normalizedDataList: myNormalizedDataList,
    encodedDataList: myEncodedDataList,
    outputFolderPath: @"C:\Heatmaps",
    bmpWidth: 1024,
    bmpHeight: 1024,
    enlargementFactor: 4
);
```

# Result  Discussion and Visualization of Permanence Values All Examples 
### 1. Visualization Examples for SpatialPatternLearning Experiment
In the below figure(3) we can see our combined Heatmap which has three separate parameters. The Generated Permanence visualization image consists of three parts. It has a heatmap, and it generates and interpolates colors according to the Permanent values that are restructured using the spatial pooler reconstruction method. In the second property, we visualize the original encoded Input bits for numbers and images for two types of inputs. The third and final property is the Reconstructed input. We keep all three things together for better understanding and comparing purposes to understand the specific input by comparing it with the original encoded input bits.
- Visualization of Reconstructed Inputs
  - HeatMapImage
  - Original Encoded Inputs
  - Reconstructed Normalized Inputs

A Complete Image Of Heatmap 
![heatmap_1](https://github.com/user-attachments/assets/50544549-8979-4123-9fb2-a134a16f49b6)
*Fig3. A Complete Heatmap Output Image.


### A Similarity Graph Between Original Inputs and Reconstructed Inputs 
In our experiment, we checked how well the reconstructed inputs matched the original encoded inputs by calculating the Jaccard similarity coefficient. To show our findings, we will display a bar graph that compares the similarities for both types of input data. We will start by focusing on numerical inputs, where we measure the similarity for all inputs that are of integer type. This approach helps us clearly see how closely the reconstructed inputs resemble the original ones for each data type. Here, in this graph, the Y-axis represents the similarity for each input 0 to 100%, and the X-axis represents the number of outputs. 

From Figure 4 below we can see the similarity graph which has 100 Inputs and Visualizes the level of similarity for each input. 
![combined_similarity_plot](https://github.com/user-attachments/assets/523ae068-ecf9-47b4-9099-42a6eb4908d1)
*Fig4. A Complete Similarity Graph Image.

##### Example of Input value 1 (Numeric Input) Visualization Examples are given below:
Here for value, 1 Scaler Encoder encodes the value as 0 and 1 bits with a range of 200 bits. From the output Image, we will get the top Heatmap image using the Permanence values and this is the visualization of permanence values using the Heatmap. Secondly, we will get the Enocded original input bit and after that the reconstructed Inputs by the reconstruction method of SP. We are Printing this way to understand the better visualization and to check the changes for each input in a single Image.
An example is below:
* A Spilt of HeatMap Output Image Contains
  * Heatmap Image
  * Encoded Inputs
  * Reconstructed Inputs

![image](https://github.com/user-attachments/assets/f35452e1-dacd-41c1-b9c1-09fec1848a93)

*Fig5. A Spilited Image of Heatmap Output.
* From Figure 6 we can see the output Image for Numeric Input 1 
  * Heatmap Image Represents the Permanence Values with Color Interpolation Here More Heat (Color Red) Represents Higher Permanence Colder means low Permanence (Color Green) and Yellow middle or Interpolation Represents the Permanence Values are not high or not low.
  * Encoded Inputs shows the Original Input bits here As an Example value 1 bit is 111111111111111000000000000000000.........
  * Reconstructed Inputs are Reconstructs the inputs as same as the original as an example for input value 1 
  Reconstructed input bits are 
1111111111111111000000000000000000.........
![image](https://github.com/user-attachments/assets/a3036ecb-7f0b-4a9c-a1d9-75d1c1e85970)
*Fig6. For Input 1 Heatmap Output Image.

For the similarity, here For Value 1, Similarity is Marked in Red as the First value in the Graph (x- Axix Inputs). From Figure 7 we will get the Similarity for Input 1 to 25 (Splitted Image) The Bar Marked Red has a Similarity of 90% with respect to Y- Axix and it is the Output Similarity of input Value 1 Resprent the x-Axis with the X-label of 1. 
![image](https://github.com/user-attachments/assets/59cb31b9-14d0-4565-8f63-47b9dea85d03)
*Fig7. Similarity Graph Output Image for Input 1.
##### Difference of Heatmaps:

The GenerateDifferenceHeatmaps function creates visual heatmaps that compare arrays of normalized and encoded data by highlighting their differences using an XOR operation. For each pair of arrays, the function generates a heatmap using only red and green colors, where red indicates no match, and green indicates a match between the encoded and reconstructed inputs. It labels each section with "Encoded," "Normalized Permanence," and "Difference," printing the corresponding values and connecting them with vertical lines to the heatmap for better clarity. The final output is saved as PNG files in a specified folder, providing a clear and informative visual representation of the data comparison. The newly modified Difference Heatmap function specifically addresses the need for clearer visualization by distinctly showing where encoded inputs, reconstructed inputs, and their differences occur: red highlights mismatches, while green confirms matches, enhancing overall understanding.
* Differences of HeatMap
  * Encoded Inputs
  * Reconstructed Inputs
  * Difference of Bits compared to Encoded and Reconstructed inputs
  * Difference of HeatMap
 ![difference_heatmap_1](https://github.com/user-attachments/assets/d11c0a59-3a84-4173-b5cf-da19034c1f7b)
*Fig8. A Complete Image of the Difference of HeatMap.
For Input 1, Difference of HeatMap is given below:
* A Difference of  HeatMap Output Image Contains
   * Encoded Inputs shows the Original Input bits here As an Example value 1 bit is 111111111111111000000000000000000.........
  * Reconstructed Inputs are Reconstructs the inputs as same as the original as an example for input value 1 
  Reconstructed input bits are 
1111111111111111000000000000000000.........
 * Differences in Inputs are generated by performing an XOR operation between Encoded inputs and Reconstructed Inputs
  Differences are shown in this 
000000000000001000000000000000000.........
This one in the middle represents the bit difference means this one will be Red to represent mismatched and for all 0 the color 

![image](https://github.com/user-attachments/assets/6f1c8d51-c0ec-4fce-8136-577130c3da1c)

*Fig9.  Difference of HeatMap for Input one.


### 2. Visualization Examples for ImageBinarizerSpatialPattern Experiment
For the image input, the output is also the same category as this but different in size because the size of the dependent variable is due to the size of the encoder and types of the input data. For instance, the size of the output of the scaler encoder is 200. On the other hand, for the image input, the encoded bits depend on the size of the input image size both (Height and Width). Here in this figure (10), we showed a full input of the reconstruction result and permanence values as a heatmap. In this figure (11) is clearly visible that the Reconstruction method can reconstruct the input, we presented the encoded input for each numerical input (0 to 1) as well to compare with the Reconstructed input to better understand
- Visualization of Reconstructed Inputs
  - HeatMapImage
  - Original Encoded Inputs
  - Reconstructed Normalized Inputs
![heatmap_1](https://github.com/user-attachments/assets/e5e6c126-3d32-4960-a149-34b8e991aefe)
*Fig10.  Output of HeatMap Image for Image Inputs.

![image](https://github.com/user-attachments/assets/fadd7a56-ff46-48e7-8ec3-36578a800cf8)
*Fig11.  Output of HeatMap Image for Image Inputs1.


- A Similarity Graph Between Original Inputs and Reconstructed Inputs 


![combined_similarity_plot_Image_Inputs](https://github.com/user-attachments/assets/2b426828-3378-4e63-839e-d2919f9e35d0)
*Fig12.  Similarity Graph Output Image.

* Differences of HeatMap
  * Encoded Inputs
  * Reconstructed Inputs
  * Difference of Bits compared to Encoded and Reconstructed inputs
  * Difference of HeatMap
![image](https://github.com/user-attachments/assets/12c9acf4-94a3-4ff5-933e-5102ccc1f6d0)
*Fig13.  Differences of HeatMap for Image inputs.



##### Example of Input as an Image Visualization Examples are given below:
Here the input is "image.png" which is (28×28) pixels
So, for that, the input bit will be (28×28) = 784 Encoded bits
In this Experiment, the Image Encoder is ImageBinarizer 

HeatMap For 7th Image Input 
![image](https://github.com/user-attachments/assets/c670acae-a2d4-4a54-9d8c-7d073256a178)
*Fig14.   HeatMap for Image 7th inputs.

Similarity Graph of the Image Comparing Encoed Inputs with Reconstructed Inputs
![image](https://github.com/user-attachments/assets/44558442-cc31-42b1-b6ab-7b188a94fae8)

*Fig15.   Similarity Graph for Image 7th inputs.

Differences in the Heatmap of that Image

![difference_heatmap_7](https://github.com/user-attachments/assets/5d5020eb-e05f-414a-afab-21714be519a3)
*Fig16.   Differences of Heatmaps for Image 7th inputs.




# Azure Cloud Adaptation

## Experiment Processing Workflow

### 1. Create and Push Docker Image

1. **Define Docker Image**: Create a `Dockerfile` to specify the Docker image configuration.

2. **Build Docker Image**: Use the Docker build command to create the Docker image with a specific tag.

3. **Tag Docker Image**: Tag the image to prepare it for upload to a container registry.

4. **Push Docker Image**: Upload the tagged Docker image to the Azure Container Registry.
![1](https://github.com/user-attachments/assets/20649f45-640a-407e-90c4-5b68dd219e5c)

### 2. Run Docker Container Instance
![8](https://github.com/user-attachments/assets/9465acf7-68a7-416b-900e-82ea54dcb2ab)

1. **Deploy Docker Container**: Use Azure CLI to create and start a Docker container instance, specifying the Docker image, resource group, and container configuration.

### 3. Request Experiment through Queue Message

1. **Send Experiment Request**: Submit a request to the Azure Storage queue with details about the input files, experiment type, and other parameters.
![3](https://github.com/user-attachments/assets/8b151edc-1bf5-4d9c-aaae-721b0d250130)

### 4. Download Input Data from Blob Storage

1. **Download Input Data**: Retrieve the required input files from Azure Blob Storage to a local directory.
![4](https://github.com/user-attachments/assets/0dfb1c59-6542-4589-aa7e-101b1d826ef6)

### 5. Process the Experiment and Save Results to Blob Storage
![5](https://github.com/user-attachments/assets/fe35f0f1-35e9-4473-8a9b-29ed86037373)

1. **Run the Experiment**: Execute the experiment based on the received request.
![2](https://github.com/user-attachments/assets/62a7474c-c7ca-41f1-a57f-d7fb6edee778)

2. **Save Results**: Upload the results of the experiment to the designated blob container for storing results.
![7](https://github.com/user-attachments/assets/873d6c7e-730a-45cf-b306-2fd3acac9804)

 **Update Data**: Modify or update the experiment data in the result table or database with the latest results.
# Result Table Schema

This table outlines the parameters and properties used when creating an entry in the result table.

| **Component**         | **Description**                                                                 |
|-----------------------|---------------------------------------------------------------------------------|
| `PartitionKey`        | A unique identifier for partitioning the table, set to `result.ExperimentId`.   |
| `RowKey`              | A unique identifier for the row, generated using `Guid.NewGuid().ToString()`.   |
| `Timestamp`           | The timestamp when the entity is created, is set to the current UTC time.          |
| `ExperimentId`        | The ID of the experiment, taken from `request.ExperimentId`.                     |
| `InputFile`           | The name of the input file used in the experiment, from `request.InputFile`.     |
| `InputDirectory`      | The directory of the input file, from `request.InputDirectory`.                 |
| `ExperimentType`      | The type of experiment, from `request.ExperimentType`.                          |
| `RequestedBy`         | The identity of the person or system that requested the experiment, from `request.RequestedBy`. |
| `MaxValue`            | The maximum value parameter for the experiment, from `request.MaxValue` Only for SpatialPatternLearning and Mandatory.         |
| `Description`         | A description of the experiment, from `request. Description`.                    |
| `MessageId`           | The ID of the message in the queue, from `request.MessageId`.                    |
| `MessageReceipt`      | The receipt of the message in the queue, from `request.MessageReceipt`.          |
| `ResultData`          | The serialized result data, using `JsonSerializer.Serialize(result)`.            |
| `OutputUrls`          | The serialized list of output URLs, using `JsonSerializer.Serialize(fileUrls)`.  |
| `CompletionTimestamp` | The timestamp when the result was created, set to the current UTC time.          |

Table-3: Structure of the Result Table.

2. **Clear the Queue**: Remove or clear messages from the Azure Storage queue after processing to ensure the queue is ready for new messages.


