namespace ProposalBuilder.Api.Application.Contracts;

using ProposalBuilder.Api.Domain;

public interface IScenarioService
{
 Task<HouseholdScenario> CreateScenarioAsync(HouseholdScenario scenario);
 Task<HouseholdScenario?> GetScenarioAsync(Guid id);
 Task<IEnumerable<HouseholdScenario>> GetAllScenariosAsync();
 Task<ProposalComparison> CompareAsync(Guid currentId, Guid proposedId);
 Task<ScenarioResult> CalculateProposalImpactAsync(HouseholdScenario current, HouseholdScenario proposed);
}

public interface IScenarioExportService
{
 Task<byte[]> ExportComparisonAsExcelAsync(ProposalComparison comparison);
 Task<string> ExportComparisonAsPdfSummaryAsync(ProposalComparison comparison);
}

public sealed record CreateScenarioRequest
(
 string Name,
 string ClientName,
 decimal PortfolioValue,
 decimal AnnualIncome,
 int YearsToRetirement,
 decimal InflationRate,
 decimal AssumedReturn,
 Dictionary<string, decimal> Allocation
);

public sealed record ComparisonResultDto
(
 decimal CurrentPortfolioFV,
 decimal ProposedPortfolioFV,
 decimal DifferenceAmount,
 decimal DifferencePercent,
 decimal RetirementYearDifference,
 string Summary
);
