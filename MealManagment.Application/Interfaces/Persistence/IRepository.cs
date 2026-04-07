namespace MealManagment.Application.Interfaces.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
	Task<TEntity?> GetOneAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
	
	Task<TResult?> GetOneWithSelectAsync<TResult>(ISpecification<TEntity, TResult> spec, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<TResult>> GetAllWithSelectAsync<TResult>(ISpecification<TEntity, TResult> spec, CancellationToken cancellationToken = default);

	Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default);

	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
	void Update(TEntity entity);
	void Delete(TEntity entity);
	void DeleteRange(IEnumerable<TEntity> entities);


	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}