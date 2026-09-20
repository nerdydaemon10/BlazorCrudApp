using FluentValidation;

namespace BlazorCrudApp.Api.Features.Categories;

public sealed class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    public  UpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}