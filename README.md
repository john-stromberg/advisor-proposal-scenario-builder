# Proposal Scenario Builder

## Overview
This is a professional wealth strategy tool integrated with the Advisory Workflow Dashboard hub. It enables advisors to compare current vs. proposed client portfolio strategies with impact analysis.

## Features
- Create and manage household scenarios with portfolio parameters
- Compare current vs. proposed allocations side-by-side
- Analyze future value impact with inflation and return assumptions
- Calculate retirement timeline changes
- Export comparison reports

## Quick Start
```
dotnet run --project src/api/ProposalBuilder.Api.csproj --urls http://localhost:5057
```

Navigate to http://localhost:5057 to load the scenario builder UI.

## Usage Workflow
1. **Enter Client Details**: Input portfolio value, income, and time horizon
2. **Create Current Scenario**: Capture the existing allocation
3. **Create Proposed Scenario**: Model a new strategy (with slightly different return assumptions)
4. **Compare**: View side-by-side impact analysis
5. **Review Results**: Analyze FV differences and retirement timeline impact

## API Endpoints
- `POST /api/scenarios/create` - Create a new scenario
- `GET /api/scenarios` - List all scenarios
- `GET /api/scenarios/{id}` - Retrieve a specific scenario
- `POST /api/scenarios/compare` - Compare two scenarios

## Stack
- ASP.NET Core 10 Web API
- C#
- Plain HTML/JavaScript UI

## Integration with Dashboard Hub
This repo is indexed in the `advisory-workflow-dashboard` module registry. See the hub repo for loose integration details.

## Development
The project uses in-memory storage for MVP. Calculation logic is simplified with mock future-value analysis.

