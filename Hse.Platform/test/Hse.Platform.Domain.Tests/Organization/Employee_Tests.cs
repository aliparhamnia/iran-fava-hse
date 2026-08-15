using System.Threading.Tasks;
using Hse.Platform.Organization;
using Shouldly;
using Xunit;

namespace Hse.Platform.Organization;

public class Employee_Tests
{
    [Fact]
    public void Should_Create_Active_Employee()
    {
        var employee = new Employee(System.Guid.NewGuid(), "E-100", "Ali", "Rezaei");

        employee.Status.ShouldBe(EmployeeStatus.Active);
        employee.EmployeeNumber.ShouldBe("E-100");
    }

    [Fact]
    public void Terminate_Should_Fail_When_Already_Terminated()
    {
        var employee = new Employee(System.Guid.NewGuid(), "E-101", "Sara", "Ahmadi");
        employee.Terminate();

        var ex = Should.Throw<Volo.Abp.BusinessException>(() => employee.Terminate());
        ex.Code.ShouldBe(PlatformDomainErrorCodes.EmployeeAlreadyTerminated);
    }
}
