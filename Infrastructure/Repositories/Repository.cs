using System.Linq.Expressions;
using Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T>(DbContext dbContext)
	: IRepository<T>
	where T : class
{
	public T Add(T entity, CancellationToken cancellationToken = default)
		=> dbContext.Set<T>().Add(entity).Entity;

	public T Update(T entity, CancellationToken cancellationToken = default)
		=> dbContext.Set<T>().Update(entity).Entity;

	public T Delete(T entity, CancellationToken cancellationToken = default)
		=> dbContext.Set<T>().Remove(entity).Entity;

	public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		=> await dbContext.Set<T>().FindAsync([id], cancellationToken: cancellationToken);

	public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
	{
		return predicate is null
			? await dbContext.Set<T>().ToListAsync(cancellationToken)
			: await dbContext.Set<T>().Where(predicate).ToListAsync(cancellationToken);
	}

	public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
		=> await dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync(cancellationToken);

	public Task<List<T>> GetPageAsync(int page, int pageSize, Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
		=> predicate is null
			? dbContext.Set<T>().Skip(page-1 * pageSize).Take(pageSize).ToListAsync(cancellationToken)
			: dbContext.Set<T>().Where(predicate).Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
}