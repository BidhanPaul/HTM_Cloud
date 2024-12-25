using Azure;
using Azure.Data.Tables;
using MyCloudProject.Common;
using System;
using System.Collections.Generic;

namespace MyExperiment
{
    public class ExperimentResult : ITableEntity, IExperimentResult
    {
        // Constructors
        public ExperimentResult(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public ExperimentResult(string experimentId) : this(experimentId, Guid.NewGuid().ToString())
        {
            ExperimentId = experimentId;
            FileUrls = new List<string>(); // Initialize the list
        }

        // ITableEntity Properties
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        // IExperimentResult Properties
        public string ExperimentId { get; set; }
        public string InputFileUrl { get; set; }
        public string[] OutputFiles { get; set; }
        public string OutputDirectory { get; set; }
        public string ExperimentType { get; set; }
        public string RequestedBy { get; set; }
        public DateTime? StartTimeUtc { get; set; }
        public DateTime? EndTimeUtc { get; set; }
        public string ResultData { get; set; }
        public TimeSpan Duration { get; set; }

        // Additional Property
        public List<string> FileUrls { get; set; } // Initialize this list in the constructor
    }
}
