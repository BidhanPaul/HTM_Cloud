using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCloudProject.Common;
using NeoCortexApiSample;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyExperiment
{
    /// <summary>
    /// Asynchronously executes an experiment based on the provided parameters and manages the associated file operations.
    /// The method performs the following steps:
    /// 1. Initializes experiment result tracking and ensures the base output directory exists.
    /// 2. Downloads input files from a storage provider.
    /// 3. Runs the specified experiment type and generates output files:
    ///    - For "imagebinarizerspatialpattern": Handles specific output folders and files, then uploads and logs results.
    ///    - For "spatialpatternlearning": Handles different output folders and files, uploads results, and logs progress.
    /// 4. Updates the experiment result with file URLs and uploads the results to the storage provider.
    /// 5. Creates an entry in the result table with experiment details and result URLs.
    /// 6. Cleans up by deleting uploaded files and directories, preserving input directories for debugging.
    /// 7. Handles and logs any errors encountered during execution.
    /// 8. Ensures the experiment result includes accurate timing information and returns the result.
    /// </summary>
    /// <param name="containerName">The name of the storage container for input files.</param>
    /// <param name="inputDirectory">The directory within the container where input files are located.</param>
    /// <param name="experimentType">The type of experiment to run, determining processing and output.</param>
    /// <param name="maxValue">Optional parameter for experiments requiring a maximum value (e.g., spatial pattern learning).</param>
    /// <param name="request">The request object containing details about the experiment request.</param>
    /// <returns>A task representing the asynchronous operation, with the final <see cref="IExperimentResult"/> containing the result of the experiment.</returns>

    public class Experiment : IExperiment
    {
        private readonly IStorageProvider storageProvider;
        private readonly ILogger logger;
        private readonly MyConfig config;

        public Experiment(IConfigurationSection configSection, IStorageProvider storageProvider, ILogger log)
        {
            this.storageProvider = storageProvider;
            this.logger = log;

            config = new MyConfig();
            configSection.Bind(config);
        }

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
    }
}
