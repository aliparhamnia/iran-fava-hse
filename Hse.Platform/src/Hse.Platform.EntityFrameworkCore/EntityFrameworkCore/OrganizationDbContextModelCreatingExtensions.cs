using Hse.Platform.Organization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Hse.Platform.EntityFrameworkCore;

public static class OrganizationDbContextModelCreatingExtensions
{
    public static void ConfigureOrganization(this ModelBuilder builder)
    {
        builder.Entity<Employee>(b =>
        {
            b.ToTable("Employees", HseSchemas.Organization);
            b.ConfigureByConvention();
            b.Property(x => x.EmployeeNumber).IsRequired().HasMaxLength(EmployeeConsts.MaxEmployeeNumberLength);
            b.Property(x => x.FirstName).IsRequired().HasMaxLength(EmployeeConsts.MaxNameLength);
            b.Property(x => x.LastName).IsRequired().HasMaxLength(EmployeeConsts.MaxNameLength);
            b.Property(x => x.NationalIdCiphertext).HasMaxLength(EmployeeConsts.MaxNationalIdCipherLength);
            b.Property(x => x.NationalIdIndex).HasMaxLength(EmployeeConsts.MaxNationalIdIndexLength);
            b.HasIndex(x => new { x.TenantId, x.EmployeeNumber }).IsUnique();
            b.HasIndex(x => new { x.TenantId, x.NationalIdIndex });
            b.HasIndex(x => x.Status);
        });
    }
}
