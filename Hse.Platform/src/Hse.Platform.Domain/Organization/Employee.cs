using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Hse.Platform.Organization;

public class Employee : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; protected set; }

    public string EmployeeNumber { get; protected set; } = default!;

    public string FirstName { get; protected set; } = default!;

    public string LastName { get; protected set; } = default!;

    public string? NationalIdCiphertext { get; protected set; }

    public string? NationalIdIndex { get; protected set; }

    public Guid? OrganizationUnitId { get; protected set; }

    public EmployeeStatus Status { get; protected set; }

    public DateOnly? HireDate { get; protected set; }

    protected Employee()
    {
    }

    public Employee(
        Guid id,
        string employeeNumber,
        string firstName,
        string lastName,
        Guid? organizationUnitId = null,
        DateOnly? hireDate = null)
        : base(id)
    {
        SetEmployeeNumber(employeeNumber);
        SetName(firstName, lastName);
        OrganizationUnitId = organizationUnitId;
        HireDate = hireDate;
        Status = EmployeeStatus.Active;
    }

    public void SetEmployeeNumber(string employeeNumber)
    {
        EmployeeNumber = Check.NotNullOrWhiteSpace(employeeNumber, nameof(employeeNumber), maxLength: EmployeeConsts.MaxEmployeeNumberLength);
    }

    public void SetName(string firstName, string lastName)
    {
        FirstName = Check.NotNullOrWhiteSpace(firstName, nameof(firstName), maxLength: EmployeeConsts.MaxNameLength);
        LastName = Check.NotNullOrWhiteSpace(lastName, nameof(lastName), maxLength: EmployeeConsts.MaxNameLength);
    }

    public void SetNationalId(string? ciphertext, string? index)
    {
        NationalIdCiphertext = ciphertext;
        NationalIdIndex = index;
    }

    public void TransferTo(Guid? organizationUnitId)
    {
        OrganizationUnitId = organizationUnitId;
    }

    public void Terminate()
    {
        if (Status == EmployeeStatus.Terminated)
        {
            throw new BusinessException(PlatformDomainErrorCodes.EmployeeAlreadyTerminated);
        }

        Status = EmployeeStatus.Terminated;
    }
}
