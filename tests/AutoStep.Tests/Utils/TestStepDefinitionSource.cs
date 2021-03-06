﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using AutoStep.Definitions;
using AutoStep.Elements;
using AutoStep.Execution;
using AutoStep.Execution.Contexts;
using AutoStep.Execution.Dependency;
using Microsoft.Extensions.Configuration;

namespace AutoStep.Tests.Utils
{
    public class TestStepDefinitionSource : IStepDefinitionSource
    {
        public static readonly TestStepDefinitionSource Blank = new TestStepDefinitionSource();

        private readonly List<StepDefinition> defs;

        public TestStepDefinitionSource(string uid, params StepDefinition[] defs)
            : this(defs)
        {
            Uid = uid;
        }

        public TestStepDefinitionSource(params StepDefinition[] defs)
        {
            this.defs = defs.ToList();
        }

        public string Uid { get; } = "test";

        public string Name => "Test";

        public bool ConfigureServicesCalled { get; private set; }

        public virtual void ReplaceStepDefinitions(params StepDefinition[] newDefs)
        {
            defs.Clear();
            defs.AddRange(newDefs);
        }

        public virtual void AddStepDefinition(StepType type, string declaration)
        {
            defs.Add(new LocalStepDef(this, type, declaration));
        }

        public virtual void RemoveStepDefinition(StepDefinition def)
        {
            defs.Remove(def);
        }

        public IEnumerable<StepDefinition> GetStepDefinitions()
        {
            return defs;
        }

        public void ConfigureServices(ContainerBuilder servicesBuilder, IConfiguration config)
        {
            ConfigureServicesCalled = true;
        }

        private class LocalStepDef : StepDefinition
        {
            public LocalStepDef(IStepDefinitionSource source, StepType type, string declaration) : base(source, type, declaration)
            {
            }

            public override StepTableRequirement TableRequirement => StepTableRequirement.Optional;

            public override ValueTask ExecuteStepAsync(ILifetimeScope stepScope, StepContext context, VariableSet variables, CancellationToken cancelToken)
            {
                if (stepScope is null)
                {
                    throw new ArgumentNullException(nameof(stepScope));
                }

                throw new System.NotImplementedException();
            }

            public override string? GetDocumentation()
            {
                throw new NotImplementedException();
            }

            public override bool IsSameDefinition(StepDefinition def)
            {
                return def.Declaration == Declaration && def.Source == Source;
            }
        }
    }
}
