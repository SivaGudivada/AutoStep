﻿using System.Collections.Generic;
using System.Linq;
using AutoStep.Definitions;
using AutoStep.Definitions.Interaction;
using AutoStep.Elements.Interaction;

namespace AutoStep.Language.Interaction
{
    /// <summary>
    /// Defines an interaction set, defining the set of components available for interaction behaviour,
    /// and access to the set of step definitions.
    /// </summary>
    public class InteractionSet : IInteractionSet
    {
        private readonly IEnumerable<InteractionStepDefinitionElement> steps;

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionSet"/> class.
        /// </summary>
        /// <param name="constants">The set of available constants.</param>
        /// <param name="components">The set of all components, indexed by name.</param>
        /// <param name="steps">The set of all step definitions.</param>
        /// <param name="tableReference">The set of extended method table data.</param>
        public InteractionSet(InteractionConstantSet constants, IReadOnlyDictionary<string, BuiltComponent> components, IEnumerable<InteractionStepDefinitionElement> steps, IExtendedMethodTableReferences? tableReference)
        {
            this.Components = components;
            this.Constants = constants;
            this.steps = steps;
            this.ExtendedMethodReferences = tableReference;
        }

        /// <summary>
        /// Gets the set of all components, indexed by name.
        /// </summary>
        public IReadOnlyDictionary<string, BuiltComponent> Components { get; }

        /// <summary>
        /// Gets the set of available constants.
        /// </summary>
        public InteractionConstantSet Constants { get; }

        /// <summary>
        /// Gets the set of extended method table look-up info.
        /// </summary>
        public IExtendedMethodTableReferences? ExtendedMethodReferences { get; }

        /// <summary>
        /// Gets the set of all step definitions.
        /// </summary>
        /// <param name="stepSource">The step source to attach them to.</param>
        /// <returns>A set of steps.</returns>
        public IEnumerable<StepDefinition> GetStepDefinitions(IStepDefinitionSource stepSource)
        {
            return steps.Select(s => new InteractionStepDefinition(stepSource, s));
        }
    }
}
