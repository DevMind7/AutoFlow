using AutoFlow.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlow.Domain.Modules.Leave
{
    // Vérifie si le solde de congés est suffisant pour la durée demandée.

    public static class LeaveBalanceCheckerExecutor
    {
        public static Task<WorkflowResult> Execute(IReadOnlyDictionary<string, string> parameters)
        {
            return Task.FromResult(Check(parameters));
        }

        private static WorkflowResult Check(IReadOnlyDictionary<string, string> p)
        {
            if (!p.TryGetValue("startDate", out var start) || !DateTime.TryParse(start, out var startDate)) return Error("startDate");
            if (!p.TryGetValue("endDate", out var end) || !DateTime.TryParse(end, out var endDate)) return Error("endDate");
            if (!p.TryGetValue("availableDays", out var balanceStr) || !int.TryParse(balanceStr, out var balance)) return Error("availableDays");

            var requested = (endDate - startDate).TotalDays + 1;
            return requested <= balance
                ? new WorkflowResult(true, $"[LeaveBalance] OK: {requested} ≤ {balance} days available")
                : new WorkflowResult(false, $"[LeaveBalance] Not enough days: requested {requested}, only {balance}");
        }

        private static WorkflowResult Error(string name) => new(false, $"[LeaveBalance] Missing or invalid '{name}'");
    }

}
