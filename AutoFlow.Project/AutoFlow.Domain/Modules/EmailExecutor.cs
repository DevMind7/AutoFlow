using AutoFlow.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlow.Domain.Modules
{
    //100% stateless, purement fonctionnel, sans mutation.


    public static class EmailExecutor
    {
        public static Task<WorkflowResult> Execute(IReadOnlyDictionary<string, string> parameters)
        {
            return Task.FromResult(ValidateAndSimulateEmail(parameters));
        }

        private static WorkflowResult ValidateAndSimulateEmail(IReadOnlyDictionary<string, string> parameters)
        {
            return parameters.ContainsKey("to") && parameters.ContainsKey("subject")
                ? new WorkflowResult(true, $"[EmailModule] Would send email to {parameters["to"]} with subject: '{parameters["subject"]}'")
                : new WorkflowResult(false, "[EmailModule] Missing required parameters: 'to' and 'subject'");
        }
    }
}
