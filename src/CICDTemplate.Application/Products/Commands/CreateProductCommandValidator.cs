using FluentValidation;

namespace CICDTemplate.Application.Products.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(200);
    }
}
