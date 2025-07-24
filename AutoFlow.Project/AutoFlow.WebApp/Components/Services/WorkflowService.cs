using AutoFlow.WebApp.Components.Models;

namespace AutoFlow.WebApp.Components.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly HttpClient _http;

        public WorkflowService(HttpClient http)
        {
            _http = http;
        }

        public async Task<WorkflowResponse?> ExecuteWorkflowAsync(Workflow workflow)
        {
            var response = await _http.PostAsJsonAsync("api/WorkFlow/execute", workflow);
            return await response.Content.ReadFromJsonAsync<WorkflowResponse>();
        }
    }
}
