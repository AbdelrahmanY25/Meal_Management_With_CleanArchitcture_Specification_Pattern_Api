namespace MealManagement.Application.Specifications;

internal abstract class Specification<TEntity> : ISpecification<TEntity> where TEntity : class
{
	private List<Expression<Func<TEntity, object>>>? _includes;

	public Expression<Func<TEntity, bool>>? Filter { get; private set; }
	public IReadOnlyList<Expression<Func<TEntity, object>>>? Includes => _includes;

	public Expression<Func<TEntity, object>>? OrderBy { get; protected set; }
	public Expression<Func<TEntity, object>>? OrderByDescending { get; protected set; }

	public bool IsPagingEnabled { get; private set; } = false;
	public int? Skip { get; private set; }
	public int? Take { get; private set; }

	public bool AsNoTracking { get; protected set; } = true;
	public bool IgnoreQueryFilters { get; protected set; } = false;


	protected void AddFilter(Expression<Func<TEntity, bool>> filter)
		=> Filter = filter;

	protected void AddInclude(Expression<Func<TEntity, object>> include) 
	{
		_includes ??= [];
		_includes.Add(include);
	}


	protected void ApplyPaging(int skip, int take)
	{
		IsPagingEnabled = true;
		Skip = skip;
		Take = take;
	}
}

internal abstract class Specification<TEntity, TResult> : Specification<TEntity>, ISpecification<TEntity, TResult> where TEntity : class
{
	public Expression<Func<TEntity, TResult>> Selector { get; protected set; } = default!;
}