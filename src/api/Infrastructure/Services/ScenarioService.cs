namespace ProposalBuilder.Api.Infrastructure.Services;

using ProposalBuilder.Api.Application.Contracts;
using ProposalBuilder.Api.Domain;
using System.Collections.Concurrent;

public sealed class ScenarioService : IScenarioService
{
    private static readonly ConcurrentDictionary<Guid, HouseholdScenario> _scenarios = new();
    private readonly IScenarioExportService _exportService;

    public ScenarioService(IScenarioExportService exportService)
    {
        _exportService = exportService;
    }

    public async Task<HouseholdScenario> CreateScenarioAsync(HouseholdScenario scenario)
    {
        scenario.Id = Guid.NewGuid();
        _scenarios[scenario.Id] = scenario;
        return await Task.FromResult(scenario);
    }

    public async Task<HouseholdScenario?> GetScenarioAsync(Guid id)
    {
        _scenarios.TryGetValue(id, out var scenario);
        return await Task.FromResult(scenario);
    }

    public async Task<IEnumerable<HouseholdScenario>> GetAllScenariosAsync()
    {
        return await Task.FromResult(_scenarios.Values);
    }

    public async Task<ProposalComparison> CompareAsync(Guid currentId, Guid proposedId)
    {
        var current = await GetScenarioAsync(currentId);
        var proposed = await GetScenarioAsync(proposedId);

        if (current == null || proposed == null)
        {
            throw new InvalidOperationException("Scenarios not found.");
        }

        var result = await CalculateProposalImpactAsync(current, proposed);

        var comparison = new ProposalComparison
        {
            Id = Guid.NewGuid(),
            CurrentScenarioId = currentId,
            ProposedScenarioId = proposedId,
            CurrentScenario = current,
            ProposedScenario = proposed,
            ResultsJson = System.Text.Json.JsonSerializer.Serialize(result),
        };

        return comparison;
    }

    public async Task<ScenarioResult> CalculateProposalImpactAsync(HouseholdScenario current, HouseholdScenario proposed)
    {
        var currentFv = CalculateFutureValue(current.PortfolioValue, current.AssumedReturn, current.YearsToRetirement);
        var proposedFv = CalculateFutureValue(proposed.PortfolioValue, proposed.AssumedReturn, proposed.YearsToRetirement);

        var diff = proposedFv - currentFv;
        var diffPct = currentFv > 0 ? (diff / currentFv) * 100 : 0;

        var retirementDiffYears = (int)((proposedFv - currentFv) / ((currentFv / current.YearsToRetirement) + 0.01m));

        return await Task.FromResult(new ScenarioResult
        {
            CurrentPortfolioFV = currentFv,
            ProposedPortfolioFV = proposedFv,
            DifferenceAmount = diff,
            DifferencePercent = (decimal)diffPct,
            CurrentRetirementYear = currentFv,
            ProposedRetirementYear = proposedFv,
            RetirementYearDifference = Math.Abs(retirementDiffYears),
            AnalysisSummary = GenerateSummary(current, proposed, diff, diffPct)
        });
    }

    private static decimal CalculateFutureValue(decimal pv, decimal rate, int years)
    {
        return pv * (decimal)Math.Pow((double)(1 + rate), years);
    }

    private static string GenerateSummary(HouseholdScenario current, HouseholdScenario proposed, decimal diff, decimal diffPct)
    {
        if (diff > 0)
        {
            return $"Proposed allocation would improve portfolio by {diff:C} ({diffPct:F2}%) over {current.YearsToRetirement} years.";
        }
        else
        {
            return $"Current allocation is more favorable by {Math.Abs(diff):C} ({Math.Abs(diffPct):F2}%).";
        }
    }
}
