using System;
using System.Linq;
using System.Threading.Tasks;
using Hse.Platform.Permissions;
using Hse.Platform.Security;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Hse.Platform.Organization;

[Authorize(PlatformPermissions.Employees.Default)]
public class EmployeeAppService : ApplicationService, IEmployeeAppService
{
    private readonly IRepository<Employee, Guid> _employeeRepository;
    private readonly SensitiveDataProtector _protector;

    public EmployeeAppService(
        IRepository<Employee, Guid> employeeRepository,
        SensitiveDataProtector protector)
    {
        _employeeRepository = employeeRepository;
        _protector = protector;
    }

    public async Task<EmployeeDto> GetAsync(Guid id)
    {
        var employee = await _employeeRepository.GetAsync(id);
        return Map(employee, includeNationalId: true);
    }

    public async Task<PagedResultDto<EmployeeDto>> GetListAsync(GetEmployeeListInput input)
    {
        var queryable = await _employeeRepository.GetQueryableAsync();

        queryable = queryable
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                x => x.EmployeeNumber.Contains(input.Filter!) ||
                     x.FirstName.Contains(input.Filter!) ||
                     x.LastName.Contains(input.Filter!))
            .WhereIf(input.Status.HasValue, x => x.Status == input.Status);

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        queryable = queryable
            .OrderBy(x => x.EmployeeNumber)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var items = await AsyncExecuter.ToListAsync(queryable);

        return new PagedResultDto<EmployeeDto>(
            totalCount,
            items.Select(x => Map(x, includeNationalId: false)).ToList());
    }

    [Authorize(PlatformPermissions.Employees.Create)]
    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto input)
    {
        var exists = await _employeeRepository.AnyAsync(x => x.EmployeeNumber == input.EmployeeNumber);
        if (exists)
        {
            throw new BusinessException(PlatformDomainErrorCodes.EmployeeNumberAlreadyExists)
                .WithData("EmployeeNumber", input.EmployeeNumber);
        }

        var employee = new Employee(
            GuidGenerator.Create(),
            input.EmployeeNumber,
            input.FirstName,
            input.LastName,
            input.OrganizationUnitId,
            input.HireDate);

        employee.SetNationalId(
            _protector.Encrypt(input.NationalId),
            _protector.CreateBlindIndex(input.NationalId));

        await _employeeRepository.InsertAsync(employee, autoSave: true);
        return Map(employee, includeNationalId: true);
    }

    [Authorize(PlatformPermissions.Employees.Update)]
    public async Task TerminateAsync(Guid id)
    {
        var employee = await _employeeRepository.GetAsync(id);
        employee.Terminate();
        await _employeeRepository.UpdateAsync(employee, autoSave: true);
    }

    private EmployeeDto Map(Employee employee, bool includeNationalId)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            EmployeeNumber = employee.EmployeeNumber,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            NationalId = includeNationalId ? _protector.Decrypt(employee.NationalIdCiphertext) : null,
            OrganizationUnitId = employee.OrganizationUnitId,
            Status = employee.Status,
            HireDate = employee.HireDate
        };
    }
}
