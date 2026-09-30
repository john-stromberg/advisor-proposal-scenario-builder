namespace ProposalBuilder.Api.Domain;

public sealed class ScenarioResult
{
    public decimal CurrentPortfolioFV { get; set; }
    public decimal ProposedPortfolioFV { get; set; }
    public decimal DifferenceAmount { get; set; }
    public decimal DifferencePercent { get; set; }
    public decimal CurrentRetirementYear { get; set; }
    public decimal ProposedRetirementYear { get; set; }
    public int RetirementYearDifference { get; set; }
    public string AnalysisSummary { get; set; } = string.Empty;
}
