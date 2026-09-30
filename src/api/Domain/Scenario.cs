namespace ProposalBuilder.Api.Domain;

public sealed class HouseholdScenario
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly CreatedDate { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public decimal PortfolioValue { get; set; }
    public decimal AnnualIncome { get; set; }
    public int YearsToRetirement { get; set; }
    public decimal InflationRate { get; set; } = 0.03m;
    public decimal AssumedReturn { get; set; } = 0.07m;
    public string AllocationJson { get; set; } = "{}";
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}

public sealed class ProposalComparison
{
    public Guid Id { get; set; }
    public Guid CurrentScenarioId { get; set; }
    public Guid ProposedScenarioId { get; set; }
    public HouseholdScenario? CurrentScenario { get; set; }
    public HouseholdScenario? ProposedScenario { get; set; }
    public string ResultsJson { get; set; } = "{}";
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
