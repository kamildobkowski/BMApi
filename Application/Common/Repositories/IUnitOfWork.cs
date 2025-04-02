namespace Application.Common.Repositories;

public interface IUnitOfWork
{
	public Task CommitAsync(CancellationToken cancellationToken = default);
}