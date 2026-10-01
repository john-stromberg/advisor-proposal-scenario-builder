namespace ProposalBuilder.Api.Infrastructure.Services;

using ProposalBuilder.Api.Application.Contracts;
using ProposalBuilder.Api.Domain;

public sealed class WorkflowOfficeService : IWorkflowOfficeService
{
    public Task<ProposalDeliverables> GenerateDeliverablesAsync(ProposalComparison comparison)
    {
        var slug = (comparison.CurrentScenario?.ClientName ?? "client")
            .Trim()
            .ToLowerInvariant()
            .Replace(" ", "-");

        return Task.FromResult(new ProposalDeliverables
        {
            WordPacketPath = $"/outputs/word/{slug}-proposal-packet.docx",
            ExcelComparisonPath = $"/outputs/excel/{slug}-proposal-comparison.xlsx",
            OutlookDraftReference = $"draft://outlook/{slug}-proposal-update",
            SharePointTargetPath = $"/sharepoint/clients/{slug}/proposal-packet"
        });
    }
}
