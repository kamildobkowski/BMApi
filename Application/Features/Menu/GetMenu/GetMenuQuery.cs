using System.Net;
using Application.Common.Repositories;
using Domain.Common;
using MediatR;

namespace Application.Features.Menu.GetMenu;

public record GetMenuQuery(int Page, int PageSize) : IRequest<Result<GetMenuResponse>>;

internal class GetMenuQueryHandler(IItemRepository itemRepository) 
	: IRequestHandler<GetMenuQuery, Result<GetMenuResponse>>
{
	public async Task<Result<GetMenuResponse>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
	{
		var entities = await itemRepository.GetPageAsync(request.Page, request.PageSize, cancellationToken: cancellationToken);
		if(entities.Count == 0)
		{
			return Result.Failure<GetMenuResponse>(new ErrorResult
			{
				StatusCode = HttpStatusCode.NotFound,
				Title = "No items found"
			});
		}
		var responseItems = entities.Select(x => new GetMenuResponseItem(x.Id, x.Price, x.Name, x.Description)).ToList();
		return Result.Success(new GetMenuResponse(responseItems));
	}
}	