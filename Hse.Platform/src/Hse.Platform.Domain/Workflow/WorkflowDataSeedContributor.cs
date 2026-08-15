using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Hse.Platform.Workflow;

public class WorkflowDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<WorkflowDefinition, Guid> _definitions;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;

    public WorkflowDataSeedContributor(
        IRepository<WorkflowDefinition, Guid> definitions,
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant)
    {
        _definitions = definitions;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _definitions.AnyAsync(x => x.EntityType == "health.medical-examination"))
            {
                return;
            }

            var definition = new WorkflowDefinition(_guidGenerator.Create(), "health.medical-examination");
            definition.AddState("draft", "Draft", isInitial: true);
            definition.AddState("submitted", "Submitted");
            definition.AddState("completed", "Completed", isTerminal: true);
            definition.AddState("cancelled", "Cancelled", isTerminal: true);
            definition.AddTransition("draft", "submitted");
            definition.AddTransition("submitted", "completed");
            definition.AddTransition("draft", "cancelled");
            definition.AddTransition("submitted", "draft");

            await _definitions.InsertAsync(definition, autoSave: true);
        }
    }
}
