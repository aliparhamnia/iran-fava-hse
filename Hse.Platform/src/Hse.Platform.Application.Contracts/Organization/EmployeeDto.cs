using System;
using Hse.Platform.Organization;
using Volo.Abp.Application.Dtos;

namespace Hse.Platform.Organization;

public class EmployeeDto : EntityDto<Guid>
{
    public string EmployeeNumber { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string? NationalId { get; set; }

    public Guid? OrganizationUnitId { get; set; }

    public EmployeeStatus Status { get; set; }

    public DateOnly? HireDate { get; set; }
}
