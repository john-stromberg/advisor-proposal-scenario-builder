using Microsoft.AspNetCore.Mvc;
using ProposalBuilder.Api.Application.Contracts;
using ProposalBuilder.Api.Domain;

namespace ProposalBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ScenariosController(IScenarioService scenarioService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateScenario([FromBody] CreateScenarioRequest req)
    {
        var scenario = new HouseholdScenario
        {
            Name = req.Name,
            ClientName = req.ClientName,
            PortfolioValue = req.PortfolioValue,
            AnnualIncome = req.AnnualIncome,
            YearsToRetirement = req.YearsToRetirement,
            InflationRate = req.InflationRate,
            AssumedReturn = req.AssumedReturn,
            AllocationJson = System.Text.Json.JsonSerializer.Serialize(req.Allocation),
            CreatedDate = DateOnly.FromDateTime(DateTime.Now),
        };

        var created = await scenarioService.CreateScenarioAsync(scenario);
        return Ok(new { id = created.Id, name = created.Name });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetScenario(Guid id)
    {
        var scenario = await scenarioService.GetScenarioAsync(id);
        if (scenario == null)
            return NotFound();

        return Ok(scenario);
    }

    [HttpGet]
    public async Task<IActionResult> ListScenarios()
    {
        var scenarios = await scenarioService.GetAllScenariosAsync();
        return Ok(scenarios);
    }

    [HttpPost("compare")]
    public async Task<IActionResult> CompareScenarios([FromBody] CompareRequest req)
    {
        var comparison = await scenarioService.CompareAsync(req.CurrentScenarioId, req.ProposedScenarioId);

        var result = System.Text.Json.JsonDocument.Parse(comparison.ResultsJson).RootElement;
        return Ok(new
        {
            comparison.Id,
            comparison.CurrentScenarioId,
            comparison.ProposedScenarioId,
            result
        });
    }
}

public sealed record CompareRequest(Guid CurrentScenarioId, Guid ProposedScenarioId);
