namespace MealManagement.Application.Interfaces.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
	Task<TEntity?> GetOneAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);
	Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);
	
	Task<TResult?> GetOneWithSelectAsync<TResult>(ISpecification<TEntity, TResult> spec, CancellationToken cancellationToken);
	Task<IReadOnlyList<TResult>> GetAllWithSelectAsync<TResult>(ISpecification<TEntity, TResult> spec, CancellationToken cancellationToken);

	Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default);

	TEntity Add(TEntity entity);
	IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
	
	void Update(TEntity entity);
	void UpdateRange(IEnumerable<TEntity> entities);

	void Delete(TEntity entity);
	void DeleteRange(IEnumerable<TEntity> entities);

	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}