namespace AutoFlow.WebApp.Components.Models
{
    public class Workflow
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<WorkflowStep> Steps { get; set; } = new();
    }
}
