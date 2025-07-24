using AutoFlow.Domain.Entities;
using AutoFlow.Domain.Modules;
using AutoFlow.Domain.Modules.Leave;
using AutoFlow.Modules.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoFlow.AutomationEngine
{
    public class WorkflowEngine
    {
        // Dictionnaire des modules disponibles
        private readonly IReadOnlyDictionary<string, Func<IReadOnlyDictionary<string, string>, Task<WorkflowResult>>> _modules;

        public WorkflowEngine()
        {
            _modules = new Dictionary<string, Func<IReadOnlyDictionary<string, string>, Task<WorkflowResult>>>()
            {
                // Modules de base
                ["Email"] = EmailExecutor.Execute,
                ["Transform"] = TransformExecutor.Execute,

                // Modules pour la gestion des congés
                ["LeaveValidator"] = LeaveRequestValidatorExecutor.Execute,
                ["LeaveBalance"] = LeaveBalanceCheckerExecutor.Execute,
                ["LeaveNotify"] = LeaveNotificationExecutor.Execute,
                ["LeaveSummary"] = LeaveSummaryFormatterExecutor.Execute
            };
        }

        /// <summary>
        /// Exécute chaque étape du workflow en appelant le module correspondant.
        /// </summary>
        /// <param name="workflow">Le workflow à exécuter</param>
        /// <returns>Liste des résultats d'exécution de chaque étape</returns>
        public async Task<List<WorkflowResult>> ExecuteAsync(Workflow workflow)
        {
            if (workflow == null)
                throw new ArgumentNullException(nameof(workflow));

            var results = new List<WorkflowResult>();

            foreach (var step in workflow.Steps)
            {
                if (!_modules.TryGetValue(step.Module, out var executor))
                {
                    results.Add(new WorkflowResult(false, $"[Engine] Module '{step.Module}' not found."));
                    continue;
                }

                // Fusion paramètres globaux + locaux
                var mergedParams = new Dictionary<string, string>(workflow.GlobalParameters);
                foreach (var kv in step.Parameters)
                    mergedParams[kv.Key] = kv.Value;

                // Nombre de retries
                int retries = 0;
                if (mergedParams.TryGetValue("retry", out var retryValue))
                    _ = int.TryParse(retryValue, out retries);

                int attempt = 0;
                WorkflowResult result = null!;

                while (attempt <= retries)
                {
                    try
                    {
                        result = await executor(mergedParams);
                        if (result.Success)
                            break;
                    }
                    catch (Exception ex)
                    {
                        result = new WorkflowResult(false, $"[Engine] Step '{step.Name}' failed on attempt {attempt + 1}: {ex.Message}");
                    }

                    attempt++;

                    // Optionnel : attendre avant retry
                    if (attempt <= retries)
                        await Task.Delay(500); // 500ms entre tentatives
                }

                results.Add(result);
            }

            return results;
        }

    }
}
