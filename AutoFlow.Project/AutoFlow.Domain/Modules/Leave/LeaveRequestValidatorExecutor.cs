using AutoFlow.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlow.Domain.Modules.Leave
{
    // Ce module vérifie que la demande de congé contient toutes les infos requises et que les dates sont valides.

    public class LeaveRequestValidatorExecutor
    {
        public static Task<WorkflowResult> Execute(IReadOnlyDictionary<string, string> parameters)
        {
            return Task.FromResult(Validate(parameters));
        }

        private static WorkflowResult Validate(IReadOnlyDictionary<string, string> p)
        {
            if (!p.TryGetValue("startDate", out var start) || !DateTime.TryParse(start, out var startDate))
                return new WorkflowResult(false, "[LeaveValidator] Invalid or missing 'startDate'");

            if (!p.TryGetValue("endDate", out var end) || !DateTime.TryParse(end, out var endDate))
                return new WorkflowResult(false, "[LeaveValidator] Invalid or missing 'endDate'");

            if (endDate < startDate)
                return new WorkflowResult(false, "[LeaveValidator] 'endDate' must be after 'startDate'");

            var days = (endDate - startDate).TotalDays + 1;
            return new WorkflowResult(true, $"[LeaveValidator] Valid request for {days} day(s)");
        }
    }
}
