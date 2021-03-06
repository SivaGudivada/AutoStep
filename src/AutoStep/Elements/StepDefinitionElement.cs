﻿using System.Collections.Generic;
using System.Linq;
using AutoStep.Elements.Metadata;
using AutoStep.Elements.Parts;
using AutoStep.Elements.Test;

namespace AutoStep.Elements
{
    /// <summary>
    /// Represents a built 'Scenario', that can have a name, annotations, a description and a set of steps.
    /// </summary>
    public class StepDefinitionElement : StepCollectionElement, IAnnotatableElement, IStepDefinitionInfo
    {
        private readonly List<DefinitionPart> parts = new List<DefinitionPart>();
        private readonly List<ArgumentPart> arguments = new List<ArgumentPart>();

        /// <summary>
        /// Gets the annotations applied to the step definition, in applied order.
        /// </summary>
        public List<AnnotationElement> Annotations { get; } = new List<AnnotationElement>();

        /// <inheritdoc/>
        IReadOnlyList<IAnnotationInfo> IStepDefinitionInfo.Annotations => Annotations;

        /// <summary>
        /// Gets or sets the type of step.
        /// </summary>
        public StepType Type { get; set; }

        /// <summary>
        /// Gets or sets the raw text of the Step declaration.
        /// </summary>
        public string? Declaration { get; set; }

        /// <inheritdoc/>
        string IStepDefinitionInfo.Declaration => Declaration ?? throw new LanguageEngineAssertException();

        /// <summary>
        /// Gets or sets the step definition description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets the set of arguments presented by the Step Definition as being available.
        /// </summary>
        public IReadOnlyList<ArgumentPart> Arguments => arguments;

        /// <summary>
        /// Gets the set of matching parts used by the step definition element.
        /// </summary>
        public IReadOnlyList<DefinitionPart> Parts => parts;

        /// <summary>
        /// Check if this step definition contains an argument with the specified name.
        /// </summary>
        /// <param name="argumentName">The argument to check for.</param>
        /// <returns>true if available, false otherwise.</returns>
        public bool ContainsArgument(string argumentName)
        {
            if (Arguments == null)
            {
                return false;
            }

            return Arguments.Any(a => a.Name == argumentName);
        }

        /// <summary>
        /// Adds a part to the step definition.
        /// </summary>
        /// <param name="part">The part to add.</param>
        internal void AddPart(DefinitionPart part)
        {
            part = part.ThrowIfNull(nameof(part));

            parts.Add(part);

            if (part is ArgumentPart argPart)
            {
                arguments.Add(argPart);
            }
        }
    }
}
