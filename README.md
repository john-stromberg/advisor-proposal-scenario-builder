# Proposal Scenario Builder

## Overview
This is a workflow automation suite tool integrated with the Advisory Workflow Dashboard hub. It enables advisors to compare current vs. proposed client strategy scenarios and generate Office-ready proposal packet references.

## Features
- Create and manage household proposal scenarios with planning notes
- Compare current vs. proposed strategies side-by-side
- Analyze future value impact with inflation and return assumptions
- Generate Office-ready deliverable references for Word, Excel, Outlook, and SharePoint
- Support advisor workflow handoff for proposal packets

## Quick Start
```
dotnet run --project src/api/ProposalBuilder.Api.csproj --urls http://localhost:5057
```

Navigate to http://localhost:5057 to load the scenario builder UI.

## Usage Workflow
1. **Enter Client Details**: Input portfolio value, income, time horizon, and planning notes
2. **Create Current Scenario**: Capture the existing strategy baseline
3. **Create Proposed Scenario**: Model the proposed strategy case
4. **Compare**: Generate side-by-side proposal impact analysis
5. **Review Deliverables**: Use Word/Excel/Outlook/SharePoint references for advisor packet workflow

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
The project uses in-memory storage for MVP. Calculation logic remains simplified, and Office outputs are represented as Graph-ready deliverable references for workflow alignment.

