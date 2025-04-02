using Application.Common.Repositories;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class UnitOfWork(ItemsDbContext itemsDbContext) : IUnitOfWork
{
	public Task CommitAsync(CancellationToken cancellationToken)
	{
		return itemsDbContext.SaveChangesAsync(cancellationToken);
	}
}