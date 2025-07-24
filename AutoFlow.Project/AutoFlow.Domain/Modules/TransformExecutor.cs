using AutoFlow.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlow.Domain.Modules
{
    public static class TransformExecutor
    {
        public static Task<WorkflowResult> Execute(IReadOnlyDictionary<string, string> parameters)
        {
            return Task.FromResult(UpperCase(parameters));
        }

        private static WorkflowResult UpperCase(IReadOnlyDictionary<string, string> parameters)
        {
            return parameters.TryGetValue("input", out var input)
                ? new WorkflowResult(true, $"[TransformModule] {input.ToUpperInvariant()}")
                : new WorkflowResult(false, "[TransformModule] Missing 'input' parameter");
        }
    }
}
