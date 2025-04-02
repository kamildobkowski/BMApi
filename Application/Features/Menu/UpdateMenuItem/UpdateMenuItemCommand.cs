using System.Net;
using Application.Common.Repositories;
using Domain.Common;
using MediatR;

namespace Application.Features.Menu.UpdateMenuItem;

public record UpdateMenuItemCommand(int Id, string Name, string? Description, decimal Price) : IRequest<Result<UpdateMenuItemResponse>>;

internal class UpdateMenuItemHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork) 
	: IRequestHandler<UpdateMenuItemCommand, Result<UpdateMenuItemResponse>>
{
	public async Task<Result<UpdateMenuItemResponse>> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await itemRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
		if(entity is null)
			return Result.Failure<UpdateMenuItemResponse>(new ErrorResult
			{
				StatusCode = HttpStatusCode.NotFound,
				Title = "Item not found"
			});
		entity.Price = request.Price;
		entity.Description = request.Description;
		entity.Name = request.Name;
		await unitOfWork.CommitAsync(cancellationToken);
		return Result.Success(new UpdateMenuItemResponse(entity.Id, entity.Name, entity.Description, entity.Price));
	}
}