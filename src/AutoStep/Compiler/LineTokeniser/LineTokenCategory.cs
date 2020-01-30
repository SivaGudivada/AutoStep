﻿namespace AutoStep
{
    /// <summary>
    /// Defines the possible line token categories.
    /// </summary>
    public enum LineTokenCategory
    {
        /// <summary>
        /// Annotation, e.g. Tag/Option.
        /// </summary>
        Annotation,

        /// <summary>
        /// A step type keyword (Given/When/Then/And).
        /// </summary>
        StepTypeKeyword,

        /// <summary>
        /// The entrance to a block, such as Feature:, Scenario:, Step: etc.
        /// </summary>
        EntryMarker,

        /// <summary>
        /// A comment.
        /// </summary>
        Comment,

        /// <summary>
        /// An entity name, like the name of a scenario or feature.
        /// </summary>
        EntityName,

        /// <summary>
        /// A table border character '|'.
        /// </summary>
        TableBorder,

        /// <summary>
        /// Description free-text.
        /// </summary>
        Text,

        /// <summary>
        /// A variable reference.
        /// </summary>
        Variable,

        /// <summary>
        /// Step body text.
        /// </summary>
        StepText,

        /// <summary>
        /// A bound argument value.
        /// </summary>
        BoundArgument,
    }
}
