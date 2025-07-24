
namespace AutoFlow.WebApp.Components.Models
{
    public class WorkflowStep
    {
        public string Name { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public Dictionary<string, string> Parameters { get; set; } = new();

#if DEBUG
        // Utilisé uniquement pour la construction visuelle dans Blazor
        public List<KeyValuePair<string, string>> TempParameters { get; set; } = new();
#endif

    }
}
