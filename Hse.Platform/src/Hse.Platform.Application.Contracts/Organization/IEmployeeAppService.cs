using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Hse.Platform.Organization;

public interface IEmployeeAppService : IApplicationService
{
    Task<EmployeeDto> GetAsync(Guid id);

    Task<PagedResultDto<EmployeeDto>> GetListAsync(GetEmployeeListInput input);

    Task<EmployeeDto> CreateAsync(CreateEmployeeDto input);

    Task TerminateAsync(Guid id);
}
