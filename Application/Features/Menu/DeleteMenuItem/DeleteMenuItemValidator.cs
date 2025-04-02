using FluentValidation;

namespace Application.Features.Menu.DeleteMenuItem;

public class DeleteMenuItemValidator : AbstractValidator<DeleteMenuItemCommand>
{
	public DeleteMenuItemValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty()
			.GreaterThan(0);
	}
}