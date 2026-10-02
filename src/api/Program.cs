using ProposalBuilder.Api.Application.Contracts;
using ProposalBuilder.Api.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IScenarioService, ScenarioService>();
builder.Services.AddScoped<IScenarioExportService, ScenarioExportService>();
builder.Services.AddScoped<IWorkflowOfficeService, WorkflowOfficeService>();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapGet("/", () => Results.Ok(new { message = "Proposal Scenario Builder API only. Use the dashboard UI for the interactive page." }));
app.MapControllers();

app.Run();
