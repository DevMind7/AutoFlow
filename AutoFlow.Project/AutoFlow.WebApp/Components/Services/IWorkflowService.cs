using AutoFlow.WebApp.Components.Models;

namespace AutoFlow.WebApp.Components.Services
{
    public interface IWorkflowService
    {
        Task<WorkflowResponse?> ExecuteWorkflowAsync(Workflow workflow);

    }
}
