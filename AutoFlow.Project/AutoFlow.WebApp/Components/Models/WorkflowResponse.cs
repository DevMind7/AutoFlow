namespace AutoFlow.WebApp.Components.Models
{
    public class WorkflowResponse
    {
        public Guid WorkflowId { get; set; }
        public bool Success { get; set; }
        public List<WorkflowResult> Results { get; set; } = new();
    }
}
