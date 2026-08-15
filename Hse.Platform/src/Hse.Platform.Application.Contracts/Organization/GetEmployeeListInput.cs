using System;
using Hse.Platform.Organization;
using Volo.Abp.Application.Dtos;

namespace Hse.Platform.Organization;

public class GetEmployeeListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }

    public EmployeeStatus? Status { get; set; }
}
