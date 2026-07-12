using FluentValidation;

namespace Application.UseCases.Listings.Commands.Create;

public class Validator : AbstractValidator<CreateCommand>
{
    public Validator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(100)
            .WithMessage("Title must be less than 100 characters");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(1000)
            .WithMessage("Description must be less than 1000 characters");
        
        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is required")
            .GreaterThan(1)
            .WithMessage("Price must be greater than 1")
            .LessThan(9999.99m)
            .WithMessage("Price must be less than 9999.99");
    }
}