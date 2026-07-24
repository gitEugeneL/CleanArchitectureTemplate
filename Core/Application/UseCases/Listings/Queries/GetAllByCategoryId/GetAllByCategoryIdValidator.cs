using Application.UseCases.Common;
using FluentValidation;

namespace Application.UseCases.Listings.Queries.GetAllByCategoryId;

public class GetAllByCategoryIdValidator : AbstractValidator<GetAllByCategoryIdQuery>
{
    public GetAllByCategoryIdValidator()
    {
        Include(new PaginationValidator());
        
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category id is required");
    }
}