using FluentValidation;

namespace Application.Features.Menu.UpdateMenuItem;

public class UpdateMenuItemValidator : AbstractValidator<UpdateMenuItemCommand>
{
	public UpdateMenuItemValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MaximumLength(50);
		RuleFor(x => x.Description)
			.MaximumLength(150);
		RuleFor(x => x.Price)
			.NotEmpty()
			.GreaterThan(0.0m);
	}
}