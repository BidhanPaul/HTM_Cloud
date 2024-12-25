using MyCloudProject.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExperiment
{
    internal class ExerimentRequestMessage : IExerimentRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier for the experiment.
        /// This property is used to uniquely identify and track the experiment throughout its lifecycle.
        /// </summary>
        public string ExperimentId { get; set; }
        /// <summary>
        /// Gets or sets the name or path of the input file associated with the experiment.
        /// This property specifies the file that will be used as input for processing in the experiment.
        /// </summary>
        public string InputFile { get; set; }
        /// <summary>
        /// Gets or sets the directory path containing input files for the experiment.
        /// This property specifies the location of the directory where input files are stored for processing.
        /// </summary>
        public string InputDirectory { get; set; }
        // <summary>
        /// Gets or sets the type of experiment to be conducted.
        /// This property defines the category or nature of the experiment, influencing how the processing and results are handled.
        /// </summary>
        public string ExperimentType { get; set; }
        /// <summary>
        /// Gets or sets the identifier or name of the person or entity that requested the experiment.
        /// This property indicates who initiated the request for the experiment, providing context or accountability.
        /// </summary>
        public string RequestedBy { get; set; }
        /// <summary>
        /// Gets or sets the maximum value parameter for the experiment, if applicable.
        /// This optional property specifies the upper limit or threshold used in certain types of experiments.
        /// It can be null if the maximum value is not required or applicable for the experiment.
        /// </summary>
        public double? MaxValue { get; set; }
        /// <summary>
        /// Gets or sets a brief description of the experiment.
        /// This property provides additional details or context about the experiment, helping to clarify its purpose or objectives.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier for the message associated with the experiment request.
        /// This property is used to track and reference the specific message in a messaging system or queue.
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// Gets or sets the receipt or token associated with the message in the queue.
        /// This property is used to verify or acknowledge the receipt of the message and manage its lifecycle in the queue system.
        /// </summary>
        public string MessageReceipt { get; set; }
    }
}
