using AutoFlow.AutomationEngine;
using AutoFlow.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WorkFlowController(WorkflowEngine engine) : ControllerBase
{
    private readonly WorkflowEngine _engine = engine;

    /// <summary>
    /// Exécute un workflow complet avec ses étapes.
    /// </summary>
    /// <param name="workflow">Workflow à exécuter</param>
    /// <returns>Résultats de chaque étape</returns>

    [HttpPost("execute")]
    public async Task<IActionResult> ExecuteWorkflow([FromBody] Workflow workflow)
    {
        if (workflow == null || workflow.Steps == null || workflow.Steps.Count == 0)
            return BadRequest("Le workflow est vide ou invalide.");

        try
        {
            var results = await _engine.ExecuteAsync(workflow);

            var success = results.All(r => r.Success);
            return Ok(new
            {
                workflow.Id,
                workflow.Name,
                Success = success,
                Results = results
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal error: {ex.Message}");
        }
    }
}
