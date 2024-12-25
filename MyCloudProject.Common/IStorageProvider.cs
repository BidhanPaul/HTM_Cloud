using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyCloudProject.Common
{
    /// <summary>
    /// Defines the contract for all storage operations.
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// Receives the next message from the queue.
        /// </summary>
        /// <param name="token">Cancellation token to cancel the operation.</param>
        /// <returns>NULL if there are no messages in the queue, otherwise an instance of <see cref="IExerimentRequest"/>.</returns>
        Task<(IExerimentRequest request, string messageId, string popReceipt)> ReceiveExperimentRequestAsync(CancellationToken token);

        /// <summary>
        /// Downloads the input files for the experiment. This method fetches the files from a remote location and saves them locally.
        /// </summary>
        /// <param name="containerName">The name of the container where the files are stored.</param>
        /// <param name="inputDirectory">The directory within the container where the files are located.</param>
        /// <returns>The local path to the directory where the files have been downloaded.</returns>
        Task<string> DownloadInputAsync(string containerName, string inputDirectory);

        /// <summary>
        /// Uploads the result of the experiment to a remote location.
        /// </summary>
        /// <param name="result">The result of the experiment to be uploaded.</param>
        /// <param name="outputDir">The local directory containing the result files to be uploaded.</param>
        /// <returns>A task representing the asynchronous upload operation.</returns>

        Task<List<string>> UploadExperimentResult(IExperimentResult result, string baseOutputDir);


        Task CreateResultTableAsync(IExperimentResult result, List<string> fileUrls, IExerimentRequest request);


        /// <summary>
        /// Commits the request by deleting it from the queue.
        /// </summary>
        /// <param name="request">The request to be committed.</param>
        /// <returns>A task representing the asynchronous commit operation.</returns>
        Task CommitRequestAsync(string messageId, string popReceipt);
    }
}
