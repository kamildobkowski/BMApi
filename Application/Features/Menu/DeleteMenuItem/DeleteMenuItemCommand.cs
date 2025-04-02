using System.Net;
using Application.Common.Repositories;
using Domain.Common;
using MediatR;

namespace Application.Features.Menu.DeleteMenuItem;

public record DeleteMenuItemCommand(int Id) : IRequest<Result<DeleteMenuItemResponse>>;

internal class DeleteMenuItemCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork) 
	: IRequestHandler<DeleteMenuItemCommand, Result<DeleteMenuItemResponse>>
{
	public async Task<Result<DeleteMenuItemResponse>> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await itemRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
		if(entity == null)
		{
			return Result.Failure<DeleteMenuItemResponse>(new ErrorResult
			{
				StatusCode = HttpStatusCode.NotFound,
				Title = "Item not found"
			});
		}
		itemRepository.Delete(entity, cancellationToken);
		await unitOfWork.CommitAsync(cancellationToken);
		return Result.Success(new DeleteMenuItemResponse());
	}
}