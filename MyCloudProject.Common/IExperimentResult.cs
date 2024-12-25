
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCloudProject.Common
{
    public interface IExperimentResult
    {
        string ExperimentId { get; set; }
        public string InputFileUrl { get; set; }

        public string[] OutputFiles { get; set; }

        string OutputDirectory { get; set; }

        public string ExperimentType { get; set; }
        public string RequestedBy { get; set; }

        DateTime? StartTimeUtc { get; set; }

        DateTime? EndTimeUtc { get; set; }

        public TimeSpan Duration { get; set; }
    }

}
