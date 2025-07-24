using AutoFlow.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlow.Domain.Modules.Leave
{
    // Simule l’envoi d’un email ou d’un message à un manager.
    public static class LeaveNotificationExecutor
    {
        public static Task<WorkflowResult> Execute(IReadOnlyDictionary<string, string> parameters)
        {
            return Task.FromResult(SimulateSend(parameters));
        }

        private static WorkflowResult SimulateSend(IReadOnlyDictionary<string, string> p)
        {
            if (!p.TryGetValue("to", out var to)) return new WorkflowResult(false, "[LeaveNotify] Missing 'to'");
            if (!p.TryGetValue("employeeName", out var name)) return new WorkflowResult(false, "[LeaveNotify] Missing 'employeeName'");
            if (!p.TryGetValue("startDate", out var start)) return new WorkflowResult(false, "[LeaveNotify] Missing 'startDate'");
            if (!p.TryGetValue("endDate", out var end)) return new WorkflowResult(false, "[LeaveNotify] Missing 'endDate'");

            return new WorkflowResult(true, $"[LeaveNotify] Would send request from {name} ({start} → {end}) to {to}");
        }
    }

}
