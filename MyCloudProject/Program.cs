using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCloudProject.Common;
using MyExperiment;

namespace MyCloudProject
{
    /// <summary>
    /// The entry point for the application that manages the lifecycle of an experiment process.
    /// </summary>
    /// <remarks>
    /// This class orchestrates the execution of an experiment by:
    /// 1. Initializing configuration and logging mechanisms.
    /// 2. Setting up an instance of <see cref="IStorageProvider"/> to handle storage operations.
    /// 3. Creating an instance of <see cref="IExperiment"/> to run the experiment based on requests.
    /// 4. Entering a loop that:
    ///    - Receives experiment requests from a queue.
    ///    - Downloads necessary input files.
    ///    - Executes the experiment and processes results.
    ///    - Uploads results and updates a result table in storage.
    ///    - Handles the cleanup of temporary files and directories.
    ///    - Commits and deletes messages from the queue upon successful processing.
    /// 5. Handles cancellation requests and logs relevant information throughout the process.
    /// </remarks>

    class Program
    {
        private static string projectName = "ML 22/23-5 Implementation of the Visualization of Permanence Values";

        static async Task Main(string[] args)
        {
            CancellationTokenSource tokenSrc = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                tokenSrc.Cancel();
            };

            Console.WriteLine($"Started experiment: {projectName}");

            // Init configuration
            var cfgRoot = InitHelpers.InitConfiguration(args);
            var cfgSec = cfgRoot.GetSection("MyConfig");

            // Init logging
            var logFactory = InitHelpers.InitLogging(cfgRoot);
            var logger = logFactory.CreateLogger("Train.Console");

            logger.LogInformation($"{DateTime.Now} - Started experiment: {projectName}");

            // Initialize AzureStorageProvider with IConfigurationSection and ILogger
            IStorageProvider storageProvider = new AzureStorageProvider(cfgSec, logger);

            IExperiment experiment = new Experiment(cfgSec, storageProvider, logger);

            while (!tokenSrc.Token.IsCancellationRequested)
            {
                try
                {
                    // Receive the experiment request and message details
                    var (request, messageId, popReceipt) = await storageProvider.ReceiveExperimentRequestAsync(tokenSrc.Token);

                    if (request != null)
                    {
                        string outputDir = null;
                        try
                        {
                            // Download input files
                            string localInputDirectory = await storageProvider.DownloadInputAsync(request.InputFile, request.InputDirectory);

                            // Run the experiment and get the result
                            var result = await experiment.RunAsync(
                                request.InputFile,
                                request.InputDirectory,
                                request.ExperimentType,
                                request.MaxValue,
                                request
                            );

                            outputDir = result.OutputDirectory;

                            if (outputDir != null && Directory.Exists(outputDir))
                            {
                                logger.LogInformation($"Output directory found for upload: {outputDir}");

                                // Upload results and get the URLs
                                var fileUrls = await storageProvider.UploadExperimentResult(result, outputDir);
                                // Create result table entry with the file URLs
                                await storageProvider.CreateResultTableAsync(result, fileUrls, request);
                                // Safely delete output directory after successful operations
                                try
                                {
                                    if (Directory.Exists(outputDir))
                                    {
                                        Directory.Delete(outputDir, true);
                                        logger.LogInformation($"Output directory deleted after upload: {outputDir}");
                                    }
                                }
                                catch (Exception deleteEx)
                                {
                                    logger.LogError(deleteEx, $"Error deleting the output directory: {outputDir}");
                                }
                            }
                            else
                            {
                                logger.LogInformation($"DELTED TEMPORARY FILES AND DIRECTORY");
                                logger.LogInformation($"Output directory not found or already deleted");
                                logger.LogInformation($"Successfully Uploaded to The Blob Storage Output Container");
                            }

                            try
                            {
                                // Commit the request and delete the message from the queue
                                await storageProvider.CommitRequestAsync(messageId, popReceipt);
                            }
                            catch (Exception QueueFailed)
                            {
                                logger.LogError(QueueFailed, $"Commiting Queue is not Working");
                            }

                            logger.LogInformation($"Experiment completed: {result.ExperimentId}");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "Error processing the request.");
                        }
                    }
                    else
                    {
                        await Task.Delay(1000);
                        logger.LogTrace("Queue empty...");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error in the queue processing loop.");
                }
            }

            logger.LogInformation($"{DateTime.Now} - Experiment exit: {projectName}");
        }
    }
}
