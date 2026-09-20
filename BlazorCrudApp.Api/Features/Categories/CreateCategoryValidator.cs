using FluentValidation;

namespace BlazorCrudApp.Api.Features.Categories;

public sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public  CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}