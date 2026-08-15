using System;
using Hse.Platform.Organization;

namespace Hse.Platform.Organization;

public class CreateEmployeeDto
{
    public string EmployeeNumber { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string? NationalId { get; set; }

    public Guid? OrganizationUnitId { get; set; }

    public DateOnly? HireDate { get; set; }
}
