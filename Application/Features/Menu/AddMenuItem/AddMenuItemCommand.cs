using Application.Common.Repositories;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.Menu.AddMenuItem;

public record AddMenuItemCommand(string Name, string? Description, decimal Price) : IRequest<Result<AddMenuItemResponse>>;

internal class AddMenuItemCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork) 
	: IRequestHandler<AddMenuItemCommand, Result<AddMenuItemResponse>>
{
	public async Task<Result<AddMenuItemResponse>> Handle(AddMenuItemCommand request, CancellationToken cancellationToken)
	{
		var entity = new Item 
		{
			Name = request.Name, 
			Description = request.Description, 
			Price = request.Price
		};
		itemRepository.Add(entity, cancellationToken);
		await unitOfWork.CommitAsync(cancellationToken);
		return Result.Success(new AddMenuItemResponse(entity.Id));
	}
}