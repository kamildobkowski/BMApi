using FluentValidation;

namespace Application.Features.Menu.GetMenu;

public class GetMenuValidator : AbstractValidator<GetMenuQuery>
{
	public GetMenuValidator()
	{
		RuleFor(x => x.Page).GreaterThan(0);
		RuleFor(x => x.PageSize).GreaterThan(5);
	}
}