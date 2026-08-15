using FluentValidation;
using Hse.Platform.Organization;

namespace Hse.Platform.Organization;

public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeDtoValidator()
    {
        RuleFor(x => x.EmployeeNumber)
            .NotEmpty()
            .MaximumLength(EmployeeConsts.MaxEmployeeNumberLength);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(EmployeeConsts.MaxNameLength);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(EmployeeConsts.MaxNameLength);

        RuleFor(x => x.NationalId)
            .MaximumLength(32)
            .When(x => !string.IsNullOrWhiteSpace(x.NationalId));
    }
}
