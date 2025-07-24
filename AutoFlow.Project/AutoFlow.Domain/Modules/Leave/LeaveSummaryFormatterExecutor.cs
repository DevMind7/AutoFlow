using AutoFlow.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlow.Domain.Modules.Leave
{
    // Génère un résumé clair de la demande (idéal pour logs ou pour affichage en backoffice).

    public static class LeaveSummaryFormatterExecutor
    {
        public static Task<WorkflowResult> Execute(IReadOnlyDictionary<string, string> parameters)
        {
            return Task.FromResult(Format(parameters));
        }

        private static WorkflowResult Format(IReadOnlyDictionary<string, string> p)
        {
            var name = p.GetValueOrDefault("employeeName", "Unknown");
            var start = p.GetValueOrDefault("startDate", "?");
            var end = p.GetValueOrDefault("endDate", "?");
            var reason = p.GetValueOrDefault("reason", "N/A");

            return new WorkflowResult(true, $"[LeaveSummary] {name} demande un congé du {start} au {end}. Motif: {reason}");
        }
    }

}
