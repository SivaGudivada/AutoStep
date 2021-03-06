﻿using System;
using System.Collections.Generic;

namespace AutoStep.Execution.Results
{
    /// <summary>
    /// Defines the set of all results for an individual test run.
    /// </summary>
    public interface IRunResultSet
    {
        /// <summary>
        /// Gets the results for all executed features.
        /// </summary>
        public IEnumerable<IFeatureResult> Features { get; }

        /// <summary>
        /// Gets a value indicating whether all features and scenarios executed successfully.
        /// </summary>
        public bool AllPassed { get; }

        /// <summary>
        /// Gets the start time of the test run (UTC).
        /// </summary>
        public DateTime StartTimeUtc { get; }

        /// <summary>
        /// Gets the end time of the test run (UTC).
        /// </summary>
        public DateTime EndTimeUtc { get; }
    }
}
