using FluentValidation;

namespace Application.Features.Menu.AddMenuItem;

public class AddMenuItemValidator : AbstractValidator<AddMenuItemCommand>
{
	public AddMenuItemValidator()
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