namespace ProposalBuilder.Api.Infrastructure.Services;

using ProposalBuilder.Api.Application.Contracts;
using ProposalBuilder.Api.Domain;

public sealed class ScenarioExportService : IScenarioExportService
{
    public async Task<byte[]> ExportComparisonAsExcelAsync(ProposalComparison comparison)
    {
        // Mock Excel export - returns placeholder bytes
        var content = $"Proposal Comparison Report\r\nCurrent: {comparison.CurrentScenario?.Name}\r\nProposed: {comparison.ProposedScenario?.Name}\r\n{comparison.ResultsJson}";
        return await Task.FromResult(System.Text.Encoding.UTF8.GetBytes(content));
    }

    public async Task<string> ExportComparisonAsPdfSummaryAsync(ProposalComparison comparison)
    {
        var summary = $"Proposal Comparison: {comparison.CurrentScenario?.ClientName}\r\n";
        summary += $"Results: {comparison.ResultsJson}";
        return await Task.FromResult(summary);
    }
}
