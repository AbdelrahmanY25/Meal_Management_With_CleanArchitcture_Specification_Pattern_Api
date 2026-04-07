namespace MealManagment.Application.Specifications;

public abstract class Specification<TEntity> : ISpecification<TEntity> where TEntity : class
{
	public Expression<Func<TEntity, bool>>? Filter { get; protected set; }
	public List<Expression<Func<TEntity, object>>> Includes { get; protected set; } = [];
	public List<string> IncludeStrings { get; protected set; } = [];

	public Expression<Func<TEntity, object>>? OrderBy { get; protected set; }
	public Expression<Func<TEntity, object>>? OrderByDescending { get; protected set; }

	public bool IsPagingEnabled { get; protected set; }
	public int? Skip { get; protected set; }
	public int? Take { get; protected set; }

	public bool AsNoTracking { get; protected set; } = true;
	public bool IgnoreQueryFilters { get; protected set; } = false;

	protected void ApplyPaging(int skip, int take)
	{
		IsPagingEnabled = true;
		Skip = skip;
		Take = take;
	}
}

public abstract class Specification<TEntity, TResult> : Specification<TEntity>, ISpecification<TEntity, TResult> where TEntity : class
{
	public Expression<Func<TEntity, TResult>> Selector { get; protected set; } = default!;
}