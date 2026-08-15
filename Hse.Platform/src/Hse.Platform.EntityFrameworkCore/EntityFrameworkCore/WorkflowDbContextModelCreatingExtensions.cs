using Hse.Platform.Workflow;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Hse.Platform.EntityFrameworkCore;

public static class WorkflowDbContextModelCreatingExtensions
{
    public static void ConfigureWorkflow(this ModelBuilder builder)
    {
        builder.Entity<WorkflowDefinition>(b =>
        {
            b.ToTable("WorkflowDefinitions", HseSchemas.Workflow);
            b.ConfigureByConvention();
            b.Property(x => x.EntityType).IsRequired().HasMaxLength(WorkflowConsts.MaxEntityTypeLength);
            b.HasIndex(x => new { x.TenantId, x.EntityType, x.Version }).IsUnique();
            b.OwnsMany(x => x.States, s =>
            {
                s.ToTable("WorkflowStates", HseSchemas.Workflow);
                s.WithOwner().HasForeignKey("DefinitionId");
                s.Property(x => x.Code).IsRequired().HasMaxLength(WorkflowConsts.MaxStateLength);
                s.Property(x => x.DisplayName).IsRequired().HasMaxLength(WorkflowConsts.MaxDisplayNameLength);
            });
            b.OwnsMany(x => x.Transitions, t =>
            {
                t.ToTable("WorkflowTransitions", HseSchemas.Workflow);
                t.WithOwner().HasForeignKey("DefinitionId");
                t.Property(x => x.FromState).IsRequired().HasMaxLength(WorkflowConsts.MaxStateLength);
                t.Property(x => x.ToState).IsRequired().HasMaxLength(WorkflowConsts.MaxStateLength);
                t.Property(x => x.RequiredPermission).HasMaxLength(WorkflowConsts.MaxPermissionLength);
            });
        });

        builder.Entity<WorkflowInstance>(b =>
        {
            b.ToTable("WorkflowInstances", HseSchemas.Workflow);
            b.ConfigureByConvention();
            b.Property(x => x.EntityType).IsRequired().HasMaxLength(WorkflowConsts.MaxEntityTypeLength);
            b.Property(x => x.CurrentState).IsRequired().HasMaxLength(WorkflowConsts.MaxStateLength);
            b.HasIndex(x => new { x.TenantId, x.EntityType, x.EntityId }).IsUnique();
        });

        builder.Entity<WorkflowHistory>(b =>
        {
            b.ToTable("WorkflowHistories", HseSchemas.Workflow);
            b.ConfigureByConvention();
            b.Property(x => x.FromState).IsRequired().HasMaxLength(WorkflowConsts.MaxStateLength);
            b.Property(x => x.ToState).IsRequired().HasMaxLength(WorkflowConsts.MaxStateLength);
            b.Property(x => x.Comment).HasMaxLength(WorkflowConsts.MaxCommentLength);
            b.HasIndex(x => x.InstanceId);
        });
    }
}
