using System.Linq.Expressions;

namespace MealManagement.Infrastructure.Repositories;

internal sealed class Repository<TEntity>(ApplicationDbContext context) : IRepository<TEntity> where TEntity : class
{
	private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
	private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

	public async Task<TEntity?> GetOneAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken)
	{
		var query = EfSpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), spec);

		return await query.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken)
	{
		var query = EfSpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), spec);

		return await query.ToListAsync(cancellationToken);
	}

	public async Task<TResult?> GetOneWithSelectAsync<TResult>(ISpecification<TEntity, TResult> spec, CancellationToken cancellationToken)
	{
		var query = EfSpecificationEvaluator<TEntity, TResult>.GetQuery(_dbSet.AsQueryable(), spec);

		return await query.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<IReadOnlyList<TResult>> GetAllWithSelectAsync<TResult>(ISpecification<TEntity, TResult> spec, CancellationToken cancellationToken)
	{
		var query = EfSpecificationEvaluator<TEntity, TResult>.GetQuery(_dbSet.AsQueryable(), spec);

		return await query.ToListAsync(cancellationToken);
	}



	public async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression, bool ignoreQueryFilter = false, CancellationToken cancellationToken = default)
	{
		return await (ignoreQueryFilter
			? _dbSet.AsNoTracking().IgnoreQueryFilters().AnyAsync(expression, cancellationToken)
			: _dbSet.AsNoTracking().AnyAsync(expression, cancellationToken));
	}



	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
	{
		await _dbSet.AddAsync(entity, cancellationToken);

		return entity;
	}

	public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
	{
		await _dbSet.AddRangeAsync(entities, cancellationToken);
		return entities;
	}

	public void Update(TEntity entity)
	{
		_dbSet.Update(entity);
	}

	public void Delete(TEntity entity)
	{
		_dbSet.Remove(entity);
	}

	public void DeleteRange(IEnumerable<TEntity> entities)
	{
		_dbSet.RemoveRange(entities);
	}

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
	{
		return await _context.SaveChangesAsync(cancellationToken);
	}
}