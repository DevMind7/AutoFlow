using AutoFlow.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlow.Modules
{
    public class LeaveModule
    {
        public static WorkflowResult ExtractRequest(WorkflowContext context, out LeaveRequest? request)
        {
            if (!context.Data.TryGetValue("leaveRequest", out var obj) || obj is not LeaveRequest r)
            {
                request = null;
                return new WorkflowResult(false, "Requête congé absente ou invalide");
            }

            request = r;
            return new WorkflowResult(true, "Requête extraite");
        }

        public static WorkflowResult ValidateBasic(LeaveRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
                return new WorkflowResult(false, "Utilisateur non renseigné");

            if (request.EndDate <= request.StartDate)
                return new WorkflowResult(false, "Date de fin doit être après la date de début");

            if (string.IsNullOrWhiteSpace(request.Reason))
                return new WorkflowResult(false, "Motif de congé obligatoire");

            return new WorkflowResult(true, "Validation basique OK");
        }

        public static WorkflowResult CheckAvailability(LeaveRequest request, Func<string, double> getUserLeaveBalance)
        {
            var daysRequested = (request.EndDate - request.StartDate).TotalDays + 1;
            var balance = getUserLeaveBalance(request.UserId);

            if (balance < daysRequested)
                return new WorkflowResult(false, $"Solde insuffisant : {balance} jours restants");

            return new WorkflowResult(true, "Solde suffisant");
        }

        public static WorkflowResult CheckEnvVariables(IReadOnlyDictionary<string, string> envVariables, params string[] requiredVars)
        {
            var missing = requiredVars.Where(v => !envVariables.ContainsKey(v)).ToList();
            if (missing.Any())
                return new WorkflowResult(false, $"Variables d’environnement manquantes : {string.Join(", ", missing)}");

            return new WorkflowResult(true, "Variables d’environnement OK");
        }

        public static WorkflowResult CalculateDuration(LeaveRequest request)
        {
            var days = (request.EndDate - request.StartDate).TotalDays + 1;
            return new WorkflowResult(true, $"Durée du congé : {days} jours");
        }

        public static WorkflowResult GenerateAcknowledgement(LeaveRequest request)
        {
            var ack = $"Demande de congé de {request.UserId} du {request.StartDate:dd/MM/yyyy} au {request.EndDate:dd/MM/yyyy} enregistrée.";
            return new WorkflowResult(true, ack);
        }

        public static WorkflowResult BusinessRulesCheck(LeaveRequest request, double maxDaysAllowed)
        {
            var days = (request.EndDate - request.StartDate).TotalDays + 1;
            if (days > maxDaysAllowed)
                return new WorkflowResult(false, $"Durée maximale autorisée dépassée : {maxDaysAllowed} jours");

            return new WorkflowResult(true, "Règles métier validées");
        }

        public static WorkflowResult RunLeaveRequestWorkflow(
            WorkflowContext context,
            Func<string, double> getUserLeaveBalance,
            IReadOnlyDictionary<string, string> envVariables,
            double maxDaysAllowed)
        {
            var extractRes = ExtractRequest(context, out var request);
            if (!extractRes.Success) return extractRes;

            var validateRes = ValidateBasic(request!);
            if (!validateRes.Success) return validateRes;

            var envCheckRes = CheckEnvVariables(envVariables, "API_KEY", "DB_CONN");
            if (!envCheckRes.Success) return envCheckRes;

            var availabilityRes = CheckAvailability(request!, getUserLeaveBalance);
            if (!availabilityRes.Success) return availabilityRes;

            var businessRulesRes = BusinessRulesCheck(request!, maxDaysAllowed);
            if (!businessRulesRes.Success) return businessRulesRes;

            var durationRes = CalculateDuration(request!);
            if (!durationRes.Success) return durationRes;

            var ackRes = GenerateAcknowledgement(request!);
            return ackRes;
        }
    }
}
