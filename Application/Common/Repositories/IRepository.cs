using System.Linq.Expressions;

namespace Application.Common.Repositories;

public interface IRepository<T> where T : class
{
	T Add(T entity, CancellationToken cancellationToken = default);
	T Update(T entity, CancellationToken cancellationToken = default);
	T Delete(T entity, CancellationToken cancellationToken = default);
	Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
	Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
	Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
	Task<List<T>> GetPageAsync(int page, int pageSize, Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
}